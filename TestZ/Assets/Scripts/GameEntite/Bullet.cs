using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    private Rigidbody Rb;
    private float MoveSpeed = 15f;
    private bool MoveEnable = false;
    private bool IncreaseEnable = false;
    private float IncreaseSpeed = 2f;
    private bool DeathEnable = false;
    private float DeathTimer = 3f;
    

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    void Update()//машина состояний.
    {
        Move();//надо вынести в отдельный класс. Одинаковое поведение у плейера.        
        Increase();
        Death();
    }    
    public void MoveOn()
    {
        MoveEnable = true;
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
            IncreaseOn();                
            Destroy(col.gameObject);
        }
    }
    private void IncreaseOn()
    {
        IncreaseEnable = true;       
    }
    private void Increase()//одинаковое поведение с плейером Decrease
    {
        if(IncreaseEnable)
        {           
            Vector3 size = transform.localScale;
            Vector3 newSize = new Vector3(size.x += IncreaseSpeed * Time.deltaTime, size.y += IncreaseSpeed * Time.deltaTime, size.z += IncreaseSpeed * Time.deltaTime);
            transform.localScale = newSize;
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
}
