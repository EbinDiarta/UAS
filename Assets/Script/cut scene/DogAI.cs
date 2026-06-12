using UnityEngine;

public class DogAI : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Detection")]
    public float jarakDeteksi = 5f;
    public float jarakBerhenti = 7f;

    [Header("Movement")]
    public float speed = 3f;

    [Header("Patrol Points")]
    public Transform titikA;
    public Transform titikB;

    [Header("Game Progress")]
    public int aktifMulaiHari = 1;

    private bool mengejar = false;
    private Vector3 targetPatrol;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (titikA != null)
        {
            targetPatrol = titikA.position;
        }
    }

    private void Update()
    {
        // Belum mencapai babak yang ditentukan
        if (GameClock.instance == null ||
    GameClock.instance.currentDay < aktifMulaiHari)
{
    return;
}

        // Jika player belum diisi
        if (player == null)
            return;

        float jarak = Vector2.Distance(
            transform.position,
            player.position
        );

        // Mulai mengejar
        if (jarak <= jarakDeteksi)
        {
            mengejar = true;
        }

        // Berhenti mengejar dan kembali patrol
        if (jarak > jarakBerhenti)
        {
            mengejar = false;
        }

        if (mengejar)
        {
            KejarPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void KejarPlayer()
    {
        Vector2 arah =
            (player.position - transform.position).normalized;

        Flip(arah);

        transform.position +=
            (Vector3)arah * speed * Time.deltaTime;
    }

    private void Patrol()
    {
        if (titikA == null || titikB == null)
            return;

        Vector2 arah =
            (targetPatrol - transform.position).normalized;

        Flip(arah);

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPatrol,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            targetPatrol) < 0.2f)
        {
            if (targetPatrol == titikA.position)
            {
                targetPatrol = titikB.position;
            }
            else
            {
                targetPatrol = titikA.position;
            }
        }
    }

    private void Flip(Vector2 arah)
    {
        if (sr == null)
            return;

        if (arah.x > 0)
        {
            sr.flipX = false;
        }
        else if (arah.x < 0)
        {
            sr.flipX = true;
        }
    }
}