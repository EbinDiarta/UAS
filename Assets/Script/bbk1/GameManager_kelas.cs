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
    [SerializeField] private GameObject tombolQuizPapanTulis;
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

    private List<bool> statusBabSelesai = new List<bool>();
    private List<int> nilaiPerBab = new List<int>();

    [Header("Sistem Kuis: Referensi UI Visual")]
    [SerializeField] private GameObject panelPilihBab;
    [SerializeField] private GameObject panelGameKuis;
    [SerializeField] private GameObject panelSkor;
    [SerializeField] private GameObject panelKonfirmasiExit; 
    [SerializeField] private GameObject panelScoringFinal; 
    [Space(5)]
    [SerializeField] private GameObject[] daftarTombolBabVisual;
    
    // BARU: Slot untuk memasukkan Button_exit utama yang berada di Quiz (parent)
    [SerializeField] private GameObject tombolExitUtamaKuis; 
    
    [Space(5)]
    [SerializeField] private TextMeshProUGUI uiTeksSoal; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolA; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolB; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolC; 
    [SerializeField] private TextMeshProUGUI uiTeksTombolD; 
    [SerializeField] private TextMeshProUGUI uiTeksSkorAkhir; 
    [SerializeField] private TextMeshProUGUI uiTeksScoringFinal; 
    public static  bool babakIniSelesai = false;

    void Start()
    {
        for (int i = 0; i < dataKuisBab.Count; i++)
        {
            statusBabSelesai.Add(false);
            nilaiPerBab.Add(0);
        }

        MatiTotalPopUp();

        // === LOGIKA DIPERBAIKI: SEMBUNYIKAN TOMBOL UTAMA JIKA KUIS BABAK INI SUDAH SELESAI ===
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        
        // Kita periksa apakah salah satu kuis spesifik (0, 1, atau 2) sudah menyelesaikan babak aktif saat ini
        for (int i = 0; i < dataKuisBab.Count; i++)
        {
            string kunciPenyimpanan = "KuisBabak_" + i + "_Selesai";
            // Jika ada kuis yang selesai, kita cek kecocokannya dengan progres babak
            // (Atau bisa langsung menggunakan status penyelesaian kuis babak aktif)
        }

        // Ambil status apakah babak aktif saat ini sudah menyelesaikan kuis
        // Kita buat flag universal berdasarkan BabakAktif agar sinkron dengan script GantiBabak
        string kunciBabakAktif = "KuisSelesai_Babak_" + babakAktif;
        if (PlayerPrefs.GetInt(kunciBabakAktif, 0) == 1)
        {
            if (tombolQuizPapanTulis != null) 
            {
                tombolQuizPapanTulis.SetActive(false);
                Debug.Log("Start: Tombol kuis dipapan tulis disembunyikan karena Babak " + babakAktif + " sudah selesai kuis.");
            }
        }
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

        // === TAMBAHAN JIKA PLAYER SUDAH MENYELESAIKAN KUIS DI BABAK INI ===
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        string kunciBabakIni = "KuisSelesai_Babak_" + babakAktif;
        
        if (PlayerPrefs.GetInt(kunciBabakIni, 0) == 1)
        {
            if (panelPilihBab != null) panelPilihBab.SetActive(false);
        }
    }

    public void TutupQuiz()
    {
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false);
        AktifkanGrupParent(penjelasanQuiz, false);
        if (menuUtamaPapan != null) menuUtamaPapan.SetActive(true);

        // === LOGIKA UTAMA: CEK KEMBALI TOMBOL PAPAN TULIS SAAT KUIS DITUTUP ===
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        string kunciBabakIni = "KuisSelesai_Babak_" + babakAktif;

        if (PlayerPrefs.GetInt(kunciBabakIni, 0) == 1)
        {
            if (tombolQuizPapanTulis != null) 
            {
                tombolQuizPapanTulis.SetActive(false); // Matikan tombol kuis di papan tulis secara fisik
            }
        }
        else
        {
            if (tombolQuizPapanTulis != null) 
            {
                tombolQuizPapanTulis.SetActive(true); // Pastikan tetap menyala jika belum dikerjakan
            }
        }
    }

    public void ResetPanelKuis()
    {
        if (panelPilihBab != null) panelPilihBab.SetActive(true); 
        if (panelGameKuis != null) panelGameKuis.SetActive(false); 
        if (panelSkor != null) panelSkor.SetActive(false); 
        if (panelKonfirmasiExit != null) panelKonfirmasiExit.SetActive(false); 
        if (panelScoringFinal != null) panelScoringFinal.SetActive(false); 

        // BARU: Saat berada di AWAL (pilih bab) atau kembali ke menu pilih bab, pastikan tombol exit UTAMA menyala
        if (tombolExitUtamaKuis != null) tombolExitUtamaKuis.SetActive(true);

        PerbaruiTampilanTombolBab();
    }

    private void PerbaruiTampilanTombolBab()
    {
        if (daftarTombolBabVisual == null || daftarTombolBabVisual.Length == 0) return;

        // 1. Hitung secara total, berapa banyak kuis yang SUDAH PERNAH diselesaikan pemain
        int totalKuisSelesai = 0;
        for (int i = 0; i < daftarTombolBabVisual.Length; i++)
        {
            string kunciKuisSpesifik = "KuisBabak_" + i + "_Selesai";
            if (PlayerPrefs.GetInt(kunciKuisSpesifik, 0) == 1)
            {
                totalKuisSelesai++;
            }
        }

        // 2. Ambit status babak aktif saat ini (Babak 1, Babak 2, atau Babak 3)
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        string kunciBabakAktifSelesai = "KuisSelesai_Babak_" + babakAktif;
        bool kuisBabakIniSudahSelesai = PlayerPrefs.GetInt(kunciBabakAktifSelesai, 0) == 1;

        // 3. Atur kemunculan tombol secara dinamis
        for (int i = 0; i < daftarTombolBabVisual.Length; i++)
        {
            if (daftarTombolBabVisual[i] != null)
            {
                string kunciKuisSpesifik = "KuisBabak_" + i + "_Selesai";
                bool kuisIniSudahSelesai = PlayerPrefs.GetInt(kunciKuisSpesifik, 0) == 1;

                if (kuisBabakIniSudahSelesai)
                {
                    // Jika di babak AKTIF INI player sudah beres ngerjain 1 kuis, sembunyikan SEMUA tombol kuis
                    daftarTombolBabVisual[i].SetActive(false);
                }
                else
                {
                    // JIKA BELUM SELESAI: Tampilkan tombol HANYA JIKA kuis spesifik tersebut belum pernah dikerjakan
                    daftarTombolBabVisual[i].SetActive(!kuisIniSudahSelesai);
                }
            }
        }

        Debug.Log($"Sistem Tombol: Babak Aktif = {babakAktif}, Total Kuis Selesai = {totalKuisSelesai}");
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

        // BARU: Sembunyikan/Hilangkan tombol exit utama saat menjawab soal agar tidak mengganggu
        if (tombolExitUtamaKuis != null) tombolExitUtamaKuis.SetActive(false);

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

        if (tombolExitUtamaKuis != null) tombolExitUtamaKuis.SetActive(true);

        int skorAkhir = Mathf.RoundToInt(((float)jumlahJawabanBenar / soalSesiAktif.Count) * 100f);

        if (indeksBabTerakhir < statusBabSelesai.Count)
        {
            statusBabSelesai[indeksBabTerakhir] = true;
            nilaiPerBab[indeksBabTerakhir] = skorAkhir; 
        }

        if (uiTeksSkorAkhir != null)
        {
            uiTeksSkorAkhir.text = "SKOR KAMU:\n" + skorAkhir.ToString(); 
        }

        // ===================================================================
        // LOGIKA PENYIMPANAN DATA SINKRON (SINKRONISASI TOMBOL PAPAN TULIS)
        // ===================================================================
        int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);
        
        // 1. Simpan bahwa babak aktif ini sudah menyelesaikan kuis utamanya
        PlayerPrefs.SetInt("KuisSelesai_Babak_" + babakAktif, 1);

        // 2. Simpan indeks kuis spesifik (Geografi/Ekonomi/Sosiologi) agar tidak muncul di panel pilihan lagi
        string kunciPenyimpanan = "KuisBabak_" + indeksBabTerakhir + "_Selesai";
        PlayerPrefs.SetInt(kunciPenyimpanan, 1); 
        
        PlayerPrefs.Save();                      
        
        if (panelPilihBab != null) panelPilihBab.SetActive(false);
        
        // Matikan langsung tombol kuis di papan tulis di latar belakang agar aman
        if (tombolQuizPapanTulis != null) tombolQuizPapanTulis.SetActive(false);
    }
    public void KlikTombolExitUtamaKuisTamat()
    {
        // Periksa apakah ke-3 bab sudah selesai semua atau belum
        int totalBabSelesai = 0;
        for (int i = 0; i < statusBabSelesai.Count; i++)
        {
            if (statusBabSelesai[i]) totalBabSelesai++;
        }

        // JIKA BELUM TAMAT SEMUA: klik tombol exit hanya berfungsi menutup kuis dan kembali ke papan tulis biasa
        if (totalBabSelesai < dataKuisBab.Count)
        {
            TutupQuiz();
            return;
        }

        // JIKA SUDAH SELESAI SEMUA: Munculkan pop-up Scoring Final
        if (panelPilihBab != null) panelPilihBab.SetActive(false);
        if (panelSkor != null) panelSkor.SetActive(false);
        if (panelScoringFinal != null) panelScoringFinal.SetActive(true); 
        if (tombolExitUtamaKuis != null) tombolExitUtamaKuis.SetActive(false); // Sembunyikan karena sudah ada tombol SELESAI final

        int totalNilaiGabungan = 0;
        for (int i = 0; i < nilaiPerBab.Count; i++)
        {
            totalNilaiGabungan += nilaiPerBab[i];
        }

        int rataRataFinal = totalNilaiGabungan / dataKuisBab.Count;

        if (uiTeksScoringFinal != null)
        {
            uiTeksScoringFinal.text = $"TOTAL SKOR:\n({totalNilaiGabungan} : {dataKuisBab.Count}) = {rataRataFinal}"; 
        }
    }

    public void SelesaiTotalDanKembaliKePapan()
    {
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