using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class CreateRobot : MonoBehaviour
{
    private NavMeshAgent agent;
    [HideInInspector] public GameObject target;

    public Animator anim;

    [HideInInspector] public bool NoDamage = false;

    public GameObject BulletPrefab;
    public Transform ShotPos;

    public List<GameObject> FoundObjects;
    public GameObject enemy;
    public float shortDis;
    public float range;

    float attackCurCooltime = 1;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartCoroutine(DestroyObj());
    }
    void Update()
    {
        attackCurCooltime -= Time.deltaTime;
        agent.SetDestination(target.transform.position);

        anim.SetBool("Run",true);
        Attack();
        ObjRotation(enemy);
    }
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
    void Attack()
    {
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if (FoundObjects.Count == 0)
        {
            // 처리할 내용 (예: 적이 없는 경우 처리)
            enemy = target;
        }
        else
        {
            shortDis = Vector3.Distance(gameObject.transform.position, FoundObjects[0].transform.position);

            enemy = FoundObjects[0];

            foreach (GameObject found in FoundObjects)
            {
                    float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                    if (Distance < shortDis && enemy.layer == 6)
                    {
                        enemy = found;
                        shortDis = Distance;
                }                    
            }
            if (attackCurCooltime <= 0 && shortDis < 7)
            {
                anim.SetTrigger("Shot");

                var bullet = PoolingManager.instance.GetGo("Bullet");
                bullet.transform.position = ShotPos.transform.position;
                bullet.transform.rotation = ShotPos.transform.rotation;
                bullet.GetComponent<BulletController>().Player = gameObject;
                bullet.GetComponent<BulletController>().range = range;
                attackCurCooltime = 1;
            }
        }
    }
    void ObjRotation(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z), Vector3.up);
        transform.DORotateQuaternion(new Quaternion(transform.rotation.x, lookRotation.y, transform.rotation.z, transform.rotation.w), 0.05f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, 12);
    }
}
