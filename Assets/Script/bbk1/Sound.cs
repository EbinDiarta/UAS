using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Sound : MonoBehaviour
{
    public static Sound instance;

    AudioSource musicSource;
    AudioSource sfxSource;
    AudioSource banjirSource;

    [Header("Music")]
    public AudioClip homeMusic;
    public AudioClip gameMusic;
    public AudioClip gamePasar;
    public AudioClip Tegang;
    public AudioClip Halaman;
    public AudioClip SetelahBanjir;
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
    public AudioClip Banjir;

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

        if (sources.Length >= 3)
        {
            musicSource = sources[0];
            sfxSource = sources[1];
            banjirSource = sources[2];
        }
        else
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
            banjirSource = gameObject.AddComponent<AudioSource>();
        }

        musicSource.loop = true;
        banjirSource.loop = true;
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
        }
    }

    // ================= MUSIC =================
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // ================= SFX =================
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxSource.PlayOneShot(clip);
    }

    // ================= BANJIR =================
    public void PlayBanjir()
    {
        if (Banjir == null) return;

        banjirSource.clip = Banjir;
        banjirSource.loop = true;
        banjirSource.volume = 1f;
        banjirSource.Play();
    }

    public void StopBanjir()
    {
        if (banjirSource != null && banjirSource.isPlaying)
        {
            banjirSource.Stop();
        }
    }

    public void StopBanjirSmooth()
    {
        StartCoroutine(FadeOutBanjir());
    }

    IEnumerator FadeOutBanjir()
    {
        float t = 0;
        float startVolume = banjirSource.volume;

        while (t < 1)
        {
            t += Time.deltaTime;
            banjirSource.volume = Mathf.Lerp(startVolume, 0, t);
            yield return null;
        }

        banjirSource.Stop();
        banjirSource.volume = startVolume;
    }

    // ================= GAME STATE =================
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
    public void StopMusic()
{
    if (musicSource != null && musicSource.isPlaying)
    {
        musicSource.Stop();
    }
}
}