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
    public TextMeshProUGUI timerText;
    public Transform startPoint;

    [Header("Timer")]
    public float maxTime = 5f;
    private float currentTime;
    private bool isTiming = false;

    private int score = 0;

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
        Debug.Log("WAKTU HABIS!");
        StopTimer();
        ResetAfterAnswer();
    }

    public void CheckDrop(DragTrash dragTrash, Trash.TrashType selectedBin)
    {
        StopTimer();

        if (dragTrash.trashType == selectedBin)
        {
            Debug.Log("BENAR");
            score += 10;
        }
        else
        {
            Debug.Log("SALAH");
        }

        UpdateScore();

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
}