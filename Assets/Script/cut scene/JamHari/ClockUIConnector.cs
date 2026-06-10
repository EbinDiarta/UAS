using UnityEngine;
using TMPro;

public class ClockUIConnector : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI clockText;

    void Start()
    {
        if(GameClock.instance != null)
        {
            GameClock.instance.dayText = dayText;
            GameClock.instance.clockText = clockText;
        }
        else
        {
            Debug.LogError("GameClock instance tidak ditemukan!");
        }
    }
}