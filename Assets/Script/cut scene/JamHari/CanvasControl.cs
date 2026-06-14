using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControl : MonoBehaviour
{
    public string[] sceneTidakAktif;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool aktif = true;

        foreach (string namaScene in sceneTidakAktif)
        {
            if (scene.name == namaScene)
            {
                aktif = false;
                break;
            }
        }

        gameObject.SetActive(aktif);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}