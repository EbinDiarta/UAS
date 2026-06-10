using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibu : MonoBehaviour
{
    private bool sudahNgomong = false;
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sudahNgomong)
        {
            sudahNgomong = true;
            Intro.instance.Babak1_Kamar();
        }
    }
    void Update()
    {
    if (GameClock.instance.currentDay > 1){

        sudahNgomong = true;
    }
    }
}

