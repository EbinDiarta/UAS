using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Scene Tujuan")]
    public string targetScene;

    [Header("Spawn Position di Scene Tujuan")]
    public Vector3 spawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (SceneResume.instance != null)
            {
                SceneResume.instance.SavePlayer(spawnPosition, targetScene);
            }

            // Pindah scene
            SceneManager.LoadScene(targetScene);
        }
    }
}