using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch14Stat : HeroStat
{
    public AudioClip Skill2Audio;
    Animator anim;

    public GameObject SecondSkillObj;
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
        //고유스킬 엄청난 기본 스탯

        //거인
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SecondSkillObj.SetActive(true);

                SecondSkillOn();
                SoundManager.Instance.SoundPlay("Ch14_Skill2", Skill2Audio);
                Invoke("SecondSkillOff", 10);

                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }
        }
    }
    void SecondSkillOn()
    {
        gameObject.transform.DOScale(2, 1);
        herodata.damage *= 2;
        herodata.maxbulletCount *= 2;
    }
    void SecondSkillOff()
    {
        herodata.damage /= 2;
        herodata.maxbulletCount /= 2;
        gameObject.transform.DOScale(1, 1);
        SecondSkillObj.SetActive(false);
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
