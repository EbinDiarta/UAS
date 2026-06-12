using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public int id;

    public void Init(int newId)
    {
        id = newId;
    }

    void OnMouseDown()
    {
        TrashSpawner.cleanedTrash.Add(id);
        Destroy(gameObject);
    }
}