using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    public static Sound instance;

    [Header("Music")]
    public AudioClip homeMusic;
    public AudioClip gameMusic;
    public AudioClip gameOverMusic;
    public AudioClip gameWinMusic;

    [Header("SFX")]
    public AudioClip salah;
    public AudioClip benar;
    public AudioClip win;
    public AudioClip tab;
    public AudioClip jump;

    private AudioSource musicSource;
    private AudioSource sfxSource;

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
            return; //  penting
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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        if (musicSource == null || sfxSource == null)
        {
            SetupAudioSource();
        }

        if (scene.name == "Home")
            PlayMusic(homeMusic);
        else if (scene.name == "Select")
            PlayMusic(homeMusic);
        else if (scene.name == "Game1")
            PlayMusic(gameMusic);
        else if (scene.name == "Game2")
            PlayMusic(gameMusic);
    }

    void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource == null)
        {
            SetupAudioSource();
        }

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        if (sfxSource == null)
        {
            SetupAudioSource();
        }

        sfxSource.PlayOneShot(clip);
    }

    // GAME OVER & WIN
    public void PlayGameOverMusic()
    {
        if (gameOverMusic == null) return;

        if (musicSource == null)
        {
            SetupAudioSource();
        }

        musicSource.Stop();
        musicSource.clip = gameOverMusic;
        musicSource.loop = false;
        musicSource.Play();
    }
    // GAME WIN
    public void PlayWinSound()
    {
        if (gameWinMusic == null) return;
        if (musicSource == null)
        {
            SetupAudioSource();
        }
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