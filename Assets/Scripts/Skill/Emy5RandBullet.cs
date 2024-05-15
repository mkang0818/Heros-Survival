using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emy5RandBullet : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    public float Damage;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            //Instantiate flash effect on projectile position
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            //Destroy flash effect depending on particle Duration time
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, 5);
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.right * speed;        
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        //ÃÑ¾Ë³¢¸® Ãæµ¹¹æÁö
        if (col.gameObject.CompareTag("EmyBullet") || col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("SideWall"))
        {
            GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(hiteffect, 0.5f);
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Player") && !col.gameObject.GetComponent<PlayerController>().herostat.isinvincible)
        {
            //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
            foreach (var detachedPrefab in Detached)
            {
                if (detachedPrefab != null)
                {
                    detachedPrefab.transform.parent = null;
                    Destroy(detachedPrefab, 1);
                }
            }
            print("Àû °ø°Ý");
            col.gameObject.GetComponent<PlayerController>().herodata.CurHp -= Damage;

            GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(hiteffect, 0.5f);
            Destroy(gameObject);
        }

        //ÃÑ¾Ë³¢¸® Ãæµ¹¹æÁö
        if (col.gameObject.CompareTag("EmyBullet") || col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        if (col.gameObject.CompareTag("shield"))
        {
            Destroy(gameObject);
        }
    }
}