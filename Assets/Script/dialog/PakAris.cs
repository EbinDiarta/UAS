using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PakAris : MonoBehaviour
{
    private bool sudahNgomong = false;
        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& !sudahNgomong)
        {
            sudahNgomong = true;
            Intro.instance.PakAris();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }
    }

