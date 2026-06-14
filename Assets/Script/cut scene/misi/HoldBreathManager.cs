using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoldBreathManager : MonoBehaviour
{
    public static HoldBreathManager instance;

    public Transform mc;
    public Transform tujuan;

    public GameObject panel;
    public Slider breathBar;

    public float targetTime = 120f;

    float breath = 100f;
    float surviveTime;
    bool activeGame;

    public bool hasVomited;

    Vector3 camOriginalPos;

    void Awake()
    {
        instance = this;
        panel.SetActive(false);
        camOriginalPos = Camera.main.transform.position;
    }

    void Update()
    {
        if (!activeGame) return;

        surviveTime += Time.deltaTime;

        float difficulty = 1f + (surviveTime / targetTime) * 2f;

        if (surviveTime > targetTime * 0.8f)
        {
            difficulty += 1.5f;
        }

        breath -= 15f * difficulty * Time.deltaTime;

        breathBar.value = breath;

        HandleEffects();

        if (surviveTime >= targetTime)
        {
            Win();
        }

        if (breath <= 0)
        {
            Fail();
        }
    }

    void HandleEffects()
    {
        if (breath < 40)
        {
            Camera.main.transform.position =
                camOriginalPos + (Vector3)Random.insideUnitCircle * 0.05f;
        }
        else
        {
            Camera.main.transform.position = camOriginalPos;
        }
    }

    public void StartHoldBreath()
    {
        panel.SetActive(true);

        breath = 100f;
        surviveTime = 0f;

        breathBar.maxValue = 100;
        breathBar.value = 100;

        activeGame = true;
    }

    public void TapButton()
    {
        if (!activeGame) return;

        float randomBoost = Random.Range(5f, 10f);
        breath += randomBoost;

        if (Random.value < 0.2f)
        {
            breath -= 10f;
        }

        if (breath > 100f)
            breath = 100f;
    }

    public void ExitZone()
    {
        activeGame = false;
        panel.SetActive(false);
        Camera.main.transform.position = camOriginalPos;
    }

    void Win()
    {
        activeGame = false;
        panel.SetActive(false);
        Camera.main.transform.position = camOriginalPos;

        Intro.instance.Babak2_SetelahTahanBau();
        Debug.Log("Berhasil!");
    }

    void Fail()
    {
        activeGame = false;
        panel.SetActive(false);

        hasVomited = true;

        StartCoroutine(MuntahSequence());
    }

    IEnumerator MuntahSequence()
    {
        yield return new WaitForSeconds(1f);

        mc.position = tujuan.position;

        Camera.main.transform.position = camOriginalPos;

        Debug.Log("Muntah");
    }
}