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

    //���� 

    public IEnumerator Pattern1()
    {
        print("�÷��̾�� �̵� ����");
        while (true)
        {
            // �÷��̾ Ÿ������ �����Ͽ� �̵���
            if (agent.isActiveAndEnabled) agent.SetDestination(target.transform.position);

            if (agent.isActiveAndEnabled)
            {
                if (target != null && agent.remainingDistance < 0.3f)
                {
                    print("�÷��̾�� ����");
                    // ���� 2 �ڷ�ƾ�� ������
                    if (gameObject != null) StartCoroutine(Pattern2());

                    // ���� 1 �ڷ�ƾ�� �Ͻ� ������
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
        print("������ǥ �̵� ����");
        while (true)
        {
            // ������ ��ġ�� �����Ͽ� Ÿ������ �����Ͽ� �̵���
            if (agent.isActiveAndEnabled) agent.SetDestination(randomPosition);

            if (agent.isActiveAndEnabled)
            {
                if (target != null && !agent.pathPending && agent.remainingDistance < 0.3f)
                {
                    print("������ǥ ����");
                    // ���� 1 �ڷ�ƾ�� �ٽ� ������
                    if (gameObject != null) StartCoroutine(Pattern1());

                    // ���� 2 �ڷ�ƾ�� ������
                    yield break;
                }
            }
            else yield break;

            yield return null;
        }
    }
}