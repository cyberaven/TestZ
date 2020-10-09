﻿using System;
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
        
    private bool DecreaseEnable = false;
    private float DecreaseSpeed = 0.5f;

    public delegate void FinishTouchDel();
    public static event FinishTouchDel FinishTouchEvent;

    public delegate void EnemyTouchDel();
    public static event EnemyTouchDel EnemyTouchEvent;  

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();  
        SphereCollider = GetComponent<SphereCollider>();  
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
        Decrease();
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
        DecreaseEnable = true;
        CreateBullet();
    }
    private void Decrease()
    {
        if(DecreaseEnable)
        {           
            Vector3 size = transform.localScale;
            Vector3 newSize = new Vector3(size.x -= DecreaseSpeed * Time.deltaTime, size.y -= DecreaseSpeed * Time.deltaTime, size.z -= DecreaseSpeed * Time.deltaTime);
            transform.localScale = newSize;
        }
    }
    private void Shoot()
    {
        Bullet.transform.SetParent(null);
        Bullet.MoveOn();
        DecreaseEnable = false;               
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Finish")
        {
            FinishTouchEvent?.Invoke();
        }
        if(col.gameObject.tag == "Enemy")
        {
            EnemyTouchEvent?.Invoke();
        }
    }
    private void CreateBullet()
    {
        Bullet = Instantiate(BulletPrefab);
        Bullet.transform.SetParent(transform); 
        Bullet.transform.localPosition = new Vector3(transform.position.x + 1,transform.position.y, transform.position.z);
    }
}
