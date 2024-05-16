using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch8Stat : HeroStat
{
    Animator anim;
    public GameObject Turret;
    public GameObject RandMineObj;

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
        //Turret - H8
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                float Xpos = Random.Range(-8, 8);
                float Zpos = Random.Range(-8, 8);

                GameObject TurretObj = Instantiate(Turret, new Vector3(Xpos, 0, Zpos), Quaternion.identity);
                TurretObj.GetComponent<TurretController>().BulletText = "TurretBombBullet";

                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }           
        }

        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //print("지뢰 설치");
                GameObject randmine = Instantiate(RandMineObj,transform.position,Quaternion.identity);

                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
