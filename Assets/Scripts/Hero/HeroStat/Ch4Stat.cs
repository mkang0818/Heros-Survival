using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Ch4Stat : HeroStat
{
    public AudioClip AttackAudio;
    public AudioClip Skill1Audio;
    public AudioClip Skill2AudioArr;

    GameObject player;
    Animator anim;
    [HideInInspector] public bool isDash = false;
    [HideInInspector] public bool isSpin = false;

    public GameObject SpinEffect;

    public GameObject AttackPos;
    public GameObject AttackEffect;
    public GameObject HitEffect;
    public GameObject SkillTrailRenderer;

    public GameObject DamageText;

    public Sprite skill1_sprite;
    public Sprite skill2_sprite;
    GameObject SkillTrail;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isSpin)
        {
            transform.Rotate(Vector3.up, 10000 * Time.deltaTime);
        }
    }
    public override void SkillUI(Image skill1, Image skill2)
    {
        skill1.sprite = skill1_sprite;
        skill2.sprite = skill2_sprite;
    }
    // 스킬1 - 우클릭 스킬2 - 시프트
    public override void Skill()
    {
        //스핀썰기
        if (herodata.second_skillcurTime <= 0 && !isSpin)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(SpinSkill());
                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }
        }
        //Dash - H4
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1) && transform.position != Vector3.zero)
            {
                isinvincible = true; 
                isDash = true;
                SkillTrail = Instantiate(SkillTrailRenderer, transform.position, Quaternion.identity);
                SkillTrail.GetComponent<DropController>().target = gameObject;
                SkillTrail.GetComponent<DropController>().isFollow = true;
                anim.SetTrigger("Dash");
                SoundManager.Instance.SoundPlay("Ch4Skill1", Skill1Audio);
                if (isinvincible)
                {
                    herodata.moveSp *= 2.5f;
                }
                Invoke("DashEnd", 0.4f);
                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }

    }

    IEnumerator SpinSkill()
    {
        isinvincible = true;
        GameObject SpinEfect = Instantiate(SpinEffect, transform.position, Quaternion.identity);
        SpinEfect.GetComponent<DropController>().target = gameObject;
        player.GetComponent<PlayerController>().isMouse = false;
        isSpin = true;
        Destroy(SpinEfect, 3);
        StartCoroutine(Skill2Sound());
        yield return new WaitForSeconds(3);
        StopCoroutine(Skill2Sound());
        isinvincible = false;
        isSpin = false;
        player.GetComponent<PlayerController>().isMouse = true;
    }
    IEnumerator Skill2Sound()
    {
        float elapsedTime = 0f; // 경과 시간을 추적하기 위한 변수

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            SoundManager.Instance.SoundPlay("Ch4Skill2", Skill2AudioArr);

            // 경과 시간을 누적
            elapsedTime += 0.2f;

            // 3초가 경과하면 루프를 빠져나옴
            if (elapsedTime >= 2.8f)
            {
                yield break;
            }
        }
    }
    void DashEnd()
    {
        herodata.moveSp /= 2.5f;
        isinvincible = false;
        isDash = false;
        Destroy(SkillTrail);
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim); this.player = player;
    }
    public override void AIAttack(Animator anim, GameObject BulletPrefab, Transform[] ShotPos, GameObject ReloadGauge, GameObject bulletCase, GameObject MeleeAtObj)
    {
        herodata.attackcoolTime -= Time.deltaTime;
        FoundObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if (FoundObjects.Count == 0 || isSpin)
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

        if (shortDis < 5)
        {
            if (herodata.attackcoolTime <= 0)
            {
                //print("발사");
                Quaternion rotation = AttackPos.transform.rotation * Quaternion.Euler(0, 180, 0);
                GameObject hiteffect = Instantiate(AttackEffect, AttackPos.transform.position, rotation);
                Destroy(hiteffect, 0.2f);

                anim.SetTrigger("Shot");
                SoundManager.Instance.SoundPlay("CH4At", AttackAudio);
                AttackPos.SetActive(true);

                Invoke("AttackOff", 0.2f);
                herodata.attackcoolTime = herodata.attackSp;
            }
        }
    }
    public override void Reload(Animator anim, GameObject ReloadGauge)
    {
        return;
    }
    public override void Shot(Animator anim, GameObject BulletPrefab, Transform[] ShotPos, GameObject ReloadGauge, GameObject bulletCase, GameObject MeleeAtObj)
    {
        herodata.attackcoolTime -= Time.deltaTime;
        if (herodata.attackcoolTime <= 0 && !isSpin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //print("공격");
                Quaternion rotation = AttackPos.transform.rotation * Quaternion.Euler(0, 180, 0);
                GameObject hiteffect = Instantiate(AttackEffect, AttackPos.transform.position, rotation);
                Destroy(hiteffect, 0.2f);

                anim.SetTrigger("Shot");
                SoundManager.Instance.SoundPlay("CH4At", AttackAudio);
                AttackPos.SetActive(true);

                herodata.attackcoolTime = herodata.attackSp;
                Invoke("AttackOff", 0.2f);
            }
        }
    }
    void AttackOff() => AttackPos.SetActive(false);

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Enemy") && col.gameObject.GetComponent<EnemyController>() != null)
        {
            if (isinvincible)
            {
                col.gameObject.GetComponent<EnemyController>().emydata.emyCurHP -= herodata.skillDamage;
                GameObject hiteffect = Instantiate(HitEffect, col.transform.position, Quaternion.identity);
                if (col.gameObject.GetComponent<EnemyController>().emydata.emyCurHP <= 0)
                {
                    //print("처지완료");
                    herodata.CurHp += 1;
                    herodata.curExp += 1;
                    DamageHeelText(DamageText, herodata.skillDamage);
                    herodata.skillcurTime = 0.2f;
                }

            }
        }
    }
    void DamageHeelText(GameObject TextObj, float Value)
    {
        if ((int)Value > 0)
        {
            float randX = Random.Range(-1, 1);
            float randZ = Random.Range(-1, 1);
            Vector3 EffectPos = new Vector3(transform.position.x + randX, transform.position.y + 2, transform.position.z + randZ);
            GameObject damageEffect = Instantiate(TextObj, EffectPos, Quaternion.identity);

            if (TextObj.tag == "HeelTextEffect")
            {
                damageEffect.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = ((int)Value).ToString();
            }
            else if (TextObj.tag == "DamageTextEffect")
            {
                damageEffect.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + ((int)Value).ToString();
            }

            Destroy(damageEffect, 1f);
        }
    }
}
