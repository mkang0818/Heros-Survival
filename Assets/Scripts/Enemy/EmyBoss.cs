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
        // ��Ʈ : NavMeshAgent�� ������ �ִ� ��ũ��Ʈ��� [RequireComponent] ��Ʈ����Ʈ ����� �����ϼ���
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(BossFSM());

        transform.position = new Vector3(Random.Range(-10,10),0, Random.Range(-10, 10));
    }
    private void Update()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.y = transform.position.y; // Ÿ���� y ��ǥ�� ���� ������Ʈ�� y ��ǥ�� ����
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
        //1. �̴ϸ��� ���� 2. ���� 3. �һձ� 4. 
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
        print("�һձ�");
        //�һձ�
        Fire.SetActive(true);
        Fire.GetComponent<BossFireController>().damage = emydata.emyAttack;
        //print("skill1");

        yield return new WaitForSeconds(4f);
        Fire.SetActive(false);
        yield return BossFSM();
    }
    IEnumerator BossMonster()
    {
        print("���ͼ�ȯ");
        int MonsterCount = Random.Range(5, 10);
        Vector3[] SpawnPosArr = new Vector3[MonsterCount];
        //�̴ϸ��� ��ȯ
        for (int i = 0; i < MonsterCount; i++)
        {
            //��������Ʈ �� ��ȯ
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
        print("����");
        agent.isStopped = true;
        //print("skill3");

        int bulletCount = Random.Range(5,10);
        //����
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
        print("����");
        // �Ѿ� �߻�
        for (int i = 0; i < 9; i++)
        {
            Instantiate(BasicBullet, transform.position, transform.rotation * Quaternion.Euler(0, 45 * i, 0));
        }
        yield return BossFSM();
    }
    // ��Ʈ : ������� �ʴ� ������ _ �� �����ؼ� ������� ������ ��Ÿ���°� �����ϴ�(isBomb, BombEffect) - �ַ� out Ű���� ��� �ÿ� ���
    // ��Ʈ : ������� �ʴ� ������ ������ �Ű�����(�Ű������� �⺻���� �Ҵ��ϴ� ��)�� �����ϴ� ����� �����ϴ�
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
                //print("���̾�");
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

            print("����");
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
