using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

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
        public string jawabanBenar; 
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
    private int indeksBabTerakhir = 0; 

    // Menyimpan daftar status apakah bab tersebut sudah selesai dikerjakan
    private List<bool> statusBabSelesai = new List<bool>();
    // BARU: Menyimpan nilai skor per BAB untuk kalkulasi nilai akhir
    private List<int> nilaiPerBab = new List<int>();

    [Header("Sistem Kuis: Referensi UI Visual")]
    [SerializeField] private GameObject panelPilihBab;
    [SerializeField] private GameObject panelGameKuis;
    [SerializeField] private GameObject panelSkor;
    [SerializeField] private GameObject panelKonfirmasiExit; 
    [Space(5)]
    [SerializeField] private GameObject[] daftarTombolBabVisual;
    // BARU: Daftarkan objek tombol "Button_Keluar_Bab" di sini agar bisa diatur kemunculannya
    [SerializeField] private GameObject tombolKeluarBabUtama; 
    // BARU: Pop-up Panel khusus untuk menampilkan Scoring Final di akhir game
    [SerializeField] private GameObject panelScoringFinal; 
    [Space(5)]
    [SerializeField] private TextMeshProUGUI uiTeksSoal; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolA; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolB; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolC; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolD; 
    [SerializeField] private TextMeshProUGUI uiTeksSkorAkhir; 
    // BARU: Komponen teks untuk memunculkan kalkulasi rata-rata skor final
    [SerializeField] private TextMeshProUGUI uiTeksScoringFinal; 

    void Start()
    {
        // Inisialisasi status semua bab & nilai di awal game
        for (int i = 0; i < dataKuisBab.Count; i++)
        {
            statusBabSelesai.Add(false);
            nilaiPerBab.Add(0); // Set nilai awal tiap bab adalah 0
        }

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
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(false);

        halamanSekarang = 0; 
        UpdateTampilanHalaman();
    }

    public void TutupMateri()
    {
        if (penjelasanMateri != null) penjelasanMateri.SetActive(false);
        AktifkanGrupParent(penjelasanMateri, false);
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
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(false);

        ResetPanelKuis();
    }

    public void TutupQuiz()
    {
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false);
        AktifkanGrupParent(penjelasanQuiz, false);
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(true);
    }

    public void ResetPanelKuis()
    {
        if (panelPilihBab != null) panelPilihBab.SetActive(true); 
        if (panelGameKuis != null) panelGameKuis.SetActive(false); 
        if (panelSkor != null) panelSkor.SetActive(false); 
        if (panelKonfirmasiExit != null) panelKonfirmasiExit.SetActive(false); 
        if (panelScoringFinal != null) panelScoringFinal.SetActive(false); // Pastikan panel final tertutup

        PerbaruiTampilanTombolBab();
    }

    private void PerbaruiTampilanTombolBab()
    {
        if (daftarTombolBabVisual == null || daftarTombolBabVisual.Length == 0) return;

        int totalBabSelesai = 0;

        for (int i = 0; i < daftarTombolBabVisual.Length; i++)
        {
            if (daftarTombolBabVisual[i] != null && i < statusBabSelesai.Count)
            {
                daftarTombolBabVisual[i].SetActive(!statusBabSelesai[i]);
                
                if (statusBabSelesai[i])
                {
                    totalBabSelesai++;
                }
            }
        }

        // BARU: Tombol Exit Bab hanya aktif/muncul jika ke-3 bab sudah selesai dikerjakan!
        if (tombolKeluarBabUtama != null)
        {
            tombolKeluarBabUtama.SetActive(totalBabSelesai >= dataKuisBab.Count);
        }
    }

    public void MulaiKuisBab(int indeksBab)
    {
        if (indeksBab >= dataKuisBab.Count || dataKuisBab[indeksBab].kumpulanSoal.Count == 0)
        {
            Debug.LogError("Data soal kuis pada BAB ini kosong atau tidak ditemukan!");
            return;
        }

        indeksBabTerakhir = indeksBab;
        soalSesiAktif = new List<SoalKuis>(dataKuisBab[indeksBab].kumpulanSoal); 

        // Algoritma Pengacakan Soal
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

        // Simpan skor bab ini ke dalam list memori data
        if (indeksBabTerakhir < statusBabSelesai.Count)
        {
            statusBabSelesai[indeksBabTerakhir] = true;
            nilaiPerBab[indeksBabTerakhir] = skorAkhir; 
        }

        if (uiTeksSkorAkhir != null)
        {
            uiTeksSkorAkhir.text = "SKOR KAMU:\n" + skorAkhir.ToString(); 
        }
    }

    // BARU: Fungsi hitung skor gabungan total yang dipicu saat menekan tombol Exit Utama
    public void KlikTombolExitUtamaKuisTamat()
    {
        if (panelPilihBab != null) panelPilihBab.SetActive(false);
        if (panelScoringFinal != null) panelScoringFinal.SetActive(true);

        int totalNilaiGabungan = 0;
        for (int i = 0; i < nilaiPerBab.Count; i++)
        {
            totalNilaiGabungan += nilaiPerBab[i];
        }

        int rataRataFinal = totalNilaiGabungan / dataKuisBab.Count;

        if (uiTeksScoringFinal != null)
        {
            // Menampilkan visual teks simulasi hitungan: (300 : 3) = 100
            uiTeksScoringFinal.text = $"TOTAL SKOR:\n({totalNilaiGabungan} : {dataKuisBab.Count}) = {rataRataFinal}";
        }
    }

    // BARU: Fungsi untuk benar-benar menyelesaikan game dari panel scoring final dan kembali ke menu utama papan tulis
    public void SelesaiTotalDanKembaliKePapan()
    {
        // Reset status bermain agar game bisa diulang dari awal jika membuka kuis lagi nanti
        for (int i = 0; i < statusBabSelesai.Count; i++)
        {
            statusBabSelesai[i] = false;
            nilaiPerBab[i] = 0;
        }

        if (panelScoringFinal != null) panelScoringFinal.SetActive(false);
        TutupQuiz();
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
        
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(true);
    }
}