using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LewatPasar : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform mc;
    public GameObject zone;
    public GameObject gantibbk;
    public GameObject gantibbk1;
    public Transform tujuan;
    public GameObject button;


    void Start()
    {
        button.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameClock.instance != null &&
            GameClock.instance.currentDay >= 1)
        {
            button.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            button.SetActive(false);
        }
    }
    public void LewatPasar1()
    {
        zone.SetActive(true);
        mc.transform.position = tujuan.transform.position;
        gantibbk.SetActive(false);
        gantibbk1.SetActive(false);
    }
}
