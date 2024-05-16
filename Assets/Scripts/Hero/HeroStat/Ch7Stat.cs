using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch7Stat : HeroStat
{
    public AudioClip Skill1Audio;
    public AudioClip Skill2Audio;
    Animator anim;
    [HideInInspector] public GameObject FireBullet;
    public GameObject FireBulletHit;

    public GameObject SkillObj;
    public GameObject SecondSkillObj;
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
        //사방 총알
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SoundManager.Instance.SoundPlay("Ch7_Skill2", Skill2Audio);
                for (int i = 0; i < 20; i++)
                {
                    var Bullet = PoolingManager.instance.GetGo("Bullet");
                    Bullet.transform.position = transform.position;
                    Bullet.transform.rotation = transform.rotation * Quaternion.Euler(0, 22.5f * i, 0);
                    Bullet.GetComponent<BulletController>().range = herodata.range;
                    Bullet.GetComponent<BulletController>().Player = gameObject;
                }

                herodata.second_skillcurTime = herodata.second_skillmaxTime;
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

                SoundManager.Instance.SoundPlay("Ch7_Skill1", Skill1Audio);
                SecondSkillObj.SetActive(true);
                Invoke("SkillOff", 1);
                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    void SkillOff() => SecondSkillObj.SetActive(false);
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
