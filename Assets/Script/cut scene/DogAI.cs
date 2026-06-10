using UnityEngine;

public class DogAI : MonoBehaviour
{
    public Transform player;

    [Header("Jarak")]
    public float jarakDeteksi = 5f;
    public float jarakBerhenti = 7f;

    [Header("Speed")]
    public float speed = 3f;

    [Header("Timer Kejar")]
    public float waktuTrigger = 5f;

    [Header("Area Patrol")]
    public Transform titikA;
    public Transform titikB;

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
        float jarak = Vector2.Distance(transform.position, player.position);

        // START CHASE
        if (jarak < jarakDeteksi)
        {
            mengejar = true;
        }

        // STOP CHASE
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

    // =========================
    // CHASE
    // =========================
    void KejarPlayer()
    {
        Vector2 arah = (player.position - transform.position).normalized;
        transform.position += (Vector3)arah * speed * Time.deltaTime;
    }

    // =========================
    // PATROL (BOLAK-BALIK)
    // =========================
    void Patrol()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPatrol,
            speed * Time.deltaTime
        );

        // kalau sampai titik → pindah target
        if (Vector2.Distance(transform.position, targetPatrol) < 0.2f)
        {
            if (targetPatrol == titikA.position)
                targetPatrol = titikB.position;
            else
                targetPatrol = titikA.position;
        }
    }

    // =========================
    // TIMER
    // =========================
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
        Debug.Log("Dikejar terlalu lama!");

        Intro.instance.Babak2_SetelahAnjing();

        mengejar = false;
    }
}