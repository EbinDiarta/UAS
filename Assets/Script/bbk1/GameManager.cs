using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Pause;
    public GameObject PauseBtn;
    public void pause()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale = 0f;
        Pause.SetActive(true);
        PauseBtn.SetActive(false);
    }
    public void restart()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale= 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Pause.SetActive(false);
        PauseBtn.SetActive(true);
    }
    public void resume()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Time.timeScale= 1f;
        Pause.SetActive(false);
        PauseBtn.SetActive(true);
    }
    public void exit()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        SceneManager.LoadScene(SceneData.rumah);
    }
    public void masuk_kamar()
{
    if (Sound.instance != null)
    {
        Sound.instance.PlaySFX(Sound.instance.tab);
    }
    
    PlayerPrefs.DeleteKey("useSpawn");
    PlayerPrefs.DeleteKey("KeluarKelas");

    PlayerPrefs.SetInt("MasukKamar", 1);

    SceneManager.LoadScene(SceneData.game1);
}
    public void halaman()
{
    if (Sound.instance != null)
    {
        Sound.instance.PlaySFX(Sound.instance.tab);
    }
    SceneManager.LoadScene(SceneData.halaman);
}
    public void masukRumah()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        PlayerPrefs.SetInt("useSpawn", 1);
        SceneManager.LoadScene(SceneData.rumah);
    }

    public void masukkelas()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        SceneManager.LoadScene(SceneData.sekolah);
    }

}
