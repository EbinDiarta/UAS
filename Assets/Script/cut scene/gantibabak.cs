using System.Collections;
using UnityEngine;

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
        mc.position = tujuan.position;
        GameClock.instance.gantibbk();
        cutscene.SetActive(false);
        gameObject.SetActive(false);
    }
}