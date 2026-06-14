using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class masuksekolah : MonoBehaviour
{
    public CameraFollow cam;
    public Image fadePanel;

    public Transform mc;
    public Transform tujuan;

    public void Pindah()
    {
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        fadePanel.gameObject.SetActive(true);

        yield return StartCoroutine(FadeIn());

        mc.position = tujuan.position;

        yield return null;

        cam.SnapToTarget();

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