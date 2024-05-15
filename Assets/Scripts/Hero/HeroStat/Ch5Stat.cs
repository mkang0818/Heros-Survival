using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch5Stat : HeroStat
{
    public AudioClip Skill1_1Audio;
    public AudioClip Skill1_2Audio;
    public AudioClip ShotAudio;

    Animator anim;

    public GameObject BulletPrefab;
    public Transform[] ShotPos;
    public GameObject bulletCase;

    bool isSkill = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public Sprite skill1_sprite;
    public Sprite skill2_sprite;
    public override void SkillUI(Image skill1, Image skill2)
    {
        skill1.sprite = skill1_sprite;
        skill2.sprite = skill2_sprite;
    }
    // 스킬1 - 우클릭 스킬2 - 시프트
    public override void Skill()
    {
        //Dodge - H5
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && transform.position != Vector3.zero && !isSkill)
            {
                base.isReload = true;
                anim.SetTrigger("IsDodge");
                if (base.isReload)
                {
                    Invoke("DodgeStart", 0.1f);
                }
                Invoke("DodgeEnd", 0.5f);
                Invoke("init", 1.3f);
                herodata.curbulletCount = herodata.maxbulletCount;
                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }
        }
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!base.isReload && !isSkill)
                {
                    isSkill = true;
                    StartCoroutine(SecondSkill());
                    herodata.skillcurTime = herodata.skillmaxTime;
                }
            }
        }
    }
    IEnumerator SecondSkill()
    {
        int count = herodata.maxbulletCount / 3;
        for (int i = 0; i < count; i++)
        {
            if (herodata.curbulletCount >= 1)
            {
                anim.SetTrigger("Shot");
                base.ShotMode(BulletPrefab, ShotPos);
            }
            yield return new WaitForSeconds(0.2f);
        }
        isSkill = false;
    }
    void DodgeStart()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 lookVec = new Vector3(h, 0, v);
        transform.LookAt(transform.position + lookVec);
        herodata.moveSp *= 2;
        SoundManager.Instance.SoundPlay("Ch5_Skill2", Skill1_1Audio);
        SoundManager.Instance.SoundPlay("Ch5_Reload", Skill1_2Audio);
    }
    void DodgeEnd()
    {
        base.IsCh5Skill2 = true;
        herodata.moveSp /= 2;
    }
    void init()
    {
        base.isReload = false;
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
