using UnityEngine;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoObject;

    public ParticleSystem particle1;
    public ParticleSystem particle2;

    void Start()
    {
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
    }
}