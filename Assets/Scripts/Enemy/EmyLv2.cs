using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmyLv2 : Enemy
{
    private NavMeshAgent agent;
    public Animator Anim;
    GameObject target;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.speed = EmyStat.EmyMoveSp;
    }
    public override void Attack(GameObject target)
    {
        StopCoroutine(Pattern1());
        StopCoroutine(Pattern2());
        this.target = target;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Pattern1());
    }
    public void StartPattern()
    {
    }

    //패턴 

    public IEnumerator Pattern1()
    {
        print("플레이어에게 이동 시작");
        while (true)
        {
            // 플레이어를 타겟으로 설정하여 이동함
            if (agent.isActiveAndEnabled) agent.SetDestination(target.transform.position);

            if (agent.isActiveAndEnabled)
            {
                if (target != null && agent.remainingDistance < 0.3f)
                {
                    print("플레이어에게 도착");
                    // 패턴 2 코루틴을 시작함
                    if (gameObject != null) StartCoroutine(Pattern2());

                    // 패턴 1 코루틴을 일시 중지함
                    yield break;
                }

            }
            else yield break;


            yield return null;
        }
    }

    IEnumerator Pattern2()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-11, 11), 0f, Random.Range(-11, 11));
        print("랜덤좌표 이동 시작");
        while (true)
        {
            // 랜덤한 위치를 생성하여 타겟으로 설정하여 이동함
            if (agent.isActiveAndEnabled) agent.SetDestination(randomPosition);

            if (agent.isActiveAndEnabled)
            {
                if (target != null && !agent.pathPending && agent.remainingDistance < 0.3f)
                {
                    print("랜덤좌표 도착");
                    // 패턴 1 코루틴을 다시 시작함
                    if (gameObject != null) StartCoroutine(Pattern1());

                    // 패턴 2 코루틴을 종료함
                    yield break;
                }
            }
            else yield break;

            yield return null;
        }
    }
}