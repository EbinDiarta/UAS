using System.Collections;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public Transform titikA;
    public Transform titikB;
    public float speed = 2f;
    public float waktuDiam = 2f;

    public Animator anim;

    private Vector3 target;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (titikA == null || titikB == null)
        {
            Debug.LogError("TitikA atau TitikB belum diisi!");
            return;
        }

        target = titikB.position;

        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            while (Vector2.Distance(transform.position, target) > 0.1f)
            {
                Vector2 arah = (target - transform.position).normalized;

                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target,
                    speed * Time.deltaTime
                );

                if (sr != null)
                {
                    if (arah.x > 0)
                        sr.flipX = false;
                    else if (arah.x < 0)
                        sr.flipX = true;
                }

                if (anim != null)
                    anim.SetBool("isWalking", true);

                yield return null;
            }

            if (anim != null)
                anim.SetBool("isWalking", false);

            yield return new WaitForSeconds(waktuDiam);

            target = (target == titikA.position) ? titikB.position : titikA.position;
        }
    }
}