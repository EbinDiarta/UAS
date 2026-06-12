using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SekolahManager : MonoBehaviour
{
        public void pulang()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
    
        SceneManager.LoadScene(SceneData.pulangsklh);
    }
    public void sklh()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }


        PlayerPrefs.SetInt("useSpawn", 1);
    
        SceneManager.LoadScene(SceneData.halaman);

    }
    public void keluar_kelas()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }


        PlayerPrefs.SetInt("KeluarKelas", 1);
    
        SceneManager.LoadScene(SceneData.halaman);

    }
}
