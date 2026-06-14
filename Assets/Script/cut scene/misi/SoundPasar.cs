using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPasar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Sound.instance.StopMusic();
        Sound.instance.PlayMusic(Sound.instance.gamePasar);
    }
    
}
    private void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        Sound.instance.StopMusic();
        Sound.instance.PlayMusic(Sound.instance.Halaman);
    }
    
}
}
