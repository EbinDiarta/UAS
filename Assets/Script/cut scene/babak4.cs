using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class babak4 : MonoBehaviour
{
    public GameObject cutscene;

    private bool sudahTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sudahTrigger)
        {
            sudahTrigger = true;
            StartCoroutine(GantiBabakCoroutine());
        }
    }

    IEnumerator GantiBabakCoroutine()
    {
        cutscene.SetActive(true);
        yield return new WaitForSeconds(3f);
        SimpanSampahTersisa();
        GameClock.instance.gantibbk();
        SceneManager.LoadScene(SceneData.stlhbanjir);
        cutscene.SetActive(false);
        gameObject.SetActive(false);
    }
    void SimpanSampahTersisa()
{
    TrashData.remainingTrashIDs.Clear();

    Trash[] semuaSampah = FindObjectsOfType<Trash>();

    foreach (Trash sampah in semuaSampah)
    {
        TrashData.remainingTrashIDs.Add(
            sampah.trashID
        );
    }

    Debug.Log(
        "Sampah tersisa : " +
        TrashData.remainingTrashIDs.Count
    );
}
}