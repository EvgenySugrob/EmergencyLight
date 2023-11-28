using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool onUI;
    public void OnPointerEnter(PointerEventData eventData)
    {
        onUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onUI = false;
    }

    public bool ReturnStateMouse()
    {
        return onUI;
    }
}
