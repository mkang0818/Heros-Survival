using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Ch11SkillController : MonoBehaviour
{
    public Transform player;
    Transform target;
    public List<GameObject> FoundObjects;
    public GameObject enemy;
    public float shortDis;

    bool isFolow = false;
    bool isMove = false;
    private bool isCollided;
    public float damage;

    public GameObject DamageText;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Move",1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            if (isFolow && target != null)
            {
                print("이동");
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * 4 * Time.deltaTime;
            }
            else
            {
                isFolow = false;
                Attack();
            }
        }
    }
    void Attack()
    {
        if (!isFolow)
        {
            FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

            if (FoundObjects.Count == 0)
            {
                // 처리할 내용 (예: 적이 없는 경우 처리)
                print("적없음");

                Vector3 playerPos = (player.position - transform.position).normalized;
                transform.position += playerPos * 2 * Time.deltaTime;
            }
            else
            {
                shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);

                enemy = FoundObjects[0];

                foreach (GameObject found in FoundObjects)
                {
                    float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                    if (Distance < shortDis)
                    {
                        enemy = found;
                        shortDis = Distance;
                    }
                }
                if (enemy.layer == 6)
                {
                    print(enemy.gameObject);
                    isFolow = true;
                    Vector3 direction = enemy.transform.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
                    transform.DORotateQuaternion(new Quaternion(transform.rotation.x, lookRotation.y, transform.rotation.z, transform.rotation.w), 0.1f);

                    target = enemy.transform;
                    ObjRotation(target.gameObject);
                }
            }
        }
    }
    void Move()
    {
        isMove = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }
    void ObjRotation(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
        transform.DORotateQuaternion(new Quaternion(transform.rotation.x, lookRotation.y, transform.rotation.z, transform.rotation.w), 0.05f);
    }
    private void OnTriggerEnter(Collider col)
    {
        int EmyLayer = LayerMask.NameToLayer("Enemy");
        if (col.gameObject.layer == EmyLayer && !isCollided)
        {
            col.GetComponent<EnemyController>().isCollided = true;
            if (col.GetComponent<EnemyController>().isCollided)
            {
                col.GetComponent<EnemyController>().Emy.emydata.emyCurHP -= damage;
                Destroy(gameObject);
                DamageHeelText(DamageText, damage);
            }
            //Destroy(gameObject);
        }
    }
    void DamageHeelText(GameObject TextObj, float Value)
    {
        if ((int)Value > 0)
        {
            float randX = Random.Range(-1, 1);
            float randZ = Random.Range(-1, 1);
            Vector3 EffectPos = new Vector3(transform.position.x + randX, transform.position.y + 2, transform.position.z + randZ);
            GameObject damageEffect = Instantiate(TextObj, EffectPos, Quaternion.identity);

                damageEffect.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + ((int)Value).ToString();
            Destroy(damageEffect, 1f);
        }
    }
}
