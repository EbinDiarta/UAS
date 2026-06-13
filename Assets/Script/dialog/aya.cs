using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aya : MonoBehaviour
{
    
    public GameObject Oknum;

    public static bool sudahNgomong = false;

    void Start()
    {
        if (GameClock.instance != null)
        {
            Oknum.SetActive(GameClock.instance.currentDay == 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&
            !sudahNgomong &&
            GameClock.instance != null &&
            GameClock.instance.currentDay == 1)
        {
            sudahNgomong = true;
            Intro.instance.Babak2_Sekolah();
        }
    }
}
