using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LewatPasar : MonoBehaviour
{
    public CameraFollow cam;
    public Image fadePanel;

    public Transform mc;
    public GameObject zone;
    public GameObject gantibbk;
    public GameObject gantibbk1;
    public Transform tujuan;
    public GameObject button;

    void Start()
    {
        button.SetActive(false);
        fadePanel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&
            GameClock.instance != null &&
            GameClock.instance.currentDay >= 1)
        {
            button.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            button.SetActive(false);
        }
    }

    public void LewatPasar1()
    {
        StartCoroutine(PindahPasar());
    }

    IEnumerator PindahPasar()
    {
        fadePanel.gameObject.SetActive(true);

        yield return StartCoroutine(FadeIn());

        zone.SetActive(true);

        mc.position = tujuan.position;

        yield return null;

        cam.SnapToTarget();

        gantibbk.SetActive(false);
        gantibbk1.SetActive(false);

        yield return StartCoroutine(FadeOut());

        fadePanel.gameObject.SetActive(false);
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