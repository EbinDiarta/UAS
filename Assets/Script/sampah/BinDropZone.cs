using UnityEngine;
using UnityEngine.EventSystems;

public class BinDropZone : MonoBehaviour, IDropHandler
{
    public Trash.TrashType binType;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DROP KE : " + binType);

        DragTrash dragTrash =
            eventData.pointerDrag.GetComponent<DragTrash>();

        if (dragTrash == null)
            return;

        QuizManager.instance.CheckDrop(
            dragTrash,
            binType
        );
    }
}