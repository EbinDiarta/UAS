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
        GameClock.instance.gantibbk();
        SceneManager.LoadScene(SceneData.stlhbanjir);
        cutscene.SetActive(false);
        gameObject.SetActive(false);
    }
}