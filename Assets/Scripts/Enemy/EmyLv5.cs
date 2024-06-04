using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmyLv5 : Enemy
{
    private Animator Anim;
    GameObject target;

    public GameObject HPBar;

    public GameObject randomBullet;
    public GameObject fireBallPosVFX;
    public GameObject fireBall;
    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.DOLookAt(target.transform.position, 0.2f);
    }
    public override void Attack(GameObject target)
    {
        this.target = target;

        if (gameObject != null) StartCoroutine(Emy5FSM());
    }
    IEnumerator Dead()
    {
        Anim.SetTrigger("Dead");
        HPBar.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().herodata.curExp++;
        yield return new WaitForSeconds(2f);
        GetComponent<EnemyController>().DeadEvent();
        yield return null;
    }

    // 1. 접근
    // 2. 파이어볼 수직낙하
    // 3. 랜덤 방향으로 발사
    IEnumerator Emy5FSM()
    {
        yield return new WaitForSeconds(0.1f);

        int randAction = Random.Range(0, 3);
        switch (randAction)
        {
            case 0:
                //접근
                if (gameObject != null) StartCoroutine(Move());
                break;
            case 1:
                //총알 낙하
                if (gameObject != null) StartCoroutine(MissileShot());
                break;
            case 2:
                //랜덤 발사
                if (gameObject != null) StartCoroutine(RandomShot());
                break;
        }
    }
    IEnumerator Move()
    {
        if (gameObject != null) StartCoroutine(AgentStart());
        yield return new WaitForSeconds(4f);

        Anim.SetBool("Walk", false);
        StopAllCoroutines();
        if (gameObject != null) StartCoroutine(Emy5FSM());
    }
    IEnumerator AgentStart()
    {
        while (true)
        {
            Anim.SetBool("Walk", true);
            transform.position = Vector3.MoveTowards(transform.position, this.target.transform.position, Time.deltaTime * emydata.emyMoveSp);
            yield return null;
        }
    }
    IEnumerator RandomShot()
    {
        Anim.SetTrigger("RandomShot");
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 10; i++)
        {
            float randomRotation = Random.Range(0, 360);
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject randBullet = Instantiate(randomBullet, spawnPos, Quaternion.Euler(0, randomRotation, 0));
            randBullet.GetComponent<Emy5RandBullet>().Damage = emydata.emyAttack;
            Destroy(randBullet, 3f);
        }
        yield return new WaitForSeconds(2);
        if (gameObject != null) StartCoroutine(Emy5FSM());
    }
    IEnumerator MissileShot()
    {
        Vector3[] randPosArr = new Vector3[5];
        GameObject[] randPosObjArr = new GameObject[5];


        for (int i = 0; i < 5; i++)
        {
            randPosArr[i] = new Vector3(Random.Range(-11, 11), 0, Random.Range(-11, 11));

            randPosObjArr[i] = Instantiate(fireBallPosVFX, randPosArr[i], Quaternion.identity);
        }
        Anim.SetTrigger("Rocket");
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 5; i++)
        {
            Destroy(randPosObjArr[i]);
            Instantiate(fireBall, new Vector3(randPosArr[i].x, randPosArr[i].y + 5, randPosArr[i].z), Quaternion.identity);
        }
        yield return new WaitForSeconds(1);
        if (gameObject != null) StartCoroutine(Emy5FSM());
    }
}
