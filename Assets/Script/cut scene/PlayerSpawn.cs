using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    [Header("Spawn Awal")]
    public Transform startPoint;

    void Start()
    {
        if (SceneResume.instance != null && SceneResume.instance.hasSavedPosition)
        {
            if (SceneManager.GetActiveScene().name == SceneResume.instance.lastSceneName)
            {
                transform.position = SceneResume.instance.playerPosition;
                return;
            }
        }
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
    }
}