using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class masuksekolah : MonoBehaviour
{
    public Transform mc;
    public Transform tujuan;

    public void Pindah()
    {
        mc.transform.position = tujuan.transform.position;
    }
}
