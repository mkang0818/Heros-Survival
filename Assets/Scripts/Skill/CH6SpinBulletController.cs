using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH6SpinBulletController : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 참조
    public float radius = 5f; // 원의 반지름
    public float speed = 2f; // 원 운동 속도

    private float angle = 0f;

    public GameObject SkillBulletHit;
    private void Start()
    {
        StartCoroutine(DestroySpinBullet());
    }
    void Update()
    {
        if (player != null) MoveInCircularMotion();
        else if (player != null) Destroy(gameObject);
    }

    IEnumerator DestroySpinBullet()
    {
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }

    void MoveInCircularMotion()
    {
        // 원운동의 위치 계산
        float x = player.position.x + radius * Mathf.Cos(angle);
        float y = player.position.y;
        float z = player.position.z + radius * Mathf.Sin(angle);

        // 계산된 위치로 이동
        transform.position = new Vector3(x, y, z);

        // 각도 증가
        angle += speed * Time.deltaTime;

        // 각도가 360도를 넘으면 0으로 초기화
        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Enemy"))
        {
            if (col.GetComponent<EnemyController>() != null)
            {
                col.GetComponent<EnemyController>().emydata.emyCurHP -= player.GetComponent<PlayerController>().herodata.skillDamage;

                Destroy(gameObject);
                GameObject hit = Instantiate(SkillBulletHit, transform.position, Quaternion.identity);
                Destroy(hit, 1);
            }
        }
    }
}
