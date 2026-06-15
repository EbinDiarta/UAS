using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum TrashType
    {
        Organik,
        NonOrganik
    }

    public TrashType jenisSampah;

    [HideInInspector]
    public string trashID;

    private void Awake()
    {
        trashID = gameObject.name.Replace("(Clone)", "");
    }
}