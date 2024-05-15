using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmyBossMonster : Enemy
{
    private NavMeshAgent agent;

    Animator Anim;

    GameObject target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }
    private void Update()
    {
        agent.SetDestination(target.transform.position);
        Anim.SetBool("Walk",true);
    }

    public override void Attack(GameObject target)
    {
        this.target = target;
    }
}
