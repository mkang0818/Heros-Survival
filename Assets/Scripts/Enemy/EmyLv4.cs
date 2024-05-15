using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EmyLv4 : Enemy
{
    private NavMeshAgent agent;
    public Animator Anim;
    GameObject target;
    Vector3 lookVec;
    Vector3 randomPos;

    public Transform ShotPos;
    public GameObject BulletPrefab;
    bool isEscape = true;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        emydata.emyCurAttackSp -= Time.deltaTime;

        Anim.SetBool("Walk", true);

        if (isEscape) //추격
        {
            transform.DOLookAt(target.transform.position, 0.1f);
            if (gameObject != null) agent.SetDestination(target.transform.position);

            float TargetToDistance = Vector3.Distance(gameObject.transform.position, target.transform.position);

            if (emydata.emyCurAttackSp <= 0)
            {
                Anim.SetTrigger("Attack");
                
                emydata.emyCurAttackSp = emydata.emyMaxAttackSp;
            }

            if (TargetToDistance < 5) //타겟과의 거리가 가까울 경우
            {
                StopAllCoroutines();
                isEscape = false;
                agent.isStopped = true;
            }
        }
        else // 도망
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(-targetDirection);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            Vector3 moveDir = transform.forward;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDir, Time.deltaTime * emydata.emyMoveSp);

            float TargetToDistance = Vector3.Distance(gameObject.transform.position, target.transform.position);

            if (TargetToDistance > 8) //타겟과의 거리가 가까울 경우
            {
                if (gameObject != null) StartCoroutine(TargetToMove()); // 다시 추격
            }
        }
    }
    public void EmyShot()
    {
        GameObject bullet = Instantiate(BulletPrefab, ShotPos.position, transform.rotation);
        bullet.GetComponent<EmyBulletController>().Damage = emydata.emyAttack;
    }
    public override void Attack(GameObject target)
    {
        this.target = target;
    }
    IEnumerator TargetToMove()
    {
        agent.isStopped = false;
        isEscape = true;

        yield return null;
    }
}
