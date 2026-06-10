using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public static FadeIn instance;
    public Image panelHitam;
    public GameObject img;
    public float durasi = 3f;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(FadeMasuk());
    }
    public void masuk()
    {
        StartCoroutine(FadeMasuk());
    }

    IEnumerator FadeMasuk()
    {
        Color warna = panelHitam.color;

        float waktu = 0;

        while (waktu < durasi)
        {
            waktu += Time.deltaTime;

            float alpha = Mathf.Lerp(1, 0, waktu / durasi);

            panelHitam.color = new Color(
                warna.r,
                warna.g,
                warna.b,
                alpha
            );

            yield return null;
        }

        panelHitam.color = new Color(
            warna.r,
            warna.g,
            warna.b,
            0
        );
        img.SetActive(false);
    }
}