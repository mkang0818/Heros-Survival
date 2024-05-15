using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform ShotPos;
    public GameObject BulletPrefab;

    public GameObject bulletCase;
    public Transform bulletCasePos;
    public void ShotEvent()
    {
        if (GetComponent<PlayerController>().herodata.curbulletCount > 0)
        {
            BulletCaseIntant();
        }
    }
    public void ShotGunEvent()
    {
        if (GetComponent<PlayerController>().herodata.curbulletCount > 2)
        {
            BulletCaseIntant();
            BulletCaseIntant();
            BulletCaseIntant();
        }
    }

    public void DoubleShotEvent()
    {
        if (GetComponent<PlayerController>().herodata.curbulletCount > 1)
        {
            BulletCaseIntant();
            BulletCaseIntant();
}
    }

    public void CloneShotEvent()
    {
        BulletCaseIntant();
    }

    void BulletCaseIntant()
    {
        /*GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.right * Random.Range(2, 3) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);

        Destroy(intantCase, 3f);*/
    }
}
