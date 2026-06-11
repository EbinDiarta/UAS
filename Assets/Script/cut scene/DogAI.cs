using UnityEngine;

public class DogAI : MonoBehaviour
{
    public Transform player;

    public float jarakDeteksi = 5f;
    public float jarakBerhenti = 7f;

    public float speed = 3f;

    public float waktuTrigger = 5f;

    public Transform titikA;
    public Transform titikB;

    public int aktifMulaiHari = 1;

    private bool mengejar = false;
    private float timerKejar = 0f;
    private bool sudahTrigger = false;

    private Vector3 targetPatrol;

    void Start()
    {
        targetPatrol = titikA.position;
    }

    void Update()
    {
        if (GameClock.instance == null || GameClock.instance.currentDay < aktifMulaiHari)
        {
            return;
        }

        float jarak = Vector2.Distance(transform.position, player.position);

        if (jarak <= jarakDeteksi)
        {
            mengejar = true;
        }

        if (jarak > jarakBerhenti)
        {
            mengejar = false;
            ResetTimer();
        }

        if (mengejar)
        {
            KejarPlayer();
            HitungTimer();
        }
        else
        {
            Patrol();
        }
    }

    void KejarPlayer()
    {
        Vector2 arah = (player.position - transform.position).normalized;
        transform.position += (Vector3)arah * speed * Time.deltaTime;
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPatrol,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPatrol) < 0.2f)
        {
            if (targetPatrol == titikA.position)
                targetPatrol = titikB.position;
            else
                targetPatrol = titikA.position;
        }
    }

    void HitungTimer()
    {
        if (sudahTrigger) return;

        timerKejar += Time.deltaTime;

        if (timerKejar >= waktuTrigger)
        {
            sudahTrigger = true;
            TriggerEvent();
        }
    }

    void ResetTimer()
    {
        timerKejar = 0f;
        sudahTrigger = false;
    }

    void TriggerEvent()
    {
        Intro.instance.Babak2_SetelahAnjing();
        mengejar = false;
    }
}