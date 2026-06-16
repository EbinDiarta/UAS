using System.Collections;
using UnityEngine;

public class GantiBabak : MonoBehaviour
{
    public GameObject cutscene;
    public Transform mc;
    public Transform tujuan;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ambil data babak saat ini, jika belum ada set ke babak 1
            int babakAktif = PlayerPrefs.GetInt("BabakAktif", 1);

            // LOGIKA VALIDASI: Pastikan kuis di babak ini sudah selesai sebelum bisa ganti babak
            if (PlayerPrefs.GetInt("KuisSelesai_Babak_" + babakAktif, 0) == 1)
            {
                StartCoroutine(GantiBabakCoroutine(babakAktif));
            }
            else
            {
                Debug.LogWarning("Maju ke babak berikutnya ditolak! Kuis belum selesai.");
            }
        }
    }

    IEnumerator GantiBabakCoroutine(int babakLama)
    {
        cutscene.SetActive(true);
        yield return new WaitForSeconds(3f);

        // Naikkan angka babak aktif ke babak selanjutnya
        int babakBaru = babakLama + 1;
        PlayerPrefs.SetInt("BabakAktif", babakBaru);
        
        // Reset status kuis untuk babak baru (0 = belum dikerjakan)
        PlayerPrefs.SetInt("KuisSelesai_Babak_" + babakBaru, 0);
        PlayerPrefs.Save();

        // Panggil sistem jam/clock bawaan game Anda
        if (GameClock.instance != null)
        {
            GameClock.instance.gantibbk();
        }

        // Teleportasi player ke titik awal halaman utama
        mc.position = tujuan.position;
        
        cutscene.SetActive(false);
        
        // Karena sistem melompati halaman utama secara loop, objek trigger ini diaktifkan lagi nanti 
        // atau biarkan hancur tergantung manajemen prefab Anda.
        gameObject.SetActive(false);
    }
}