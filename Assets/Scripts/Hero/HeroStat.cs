using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public abstract class HeroStat : MonoBehaviour
{
    [SerializeField] public HeroStatScriptable herodata;

    [Header("Move")]
    float hAxis;
    float vAxis;
    Vector3 moveVec;

    float RangeDistance;

    [HideInInspector] public List<GameObject> FoundObjects;
    [HideInInspector] public GameObject enemy;
    [HideInInspector] public float shortDis;

    public bool isReload = false; //장전상태
    public bool isDodge = false; //구르기상태
    public bool isinvincible = false; //무적상태

    GameObject Player;

    public bool IsCh5Skill2 = false;
    public virtual void InitStat(HeroStatScriptable herodata)
    {
        this.herodata = herodata; 
        //print("공격력" + herodata.damage);
    }
    public abstract void Skill();
    public abstract void SkillUI(Image skill1, Image skill2);

    public virtual void Move(GameObject player, Animator anim)
    {
        this.Player = player;
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        player.transform.position += moveVec * herodata.moveSp * Time.deltaTime;

        anim.SetBool("Run", moveVec != Vector3.zero);
    }
    public virtual void AIAttack(Animator anim, GameObject BulletPrefab, Transform[] ShotPos, GameObject ReloadGauge, GameObject bulletCase)
    {
        herodata.attackcoolTime -= Time.deltaTime;
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if (FoundObjects.Count == 0)
        {
            // 처리할 내용 (예: 적이 없는 경우 처리)
            return;
        }

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
        transform.DOLookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), 0.4f);

        if (herodata.CharCode != 4) RangeDistance = 10;
        else RangeDistance = 5;

        if (shortDis < herodata.range * RangeDistance)
        {
            if (herodata.curbulletCount > 0)
            {
                if (herodata.attackcoolTime <= 0)
                {
                    ShotMode(BulletPrefab, ShotPos);
                    herodata.attackcoolTime = herodata.attackSp;
                }
            }
            else if (!isReload)
            {
                StartCoroutine(ReloadCoroutine(anim, ReloadGauge));
            }
        }
    }
    public virtual void Reload(Animator anim, GameObject ReloadGauge)
    {
        if (Player.GetComponent<PlayerController>().isStore) isReload = false;
        if (herodata.curbulletCount < herodata.maxbulletCount)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReload)
            {
                StartCoroutine(ReloadCoroutine(anim, ReloadGauge));
            }
        }
        else if (herodata.curbulletCount <= 0)
        {
            StartCoroutine(ReloadCoroutine(anim, ReloadGauge));
        }
    }
    // 멘트 : 가능한 함수의 매개변수는 4개 이하가 적당합니다.
    public virtual void Shot(Animator anim, GameObject BulletPrefab, Transform[] ShotPos, GameObject ReloadGauge, GameObject bulletCase)
    {
        herodata.attackcoolTime -= Time.deltaTime;

        if (herodata.curbulletCount > 0)
        {
            if (herodata.attackcoolTime <= 0)
            {
                if (Input.GetMouseButton(0) && !isReload )
                {
                    anim.SetTrigger("Shot");
                    ShotMode(BulletPrefab, ShotPos);
                }
            }
        }
        else if (!isReload)
        {
            StartCoroutine(ReloadCoroutine(anim, ReloadGauge));
        }
    }
    public void ShotMode(GameObject BulletPrefab, Transform[] ShotPos)
    {
        switch (herodata.CharCode)
        {
            case 4:
                break;
            //Normal Shot
            case 1:
            case 2:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 12:
            case 13:
            case 14:
                CreateBullet(ShotPos[0]);
                break;

            //Double Shot
            case 11:
                CreateBullet(ShotPos[0]);
                if (herodata.curbulletCount >= 1) StartCoroutine(SecondBullet(ShotPos[1]));
                break;

            //Tripple Shot
            case 3: case 5:
                CreateBullet(ShotPos[0]);
                if(herodata.curbulletCount >= 1) CreateBullet(ShotPos[0]);
                if (herodata.curbulletCount >= 1) CreateBullet(ShotPos[0]);
                break;
        }
    }
    void CreateBullet(Transform ShotPos)
    {
        float Accuracy = Random.Range(-herodata.accuracy, herodata.accuracy);

        var bullet = PoolingManager.instance.GetGo("Bullet");
        bullet.GetComponent<BulletController>().isCollided = false;
        bullet.transform.position = ShotPos.transform.position;
        bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, Accuracy, 0);

        if (Player.GetComponent<Ch7Stat>() != null) Player.GetComponent<Ch7Stat>().FireBullet = bullet;
        
        BulletHp(bullet);
        BulletCaseIntant(ShotPos);

        herodata.curbulletCount -= 1;
        herodata.attackcoolTime = herodata.attackSp;
    }
    void BulletHp(GameObject Bullet)
    {
        Bullet.GetComponent<BulletController>().BulletcurHP = herodata.bulletcurHP;
        Bullet.GetComponent<BulletController>().Player = this.Player;
        Bullet.GetComponent<BulletController>().range = herodata.range;
    }
    IEnumerator SecondBullet(Transform ShotPos)
    {
        yield return new WaitForSeconds(0.2f);
        CreateBullet(ShotPos);
    }
    void BulletCaseIntant(Transform ShotPos)
    {
        Vector3 bulletCasePos = new Vector3(ShotPos.position.x, ShotPos.position.y, ShotPos.position.z - 0.8f);
        var bulletCase = PoolingManager.instance.GetGo("BulletCase");
        bulletCase.transform.position = bulletCasePos;
        bulletCase.GetComponent<BulletCase>().BulletCaseOn();

        Rigidbody caseRigid = bulletCase.GetComponent<Rigidbody>();
        Vector3 caseVec = ShotPos.right * Random.Range(2, 3) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);

    }
    IEnumerator ReloadCoroutine(Animator anim, GameObject ReloadGauge)
    {
        ReloadGauge.SetActive(true);
        isReload = true;
        anim.SetFloat("ReloadSpeed", (1 / herodata.reloadTime) - 0.1f);
        anim.SetTrigger("Reload");
        while (herodata.reloadCoolTime > 0)
        {
            if (IsCh5Skill2) break;
            herodata.reloadCoolTime -= Time.deltaTime;


            ReloadGauge.GetComponent<Slider>().value = herodata.reloadCoolTime / herodata.reloadTime;

            yield return null; // 한 프레임 대기
        }

        //print("장전종료");
        IsCh5Skill2 = false;
        herodata.curbulletCount = herodata.maxbulletCount;
        herodata.reloadCoolTime = herodata.reloadTime;
        herodata.attackcoolTime = herodata.attackSp;
        isReload = false;
        ReloadGauge.SetActive(false);
    }
    public virtual void LevelUp(GameObject LevelUp, InGameManager InGameManager, AudioClip audioClip)
    {
        //Stat Upgrade
        if (herodata.maxExp <= herodata.curExp)
        {
            SoundManager.Instance.SoundPlay("PlayerLvUp", audioClip);

            StartCoroutine(LevelUpEffectOn(LevelUp));
            herodata.Level++;
            herodata.maxExp *= 1.15f; //최고 경험치
            herodata.curExp = 0;
            InGameManager.lvUpCount += 1;
        }
    }

    IEnumerator LevelUpEffectOn(GameObject LevelUp)
    {
        LevelUp.SetActive(true);
        yield return new WaitForSeconds(2);
        LevelUp.SetActive(false);
    }
    public void UpgraderStat(int CharNum, int Grade)
    {
        int Value = Grade switch
        {
            1 => 1,
            2 => 1,
            3 => 2,
            4 => 3,
            5 => 4,
            6 => 5,
            7 => 6,
            8 => 7,
            9 => 8,
            10 => 9,
            _ => 1,
        };
        if (Grade > 1)
        {
            for (int i = 0; i < Value; i++)
            {
                //스탯 상향
                herodata.maxHp += 2; //최대 체력
                herodata.curHp = herodata.maxHp;

                herodata.hpRecovery += 1; //회복력
                print(CharNum);
                if(CharNum != 4) herodata.maxbulletCount += 2; // 최대 탄창 수
                herodata.reloadTime *= 0.97f; //재장전 속도

                herodata.attackSp *= 0.97f; //공격속도
                herodata.moveSp *= 1.01f; //이동속도
                herodata.damage += 1; //공격력
                herodata.skillDamage += 1; //스킬데미지
                herodata.bombDamage += 1; // 폭발데미지
                herodata.science += 1; //기계화
                herodata.accuracy *= 0.95f; //명중률
                herodata.critical += 0.5f; //치명타

                herodata.defense *= 1.05f; //방어력

                herodata.absorption *= 1.01f; //체력흡수
                herodata.evasion *= 1.01f; //회피율
                herodata.hasExp *= 1.03f; //경험치획득

                herodata.range *= 1.03f; //아이템 획득 범위
                herodata.diamondPer *= 1.01f; //보석 획득률
                                          //public float RandomBoxPer; //상자 드랍 확률

                herodata.skillmaxTime *= 0.99f; //스킬 쿨타임
                herodata.second_skillmaxTime *= 0.99f; //두번째 스킬 쿨타임
            }
        }
    }
}