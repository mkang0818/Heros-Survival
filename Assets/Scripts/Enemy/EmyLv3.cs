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
                        print("����");
                        break;
                    }
                }
                // ���� ��ġ�� ��ǥ ������ �Ÿ��� ����մϴ�.
                float Distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
                //print("ã���� �Ÿ� :" + Distance);

                // ���� �Ÿ��� 5���� ������ �߰ߵǾ����� ����ϰ� Rush �ڷ�ƾ�� �����մϴ�.
                if (Distance < 5)
                {
                    //print("�߰�");
                    if (gameObject != null) StartCoroutine(Rush());
                    agent.isStopped = true;
                    yield break; // ���� �ڷ�ƾ ����
                }

                // �� �����Ӹ��� ������ Ȯ���ϱ� ���� �� �������� ��ٸ��ϴ�.
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

            // ������ ������ ��
            if (Time.time - waitTime >= 1)
            {
                break; // ���ѷ����� ��������
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
            //print("����");

            Vector3 moveDir = transform.forward;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, Time.deltaTime * 7);

            // ������ ������ ��
            if (Time.time - startTime >= 0.8f)
            {
                break; // ���ѷ����� ��������
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