using UnityEngine;

public class GameManager_kelas : MonoBehaviour
{
    [Header("Grup Pop-Up Utama")]
    [SerializeField] private GameObject penjelasanMateri; 
    [SerializeField] private GameObject penjelasanQuiz;   

    [Header("Sistem Halaman Materi")]
    [SerializeField] private GameObject[] daftarHalaman; 
    private int halamanSekarang = 0;

    [Header("Tombol Navigasi Materi")]
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonBack;

    void Start()
    {
        // Memastikan semua sistem pop-up tertutup rapi saat game pertama kali dijalankan
        MatiTotalPopUp();
    }

    // ==========================================
    //          SISTEM UTAMA: MATERI
    // ==========================================
    
    public void BukaMateri()
    {
        // 1. Nyalakan Parent Utama (PopUP_Group) agar efek gelap muncul
        AktifkanGrupParent(penjelasanMateri, true);

        // 2. Nyalakan kontainer materi dan reset ke halaman pertama (indeks 0)
        if (penjelasanMateri != null) penjelasanMateri.SetActive(true);
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false); // Pastikan quiz mati
        
        halamanSekarang = 0; 
        UpdateTampilanHalaman();
    }

    public void TutupMateri()
    {
        if (penjelasanMateri != null) penjelasanMateri.SetActive(false);
        
        // Matikan Parent Utama (PopUP_Group) agar layar kembali normal
        AktifkanGrupParent(penjelasanMateri, false);
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
        if (halamanSekarang > 0)
        {
            halamanSekarang--;
            UpdateTampilanHalaman();
        }
    }

    private void UpdateTampilanHalaman()
    {
        if (daftarHalaman == null || daftarHalaman.Length == 0) return;

        // 1. Logika ganti halaman materi (Hanya aktifkan indeks yang sesuai)
        for (int i = 0; i < daftarHalaman.Length; i++)
        {
            if (daftarHalaman[i] != null)
            {
                daftarHalaman[i].SetActive(i == halamanSekarang);
            }
        }

        // 2. Tombol Back HILANG jika berada di Halaman 1 (Indeks 0)
        if (buttonBack != null)
        {
            buttonBack.SetActive(halamanSekarang > 0);
        }

        // 3. Tombol Next HILANG jika berada di Halaman Terakhir (Indeks Max)
        if (buttonNext != null)
        {
            buttonNext.SetActive(halamanSekarang < daftarHalaman.Length - 1);
        }
    }

    // ==========================================
    //           SISTEM UTAMA: QUIZ
    // ==========================================
    
    public void BukaQuiz()
    {
        // 1. Nyalakan Parent Utama (PopUP_Group) agar efek gelap muncul
        AktifkanGrupParent(penjelasanQuiz, true);

        // 2. Nyalakan kontainer quiz
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(true);
        if (penjelasanMateri != null) penjelasanMateri.SetActive(false); // Pastikan materi mati
    }

    public void TutupQuiz()
    {
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false);
        
        // Matikan Parent Utama (PopUP_Group) agar layar kembali normal
        AktifkanGrupParent(penjelasanQuiz, false);
    }

    // ==========================================
    //         FUNGSI BANTUAN (HELPER)
    // ==========================================

    private void AktifkanGrupParent(GameObject targetObject, bool status)
    {
        // Mengontrol PopUP_Group secara otomatis melalui script tanpa merusak hierarki
        if (targetObject != null && targetObject.transform.parent != null)
        {
            targetObject.transform.parent.gameObject.SetActive(status);
        }
    }

    private void MatiTotalPopUp()
    {
        // Mematikan konten internal
        if (penjelasanMateri != null) penjelasanMateri.SetActive(false);
        if (penjelasanQuiz != null) penjelasanQuiz.SetActive(false);

        // Mematikan PopUP_Group di awal game agar tombol utama kelas bisa diklik
        if (penjelasanMateri != null && penjelasanMateri.transform.parent != null)
        {
            penjelasanMateri.transform.parent.gameObject.SetActive(false);
        }
    }
}