using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public Image bingkai;
    public Sprite[] tutorial;

    private int index = 0;

    private void Start()
    {
        StartCoroutine(BanjirCoroutine());
    }

    IEnumerator BanjirCoroutine()
    {
        while (index < tutorial.Length)
        {
            bingkai.sprite = tutorial[index];

            yield return new WaitForSeconds(2f);

            index++;
        }

        bingkai.gameObject.SetActive(false);
    }
}