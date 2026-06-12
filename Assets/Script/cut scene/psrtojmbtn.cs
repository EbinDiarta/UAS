using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class psrtojmbtn : MonoBehaviour
{
    public Transform mc;
    public Transform tujuan;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mc.transform.position = tujuan.transform.position;
        }
    }
}
