using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn1 : MonoBehaviour
{
    public static FadeIn1 instance;

    public Image panelHitam;
    public GameObject img;
    public float durasi = 1f;

    void Awake()
{
    if (instance == null)
    {
        instance = this;

        DontDestroyOnLoad(transform.root.gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}
    void Start()
    {
        StartCoroutine(FadeIn()); // dari hitam ke transparan
    }

    IEnumerator FadeIn()
    {
        img.SetActive(true);

        Color warna = panelHitam.color;
        float waktu = 0;

        while (waktu < durasi)
        {
            waktu += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(1, 0, waktu / durasi);

            panelHitam.color = new Color(
                warna.r,
                warna.g,
                warna.b,
                alpha
            );

            yield return null;
        }

        panelHitam.color = new Color(warna.r, warna.g, warna.b, 0);
        img.SetActive(false);
    }


    public IEnumerator FadeOut()
    {
        img.SetActive(true);

        Color warna = panelHitam.color;
        float waktu = 0;

        while (waktu < durasi)
        {
            waktu += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(0, 1, waktu / durasi);

            panelHitam.color = new Color(
                warna.r,
                warna.g,
                warna.b,
                alpha
            );

            yield return null;
        }

        panelHitam.color = new Color(warna.r, warna.g, warna.b, 1);
    }
}