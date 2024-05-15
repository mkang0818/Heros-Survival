using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeelTurretController : PoolAble
{
    [HideInInspector] public float turretLevel;
    public float CurHP;
    public float MaxHP;
    float AttackSp = 2;
    float CurAttackSp = 2;
    float range = 15;

    public Transform TurretHead;
    public Transform ShotPos;
    public string BulletText;

    public GameObject enemy;
    public float shortDis;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        FindEmy();
        BulletDestroy(player.transform, range);
    }
    void BulletDestroy(Transform player, float range)
    {
        float Distance = Vector3.Distance(transform.position, player.position);
        if (Distance >= range * 10)
        {
            ReleaseObject();
            //Destroy(gameObject);
        }
    }
    void FindEmy()
    {
        TurretHead.DOLookAt(player.transform.position, 0);
        shortDis = Vector3.Distance(gameObject.transform.position, player.transform.position);
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (shortDis <= range && playerScript.herodata.CurHp != playerScript.herodata.maxHp)
        {
            Shot();
        }
    }
    void Shot()
    {
        CurAttackSp -= Time.deltaTime;

        if (CurAttackSp <= 0)
        {
            var bullet = PoolingManager.instance.GetGo(BulletText);
            bullet.transform.rotation = ShotPos.rotation;
            bullet.transform.position = ShotPos.position;
            bullet.GetComponent<TurretBulletController>().player = gameObject.transform;
            bullet.GetComponent<TurretBulletController>().range = 2;
            CurAttackSp = AttackSp;
        }
    }
}
