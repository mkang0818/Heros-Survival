using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletController : PoolAble
{
    public int BulletcurHP;

    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    public Transform player;
    public float range;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;
            //transform.position += transform.forward * (speed * Time.deltaTime);         
        }
        BulletDestroy(player, range);
    }
    void BulletDestroy(Transform player, float range)
    {
        float Distance = Vector3.Distance(transform.position, player.position);
        if (Distance >= range * 10)
        {
            ReleaseObject();
            //Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        //Destroy projectile on collision �Ѿ� �浹 �� ����
        if (col.gameObject.CompareTag("Player"))
        {
            //GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            //print("�Ѿ˻���");

            ReleaseObject();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //�Ѿ˳��� �浹����
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;

        //Spawn hit effect on collision
        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt(contact.point + contact.normal); }

            //Destroy hit effects depending on particle Duration time
            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }

        //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
                Destroy(detachedPrefab, 1);
            }
        }
        //Destroy projectile on collision �Ѿ� �浹 �� ����
        if (!collision.gameObject.CompareTag("Player"))
        {
            GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            //print("�Ѿ˻���");

            ReleaseObject();
            //Destroy(gameObject);
        }
    }
}