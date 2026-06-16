using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    [Header("UI")]
    public GameObject quizPanel;
    public Image dragTrashImage;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public Transform startPoint;

    [Header("Timer")]
    public float maxTime = 5f;
    private float currentTime;
    private bool isTiming = false;

    public static int score = 0;
    public static int sampahSelesai = 0;
    public int totalSampah = 30;

    private DragTrash currentDrag;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        quizPanel.SetActive(false);
        UpdateScore();
    }

    private void Update()
    {
        if (isTiming)
        {
            currentTime -= Time.deltaTime;

            timerText.text = "Waktu: " + Mathf.Ceil(currentTime);

            if (currentTime <= 0)
            {
                TimeUp();
            }
        }
    }

    public void OpenQuiz(Trash trash)
    {
        DragTrash drag = dragTrashImage.GetComponent<DragTrash>();

        currentDrag = drag;

        drag.currentTrash = trash;
        drag.trashType = trash.jenisSampah;

        drag.transform.position = startPoint.position;

        CanvasGroup cg = drag.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.blocksRaycasts = true;
        }

        SpriteRenderer sr = trash.GetComponent<SpriteRenderer>();
        dragTrashImage.sprite = sr.sprite;

        quizPanel.SetActive(true);

        StartTimer();
    }

    void StartTimer()
    {
        currentTime = maxTime;
        isTiming = true;
    }

    void StopTimer()
    {
        isTiming = false;
    }

    void TimeUp()
{
    StopTimer();

    sampahSelesai++;

    if (sampahSelesai >= totalSampah)
    {
        FinishGame();
        return;
    }

    ResetAfterAnswer();
}

    public void CheckDrop(DragTrash dragTrash, Trash.TrashType selectedBin)
{
    StopTimer();

    if (dragTrash.trashType == selectedBin)
    {
        score += 10;
    }

    sampahSelesai++;

    UpdateScore();

    if (sampahSelesai >= totalSampah)
    {
        FinishGame();
        return;
    }

    ResetAfterAnswer();
}
    void ResetAfterAnswer()
    {
        if (currentDrag != null && currentDrag.currentTrash != null)
        {
            Destroy(currentDrag.currentTrash.gameObject);
        }

        if (currentDrag != null)
        {
            currentDrag.transform.position = startPoint.position;
            currentDrag.currentTrash = null;
        }

        quizPanel.SetActive(false);
    }
    void UpdateScore()
    {
        scoreText.text = "Poin : " + score;
    }
    void FinishGame()
{
    SceneManager.LoadScene(SceneData.ending);
    Debug.Log("GAME SELESAI");
    Debug.Log("SKOR AKHIR : " + score);
}
}