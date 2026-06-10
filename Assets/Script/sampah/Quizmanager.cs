using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    [Header("UI")]
    public GameObject quizPanel;
    public Image dragTrashImage;
    public TextMeshProUGUI scoreText;
    public Transform startPoint;

    private int score = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        quizPanel.SetActive(false);
        UpdateScore();
    }

    public void OpenQuiz(Trash trash)
    {
        DragTrash drag =
            dragTrashImage.GetComponent<DragTrash>();

        drag.currentTrash = trash;
        drag.trashType = trash.jenisSampah;

        // Reset posisi sampah UI
        drag.transform.position =
            startPoint.position;

        // Aktifkan raycast lagi
        CanvasGroup cg =
            drag.GetComponent<CanvasGroup>();

        if (cg != null)
        {
            cg.blocksRaycasts = true;
        }

        // Ambil sprite sampah asli
        SpriteRenderer sr =
            trash.GetComponent<SpriteRenderer>();

        dragTrashImage.sprite = sr.sprite;

        quizPanel.SetActive(true);
    }

    public void CheckDrop(
        DragTrash dragTrash,
        Trash.TrashType selectedBin)
    {
        Debug.Log("Jenis Sampah : " +
            dragTrash.trashType);

        Debug.Log("Tong Dipilih : " +
            selectedBin);

        // JAWABAN BENAR
        if (dragTrash.trashType == selectedBin)
        {
            Debug.Log("BENAR");

            score += 10;
        }
        // JAWABAN SALAH
        else
        {
            Debug.Log("SALAH");

            // Optional
            // score -= 5;
        }

        UpdateScore();

        // Hapus sampah di dunia
        if (dragTrash.currentTrash != null)
        {
            Destroy(
                dragTrash.currentTrash.gameObject
            );
        }

        // Reset posisi UI
        dragTrash.transform.position =
            startPoint.position;

        // Tutup panel
        quizPanel.SetActive(false);

        // Bersihkan referensi
        dragTrash.currentTrash = null;
    }

    void UpdateScore()
    {
        scoreText.text = "Poin : " + score;
    }
}