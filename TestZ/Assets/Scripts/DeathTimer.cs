using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    private bool DeathEnable = false;
    private float DeathTime = 3f;

    private void Update()
    {
        Death();
    }
    public void DeathOn()
    {
        DeathEnable = true;
    }
    private void Death()
    {
        if(DeathEnable)
        {
            DeathTime -= Time.deltaTime;
            if(DeathTime < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }  
}
