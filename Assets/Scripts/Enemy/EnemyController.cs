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
        emydata = Instantiate(this.emydataPrefab);
        Emy.initSetting(emydata);
        Emy.Attack(target);

        Emy.LevelUp(levelNum);

        agent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        mat = ChildMesh.GetComponent<SkinnedMeshRenderer>().material;

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
            ingameManager.GetComponent<InGameManager>().IsBossDead = true;
            ingameManager.GetComponent<InGameManager>().CurTimer = 0;
        }
    }
    void UpdateHP()
    {
        HpBar.value = Mathf.Lerp(HpBar.value, (float)emydata.emyCurHP / (float)emydata.emyMaxHP, Time.deltaTime * 10);

        //print(gameObject.name+emydata.emyMaxHP);
        //print(gameObject.name + emydata.emyAttack);
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
        SoundManager.Instance.SoundPlay(SoundName, audioclipArr[randomNum]);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Bomb"))
        {
            AttackSound("PlayerBomb", PlayerBombAudio);
            float Value = target.GetComponent<PlayerController>().herodata.bombDamage;
            emydata.emyCurHP -= Value;
            DamageHeelText("DamageText", Value);
            KnockBack();

            StartCoroutine(OnDamage());
            StartCoroutine(BombTrigger(col.gameObject));
        }
        if (col.gameObject.CompareTag("SlowBullet"))
        {
            StartCoroutine(SlowMove());
            emydata.emyCurHP -= target.GetComponent<PlayerController>().herodata.science;
            StartCoroutine(OnDamage());
            col.gameObject.GetComponent<BulletController>().ReleaseObject();
        }
        else if (col.gameObject.CompareTag("TurretBullet"))
        {
            AttackSound("PlayerAttack", ArrPlayerAtAudio);
            emydata.emyCurHP -= target.GetComponent<PlayerController>().herodata.science;

            float Value = target.GetComponent<PlayerController>().herodata.science;
            DamageHeelText("DamageText", Value);

            StartCoroutine(OnDamage());
            col.gameObject.GetComponent<BulletController>().ReleaseObject();
        }

        if (col.gameObject.CompareTag("Bullet") || col.gameObject.CompareTag("AttackPos"))
        {
            float Value, Absorption;
            float Damage = (target.GetComponent<PlayerController>().herodata.damage);

            //총알 체력 감소
            if (col.gameObject.GetComponent<BulletController>() != null) col.gameObject.GetComponent<BulletController>().BulletcurHP -= 1;

            // 치명타 확률 구현
            float CriticalPer = target.GetComponent<PlayerController>().herodata.critical;
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
                Absorption = emydata.emyCurHP * target.GetComponent<PlayerController>().herodata.absorption;
                emydata.emyCurHP -= Absorption;
                //target.GetComponent<PlayerController>().herodata.CurHp += Absorption;
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
        if(HitObj!=null) HitObj.GetComponent<SphereCollider>().enabled = false;
        //오류남
    }
    IEnumerator SlowMove()
    {
        emydata.emyMoveSp /= 2;
        yield return new WaitForSeconds(3f);
        emydata.emyMoveSp *= 2;
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("SkillBullet"))
        {
            SkillBulletTime += Time.deltaTime;

            if (SkillBulletTime >= 2f)
            {
                if (col.gameObject.GetComponent<BulletController>() != null) col.gameObject.GetComponent<BulletController>().BulletcurHP -= 1;

                float Value;
                float Damage = (target.GetComponent<PlayerController>().herodata.skillDamage);
                emydata.emyCurHP -= Damage;
                Value = Damage;

                DamageHeelText("DamageText", Value);
                StartCoroutine(OnDamage()); //피격 구현

                SkillBulletTime = 0;
            }
        }
        if (col.gameObject.CompareTag("Player") && !col.GetComponent<PlayerController>().isStore)
        {
            AttackCurTime -= Time.deltaTime;
            if (AttackCurTime <= 0)
            {
                //print("11");
                bool isEvasion = RandomEvasion(col.GetComponent<PlayerController>().herodata.evasion);

                if (isEvasion)
                {
                    print("회피!!");
                    AttackCurTime = AttackMaxTime;
                }
                else
                {
                    if (!col.GetComponent<PlayerController>().herostat.isinvincible)
                    {
                        float EmyDamage = (int)(emydata.emyAttack - (col.GetComponent<PlayerController>().herodata.defense/100)* (int)(emydata.emyAttack));
                        print("플레이어 체력감소 : " + EmyDamage);
                        if(EmyDamage > 0) col.GetComponent<PlayerController>().herodata.CurHp -= EmyDamage;

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
                    if (col.GetComponent<Ch4Stat>() != null && (col.GetComponent<Ch4Stat>().isDash || col.GetComponent<Ch4Stat>().isSpin))
                    {
                        return;
                    }
                    Destroy(Instantiate(DeadEffect, transform.position, Quaternion.identity), 2f);
                    SoundManager.Instance.SoundPlay("PlayerAt", ArrPlayerAtAudio[Random.Range(0, ArrPlayerAtAudio.Length)]);
                    //SoundManager.Instance.SoundPlay("PlayerAt", SoundManager.Instance.PlayerPainAudio[Random.Range(0, SoundManager.Instance.PlayerPainAudio.Length)]);
                    //Destroy(gameObject);
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
        emydata.emyCurHP -= target.GetComponent<PlayerController>().herodata.skillDamage;
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

                Invoke("DeadEvent",4);
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
        else if((int)Value < 0)
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
        //Instantiate(MoneyPrefab, transform.position, Quaternion.identity);
    }
}