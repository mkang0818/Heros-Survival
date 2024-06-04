using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmyBulletController : MonoBehaviour
{
    public AudioClip AttackAudio;

    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    [HideInInspector] public float Damage;

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
            rb.velocity = transform.forward * speed;
            //transform.position += transform.forward * (speed * Time.deltaTime);         
        }
    }
    private void OnCollisionEnter(Collision col)
    {

        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = col.contacts[0];
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
                SoundManager.Instance.SoundPlay("EmyBullet", AttackAudio);
                Destroy(detachedPrefab, 1);
            }
        }
        //Destroy projectile on collision 총알 충돌 시 삭제
        if (!col.gameObject.CompareTag("Player"))
        {
            //print("총알삭제");
            SoundManager.Instance.SoundPlay("EmyBullet", AttackAudio);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("shield"))
        {
            SoundManager.Instance.SoundPlay("EmyBullet", AttackAudio);
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player") && !col.gameObject.GetComponent<PlayerController>().herostat.isinvincible)
        {
            print("적 공격");
            SoundManager.Instance.SoundPlay("EmyBullet", AttackAudio);
            col.gameObject.GetComponent<PlayerController>().herodata.CurHp -= Damage;
            DamageHeelText("EmyDamageText", Damage);

            GameObject hiteffect = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(hiteffect, 0.5f);
            Destroy(gameObject);
        }
        //총알끼리 충돌방지
        if (col.gameObject.CompareTag("EmyBullet") || col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
    }
    void DamageHeelText(string ObjText, float Value)
    {
        float randX = Random.Range(-1, 1);
        float randZ = Random.Range(-1, 1);
        Vector3 EffectPos = new Vector3(transform.position.x + randX, transform.position.y + 2, transform.position.z + randZ);
        var damageEffect = PoolingManager.instance.GetGo(ObjText);
        damageEffect.transform.position = EffectPos;

        damageEffect.GetComponent<DamageText>().DamageTextOn();
        if (ObjText == "EmyDamageText")
        {
            damageEffect.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + ((int)Value).ToString();
        }
    }
}