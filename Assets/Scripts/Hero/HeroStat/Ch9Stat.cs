using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch9Stat : HeroStat
{
    public AudioClip Skill2_Sound;
    Animator anim;

    public GameObject HeelTurret;
    public GameObject ThunderSkill;
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
        if (herodata.second_skillcurTime <= 0)
        {
            //SoundManager.Instance.SoundPlay("Ch9Skill2", Skill2_Sound);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Vector3 SpawnPos = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
                Instantiate(HeelTurret, SpawnPos,Quaternion.identity);

                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }
        }
        //맵 사방에서 레이저 구현
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine("Skill1");

                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    IEnumerator Skill1()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
            Instantiate(ThunderSkill, SpawnPos, Quaternion.Euler(90,0,0));
            yield return new WaitForSeconds(0.2f);
        }
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }

}
