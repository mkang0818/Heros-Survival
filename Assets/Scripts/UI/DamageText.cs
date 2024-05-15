using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageText : PoolAble
{
    void down()
    {
        transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }
    public void DamageTextOn()
    {
        float RandScale = Random.Range(1.3f, 2);
        transform.DOScale(new Vector3(RandScale, RandScale, RandScale), 0.3f);
        Invoke("down", 0.5f);
        Invoke("ReturnText", 1f);
    }
    void ReturnText()
    {
        ReleaseObject();
    }
}
