using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropController : PoolAble
{
    public GameObject target;
    public InGameManager InGameManager;
    public bool isFollow = false;
    private void Update()
    {
        if (isFollow)
        {
            if(GetComponent<ParticleSystem>() != null) GetComponent<ParticleSystem>().Play();
            if(target != null) transform.position = Vector3.Lerp(transform.position, target.transform.position, 3 * Time.deltaTime);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.transform.CompareTag("PlayerSphere") && target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 3 * Time.deltaTime);

            if (gameObject.tag=="SkillBullet")
            {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, 100);
            }
        }
    }
}