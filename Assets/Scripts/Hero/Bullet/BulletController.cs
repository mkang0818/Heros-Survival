using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletController : PoolAble
{

    public GameObject Player;
    public int BulletcurHP;

    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    [SerializeField] private bool IsSkill;
    public bool isCollided = false;

    public float range;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;        
        }

        BulletDestroy(Player.transform, range);
    }
    void BulletDestroy(Transform player, float range)
    {
        if (player == null)
        {
            if (!IsSkill) ReleaseObject();
            else Destroy(gameObject);
        }
        float Distance = Vector3.Distance(transform.position, player.position);
        if (Distance >= range * 10)
        {
            if(!IsSkill) ReleaseObject();
            else Destroy(gameObject);
        }
        else if (BulletcurHP <= 0)
        {
            if (!IsSkill) ReleaseObject();
            else Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Bullet") || col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Ground"))
        {
            return;
        }
        if (col.gameObject.CompareTag("SideWall"))
        {
            SoundManager.Instance.SoundPlay("BulletDestroy", SoundManager.Instance.BulletDestroyAudio);
            GameObject Hit = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(Hit, 0.5f);
            BombEffect(Hit);

            if (!IsSkill) ReleaseObject();
            else Destroy(gameObject);
        }
        int EmyLayer = LayerMask.NameToLayer("Enemy");
        if (col.gameObject.layer == EmyLayer && !isCollided)
        {
            //print("이펙트 실행");
            GameObject BulletHit = Instantiate(hit, transform.position, Quaternion.identity);
            BombEffect(BulletHit);
            if (BulletcurHP <= 0)
            {
                isCollided = true;
            }
        }
        int RandomBox = LayerMask.NameToLayer("RandomBox");

        if (col.gameObject.layer == RandomBox)
        {
            GameObject Hit = Instantiate(hit, transform.position, Quaternion.identity);
            Destroy(Hit, 0.5f);
            BombEffect(Hit);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //총알끼리 충돌방지
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            return;
        }
    }

    void BombEffect(GameObject bullet)
    {
        var cfxrEffect = bullet.GetComponent<CartoonFX.CFXR_Effect>();
        if (cfxrEffect != null)
        {
            // CFXR_Effect 컴포넌트에서 cameraShake 필드에 접근하여 활성화 또는 비활성화
            cfxrEffect.cameraShake.enabled = GameManager.Instance.IsShake;
        }
    }
}