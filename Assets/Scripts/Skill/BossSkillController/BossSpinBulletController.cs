using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpinBulletController : MonoBehaviour
{
    public float damage;
    public Transform taarget; // �÷��̾��� Transform�� ����
    public float radius = 5f; // ���� ������
    public float speed = 2f; // �� � �ӵ�

    private float angle = 0f;

    void Update()
    {
        if (taarget != null) MoveInCircularMotion();
        else if (taarget == null) Destroy(gameObject);
    }

    void MoveInCircularMotion()
    {
        // ����� ��ġ ���
        float x = taarget.position.x + radius * Mathf.Cos(angle);
        float y = taarget.position.y;
        float z = taarget.position.z + radius * Mathf.Sin(angle);

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
}
