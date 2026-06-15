using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GantiBabak : MonoBehaviour
{
    public GameObject cutscene;
    public Transform mc;
    public Transform tujuan;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GantiBabakCoroutine());
        }
    }

    IEnumerator GantiBabakCoroutine()
    {
        cutscene.SetActive(true);
        yield return new WaitForSeconds(3f);
        GameClock.instance.gantibbk();
        mc.position = tujuan.position;
        cutscene.SetActive(false);
        gameObject.SetActive(false);
    }
}

