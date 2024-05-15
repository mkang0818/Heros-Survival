using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLightController : PoolAble
{
    public string DropText;
    public GameObject target;


    public void CreateDropObj()
    {
        Invoke("DropObj", 0.5f);
    }
    void DropObj()
    {
        var Drop = PoolingManager.instance.GetGo(DropText);
        Drop.GetComponent<DropController>().isFollow = false;
        Drop.transform.position = transform.position;
        Drop.GetComponent<DropController>().target = target;

        ReleaseObject();
    }
}
