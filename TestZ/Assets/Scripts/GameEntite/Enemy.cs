using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool Cursed = false; 

    private ChangeSize ChangeSize;
    private DeathTimer DeathTimer;
    private CapsuleCollider CapsuleCollider;

    private void Awake()
    {       
        ChangeSize = gameObject.AddComponent<ChangeSize>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        DeathTimer = gameObject.AddComponent<DeathTimer>();
    }
    private void OnColliderEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if(enemy.GetCursed())
            {
                CursedOn();
            }
        }
    }
    public void CursedOn()
    {
        Cursed = true;
        CapsuleCollider.isTrigger = true;
        DeathTimer.DeathOn();
        ChangeSize.ChangeSizeOn();
    }
    public bool GetCursed()
    {
        return Cursed;
    }    
    
}
