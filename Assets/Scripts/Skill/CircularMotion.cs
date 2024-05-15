using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� ����
    public float radius = 5f; // ���� ������
    public float speed = 2f; // �� � �ӵ�

    private float angle = 0f;

    void Update()
    {
        if (player != null) MoveInCircularMotion();
        else if (player == null) Destroy(gameObject);
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
}
