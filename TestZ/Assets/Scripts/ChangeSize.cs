using UnityEngine;
using System.Collections;

public class ChangeSize : MonoBehaviour
{
    private bool ChangeSizeEnable = false;
    private float ChangeSizeSpeed = 1;
    
    private void Update()
    {
        Run();
    }

    private void Run()
    {
        if (ChangeSizeEnable)
        {
            Vector3 size = transform.localScale;
            Vector3 newSize = new Vector3(size.x += ChangeSizeSpeed * Time.deltaTime, size.y += ChangeSizeSpeed * Time.deltaTime, size.z += ChangeSizeSpeed * Time.deltaTime);
            transform.localScale = newSize;
        }
    }
    public void ChangeSizeOn()
    {
        ChangeSizeEnable = true;
    }
    public void ChangeSizeOn(float changeSizeSpeed)
    {
        ChangeSizeSpeed = changeSizeSpeed;
        ChangeSizeEnable = true;
    }
    public void ChangeSizeOff()
    {
        ChangeSizeEnable = false;
    }
}
