using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch10Stat : HeroStat
{
    Animator anim;

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
        //SlowTimer - H10
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<EnemyController>().IsSlowTime = true;
                }
                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }
        }
        if (herodata.skillcurTime <= 0)
        {
            //레이저 발사

            herodata.skillcurTime = herodata.skillmaxTime;
        }
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
