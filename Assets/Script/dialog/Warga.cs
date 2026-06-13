using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warga : MonoBehaviour
{
    public GameObject Oknum;

    public static bool sudahNgomong = false;

    void Start()
    {
        if (GameClock.instance != null)
        {
            Oknum.SetActive(GameClock.instance.currentDay == 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&
            !sudahNgomong &&
            GameClock.instance != null &&
            GameClock.instance.currentDay == 2)
        {
            sudahNgomong = true;
            Intro.instance.Babak4_Warga();
        }
    }
}
