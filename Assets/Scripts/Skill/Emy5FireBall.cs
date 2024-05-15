using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emy5FireBall : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    [HideInInspector] public float Damage;

    EmyLv5 EmyScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        EmyScript = GetComponent<EmyLv5>();
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.CompareTag("Player") && !col.gameObject.GetComponent<PlayerController>().herostat.isinvincible)
        {
            GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(hiteffect, 0.5f);

            col.gameObject.GetComponent<PlayerController>().herodata.CurHp -= Damage;

            Destroy(gameObject);
        }
        //총알끼리 충돌방지
        if (col.gameObject.CompareTag("EmyBullet") || col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("shield"))
        {
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player") && !col.gameObject.GetComponent<PlayerController>().herostat.isinvincible)
        {
            GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(hiteffect, 0.5f);

            col.gameObject.GetComponent<PlayerController>().herodata.CurHp -= 100;

            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            print("몬스터 스킬총알 맞음");
            GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(hiteffect, 0.5f);
            Destroy(gameObject);
        }
    }
}