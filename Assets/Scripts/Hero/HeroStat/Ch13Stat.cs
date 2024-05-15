using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch13Stat : HeroStat
{
    public AudioClip Skill1Audio;
    public AudioClip Skill2Audio;

    Animator anim;
    public GameObject Shield;
    public Sprite skill1_sprite;
    public Sprite skill2_sprite;
    public GameObject SecondSkillObj;
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
        //Invincible 무적 - H13
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isinvincible = true;
                Shield.SetActive(true);
                SoundManager.Instance.SoundPlay("Ch13_Skill2", Skill2Audio);
                herodata.second_skillcurTime = herodata.second_skillmaxTime;

                Invoke("InitSkill", 5);
            }
        }
        //회복
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (herodata.CurHp + (herodata.maxHp * 0.7f) < herodata.maxHp)
                {
                    herodata.CurHp += herodata.maxHp * 0.7f;
                }
                else herodata.CurHp = herodata.maxHp;

                SecondSkillObj.SetActive(true);
                Invoke("SkillOff", 1);
                SoundManager.Instance.SoundPlay("Ch13_Skill1", Skill1Audio);
                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    void SkillOff() => SecondSkillObj.SetActive(false);
    void InitSkill()
    {
        isinvincible = false;
        Shield.SetActive(false);
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
