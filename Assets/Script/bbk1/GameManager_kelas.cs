using UnityEngine;
using System.Collections.Generic;
using TMPro; // Wajib ditambahkan untuk mengontrol komponen TextMeshPro via kode

public class GameManager_kelas : MonoBehaviour
{
    [System.Serializable]
    public struct SoalKuis
    {
        public string teksSoal;
        public string pilihanA;
        public string pilihanB;
        public string pilihanC;
        public string pilihanD;
        public string jawabanBenar; // Diisi dengan "A", "B", "C", atau "D"
    }

    [System.Serializable]
    public struct BabKuis
    {
        public string namaBab;
        public List<SoalKuis> kumpulanSoal; 
    }

    [Header("Grup Pop-Up Utama")]
    [SerializeField] private GameObject penjelasanMateri; 
    [SerializeField] private GameObject penjelasanQuiz;   

    [Header("Sistem Menu Utama Papan Tulis")]
    // TEMPAT SLOT BARU: Masukkan objek 'Menu_Utama_Papan' di sini agar tidak menutupi klik kuis!
    [SerializeField] private GameObject menuUtamaPapan; 

    [Header("Sistem Halaman Materi")]
    [SerializeField] private GameObject[] daftarHalaman; 
    private int halamanSekarang = 0;

    [Header("Tombol Navigasi Materi")]
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonBack;

    [Header("Sistem Kuis: Referensi Data")]
    [SerializeField] private List<BabKuis> dataKuisBab = new List<BabKuis>();
    private List<SoalKuis> soalSesiAktif = new List<SoalKuis>();
    private int indeksSoalAktif = 0;
    private int jumlahJawabanBenar = 0;
    private int indeksBabTerakhir = 0; // Menyimpan BAB yang sedang dimainkan untuk fitur Restart

    [Header("Sistem Kuis: Referensi UI Visual")]
    [SerializeField] private GameObject panelPilihBab;
    [SerializeField] private GameObject panelGameKuis;
    [SerializeField] private GameObject panelSkor;
    [SerializeField] private GameObject panelKonfirmasiExit; // Slot Pop-up Konfirmasi
    [Space(5)]
    [SerializeField] private TextMeshProUGUI uiTeksSoal;
    [SerializeField] private TextMeshProUGUI uiTeksTombolA;
    [SerializeField] private TextMeshProUGUI uiTeksTombolB;
    [SerializeField] private TextMeshProUGUI uiTeksTombolC;
    [SerializeField] private TextMeshProUGUI uiTeksTombolD;
    [SerializeField] private TextMeshProUGUI uiTeksSkorAkhir;

    void Start()
    {
        MatiTotalPopUp();
    }

    // ==========================================
    //          SISTEM UTAMA: MATERI
    // ==========================================
    
    public void BukaMateri()
    {
        AktifkanGrupParent(penjelasanMateri, true);
        if (penjelasanMateri != null) penjelasanMateri.SetActive(true);
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false); 
        
        // Mematikan menu papan tulis utama agar tidak menghalangi halaman materi
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(false);

        halamanSekarang = 0; 
        UpdateTampilanHalaman();
    }

    public void TutupMateri()
    {
        if (penjelasanMateri != null) penjelasanMateri.SetActive(false);
        AktifkanGrupParent(penjelasanMateri, false);

        // Menghidupkan kembali menu papan tulis utama
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(true);
    }

    public void HalamanSelanjutnya()
    {
        if (daftarHalaman != null && halamanSekarang < daftarHalaman.Length - 1)
        {
            halamanSekarang++;
            UpdateTampilanHalaman();
        }
    }

    public void HalamanSebelumnya()
    {
        if (daftarHalaman != null && halamanSekarang > 0)
        {
            halamanSekarang--;
            UpdateTampilanHalaman();
        }
    }

    private void UpdateTampilanHalaman()
    {
        if (daftarHalaman == null || daftarHalaman.Length == 0) return;

        for (int i = 0; i < daftarHalaman.Length; i++)
        {
            if (daftarHalaman[i] != null)
            {
                daftarHalaman[i].SetActive(i == halamanSekarang);
            }
        }

        if (buttonBack != null) buttonBack.SetActive(halamanSekarang > 0);
        if (buttonNext != null) buttonNext.SetActive(halamanSekarang < daftarHalaman.Length - 1);
    }

    // ==========================================
    //           SISTEM UTAMA: QUIZ
    // ==========================================
    
    public void BukaQuiz()
    {
        AktifkanGrupParent(penjelasanQuiz, true);
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(true);
        if (penjelasanMateri != null) penjelasanMateri.SetActive(false);

        // LANGKAH B: Mematikan tombol menu utama papan tulis agar bebas dari tumpukan klik!
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(false);

        ResetPanelKuis();
    }

    public void TutupQuiz()
    {
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false);
        AktifkanGrupParent(penjelasanQuiz, false);

        // LANGKAH B: Menghidupkan kembali menu papan tulis saat player keluar dari kuis
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(true);
    }

    public void ResetPanelKuis()
    {
        if (panelPilihBab != null) panelPilihBab.SetActive(true);
        if (panelGameKuis != null) panelGameKuis.SetActive(false);
        if (panelSkor != null) panelSkor.SetActive(false);
        if (panelKonfirmasiExit != null) panelKonfirmasiExit.SetActive(false);
    }

    public void MulaiKuisBab(int indeksBab)
    {
        if (indeksBab >= dataKuisBab.Count || dataKuisBab[indeksBab].kumpulanSoal.Count == 0)
        {
            Debug.LogError("Data soal kuis pada BAB ini kosong atau tidak ditemukan!");
            return;
        }

        indeksBabTerakhir = indeksBab;

        // 1. Ambil duplikat data soal asli
        soalSesiAktif = new List<SoalKuis>(dataKuisBab[indeksBab].kumpulanSoal);

        // 2. Algoritma Pengacakan Soal (Fisher-Yates)
        for (int i = soalSesiAktif.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            SoalKuis temp = soalSesiAktif[i];
            soalSesiAktif[i] = soalSesiAktif[r];
            soalSesiAktif[r] = temp;
        }

        indeksSoalAktif = 0;
        jumlahJawabanBenar = 0;

        if (panelPilihBab != null) panelPilihBab.SetActive(false);
        if (panelGameKuis != null) panelGameKuis.SetActive(true);
        if (panelSkor != null) panelSkor.SetActive(false);
        if (panelKonfirmasiExit != null) panelKonfirmasiExit.SetActive(false);

        PerbaruiVisualTeksSoal();
    }

    private void PerbaruiVisualTeksSoal()
    {
        if (soalSesiAktif == null || soalSesiAktif.Count == 0) return;

        SoalKuis dataSoalSekarang = soalSesiAktif[indeksSoalAktif];

        if (uiTeksSoal != null) uiTeksSoal.text = dataSoalSekarang.teksSoal;
        if (uiTeksTombolA != null) uiTeksTombolA.text = dataSoalSekarang.pilihanA;
        if (uiTeksTombolB != null) uiTeksTombolB.text = dataSoalSekarang.pilihanB;
        if (uiTeksTombolC != null) uiTeksTombolC.text = dataSoalSekarang.pilihanC;
        if (uiTeksTombolD != null) uiTeksTombolD.text = dataSoalSekarang.pilihanD;
    }

    public void PilihJawabanKuis(string pilihanPemain)
    {
        if (panelKonfirmasiExit != null && panelKonfirmasiExit.activeSelf) return;
        if (soalSesiAktif == null || soalSesiAktif.Count == 0) return;

        string kunciJawaban = soalSesiAktif[indeksSoalAktif].jawabanBenar;

        if (pilihanPemain.ToUpper() == kunciJawaban.ToUpper())
        {
            jumlahJawabanBenar++;
        }

        if (indeksSoalAktif < soalSesiAktif.Count - 1)
        {
            indeksSoalAktif++;
            PerbaruiVisualTeksSoal();
        }
        else
        {
            TampilkanHalamanHasilSkor();
        }
    }

    private void TampilkanHalamanHasilSkor()
    {
        if (panelGameKuis != null) panelGameKuis.SetActive(false);
        if (panelSkor != null) panelSkor.SetActive(true);
        if (panelKonfirmasiExit != null) panelKonfirmasiExit.SetActive(false);

        int skorAkhir = Mathf.RoundToInt(((float)jumlahJawabanBenar / soalSesiAktif.Count) * 100f);

        if (uiTeksSkorAkhir != null)
        {
            uiTeksSkorAkhir.text = "SKOR KAMU:\n" + skorAkhir.ToString();
        }
    }

    // =======================================================
    //       POP-UP KONFIRMASI (RESTART / CONTINUE)
    // =======================================================

    public void KlikTombolExitKuis()
    {
        if (panelKonfirmasiExit != null) panelKonfirmasiExit.SetActive(true);
    }

    public void PilihanContinueKuis()
    {
        if (panelKonfirmasiExit != null) panelKonfirmasiExit.SetActive(false);
    }

    public void PilihanRestartKuis()
    {
        MulaiKuisBab(indeksBabTerakhir);
    }

    public void KeluarDanResetKuis()
    {
        soalSesiAktif.Clear();
        indeksSoalAktif = 0;
        jumlahJawabanBenar = 0;
        ResetPanelKuis();
        
        // Hidupkan kembali menu utama papan tulis setelah kuis direset keluar
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(true);
    }

    // ==========================================
    //         FUNGSI BANTUAN (HELPER)
    // ==========================================

    private void AktifkanGrupParent(GameObject targetObject, bool status)
    {
        if (targetObject != null && targetObject.transform.parent != null)
        {
            targetObject.transform.parent.gameObject.SetActive(status);
        }
    }

    private void MatiTotalPopUp()
    {
        if (penjelasanMateri != null) penjelasanMateri.SetActive(false);
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false);

        if (penjelasanMateri != null && penjelasanMateri.transform.parent != null)
        {
            penjelasanMateri.transform.parent.gameObject.SetActive(false);
        }
        
        // Memastikan menu utama papan tulis menyala di awal permainan
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(true);
    }
}