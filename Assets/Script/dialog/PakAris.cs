using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PakAris : MonoBehaviour
{
    public static PakAris instance;
    public GameObject Oknum;
    public static bool sudahNgomong = false;
    bool statusAktif = false;
    
        void Start()
    {
        UpdateStatus();
    }

    void Update()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (GameClock.instance == null) return;

        bool shouldActive = GameClock.instance.currentDay >= 1;

        if (shouldActive != statusAktif)
        {
            statusAktif = shouldActive;
            Oknum.SetActive(statusAktif);
        }
    }
    
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

