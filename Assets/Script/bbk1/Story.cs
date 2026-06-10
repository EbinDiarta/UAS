using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public Image bingkai;
    public Sprite[] tutorial;

    private int Index = 0;

    void Start()
    {
        bingkai.sprite = tutorial[Index];
    }

    public void selanjutnya()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Index++;
        if (Index > tutorial.Length)
        {
            Index = 0;
        }
        bingkai.sprite = tutorial[Index];
    }
    public void sebelumnya()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        Index--;
        if (Index < 0)
        {
            Index = tutorial.Length - 1;
        } 
        bingkai.sprite = tutorial[Index];
    }



    public void keluar()
    {
        if (Sound.instance != null)
        {
            Sound.instance.PlaySFX(Sound.instance.tab);
        }
        SceneManager.LoadScene(SceneData.home);
    }

}