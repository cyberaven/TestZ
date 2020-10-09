using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
    public delegate void StartButtonClick();
    public static event StartButtonClick StartButtonClickEvent;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        StartButtonClickEvent?.Invoke();
        Debug.Log("StartButton Click");
    }
}
