using UnityEngine;

public class reza : MonoBehaviour
{
    public static bool sudahNgomong = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sudahNgomong)
        {
            sudahNgomong = true;
            Intro.instance.Babak1_Sekolah();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        }
    }
}