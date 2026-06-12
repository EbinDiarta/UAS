using UnityEngine;

public class SmellZone : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (GameClock.instance != null &&
    GameClock.instance.currentDay >= 1)
{
    HoldBreathManager.instance.StartHoldBreath();
}
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            HoldBreathManager.instance.ExitZone();
        }
    }
}