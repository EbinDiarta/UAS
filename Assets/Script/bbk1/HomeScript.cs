using UnityEngine;

public class HomeScript : MonoBehaviour
{
    public GameObject InfoApk;
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneData.game1);
    }
    public void infoapk()
    {
        InfoApk.SetActive(true);
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
    }
    public void close()
    {
        InfoApk.SetActive(false);
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
    }
}
