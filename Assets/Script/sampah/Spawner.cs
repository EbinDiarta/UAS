using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject[] trashPrefabs;

    [Header("Babak 1")]
    public Transform[] spawnBabak1;

    [Header("Babak 2")]
    public Transform[] spawnBabak2;

    [Header("Babak 3")]
    public Transform[] spawnBabak3;

    public static HashSet<int> cleanedTrash = new HashSet<int>();

    void Start()
    {
        SpawnTrash();
    }
    public void RespawnTrash()
{
    GameObject[] trashes = GameObject.FindGameObjectsWithTag("Trash");

    foreach (GameObject trash in trashes)
    {
        Destroy(trash);
    }

    SpawnTrash();
}
    void SpawnTrash()
    {
        Transform[] spawnPoints = GetSpawnPoints();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (cleanedTrash.Contains(i))
                continue;

            int randomTrash = Random.Range(0, trashPrefabs.Length);

            GameObject trash = Instantiate(
                trashPrefabs[randomTrash],
                spawnPoints[i].position,
                Quaternion.identity
            );

            TrashItem item = trash.GetComponent<TrashItem>();

            if (item != null)
                item.Init(i);
        }
    }

    Transform[] GetSpawnPoints()
    {
        int day = GameClock.instance.currentDay;

        switch (day)
        {
            case 0:
                return spawnBabak1;

            case 1:
                return spawnBabak2;

            case 2:
                return spawnBabak3;

            default:
                return spawnBabak1;
        }
    }
}