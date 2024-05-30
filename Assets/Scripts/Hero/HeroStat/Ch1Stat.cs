using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch1Stat : HeroStat
{
    public AudioClip Skill1_Sound;
    public AudioClip Skill2_Sound;
    Animator anim;

    public GameObject HeelZone;
    public GameObject SkillBullet;
    public Transform SkillShotPos;

    public Sprite skill1_sprite;
    public Sprite skill2_sprite;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public override void SkillUI(Image skill1, Image skill2)
    {
        skill1.sprite = skill1_sprite;
        skill2.sprite = skill2_sprite;
    }
    // 스킬1 - 우클릭 스킬2 - 시프트
    public override void Skill()
    {
        //HeelPack-H1
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GameObject HeelPack = Instantiate(HeelZone, transform.position, Quaternion.identity);
                Invoke("Skill2Sound", 0.2f);
                herodata.second_skillcurTime = herodata.second_skillmaxTime;
                Destroy(HeelPack, 5f);
            }
        }
        //Rocket-H1
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                herodata.skillcurTime = herodata.skillmaxTime;

                SoundManager.Instance.SoundPlay("Ch1Skill1", Skill1_Sound);
                
                GameObject skillbullet = Instantiate(SkillBullet, SkillShotPos.position, SkillShotPos.rotation);
                skillbullet.GetComponent<BulletController>().Player = gameObject;
                skillbullet.GetComponent<BulletController>().range = herodata.range;
            }
        }
    }
    void Skill2Sound() => SoundManager.Instance.SoundPlay("Ch1Skill2", Skill2_Sound);
}
