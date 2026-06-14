using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class sekolahtohalaman : MonoBehaviour
{
    public CameraFollow cam;
    public Image fadePanel;
    public Transform mc;
    public Transform tujuan;
    
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        fadePanel.gameObject.SetActive(true);
        

        StartCoroutine(teleport());
    }
}

    IEnumerator teleport()
{
    fadePanel.gameObject.SetActive(true);

    yield return StartCoroutine(FadeIn());

    mc.position = tujuan.position;

    cam.SnapToTarget();

    yield return StartCoroutine(FadeOut());

    fadePanel.gameObject.SetActive(false);
}
    public void Pindah()
    {
        mc.transform.position = tujuan.transform.position;
        
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
