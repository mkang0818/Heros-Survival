using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 참조
    public float radius = 5f; // 원의 반지름
    public float speed = 2f; // 원 운동 속도

    private float angle = 0f;

    void Update()
    {
        if (player != null) MoveInCircularMotion();
        else if (player == null) Destroy(gameObject);
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
}
