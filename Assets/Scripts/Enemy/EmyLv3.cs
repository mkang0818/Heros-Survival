using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EmyLv3 : Enemy
{
    private NavMeshAgent agent;

    public Animator Anim;
    GameObject target;
    Vector3 randomPos;

    Vector3 lookVec;
    public float rushSp = 10;

    public GameObject meshObj;
    Material mat;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        mat = meshObj.GetComponent<SkinnedMeshRenderer>().material;
        target = GameObject.FindGameObjectWithTag("Player");

        //StartCoroutine(randomMove());
    }
    public override void Attack(GameObject target)
    {
        //this.target = target;
        StartCoroutine(randomMove());
    }
    /*public void StartPattern()
    {
        StartCoroutine(randomMove());
    }*/
    IEnumerator randomMove()
    {
        while (true)
        {
            int randX = Random.Range(-9, 9);
            int randZ = Random.Range(-9, 9);
            randomPos.x = randX; randomPos.z = randZ; randomPos.y = 0;

            if (target != null && gameObject != null) agent.SetDestination(randomPos);

            Anim.SetBool("Run", false);
            Anim.SetBool("Walk", true);

            yield return new WaitForSeconds(2f);
            while (true)
            {
                if (agent.isActiveAndEnabled)
                {
                    if (target != null && gameObject != null && !agent.pathPending && agent.remainingDistance < 0.1f)
                    {
                        print("도착");
                        break;
                    }
                }
                // 현재 위치와 목표 사이의 거리를 계산합니다.
                float Distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
                //print("찾는중 거리 :" + Distance);

                // 만약 거리가 5보다 작으면 발견되었음을 출력하고 Rush 코루틴을 실행합니다.
                if (Distance < 5)
                {
                    //print("발견");
                    if (gameObject != null) StartCoroutine(Rush());
                    agent.isStopped = true;
                    yield break; // 현재 코루틴 종료
                }

                // 매 프레임마다 조건을 확인하기 위해 한 프레임을 기다립니다.
                yield return null;
            }
        }
    }
    IEnumerator Rush()
    {
        float waitTime = Time.time;
        while (true)
        {
            Anim.SetBool("Run", false);
            Anim.SetBool("Walk", false);

            // 돌진이 끝났을 때
            if (Time.time - waitTime >= 1)
            {
                break; // 무한루프를 빠져나옴
            }

            mat.color = Color.red;

            transform.DOLookAt(target.transform.position, 0);
            yield return null;
        }

        mat.color = Color.white;
        float startTime = Time.time;

        while (true)
        {
            Anim.SetBool("Run", true);
            Anim.SetBool("Walk", false);
            //print("돌진");

            Vector3 moveDir = transform.forward;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, Time.deltaTime * 7);

            // 돌진이 끝났을 때
            if (Time.time - startTime >= 0.8f)
            {
                break; // 무한루프를 빠져나옴
            }

            yield return null;
        }

        Anim.SetBool("Run", false);
        Anim.SetBool("Walk", false);
        agent.isStopped = false;
        if (gameObject != null) StartCoroutine(randomMove());
        yield break;
    }

}