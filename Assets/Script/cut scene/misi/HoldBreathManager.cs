using UnityEngine;
using UnityEngine.UI;

public class HoldBreathManager : MonoBehaviour
{
    public static HoldBreathManager instance;

    public GameObject panel;
    public Slider breathBar;

    float breath = 100f;
    bool activeGame;

    public bool hasVomited;
    private void Awake()
    {
        instance = this;
        panel.SetActive(false);
    }

    void Update()
    {
        if (!activeGame) return;

        breath -= 15f * Time.deltaTime;

        breathBar.value = breath;

        if (breath <= 0)
        {
            Fail();
        }
    }

    public void StartHoldBreath()
    {
        panel.SetActive(true);

        breath = 100;

        breathBar.maxValue = 100;
        breathBar.value = 100;

        activeGame = true;
    }

    public void TapButton()
    {
        if (!activeGame) return;

        breath += 8;

        if (breath > 100)
            breath = 100;
    }

    public void ExitZone()
    {
        activeGame = false;
        panel.SetActive(false);
    }

    void Fail()
    {
        activeGame = false;
        panel.SetActive(false);

        hasVomited = true;

        Debug.Log("Muntah");
    }
}