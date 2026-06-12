using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jmbtntopsr : MonoBehaviour
{
    public GameObject cutscene;
    public Transform mc;
    public Transform tujuan;

    void Start()
    {
        cutscene.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mc.transform.position = tujuan.transform.position;
            cutscene.SetActive(true);
        }
    }
}
