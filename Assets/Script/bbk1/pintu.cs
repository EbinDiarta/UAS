using UnityEngine;

public class pintu : MonoBehaviour
{   
    public GameObject ui;

    void Start()
    {
        ui.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ui.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ui.SetActive(false);
        }
    }
}
