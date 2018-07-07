using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InscriptionButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    public bool isUsable = false;
    public bool isStartingPoint = false;
    public bool isDragged = false;
    public bool isEndingPoint = false;

    void StartingPoint()
    {
        if (isUsable)
        {
            //Debug.Log("Drag Begin " + transform.name);
            isStartingPoint = true;
            isDragged = false;
            isEndingPoint = false;
        }
    }
    void Dragged()
    {
        if(isUsable)
        {
            if (!isStartingPoint && !isEndingPoint)
            {
                //Debug.Log("Dragging " + transform.name);
                isDragged = true;
            }
        }
    }
    void EndingPoint()
    {
        if (isUsable)
        {
            //Debug.Log("Drag Ended " + transform.name);
            isEndingPoint = true;
            isStartingPoint = false;
            isDragged = false;

        }
    }

    public void ResetState()
    {
        isStartingPoint = false;
        isDragged = false;
        isEndingPoint = false;
        isUsable = false;
        Debug.Log("reset " + transform.name);
    }

    public bool hasBeenActivated() { return isStartingPoint || isDragged || isEndingPoint; }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        StartingPoint();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        StartingPoint();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse Enter");
        Dragged();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse Exit");
        EndingPoint();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
        EndingPoint();
    }
}
