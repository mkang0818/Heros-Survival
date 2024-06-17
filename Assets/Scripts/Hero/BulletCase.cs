using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCase : PoolAble
{
    public void BulletCaseOn()
    {
        Invoke("BulletCaseReturn", 1f);
    }
    void BulletCaseReturn()
    {
        ReleaseObject();
    }
}
