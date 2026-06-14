using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    public static Sound instance;

    AudioSource musicSource;
    AudioSource sfxSource;

    [Header("Music")]
    public AudioClip homeMusic;
    public AudioClip gameMusic;
    public AudioClip tenseMusic;
    public AudioClip gameOverMusic;
    public AudioClip gameWinMusic;

    [Header("SFX UI")]
    public AudioClip tab;

    [Header("SFX Gameplay")]
    public AudioClip salah;
    public AudioClip benar;
    public AudioClip jump;

    [Header("SFX Atmosphere")]
    public AudioClip napas;
    public AudioClip heartbeat;
    public AudioClip muntah;
    public AudioClip anjing;
    public AudioClip langkahAir;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SetupAudioSource();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetupAudioSource()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        if (sources.Length >= 2)
        {
            musicSource = sources[0];
            sfxSource = sources[1];
        }
        else
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        musicSource.loop = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (musicSource == null || sfxSource == null)
        {
            SetupAudioSource();
        }

        switch (scene.name)
        {
            case "Home":
            case "Select":
                PlayMusic(homeMusic);
                break;

            case "Game1":
            case "Game2":
                PlayMusic(gameMusic);
                break;
        }
    }

    void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }

    public void PlayGameOverMusic()
    {
        if (gameOverMusic == null) return;

        musicSource.Stop();
        musicSource.clip = gameOverMusic;
        musicSource.loop = false;
        musicSource.Play();
    }

    public void PlayWinSound()
    {
        if (gameWinMusic == null) return;

        musicSource.Stop();
        musicSource.clip = gameWinMusic;
        musicSource.loop = false;
        musicSource.Play();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}