using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoObject;
    public GameObject KataKata;

    public ParticleSystem particle1;
    public ParticleSystem particle2;

    public AudioSource sfxSource;
    public AudioClip petasan;

    void Start()
    {
        KataKata.SetActive(false);
        particle1.Stop();
        particle2.Stop();

        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        videoObject.SetActive(false);

        particle1.Play();
        particle2.Play();

        sfxSource.PlayOneShot(petasan);
        StartCoroutine(LoadHome());
    }

    IEnumerator LoadHome()
    {
        yield return new WaitForSeconds(5f);
        KataKata.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneData.home);
    }
}