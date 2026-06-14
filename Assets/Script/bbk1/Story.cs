using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public Image bingkai;
    public Image fadePanel;
    public Sprite[] tutorial;

    private int index = 0;

    private void Start()
    {
        bingkai.gameObject.SetActive(true);
        fadePanel.gameObject.SetActive(true);
        StartCoroutine(BanjirCoroutine());
        Sound.instance.StopMusic();
        Sound.instance.PlayBanjir();
    }

    IEnumerator BanjirCoroutine()
    {
        while (index < tutorial.Length)
        {
            yield return StartCoroutine(FadeIn());

            bingkai.sprite = tutorial[index];

            yield return StartCoroutine(FadeOut());

            yield return new WaitForSeconds(2f);

            index++;
        }
        yield return StartCoroutine(FadeIn());

        bingkai.gameObject.SetActive(false);

        yield return StartCoroutine(FadeOut());

        fadePanel.gameObject.SetActive(false);
        Sound.instance.StopBanjirSmooth();

        Intro.instance.banjir();
        Sound.instance.PlayMusic(Sound.instance.SetelahBanjir);
    }

    IEnumerator FadeIn()
{
    float t = 0;

    Vector3 posisiAwal = bingkai.rectTransform.localPosition;

    while (t < 1)
    {
        t += Time.deltaTime * 2f;

        Color c = fadePanel.color;
        c.a = Mathf.Lerp(0, 1, t);
        fadePanel.color = c;
        if (t > 0.5f)
        {
            bingkai.rectTransform.localPosition =
                posisiAwal + new Vector3(
                    Random.Range(-10f, 10f),
                    Random.Range(-10f, 10f),
                    0);
        }

        yield return null;
    }

    bingkai.rectTransform.localPosition = posisiAwal;
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
    }
}