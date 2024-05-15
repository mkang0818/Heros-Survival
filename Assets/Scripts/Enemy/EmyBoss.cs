using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmyBoss : Enemy
{
    NavMeshAgent agent;

    public Animator Anim;
    GameObject target;

    [HideInInspector] public float SpinBulletDmg = 10; //40;
    [HideInInspector] public float FallBulletDmg = 10; //40;
    [HideInInspector] public float FireDmg = 10;

    public GameObject SpinBullet;
    GameObject[] SpinBulletArr = new GameObject[6];

    public GameObject miniMonsterSpawnEffect;
    public GameObject miniMonster;

    public GameObject Fire;
    public GameObject FallBullet;
    public GameObject BasicBullet;

    private string BossMiniMonster = "BossMiniMonster";

    private void Start()
    {
        // 멘트 : NavMeshAgent가 무조건 있는 스크립트라면 [RequireComponent] 애트리뷰트 사용을 검토하세요
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(BossFSM());

        transform.position = new Vector3(Random.Range(-10,10),0, Random.Range(-10, 10));
    }
    private void Update()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.y = transform.position.y; // 타겟의 y 좌표를 현재 오브젝트의 y 좌표로 설정
        transform.LookAt(targetPosition);

        agent.SetDestination(targetPosition);
    }
    public override void Attack(GameObject target)
    {
        this.target = target;
    }
    enum BossState
    {
        BossFire,
        BossMonster,
        BossBomb,
        BossBullet
    }
    IEnumerator BossFSM()
    {
        //1. 미니몬스터 생성 2. 접근 3. 불뿜기 4. 
        BossState randomState = (BossState)Random.Range(0, System.Enum.GetValues(typeof(BossState)).Length);

        switch (randomState)
        {
            case BossState.BossFire:
                yield return BossFire();
                break;
            case BossState.BossMonster:
                yield return BossMonster();
                break;
            case BossState.BossBomb:
                yield return BossBomb();
                break;
            case BossState.BossBullet:
                yield return BossBullet();
                break;
        }
    }
    IEnumerator BossFire()
    {
        print("불뿜기");
        //불뿜기
        Fire.SetActive(true);
        Fire.GetComponent<BossFireController>().damage = emydata.emyAttack;
        //print("skill1");

        yield return new WaitForSeconds(4f);
        Fire.SetActive(false);
        yield return BossFSM();
    }
    IEnumerator BossMonster()
    {
        print("몬스터소환");
        int MonsterCount = Random.Range(5, 10);
        Vector3[] SpawnPosArr = new Vector3[MonsterCount];
        //미니몬스터 소환
        for (int i = 0; i < MonsterCount; i++)
        {
            //스폰이펙트 후 소환
            int xpos = Random.Range(-9, 9);
            int ypos = Random.Range(-9, 9);
            Vector3 spawnPos = new Vector3(xpos, 0, ypos);
            SpawnPosArr[i] = spawnPos;

            GameObject SpawnEffect = Instantiate(miniMonsterSpawnEffect, spawnPos, Quaternion.identity);
            Destroy(SpawnEffect, 1);
        }

        yield return new WaitForSeconds(1);
        for (int i = 0; i < MonsterCount; i++)
        {
            var miniMonster = PoolingManager.instance.GetGo(BossMiniMonster);
            miniMonster.transform.position = SpawnPosArr[i];
        }

        yield return new WaitForSeconds(3f);
        yield return BossFSM();
    }
    IEnumerator BossBomb()
    {
        print("폭격");
        agent.isStopped = true;
        //print("skill3");

        int bulletCount = Random.Range(5,10);
        //폭격
        for (int i = 0; i < bulletCount; i++)
        {
            int xpos = Random.Range(-9, 9);
            int ypos = Random.Range(-9, 9);
            Vector3 spawnPos = new Vector3(xpos, 0, ypos);
            GameObject FallBulletObj = Instantiate(FallBullet,spawnPos,Quaternion.identity);
            FallBulletObj.GetComponent<BossFallBulletController>().damage = emydata.emyAttack;
        }


        yield return new WaitForSeconds(3f);
        agent.isStopped = false;
        yield return BossFSM();
    }
    IEnumerator BossBullet()
    {
        print("폭격");
        // 총알 발사
        for (int i = 0; i < 9; i++)
        {
            Instantiate(BasicBullet, transform.position, transform.rotation * Quaternion.Euler(0, 45 * i, 0));
        }
        yield return BossFSM();
    }
    // 멘트 : 사용하지 않는 변수는 _ 로 선언해서 사용하지 않음을 나타내는게 좋습니다(isBomb, BombEffect) - 주로 out 키워드 사용 시에 사용
    // 멘트 : 사용하지 않는 변수는 선택적 매개변수(매개변수에 기본값을 할당하는 것)로 선언하는 방법도 좋습니다
    public override void Dead(GameObject target, bool isBomb, GameObject BombEffect)
    {
        if (emydata.emyCurHP <= 0)
        {
            for (int i = 0; i < SpinBulletArr.Length; i++)
            {
                Destroy(SpinBulletArr[i]);
            }

            float DiamondPer = Random.Range(0, 100);
            if (DiamondPer <= target.GetComponent<PlayerController>().herodata.diamondPer)
            {
                //print("다이아");
                Vector3 DiaPos = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z + Random.Range(-2, 2));
                var DiaObj = PoolingManager.instance.GetGo("DiamondLight");
                DiaObj.transform.position = DiaPos;
                DiaObj.GetComponent<DropLightController>().target = target;
            }
            Vector3 CoinPos = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z + Random.Range(-2, 2));
            var CoinObj = PoolingManager.instance.GetGo("CoinLight");
            CoinObj.transform.position = CoinPos;
            CoinObj.GetComponent<DropLightController>().target = target;
            target.GetComponent<PlayerController>().herodata.curExp += target.GetComponent<PlayerController>().herodata.hasExp;

            print("죽음");
            gameObject.GetComponent<EnemyController>().IsBossDead = true;
            Destroy(gameObject);

        }
    }

    void test(int i, string str = "")
    {

    }

    void test2()
    {
        //test(1, "234");

        test(1);
    }
}
