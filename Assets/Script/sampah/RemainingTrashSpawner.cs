using UnityEngine;

public class RemainingTrashSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    void Start()
    {
        SpawnTrash();
    }

    void SpawnTrash()
    {
        for (int i = 0; i < TrashData.remainingTrashIDs.Count; i++)
        {
            if (i >= spawnPoints.Length)
                break;

            string prefabName =
                TrashData.remainingTrashIDs[i];

            GameObject prefab =
                Resources.Load<GameObject>(
                    "Trash/" + prefabName);

            if (prefab != null)
            {
                Instantiate(
                    prefab,
                    spawnPoints[i].position,
                    Quaternion.identity
                );
            }
        }

        TrashData.remainingTrashIDs.Clear();
    }
}