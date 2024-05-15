using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEfx : PoolAble
{
    public void EfxDestroy()
    {
        ReleaseObject();
    }
}
