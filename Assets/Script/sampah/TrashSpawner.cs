using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnTrash();
    }

    void SpawnTrash()
    {
        foreach (Transform point in spawnPoints)
        {
            int randomTrash = Random.Range(0, trashPrefabs.Length);

            Instantiate(
                trashPrefabs[randomTrash],
                point.position,
                Quaternion.identity
            );
        }
    }
}