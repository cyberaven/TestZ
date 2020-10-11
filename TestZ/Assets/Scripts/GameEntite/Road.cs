using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    private ChangeSize ChangeSize;
    private float ChangeSizeSpeed = -1f;

    private void Awake()
    {
        ChangeSize = gameObject.AddComponent<ChangeSize>();        
    }
    private void OnEnable()
    {
        TapListener.PlayerTapDownEvent += Decrease;
        TapListener.PlayerTapUpEvent += StopDecrease;
    }
    private void OnDisable()
    {
        TapListener.PlayerTapDownEvent -= Decrease;
        TapListener.PlayerTapUpEvent -= StopDecrease;
    }

    //при уменьшении после 0 растет Y и выталкивает игрока
    private void Decrease()
    {
        ChangeSize.ChangeSizeOn(ChangeSizeSpeed);
    }
    private void StopDecrease()
    {
        ChangeSize.ChangeSizeOff();
    }


}
