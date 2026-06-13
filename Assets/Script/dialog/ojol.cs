using UnityEngine;

public class ojol : MonoBehaviour
{
    public GameObject Ojol;

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

        bool shouldActive = GameClock.instance.currentDay == 0;

        if (shouldActive != statusAktif)
        {
            statusAktif = shouldActive;
            Ojol.SetActive(statusAktif);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") &&
            !sudahNgomong &&
            GameClock.instance != null &&
            GameClock.instance.currentDay == 0)
        {
            sudahNgomong = true;
            Intro.instance.Babak_OjolDiPasar();
        }
    }
}