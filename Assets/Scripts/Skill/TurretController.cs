using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurretController : MonoBehaviour
{
    float turretLevel;
    float AttackSp = 2;
    float CurAttackSp = 2;
    float range = 15;

    public Transform TurretHead;
    public Transform ShotPos;
    public string BulletText;

    public List<GameObject> FoundObjects;
    public GameObject enemy;
    public float shortDis;

    // Update is called once per frame
    void Update()
    {
        FindEmy();
        CurAttackSp -= Time.deltaTime;
    }
    void FindEmy()
    {
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if (FoundObjects.Count == 0)
        {
            // 처리할 내용 (예: 적이 없는 경우 처리)
            return;
        }

        shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);

        enemy = FoundObjects[0];

        foreach (GameObject found in FoundObjects)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis)
            {
                enemy = found;
                shortDis = Distance;
            }
        }
        TurretHead.DOLookAt(new Vector3(enemy.transform.position.x, TurretHead.position.y, enemy.transform.position.z), 0.1f);

        if (shortDis < range) Shot();
    }
    void Shot()
    {
        if (CurAttackSp <= 0)
        {
            var bullet = PoolingManager.instance.GetGo(BulletText);
            bullet.transform.rotation = ShotPos.rotation;
            bullet.transform.position = ShotPos.position;
            bullet.GetComponent<BulletController>().Player = gameObject;
            bullet.GetComponent<BulletController>().range = 2;

            CurAttackSp = AttackSp;
        }
    }
}
