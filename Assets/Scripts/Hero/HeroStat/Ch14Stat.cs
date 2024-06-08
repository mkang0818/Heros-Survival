using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch14Stat : HeroStat
{
    public AudioClip Skill1Audio;
    public AudioClip Skill2Audio;
    Animator anim;

    public GameObject SkillObj;
    public GameObject HeelZone;

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
        //고유스킬 엄청난 기본 스탯

        //거인
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                SkillObj.SetActive(true);

                SkillOn();
                SoundManager.Instance.SoundPlay("Ch14_Skill1", Skill1Audio);
                Invoke("SkillOff", 10);

                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
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
    }
    void SkillOn()
    {
        gameObject.transform.DOScale(2, 1);
        herodata.damage *= 2;
        herodata.maxbulletCount *= 2;
    }
    void SkillOff()
    {
        herodata.damage /= 2;
        herodata.maxbulletCount /= 2;
        gameObject.transform.DOScale(1, 1);
        SkillObj.SetActive(false);
    }
    void Skill2Sound() => SoundManager.Instance.SoundPlay("Ch14Skill2", Skill2Audio);
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
