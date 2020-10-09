using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapListener : MonoBehaviour
{    
    public delegate void PlayerTapDownDel();
    public static event PlayerTapDownDel PlayerTapDownEvent;

    public delegate void PlayerTapUpDel();
    public static event PlayerTapUpDel PlayerTapUpEvent;

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("GetMouseButtonDown");
            PlayerTapDownEvent?.Invoke();
        }
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log("GetMouseButtonUp");
            PlayerTapUpEvent?.Invoke();
        }
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Ended)
            {   
                Debug.Log("TouchUp");            
                PlayerTapUpEvent?.Invoke();
            }
            else
            {
                Debug.Log("TouchDown");                
                PlayerTapDownEvent?.Invoke();
            }
        }        
        
        
    }
}
