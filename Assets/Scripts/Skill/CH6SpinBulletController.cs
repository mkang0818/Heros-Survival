using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH6SpinBulletController : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� ����
    public float radius = 5f; // ���� ������
    public float speed = 2f; // �� � �ӵ�

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
        // ����� ��ġ ���
        float x = player.position.x + radius * Mathf.Cos(angle);
        float y = player.position.y;
        float z = player.position.z + radius * Mathf.Sin(angle);

        // ���� ��ġ�� �̵�
        transform.position = new Vector3(x, y, z);

        // ���� ����
        angle += speed * Time.deltaTime;

        // ������ 360���� ������ 0���� �ʱ�ȭ
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
