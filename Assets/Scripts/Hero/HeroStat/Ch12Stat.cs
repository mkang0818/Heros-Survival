using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch12Stat : HeroStat
{
    public AudioClip Skill1Audio;
    public AudioClip Skill1_1Audio;
    public AudioClip Skill2Audio;

    Animator anim;
    public GameObject Portal;
    GameObject PortalObj;
    public GameObject spawnHero;
    public GameObject SecondSkillObj;

    public GameObject FollowPos;
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
        //SpawnHero - H12 아군 호출
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PortalObj = Instantiate(Portal, new Vector3(0, 1.2f, 4), Quaternion.identity);
                SoundManager.Instance.SoundPlay("Ch12_Skill2", Skill2Audio);
                
                Invoke("spawnSkill", 2f);
                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }
        }
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //폭격
                GameObject skillObj = Instantiate(SecondSkillObj,transform.position,Quaternion.identity);
                SoundManager.Instance.SoundPlay("Ch12_Skill1_1", Skill1_1Audio);
                StartCoroutine(Skill1Sound());
                Destroy(skillObj,3f);
                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    IEnumerator Skill1Sound()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < 7; i++)
        {
            SoundManager.Instance.SoundPlay("Ch13_Skill1", Skill1Audio);
            yield return new WaitForSeconds(0.2f);
        }
        
    }
    void spawnSkill()
    {
        GameObject spawnHeroObj = Instantiate(spawnHero, new Vector3(0, 0, 4), Quaternion.identity);
        spawnHeroObj.GetComponent<CreateRobot>().target = FollowPos;
        spawnHeroObj.GetComponent<CreateRobot>().range = herodata.range;
        PortalObj.SetActive(false);
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
