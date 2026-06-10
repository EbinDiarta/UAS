using UnityEngine;
using UnityEngine.EventSystems;

public class DragTrash : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private Vector3 startPosition;

    private CanvasGroup canvasGroup;

    public Trash.TrashType trashType;
    public Trash currentTrash;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;

        // Agar tong sampah bisa menerima drop
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}