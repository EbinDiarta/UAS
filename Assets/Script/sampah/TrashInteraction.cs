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
            QuizManager.instance.OpenQuiz(currentTrash);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            currentTrash = null;
        }
    }
}