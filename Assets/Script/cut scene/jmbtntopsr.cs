using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class jmbtntopsr : MonoBehaviour
{
    public CameraFollow cam;
    public Image fadePanel;

    public GameObject cutscene;
    public GameObject cutscene2;
    public GameObject zone;

    public Transform mc;
    public Transform tujuan;

    bool isTeleporting;

    void Start()
    {
        cutscene.SetActive(false);
        cutscene2.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        isTeleporting = true;

        fadePanel.gameObject.SetActive(true);

        yield return StartCoroutine(FadeIn());

        mc.position = tujuan.position;

        yield return null;

        cam.SnapToTarget();

        zone.SetActive(false);

        if (GameClock.instance.currentDay < 2)
        {
            cutscene.SetActive(true);
            cutscene2.SetActive(false);
        }
        else
        {
            cutscene.SetActive(false);
            cutscene2.SetActive(true);
        }

        yield return StartCoroutine(FadeOut());

        fadePanel.gameObject.SetActive(false);

        isTeleporting = false;
    }

    IEnumerator FadeIn()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(0, 1, t);
            fadePanel.color = c;

            yield return null;
        }

        Color akhir = fadePanel.color;
        akhir.a = 1f;
        fadePanel.color = akhir;
    }

    IEnumerator FadeOut()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2f;

            Color c = fadePanel.color;
            c.a = Mathf.Lerp(1, 0, t);
            fadePanel.color = c;

            yield return null;
        }

        Color akhir = fadePanel.color;
        akhir.a = 0f;
        fadePanel.color = akhir;
    }
}