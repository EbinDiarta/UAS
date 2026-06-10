using System.Collections;
using UnityEngine;

public class Suasana : MonoBehaviour
{
    public GameObject siang;
    public GameObject malam;

    void Start()
    {
        StartCoroutine(CekSuasana());
    }

    IEnumerator CekSuasana()
    {
        while (true)
        {
            UpdateSuasana();
            yield return new WaitForSeconds(1f);
        }
    }

    void UpdateSuasana()
    {
        GameClock clock = GameClock.instance;

        if (clock == null) return;

        if (clock.hour >= 18 || clock.hour <= 6)
        {
            malam.SetActive(true);
            siang.SetActive(false);
        }
        else
        {
            malam.SetActive(false);
            siang.SetActive(true);
        }
    }
}