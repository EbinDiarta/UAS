using UnityEngine;

public class TrashInteraction : MonoBehaviour
{
    public GameObject handButton;

    private Trash currentTrash;

    private void Start()
    {
        handButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            currentTrash = other.GetComponent<Trash>();
            handButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            currentTrash = null;
            handButton.SetActive(false);
        }
    }

    public void InteractTrash()
    {
        if (currentTrash != null)
        {
            QuizManager.instance.OpenQuiz(currentTrash);
        }
    }
}