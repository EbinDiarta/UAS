using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameClock : MonoBehaviour
{
    public static GameClock instance;

    [Header("UI")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI clockText;

    [Header("Time")]
    public int hour = 7;
    public int minute = 0;
    public float realSecondsPerMinute = 0.5f;

    private float timer;

    [Header("Progress")]
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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= realSecondsPerMinute)
        {
            timer = 0;
            AddMinute();
        }

        UpdateUI();
    }

    void AddMinute()
    {
        minute++;

        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour >= 24)
        {
            hour = 0;
            NextDay();
        }
    }

    void NextDay()
    {
        currentDay++;

        if (currentDay >= days.Length)
        {
            Time.timeScale = 0;
            Debug.Log("GAME TAMAT");
            return;
        }

        SaveData();

        // 🔥 Set spawn ke awal map
        PlayerPrefs.SetString("SpawnPoint", "SpawnAwal");

        // 🔥 Reload scene (reset dunia)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        if (dayText != null)
            dayText.text = days[currentDay];

        if (clockText != null)
            clockText.text =
                hour.ToString("00") + ":" +
                minute.ToString("00");
    }

    public void Sleep()
    {
        hour = 6;
        minute = 0;
        NextDay();
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("Day", currentDay);
        PlayerPrefs.SetInt("Hour", hour);
        PlayerPrefs.SetInt("Minute", minute);
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        currentDay = PlayerPrefs.GetInt("Day", 0);
        hour = PlayerPrefs.GetInt("Hour", 7);
        minute = PlayerPrefs.GetInt("Minute", 0);
    }

    public int GetDayIndex()
    {
        return currentDay;
    }
}