using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jmbtntopsr : MonoBehaviour
{
    public GameObject cutscene;
    public GameObject cutscene2;
    public GameObject zone;
    public Transform mc;
    public Transform tujuan;

    void Start()
    {
        cutscene.SetActive(false);
        cutscene2.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mc.transform.position = tujuan.transform.position;
            zone.SetActive(false);
            if (GameClock.instance.currentDay < 3)
            {
                cutscene.SetActive(true);
                cutscene2.SetActive(false);
            } else if(GameClock.instance.currentDay >= 3)
            {
                cutscene.SetActive(false);
                cutscene2.SetActive(true);
            }
        }
    }
}
