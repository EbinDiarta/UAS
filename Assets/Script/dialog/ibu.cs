using UnityEngine;

public class ibu : MonoBehaviour
{
    private bool sudahNgomong = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&
            !sudahNgomong &&
            GameClock.instance != null &&
            GameClock.instance.currentDay == 0)
        {
            sudahNgomong = true;
            Intro.instance.Babak1_Kamar();
        }
    }
}