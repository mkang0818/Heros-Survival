using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ��Ʈ : �� ��ũ��Ʈ�� �Ҵ��� ��ü�� ������ image ������Ʈ�� �پ��մ� ��쿡 ���� ����
[RequireComponent(typeof(Image))]
public class PlayerController : MonoBehaviour
{
    public HeroStatScriptable herodataPrefab;
    [HideInInspector] public HeroStatScriptable herodata;
    Image image;

    /*void Awake()
    {
        image = GetComponent<Image>();
    }*/

    public AudioClip ArrCoinAudio;
    public AudioClip DiaAudio;
    public AudioClip LvUpAudio;

    public Animator anim;

    public HeroStat herostat;
    InGameManager InGameManager;

    Slider hpBar;
    Slider expBar;
    TextMeshProUGUI LevelText;
    TextMeshProUGUI TxtBulletCount;

    // ��Ʈ : ����� ������Ʈ�� ���� ��� Ŭ������ ��� ó���ϴ� �͵� �����ϴ�
    /* ��Ʈ : ex)
     * public class Skill
     * {
     *   public Image Gauge;
     *   public GameObject SliderObj;
     *   public Slider Slider;
     *   public Text CoolTime;
     * }
     * 
     * - ���� ��
     * Skill Skill1;
     * Skill Skill2;
     */
    Image Skill1Gauge;
    Slider Skill1SliderObj;
    Slider Skill1Slider;
    TextMeshProUGUI Skill1CoolTime;

    Image Skill2Gauge;
    Slider Skill2SliderObj;
    Slider Skill2Slider;
    TextMeshProUGUI Skill2CoolTime;

    GameObject ReloadGauge;

    float HeelTime = 2;

    public bool isAI = false;
    public bool isRange = false;
    public bool isStart = false;
    public bool isStore = false;
    public bool isMouse = true;

    public Transform[] ShotPos;
    public GameObject BulletPrefab;
    public GameObject bulletCase;

    public GameObject LevelUpEffect;
    public SphereCollider PlayerSphere;

    public bool isSpinBullet = false;
    public GameObject SpinBullet;
    public bool isMultiBullet = false;
    public GameObject MultiBullet;
    float MultiBulletCoolTime;


    float hpCoolTime = 1;
    float hpCurTime = 1;

    public GameObject DamageText;
    public GameObject HeelText;
    [HideInInspector] public bool IsDead = false;
    [HideInInspector] public bool IsReborn = false;

    public GameObject Circle;

    private void Awake()
    {
        PoolingManager.instance.Init(BulletPrefab);

        IsReborn = false;
        if (herostat != null)
        {
            InGameManager = GameObject.Find("InGameManager").GetComponent<InGameManager>();

            herodata = Instantiate(this.herodataPrefab);
            herostat.InitStat(herodata);

            herodata.CharUpgrade = InGameManager.charUpgradeNum;
            herostat.UpgraderStat(herodata.CharCode, DataManager.Instance.nowPlayer.CharGrade[herodata.CharCode - 1]);

        }
    }
    void Start()
    {
        InGameManagerGetUI();
        //print("ü��"+herodata.curHp);
        if (isRange)
        {
            Circle.SetActive(true);
            /* ��Ʈ
             * ������ ����ȭ ���ּ���
             * var value = herodata.Range * 3.4f
             * Vector3 vec = new Vector3(value, value, value);
             * Circle.transform.localScale = vec;
             */
            if (herodata.CharCode != 4) Circle.transform.localScale = new Vector3(herodata.range * 3.4f, herodata.range * 3.4f, herodata.range * 3.4f);
            else Circle.transform.localScale = new Vector3(herodata.range * 1.7f, herodata.range * 1.7f, herodata.range * 1.7f);
        }
    }
    void Update()
    {
        // print("max" + herodata.maxHp);
        //print("cur" + herodata.CurHp);
        //print("Level" + herodata.Level);
        //print("curExp" + herodata.curExp);
        //print("damage" + herodata.damage);
        // ��Ʈ : Update() �ȿ� ���� ������ ������ �Լ�ȭ ���ּ���
        /* 
         * ex)
         * void NotPlay()
         * {
         *   if (isStart && !isStore && !InGameManager.IsOption)
         *   {
         *     ...
         *   }
         * }
         */
        if (isStart && !isStore && !InGameManager.IsOption)
        {
            HpRecovery();
            UpdateHP();

            herodata.skillcurTime -= Time.deltaTime;
            herodata.second_skillcurTime -= Time.deltaTime;
            HeelTime -= Time.deltaTime;

            herostat.Move(this.gameObject, anim);
            herostat.LevelUp(LevelUpEffect, InGameManager, LvUpAudio);

            if (!isAI)
            {
                if (isMouse) LookMouseCursor();
                herostat.Shot(anim, BulletPrefab, ShotPos, ReloadGauge, bulletCase);
                herostat.Reload(anim, ReloadGauge);
            }
            // ��Ʈ : ����� ���� �� ������ else������ �ٲ��ּ���
            else
            {
                herostat.AIAttack(anim, BulletPrefab, ShotPos, ReloadGauge, bulletCase);
            }

            if (herodata.skillcurTime <= 0 || herodata.second_skillcurTime <= 0)
            {
                herostat.Skill();
            }

            UpdateUI();
            ItemActive();
            Dead();
            PlayerMove();
        }
        else
        {
            anim.SetBool("Run", false);
            herodata.curbulletCount = herodata.maxbulletCount;
            if (herodata.CharCode == 2) GetComponent<Ch2Stat>().IsRun = true;
        }
    }
    void PlayerMove()
    {// �÷��̾��� ���� ��ġ�� ������
        Vector3 currentPosition = transform.position;

        // x��� z�� ��ġ�� ������ ����� �ش� ��ġ�� �����Ͽ� ����
        currentPosition.x = Mathf.Clamp(currentPosition.x, -14.5f, 14.5f);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -12.9f, 10.6f);

        // ������ ��ġ�� �÷��̾��� �� ��ġ�� ����
        transform.position = currentPosition;
    }
    void ItemActive()
    {
        if (isSpinBullet)
        {
            StartCoroutine(InstSpinBullet());
            print("���ɺҷ� ȣ��");
            isSpinBullet = false;
        }
        if (isMultiBullet)
        {
            InstMultiBullet();
        }
    }
    void InstMultiBullet()
    {
        MultiBulletCoolTime -= Time.deltaTime;
        if (MultiBulletCoolTime <= 0)
        {
            print("�ٹ���Ѿ� �߻�!!!");
            for (int i = 0; i < 9; i++)
            {
                var bullet = PoolingManager.instance.GetGo("Bullet");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, 45 * i, 0);
                bullet.GetComponent<BulletController>().Player = gameObject;
            }
            MultiBulletCoolTime = 7;
        }
    }
    IEnumerator InstSpinBullet()
    {
        print("�����Ѿ� �߻�!!!");
        for (int i = 0; i < 6; i++)
        {
            print(i + "����");
            yield return new WaitForSeconds(0.25f);
            GameObject spinBullet1 = Instantiate(SpinBullet, transform.position, Quaternion.identity);
            spinBullet1.GetComponent<CH6SpinBulletController>().player = gameObject.transform;
        }
        yield return null;
    }
    public void LookMouseCursor()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        // ��Ʈ : rayLength ���� ���ϰ� out float rayLength�� ���� �־ ��� �����մϴ�
        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
    }
    void HpRecovery()
    {
        hpCurTime -= Time.deltaTime;
        if (hpCurTime <= 0)
        {
            if (herodata.CurHp + (herodata.hpRecovery * 0.1f) < herodata.maxHp)
            {
                herodata.CurHp += herodata.hpRecovery * 0.1f;
            }
            hpCurTime = hpCoolTime;
        }
    }
    void UpdateHP()
    {
        LevelText.text = "LV." + herodata.Level.ToString();
        hpBar.value = Mathf.Lerp(hpBar.value, herodata.CurHp / herodata.maxHp, Time.deltaTime * 10);
        expBar.value = Mathf.Lerp(expBar.value, herodata.curExp / herodata.maxExp, Time.deltaTime * 10);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("HeelBullet"))
        {
            col.gameObject.GetComponent<TurretBulletController>().ReleaseObject();
            float Value;
            if ((herodata.CurHp + (herodata.science / 4)) < herodata.maxHp)
            {
                herodata.CurHp += herodata.science / 4;
                Value = herodata.science / 4;

                print(Value + "��ŭ ȸ��");
            }
            else
            {
                herodata.CurHp = herodata.maxHp;
                Value = herodata.maxHp - herodata.CurHp;
            }

            if (Value > 0) DamageHeelText("HeelText", Value);
        }

        //Boss
        if (col.gameObject.CompareTag("BossFallBullet"))
        {
            float damage = col.GetComponent<BossFallBulletController>().damage;
            //herodata.CurHp -= damage;
            PlayerDamage(damage);

            DamageHeelText("EmyDamageText", damage);
            print("�����Ѿ�" + damage);
        }
        else if (col.gameObject.CompareTag("BossSpinBullet"))
        {
            float damage = col.GetComponent<BossSpinBulletController>().damage;
            //herodata.CurHp -= damage;
            PlayerDamage(damage);

            DamageHeelText("EmyDamageText", damage);
            print("�����Ѿ�" + damage);
        }


        int moneyLayer = LayerMask.NameToLayer("Coin");
        int diamondLayer = LayerMask.NameToLayer("Diamond");
        int healthLayer = LayerMask.NameToLayer("Health");

        if (col.transform.CompareTag("NextCoin"))
        {
            InGameManager.Nextmoney += (int)herodata.harvesting;
            SoundManager.Instance.SoundPlay("Coin", ArrCoinAudio);
            col.gameObject.GetComponent<DropController>().ReleaseObject();
        }
        else if (col.gameObject.layer == moneyLayer)
        {
            SoundManager.Instance.SoundPlay("Coin", ArrCoinAudio);
            InGameManager.GetComponent<InGameManager>().money += (int)herodata.harvesting;
            col.gameObject.GetComponent<DropController>().ReleaseObject();
        }
        else if (col.gameObject.layer == diamondLayer)
        {
            SoundManager.Instance.SoundPlay("Dia", DiaAudio);
            InGameManager.GetComponent<InGameManager>().diamond += 1;
            col.gameObject.GetComponent<DropController>().ReleaseObject();
        }
        else if (col.gameObject.layer == healthLayer)
        {
            if ((herodata.CurHp + (herodata.maxHp * 0.2f)) < herodata.maxHp)
                herodata.CurHp += herodata.maxHp * 0.2f;
            else
                herodata.CurHp = herodata.maxHp;

            col.gameObject.GetComponent<DropController>().ReleaseObject();
        }
    }

    void PlayerDamage(float damage)
    {
        bool isEvasion = RandomEvasion(herodata.evasion);

        if (isEvasion)
        {
            print("ȸ��!!");
        }
        else
        {
            if (!herostat.isinvincible)
            {
                float EmyDamage = (int)(damage - (herodata.defense / 100) * (int)(damage));
                print("�÷��̾� ü�°��� : " + EmyDamage);
                if (EmyDamage > 0) herodata.CurHp -= EmyDamage;

                DamageHeelText("EmyDamageText", EmyDamage);
            }
            else
            {
                print("����");
            }
        }
    }
    bool RandomEvasion(float persent)
    {
        float randomValue = Random.value * 100;
        return randomValue <= persent;
    }

    private void OnTriggerStay(Collider col)
    {
        //SkillItem
        if (col.gameObject.CompareTag("HeelZone"))
        {
            if (HeelTime <= 0)
            {
                if ((herodata.CurHp + 0.1f * herodata.maxHp) < herodata.maxHp)
                {
                    herodata.CurHp += 0.1f * herodata.maxHp;
                    float Value = 0.1f * herodata.maxHp;
                    DamageHeelText("HeelText", Value);
                }
                else
                {
                    herodata.CurHp = herodata.maxHp;
                }
                HeelTime = 0.7f;
            }
        }


        if (col.gameObject.CompareTag("BossFire"))
        {
            float FireDmgCoolTime = 1.5f;
            FireDmgCoolTime -= Time.deltaTime;


            if (FireDmgCoolTime <= 0)
            {
                print("��");
                float Damage = col.GetComponent<BossFireController>().damage;
                //herodata.CurHp -= Damage;
                PlayerDamage(Damage);

                DamageHeelText("EmyDamageText", Damage);
                print("���� �Ҳɵ�����" + Damage);
            }
        }
    }
    void InGameManagerGetUI()
    {
        hpBar = InGameManager.InGameUIGroup.InPlayUI.hpBar;
        expBar = InGameManager.InGameUIGroup.InPlayUI.ExpBar;
        LevelText = InGameManager.InGameUIGroup.InPlayUI.levelText;
        TxtBulletCount = InGameManager.InGameUIGroup.InPlayUI.BulletCountText;

        Skill1Gauge = InGameManager.InGameUIGroup.InPlayUI.Skill1Img;
        Skill1SliderObj = InGameManager.InGameUIGroup.InPlayUI.Skill1Slider;
        Skill1Slider = InGameManager.InGameUIGroup.InPlayUI.Skill1Slider;
        Skill1CoolTime = InGameManager.InGameUIGroup.InPlayUI.Skill1CoolTime;

        Skill2Gauge = InGameManager.InGameUIGroup.InPlayUI.Skill2Img;
        Skill2SliderObj = InGameManager.InGameUIGroup.InPlayUI.Skill2Slider;
        Skill2Slider = InGameManager.InGameUIGroup.InPlayUI.Skill2Slider;
        Skill2CoolTime = InGameManager.InGameUIGroup.InPlayUI.Skill2CoolTime;
        ReloadGauge = InGameManager.InGameUIGroup.InPlayUI.ReloadGauge;
        
        var data = herodata;
        hpBar.value = data.CurHp / data.maxHp;
        expBar.value = data.curExp / data.maxExp;

        herostat.SkillUI(Skill1Gauge, Skill2Gauge);
        PlayerSphere.radius = data.range;
    }
    void UpdateUI()
    {
        TxtBulletCount.text = herodata.curbulletCount + " I " + herodata.maxbulletCount;

        if (herodata.skillcurTime > 0)
        {
            //Skill1SliderObj.SetActive(true);
            Skill1Slider.value = 1.0f - (Mathf.Lerp(0, 100, herodata.skillcurTime / herodata.skillmaxTime) / 100);
            Skill1CoolTime.text = ((int)herodata.skillcurTime).ToString();
        }
        else if (herodata.skillcurTime <= 0)
        {
            Skill1Slider.value = 0;
            Skill1CoolTime.text = "";
        }

        if (herodata.second_skillcurTime > 0)
        {
           // Skill2SliderObj.SetActive(true);
            Skill2Slider.value = 1.0f - (Mathf.Lerp(0, 100, herodata.second_skillcurTime / herodata.second_skillmaxTime) / 100);
            Skill2CoolTime.text = ((int)herodata.second_skillcurTime).ToString();
        }
        else if (herodata.second_skillcurTime <= 0)
        {
            Skill2Slider.value = 0;
            Skill2CoolTime.text = "";
        }
    }
    void DamageHeelText(string ObjText, float Value)
    {
        float randX = Random.Range(-1, 1);
        float randZ = Random.Range(-1, 1);
        Vector3 EffectPos = new Vector3(transform.position.x + randX, transform.position.y + 2, transform.position.z + randZ);
        var damageEffect = PoolingManager.instance.GetGo(ObjText);
        damageEffect.transform.position = EffectPos;

        damageEffect.GetComponent<DamageText>().DamageTextOn();
        if (ObjText == "HeelText")
        {
            damageEffect.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)Value).ToString();
        }
        else if (ObjText == "EmyDamageText")
        {
            damageEffect.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + ((int)Value).ToString();
        }
    }

    void Dead()
    {
        if (herodata.CurHp <= 0)
        {
            if (IsReborn)
            {
                herodata.CurHp = herodata.maxHp;
                IsReborn = false;
                StartCoroutine(Reborn());
            }
            else
            {
                IsDead = true;
            }
        }   
    }
    IEnumerator Reborn()
    {
        print("�״���");
        anim.SetTrigger("Fail");
        yield return new WaitForSeconds(1f);

        print("�ٽ� ��Ƴ�");
        //����Ʈ
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ����� ���� ����
        Vector3 scale;
        // ������ �� ��������
        if (herodata.CharCode != 4) scale = new Vector3(herodata.range * 10, 0, herodata.range * 10);
        else scale = new Vector3(herodata.range * 5, 0, herodata.range * 5);

        // �����ϸ�ŭ�� ũ��� ��ü ����� ����� �׸��� (�� ����)
        Vector3 groundPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Gizmos.DrawWireSphere(groundPosition, Mathf.Max(scale.x, scale.z));
    }
}
