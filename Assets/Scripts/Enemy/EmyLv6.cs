using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class EmyLv6 : Enemy
{
    private NavMeshAgent agent;

    Animator Anim;
    GameObject target;

    Vector3 lookVec;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        StartCoroutine(Emy6Move());
    }
    private void Update()
    {
        if (gameObject != null) agent.SetDestination(target.transform.position);
        Anim.SetBool("Walk", true);
    }
    public override void Attack(GameObject target)
    {
        this.target = target;
    }
    IEnumerator Emy6Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            agent.speed += 0.03f;
        }
    }
}