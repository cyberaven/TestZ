using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    private Rigidbody Rb;

    private float MoveSpeed = 15f;
    private bool MoveEnable = false;   

    private ChangeSize ChangeSize;
    private DeathTimer DeathTimer;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        ChangeSize = gameObject.AddComponent<ChangeSize>();
        DeathTimer = gameObject.AddComponent<DeathTimer>();
    }
    void Update()//машина состояний.
    {
        Move();//надо вынести в отдельный класс. Одинаковое поведение у плейера.                        
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
            Enemy enemy = col.GetComponent<Enemy>();
            if(!enemy.GetCursed())
            {
                MoveEnable = false;           
                DeathTimer.DeathOn();
                ChangeSize.ChangeSizeOn();           
                enemy.CursedOn();         
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
