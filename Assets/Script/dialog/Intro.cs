using TMPro;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public static Intro instance;

    public GameObject task;
    public GameObject text1;
    public TextMeshProUGUI intro;

// =======================
// BABAK 1
// =======================
string[] babak1_kamar =
{
    "Arik: Hari ini mau coba lagi... pasti bisa.",
    "Ibu: Udah bangun? Sarapan dulu sebelum pergi.",
    "Arik: Iya, Bu. Eh, Bapak ke mana?",
    "Ibu: Bapak udah pergi dari subuh, mancing kayaknya. Kenapa?",
    "Arik: Mau ngomong soal sampah di kali depan, Bu. Udah mampet parah.",
    "Ibu: Aduh, itu urusan Bapak sama warga. Kamu fokus sekolah dulu."
};

string[] babak1_jalan =
{
    "Arik: Pak, maaf... sampahnya jangan dibuang ke kali, Pak.",
    "Pak Darno: Heh? Ngomong sama siapa kamu, Nak?",
    "Pak Darno: Buang di sini udah dari dulu.",
    "Arik: Tapi kan bisa banjir, Pak—",
    "Pak Darno: Kamu masih kecil, mana ngerti urusan orang gede."
};

string[] babak1_sekolah =
{
    "Reza: Lo kenapa manyun?",
    "Arik: Tadi ketemu warga buang sampah ke kali lagi. Gue negur, diketawain.",
    "Reza: Serius? Respons mereka emang gitu terus ya.",
    "Reza: Sabar... mungkin mereka nggak tau dampaknya.",
    "Reza: Atau tau, tapi nggak peduli.",
    "Arik: Yang bikin frustrasi itu Bapak gue.",
    "Reza: Kenapa nggak minta Bapak lo yang action?",
    "Arik: Udah dicoba. Dibilangnya itu bukan prioritas."
};

// =======================
// BABAK 2
// =======================
string[] babak2_kamar =
{
    "Arik: Bu, Bapak udah pulang semalam?",
    "Ibu: Belum, kayaknya nginep di balai RT.",
    "Arik: Rapat apaan... bukan soal sampah pasti.",
    "Ibu: Hush. Udah, jalan sana nanti telat."
};

string[] babak2_setelah_anjing =
{
    "Pak Aris: Hahaha, dikejar anjing ya, Nak?",
    "Arik: Iya, Pak... Bapak lagi istirahat?",
    "Pak Aris: Iya, cape udah nyapu dari subuh, tapi sejam lagi balik berantakan lagi. Ngapain juga gue nyapu.",
    "Arik: Kok bisa gitu, Pak?",
    "Pak Aris: Orang buang sampah sembarangan terus.",
    "Arik: ... (merenung)"
};

string[] babak2_sekolah =
{
    "Arik: Tadi ketemu petugas kebersihan. Dia frustrasi banget.",
    "Nisa: Itu masalah sistemik.",
    "Reza: Terus kita mau ngapain?",
    "Arik: Kita perlu cara lain..."
};

// =======================
// BABAK 3
// =======================
string[] babak3_awal =
{
    "Arik: Aduh, jalan ditutup. Terpaksa lewat pasar."
};

string[] babak3_berhasil =
{
    "Arik: Akhirnya... napas lega."
};

string[] babak3_muntah =
{
    "Reza: Bro, muka lo pucat banget.",
    "Arik: Lewat pasar tadi... bau parah.",
    "Sari: Astaga...",
    "Nisa: Pedagangnya nggak mau bayar kebersihan.",
    "Bagas: Lingkaran setan."
};

string[] babak3_diskusi =
{
    "Arik: Kita butuh cara lebih konkret.",
    "Nisa: Kita kumpulin data.",
    "Reza: Atau kita viralkan?",
    "Bagas: Tapi siapa yang dengerin kita?",
    "Sari: Kita speak up bareng-bareng.",
    "Arik: Iya. Kita coba."
};

// =======================
// BABAK 4
// =======================
string[] babak4_rencana =
{
    "Arik: Sore ini kita speak up ke warga dan Pak RT.",
    "Reza: Siap!",
    "Nisa: Gue udah siapin data.",
    "Bagas: Strateginya?",
    "Sari: Kita sopan dulu.",
    "Arik: Setuju."
};

string[] babak4_warga =
{
    "Arik: Permisi Bu, mau ngomong soal sampah di kali.",
    "Bu Tini: Anak-anak ngapain ngurusin sampah?",
    "Reza: Ini penting, Bu.",
    "Nisa: Ini datanya, Bu.",
    "Bu Ratna: Wah... beda banget ya.",
    "Arik: Kita mau minta Pak RT gerak."
};

string[] babak4_pakrt =
{
    "Arik: Pak... bisa minta waktu?",
    "Pak RT: Ada apa?",
    "Arik: Kali makin parah, Pak.",
    "Pak RT: Bukan sekarang waktunya!",
    "Reza: Tapi kalau nunggu terus?",
    "Pak RT: Kalian anak-anak jangan ngatur!",
    "Arik: Pak, kami cuma—",
    "Pak RT: Sudah! Pulang!"
};

// =======================
// BABAK 5
// =======================
string[] babak5_awal =
{
    "Pak RT: Udah! Jangan ganggu urusan Bapak!",
    "Arik: Bu... di TV ada apa?",
    "Ibu: Katanya hujan deras datang.",
    "Arik: Kalau kali mampet..."
};

string[] babak5_banjir =
{
    "Reza: Bro... itu apa?",
    "Nisa: Banjir.",
    "Bagas: Kali meluap...",
    "Sari: Rumah warga..."
};

string[] babak5_klimaks =
{
    "Arik: Bapak-Ibu... boleh saya ngomong?",
    "Arik: Ini bukan soal umur.",
    "Arik: Ini soal kali yang kita kotori bersama.",
    "Arik: Ini bukti foto-fotonya.",
    "Bu Tini: Ya ampun...",
    "Pak Darno: Kita yang buang sampah...",
    "Pak RT: ...Bapak harusnya dengerin kamu dari dulu.",
    "Pak RT: Ayo kita bersihin kali."
};

string[] babak5_ending =
{
    "Reza: Pak RT turun tangan langsung.",
    "Sari: Orang butuh bukti dulu.",
    "Nisa: Yang penting sekarang bergerak.",
    "Bagas: Kita yang mulai.",
    "Arik: Masih panjang jalannya... tapi dimulai."
};

    private string[] dialogAktif;
    private int index = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        text1.SetActive(false);
        task.SetActive(false);
    }

    void Update()
    {
        if (task.activeSelf && Input.GetMouseButtonDown(0))
        {
            NextDialog();
        }
    }

    // =======================
    // MULAI DIALOG UMUM
    // =======================
    public void StartDialog(string[] dialog)
    {
        if (dialog == null || dialog.Length == 0) return;

        dialogAktif = dialog;
        index = 0;

        task.SetActive(true);
        text1.SetActive(true);
        intro.text = dialogAktif[index];
    }

    // =======================
    // NEXT
    // =======================
    void NextDialog()
    {
        index++;

        if (index < dialogAktif.Length)
        {
            intro.text = dialogAktif[index];
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        task.SetActive(false);
        text1.SetActive(false);
        dialogAktif = null;
    }

    // =======================
    // CONTROLLER PER SCENE
    // =======================

    public void Babak1_Kamar()
    {
        StartDialog(babak1_kamar);
    }

    public void Babak1_Jalan()
    {
        StartDialog(babak1_jalan);
    }

    public void Babak1_Sekolah()
    {
        StartDialog(babak1_sekolah);
    }

    public void Babak2_Kamar()
    {
        StartDialog(babak2_kamar);
    }

    public void Babak2_Sekolah()
    {
        StartDialog(babak2_sekolah);
    }
    public void Babak2_SetelahAnjing()
    {
        StartDialog(babak3_berhasil);
    }
    public void PakAris()
    {
        StartDialog(babak2_setelah_anjing);
    }
}