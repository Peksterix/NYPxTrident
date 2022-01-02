using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject SelectionIndicator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SelectionIndicator.SetActive(true);
        Debug.Log("Cursor Entering " + name + " GameObject");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SelectionIndicator.SetActive(false);
        Debug.Log("Cursor Exiting " + name + " GameObject");
    }
}
