using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyController : PoolAble
{
    public AudioClip[] ArrPlayerAtAudio;
    public AudioClip[] ArrPlayerCriAtAudio;
    public AudioClip[] PlayerBombAudio;

    private NavMeshAgent agent;
    Rigidbody rigid;

    public InGameManager ingameManager;

    public EmyStatScriptale emydataPrefab;
    [HideInInspector] public EmyStatScriptale emydata;
    [HideInInspector] public Enemy Emy;

    [SerializeField] public GameObject target;
    private PlayerController playerController;
    private SoundManager soundManager;

    public GameObject MoneyPrefab;
    public GameObject Diamond;

    [HideInInspector]
    public float hp = 5f;

    [HideInInspector] public bool IsSlowTime = false;
    public int levelNum;
    float CurslowTime = 5;
    float MaxslowTime = 5;

    public GameObject hitEffect;
    public GameObject DamageEffect;
    public GameObject CriticalEffect;
    public GameObject EmyDamageEffect;

    public Slider HpBar;
    public Image HpBarFillImg;

    float AttackMaxTime = 0.5f;
    float AttackCurTime = 0;

    float SkillATMaxTime = 0.2f;
    float SkillATCurTime = 0;

    public bool isCollided = false;

    public GameObject ChildMesh;
    Material mat;
    bool isKnockBack = false;
    [HideInInspector] public bool isDead = false;


    [HideInInspector] public bool isBomb = false;
    public GameObject BombEffect;
    public GameObject DeadEffect;
    public float SkillBulletTime = 3;

    public bool IsBossDead = false;

    void Start()
    {
        InitEmy();
    }
    public void InitEmy()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerController = target.GetComponent<PlayerController>();
        soundManager = SoundManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        mat = ChildMesh.GetComponent<SkinnedMeshRenderer>().material;

        emydata = Instantiate(this.emydataPrefab);
        Emy.initSetting(emydata);
        Emy.Attack(target);
        Emy.LevelUp(levelNum);

        mat.color = Color.white;
        HpBarFillImg.color = Color.green;
        HpBar.value = (float)emydata.emyCurHP / (float)emydata.emyMaxHP; //hp 초기화

        transform.rotation = Quaternion.LookRotation(target.transform.forward);
        agent.speed = emydata.emyMoveSp;
        isCollided = false;
        isDead = false;
        isKnockBack = false;
    }

    void Update()
    {
        if (isDead) InitEmy();
        else Emy.Dead(target, isBomb, BombEffect);

        SlowTime();
        UpdateHP();

        BossDead();
    }

    void BossDead()
    {
        if (IsBossDead)
        {
            print("보스사망");
            IsBossDead = false;

            InGameManager inGameManager = ingameManager.GetComponent<InGameManager>();
            inGameManager.IsBossDead = true;
            inGameManager.CurTimer = 0;
        }
    }

    void UpdateHP()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, (float)emydata.emyCurHP / (float)emydata.emyMaxHP, Time.deltaTime * 10);
    }

    void SlowTime()
    {
        if (IsSlowTime)
        {
            CurslowTime -= Time.deltaTime;

            if (CurslowTime <= 0)
            {
                agent.speed = 3f;

                IsSlowTime = false;
                CurslowTime = MaxslowTime;
            }
            agent.speed = 1f;
        }
    }

    void AttackSound(string SoundName, AudioClip[] audioclipArr)
    {
        int randomNum = Random.Range(0, audioclipArr.Length);
        soundManager.SoundPlay(SoundName, audioclipArr[randomNum]);
    }

    private void OnTriggerEnter(Collider col)
    {
        BulletController bulletController = col.gameObject.GetComponent<BulletController>();

        if (col.gameObject.CompareTag("Bomb"))
        {
            AttackSound("PlayerBomb", PlayerBombAudio);
            float Value = playerController.herodata.bombDamage;
            emydata.emyCurHP -= Value;
            DamageHeelText("DamageText", Value);
            KnockBack();

            StartCoroutine(OnDamage());
            StartCoroutine(BombTrigger(col.gameObject));
        }

        if (col.gameObject.CompareTag("SlowBullet"))
        {
            StartCoroutine(SlowMove());
            emydata.emyCurHP -= playerController.herodata.science;
            StartCoroutine(OnDamage());
            bulletController.ReleaseObject();
        }
        else if (col.gameObject.CompareTag("TurretBullet"))
        {
            AttackSound("PlayerAttack", ArrPlayerAtAudio);
            emydata.emyCurHP -= playerController.herodata.science;

            float Value = playerController.herodata.science;
            DamageHeelText("DamageText", Value);

            StartCoroutine(OnDamage());
            bulletController.ReleaseObject();
        }

        if (col.gameObject.CompareTag("Bullet") || col.gameObject.CompareTag("AttackPos"))
        {
            float Value, Absorption;
            float Damage = (playerController.herodata.damage);

            //총알 체력 감소
            if (bulletController != null) bulletController.BulletcurHP -= 1;

            // 치명타 확률 구현
            float CriticalPer = playerController.herodata.critical;
            float randCritical = Random.Range(0, 100);
            if (randCritical <= CriticalPer)
            {
                AttackSound("PlayerCriAt", ArrPlayerCriAtAudio);
                emydata.emyCurHP -= Damage;
                DamageHeelText("CriticalEffect", Damage);
            }
            else
            {
                AttackSound("PlayerAttack", ArrPlayerAtAudio);
            }

            // 적 체력 감소
            emydata.emyCurHP -= Damage;
            Value = Damage;

            // 보스가 아닐때 체력흡수 적용
            if (gameObject.layer != 12)
            {
                Absorption = emydata.emyCurHP * playerController.herodata.absorption;
                emydata.emyCurHP -= Absorption;
                Value += Absorption;
            }

            DamageHeelText("DamageText", Value);
            KnockBack();
            StartCoroutine(OnDamage()); //피격 구현
        }
        else if (col.gameObject.CompareTag("Ch1SkillBullet")) Destroy(col.gameObject);
    }

    IEnumerator BombTrigger(GameObject HitObj)
    {
        yield return new WaitForSeconds(0.1f);
        if (HitObj != null) HitObj.GetComponent<SphereCollider>().enabled = false;
    }

    IEnumerator SlowMove()
    {
        emydata.emyMoveSp *= .5f;
        yield return new WaitForSeconds(3f);
        emydata.emyMoveSp *= 2;
    }

    private void OnTriggerStay(Collider col)
    {
        BulletController bulletController = col.gameObject.GetComponent<BulletController>();
        PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
        Ch4Stat Ch4Controller = col.GetComponent<Ch4Stat>();

        if (col.gameObject.CompareTag("SkillBullet"))
        {
            SkillBulletTime += Time.deltaTime;

            if (SkillBulletTime >= 2f)
            {
                if (bulletController != null) bulletController.BulletcurHP -= 1;

                float Value;
                float Damage = (playerController.herodata.skillDamage);
                emydata.emyCurHP -= Damage;
                Value = Damage;

                DamageHeelText("DamageText", Value);
                StartCoroutine(OnDamage()); //피격 구현

                SkillBulletTime = 0;
            }
        }
        if (col.gameObject.CompareTag("Player") && !playerController.isStore)
        {
            AttackCurTime -= Time.deltaTime;
            if (AttackCurTime <= 0)
            {
                bool isEvasion = RandomEvasion(playerController.herodata.evasion);

                if (isEvasion)
                {
                    print("회피!!");
                    AttackCurTime = AttackMaxTime;
                }
                else
                {
                    if (!playerController.herostat.isinvincible)
                    {
                        float EmyDamage = (int)(emydata.emyAttack - (playerController.herodata.defense / 100) * (int)(emydata.emyAttack));
                        print("플레이어 체력감소 : " + EmyDamage);
                        if (EmyDamage > 0) playerController.herodata.CurHp -= EmyDamage;

                        DamageHeelText("EmyDamageText", EmyDamage);
                        AttackCurTime = AttackMaxTime;
                    }
                    else
                    {
                        print("무적");
                        AttackCurTime = AttackMaxTime;
                    }
                }

                if (emydata.emyCode == 1 || emydata.emyCode == 2 || emydata.emyCode == 7)
                {
                    print("삭제");
                    if (Ch4Controller != null && (Ch4Controller.isDash || Ch4Controller.isSpin))
                    {
                        return;
                    }
                    Destroy(Instantiate(DeadEffect, transform.position, Quaternion.identity), 2f);
                    soundManager.SoundPlay("PlayerAt", ArrPlayerAtAudio[Random.Range(0, ArrPlayerAtAudio.Length)]);
                    DeadEvent();
                }
            }


        }
        if (col.gameObject.CompareTag("CH12SkillZone"))
        {
            SkillATCurTime -= Time.deltaTime;
            if (SkillATCurTime <= 0)
            {
                Invoke("skillAttack", 0.3f);
                SkillATCurTime = SkillATMaxTime;
            }
        }
    }
    void skillAttack()
    {
        emydata.emyCurHP -= playerController.herodata.skillDamage;
    }
    void KnockBack()
    {
        int EmyNum = Emy.emydata.emyCode;
        if (EmyNum == 1 || EmyNum == 2 || EmyNum == 4 || EmyNum == 7)
        {
            if (!isKnockBack)
            {
                isKnockBack = true;
                Vector3 knockbackDirection = (-transform.forward).normalized;
                if (isKnockBack) StartCoroutine(KnockBack(knockbackDirection));
            }
        }

    }
    IEnumerator KnockBack(Vector3 knockbackDirection)
    {
        float elapsedTime = 0;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + knockbackDirection * 1;

        while (elapsedTime <= 0.2f)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / 0.2f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isKnockBack = false;
    }
    IEnumerator OnDamage() //피격 코루틴
    {
        if (!isDead)
        {
            mat.color = Color.red;
            HpBarFillImg.color = Color.white;
            HpBar.transform.localScale = new Vector3(5.8f, 5.8f, 5.8f);
            yield return new WaitForSeconds(0.2f);

            if (emydata.emyCurHP > 0)
            {
                mat.color = Color.white;
                HpBarFillImg.color = Color.green;
                HpBar.transform.localScale = new Vector3(5, 5, 5);
            }
            else
            {
                HpBar.transform.localScale = new Vector3(5, 5, 5);
                mat.color = Color.gray;
                HpBarFillImg.color = Color.gray;

                Invoke("DeadEvent", 4);
            }
        }
    }
    void DamageHeelText(string DamageText, float Value)
    {
        if ((int)Value >= 0)
        {
            float randX = Random.Range(-1, 1);
            float randZ = Random.Range(-1, 1);
            Vector3 EffectPos = new Vector3(transform.position.x + randX, transform.position.y + 2, transform.position.z + randZ);
            var damageEffect = PoolingManager.instance.GetGo(DamageText);
            damageEffect.transform.position = EffectPos;

            damageEffect.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + ((int)Value).ToString();
            damageEffect.GetComponent<DamageText>().DamageTextOn();
        }
        else if ((int)Value < 0)
        {
            print("방어");
        }
    }
    bool RandomEvasion(float persent)
    {
        float randomValue = Random.value * 100;
        return randomValue <= persent;
    }
    public void DeadEvent()
    {
        isDead = true;
        ReleaseObject();
    }
}