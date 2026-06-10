using System.Collections.Generic;
using UnityEngine;

public class SoalLoader : MonoBehaviour
{
    public TextAsset fileSoal;
    public List<Soal> semuaSoal = new List<Soal>();

    void Awake()
    {
        LoadSoal();
    }

    void LoadSoal()
    {
        string[] lines = fileSoal.text.Split('\n');

        Soal s = null;
        List<string> jawaban = new List<string>();

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (string.IsNullOrEmpty(line)) continue;

            //  DETEKSI SOAL (1. 2. 3.)
            if (char.IsDigit(line[0]))
            {
                if (s != null)
                {
                    s.jawaban = jawaban.ToArray();
                    semuaSoal.Add(s);
                }

                s = new Soal();
                jawaban = new List<string>();

                int titik = line.IndexOf('.');
                s.pertanyaan = line.Substring(titik + 1).Trim();
            }

            //  JAWABAN A B C D
            else if (line.StartsWith("A.") || line.StartsWith("B.") ||
                    line.StartsWith("C.") || line.StartsWith("D."))
            {
                jawaban.Add(line.Substring(2).Trim());
            }

            //  JAWABAN BENAR
            else if (line.StartsWith("Jawaban"))
            {
                char huruf = line[line.Length - 1];
                s.jawabanBenar = huruf - 'A';
            }
        }

        // TAMBAH SOAL TERAKHIR
        if (s != null)
        {
            s.jawaban = jawaban.ToArray();
            semuaSoal.Add(s);
        }

        Debug.Log("Total soal berhasil di-load: " + semuaSoal.Count);
    }
}