using UnityEngine;
using System.Collections;

public class BedSleep : MonoBehaviour
{
    public GameObject img;
    public void kasur()
    {
        GameClock clock = GameClock.instance;


        if(clock.hour >= 20 || clock.hour <= 5)
        {
            StartCoroutine(Sleep());
        }
        else
        {
            Debug.Log("Belum waktunya tidur");
        }
    }
    
    IEnumerator Sleep()
    {
        img.SetActive(true);
        GameClock clock = GameClock.instance;
        FadeIn.instance.masuk();
        yield return new WaitForSeconds(0.01f);

        clock.hour = 5;
        clock.minute = 0;

        clock.currentDay++;

        if(clock.currentDay >= clock.days.Length)
        {
            clock.currentDay = 0;
        }


    }
}