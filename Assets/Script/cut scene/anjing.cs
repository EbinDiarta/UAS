using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class anjing : MonoBehaviour
{
    public Transform mc;
    public Transform tujuan;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameClock.instance == null || GameClock.instance.currentDay < 1)
            {
                return;
            }else if (GameClock.instance.currentDay >= 1)
            {
                mc.transform.position = tujuan.transform.position;
            }
        }
    }

    
}
