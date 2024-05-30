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

        switch (DropText)
        {
            case "Coin":
                Drop.layer = 7;
                Drop.gameObject.tag = "Drop";
                break;
            case "Diamond":
                Drop.layer = 8;
                Drop.gameObject.tag = "Drop";
                break;
            case "Health":
                Drop.layer = 9;
                Drop.gameObject.tag = "Drop";
                break;
        }


        Drop.GetComponent<DropController>().isFollow = false;
        Drop.transform.position = transform.position;
        Drop.GetComponent<DropController>().target = target;

        ReleaseObject();
    }
}
