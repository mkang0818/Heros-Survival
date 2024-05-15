using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch6Stat : HeroStat
{
    public AudioClip Skill1Audio;
    Animator anim;
    public GameObject SpinBullet;
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
        //고유스킬 총알과 데미지 강함

        //스핀총알
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(InstSpinBullet());
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

                SoundManager.Instance.SoundPlay("Ch2_Skill1", Skill1Audio);
                SecondSkillObj.SetActive(true);
                Invoke("SkillOff", 1);
                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    IEnumerator InstSpinBullet()
    {
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.25f);
            GameObject spinBullet1 = Instantiate(SpinBullet, transform.position, Quaternion.identity);
            spinBullet1.GetComponent<CH6SpinBulletController>().player = gameObject.transform;
        }
        yield return null;
    }
    void SkillOff() => SecondSkillObj.SetActive(false);
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
