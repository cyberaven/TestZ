using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    private Rigidbody Rb;

    private float MoveSpeed = 15f;
    private bool MoveEnable = false;   

    private bool DeathEnable = false;
    private float DeathTimer = 3f;

    private ChangeSize ChangeSize;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        ChangeSize = gameObject.AddComponent<ChangeSize>();
    }
    void Update()//машина состояний.
    {
        Move();//надо вынести в отдельный класс. Одинаковое поведение у плейера.                
        Death();
        OnNouse();
    }    
    public void MoveOn()
    {
        MoveEnable = true;
        ChangeSize.ChangeSizeOff();
    }
    private void Move()
    {
        if(MoveEnable)
        {
            Rb.MovePosition(transform.position + transform.right * MoveSpeed * Time.deltaTime);        
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {  
            MoveEnable = false;           
            DeathOn();
            ChangeSize.ChangeSizeOn();
            Destroy(col.gameObject);
        }
    }   
    private void DeathOn()
    {
        DeathEnable = true;
    }
    private void Death()
    {
        if(DeathEnable)
        {
            DeathTimer -= Time.deltaTime;
            if(DeathTimer < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }   
    private void OnNouse()//на носу) пуля висит на игроке, не выстрелили
    {
        if(transform.parent)
        {
            transform.localPosition = new Vector3(0.5f,0,0);
            ChangeSize.ChangeSizeOn();
        }
    }
}
