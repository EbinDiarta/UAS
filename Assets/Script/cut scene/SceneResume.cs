using UnityEngine;

public class SceneResume : MonoBehaviour
{
    public static SceneResume instance;

    [Header("Player Data")]
    public Vector3 playerPosition;
    public string lastSceneName;
    public bool hasSavedPosition = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // tidak hancur saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Simpan data player
    public void SavePlayer(Vector3 position, string sceneName)
    {
        playerPosition = position;
        lastSceneName = sceneName;
        hasSavedPosition = true;
    }

    // Reset (optional)
    public void ResetData()
    {
        hasSavedPosition = false;
    }
}