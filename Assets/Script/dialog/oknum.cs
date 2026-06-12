using UnityEngine;

public class oknum : MonoBehaviour
{
    public GameObject Oknum;

    bool sudahNgomong = false;
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
        if (other.CompareTag("Player") && !sudahNgomong)
        {
            sudahNgomong = true;
            Intro.instance.Babak1_Jalan();
        }
    }
}