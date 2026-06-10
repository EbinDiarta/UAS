using UnityEngine;
using TMPro;

public class GameClock : MonoBehaviour
{
    public static GameClock instance;

    [Header("UI")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI clockText;

    [Header("Waktu Awal")]
    public int hour = 7;
    public int minute = 0;

    [Header("Kecepatan Waktu")]
    public float realSecondsPerMinute = 0.5f;

    private float timer;

    public int currentDay = 0;

    public string[] days =
    {
        "Babak 1: Hari Pertama",
        "Babak 2: Hari Kedua",
        "Babak 3: Hari Ketiga",
        "Babak 4: Hari Keempat",
        "Babak 5: Hari Kelima"
    };

    void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= realSecondsPerMinute)
        {
            timer = 0;
            AddMinute();
        }

        UpdateUI();
    }

    void AddMinute()
    {
        minute++;

        if(minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if(hour >= 24)
        {
            hour = 0;
            NextDay();
        }
    }

    void NextDay()
    {
        currentDay++;

        if(currentDay >= days.Length)
        {
            currentDay = 0;
        }
    }

    void UpdateUI()
    {
        if(dayText != null)
            dayText.text = days[currentDay];

        if(clockText != null)
            clockText.text =
                hour.ToString("00") + ":" +
                minute.ToString("00");
    }

    public void Sleep()
    {
        hour = 6;
        minute = 0;

        NextDay();

        UpdateUI();

        Debug.Log("Tidur...");
    }

    public string GetDay()
    {
        return days[currentDay];
    }
}