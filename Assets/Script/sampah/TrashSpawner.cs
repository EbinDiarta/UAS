using UnityEngine;
using System.Collections.Generic;

public class TrashSpawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public Transform[] spawnPoints;

    public static HashSet<int> cleanedTrash = new HashSet<int>();

    void Start()
    {
        SpawnTrash();
    }

    void SpawnTrash()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (cleanedTrash.Contains(i)) continue;

            int randomTrash = Random.Range(0, trashPrefabs.Length);

            GameObject trash = Instantiate(
                trashPrefabs[randomTrash],
                spawnPoints[i].position,
                Quaternion.identity
            );

            trash.GetComponent<TrashItem>().Init(i);
        }
    }
}