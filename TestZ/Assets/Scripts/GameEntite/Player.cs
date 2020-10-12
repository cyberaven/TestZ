using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{  
    [SerializeField] private Bullet BulletPrefab;
    private Bullet Bullet;
    private float BulletStartSize = 0.5f;

    private Rigidbody Rb;
    private SphereCollider SphereCollider;
    
    private bool MoveEnable = true; 
    private float MoveSpeed = 10f; 
    
    private bool JumpEnable = true; 
    private float JumpForce = 5f;
    private float JumpFrequency = 2f; 
    private float JumpDelta = 0;         
    
    private ChangeSize ChangeSize;
    private float ChangeSizeSpeed = -1f;
    private float MinSize = 0.2f;

    public delegate void FinishTouchDel();
    public static event FinishTouchDel FinishTouchEvent;

    public delegate void EnemyTouchDel();
    public static event EnemyTouchDel EnemyTouchEvent; 

    public delegate void MinSizeDel();
    public static event MinSizeDel MinSizeEvent; 

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();  
        SphereCollider = GetComponent<SphereCollider>();
        ChangeSize = gameObject.AddComponent<ChangeSize>();
    }
    private void OnEnable()
    {
        TapListener.PlayerTapDownEvent += DecreaseOn;
        TapListener.PlayerTapUpEvent += Shoot;    
    }
    private void OnDisable()
    {
        TapListener.PlayerTapDownEvent -= DecreaseOn;
        TapListener.PlayerTapUpEvent -= Shoot;    
    }

    private void Update()//лучше работать с колайдерами и ригидбоди чере фиксапдейт, но не сегодня.
    {
        Move();//поведения следовало бы вынести в отдельные классы. 
        Jump();//также при увеличении колва поведений, не лишнем будет машина состояний.  
        CheckSize();      
    }

    private void Move()
    {
        if(MoveEnable)
        {
            Rb.MovePosition(transform.position + transform.right * MoveSpeed * Time.deltaTime);
        }
    }
    private void Jump()
    {
        if(JumpEnable)
        {
            if(JumpDelta > JumpFrequency) //переодичность/частота прыжка
            {
                Rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                JumpDelta = 0;
            }       
            else
            {
                JumpDelta += Time.deltaTime;               
            }
        }
    }
    private void DecreaseOn()
    {
        ChangeSize.ChangeSizeOn(ChangeSizeSpeed);        
        CreateBullet();    
    }
    
    private void Shoot()
    {
        Bullet.transform.SetParent(null);
        Bullet.MoveOn();
        ChangeSize.ChangeSizeOff();
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Finish")
        {
            FinishTouchEvent?.Invoke();
        }
        if(col.gameObject.tag == "Enemy")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            if(!enemy.GetCursed())
            {
                EnemyTouchEvent?.Invoke();
            }
        }
    }
    private void CreateBullet()
    {
        Bullet = Instantiate(BulletPrefab);
        Bullet.transform.SetParent(transform);         
    }
    private void CheckSize()
    {        
        if(transform.localScale.x < MinSize)
        {
            MinSizeEvent?.Invoke();
        }
    }
}
