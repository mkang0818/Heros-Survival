using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch3Stat : HeroStat
{
    public AudioClip Skill1Audio;
    public AudioClip Skill2Audio;
    public AudioClip Skill2OffAudio;
    Animator anim;

    public GameObject FirstSkillObj;
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
        //고유스킬 경험치 몇배

        //둥근 방벽
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(FirstSkill());

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

                SoundManager.Instance.SoundPlay("Ch3_Skill1On", Skill1Audio);
                SecondSkillObj.SetActive(true);
                Invoke("SkillOff", 1);
                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    IEnumerator FirstSkill()
    {
        GameObject SkillObj = Instantiate(FirstSkillObj, transform.position, Quaternion.identity);
        SkillObj.GetComponent<DropController>().target = gameObject;
        Destroy(SkillObj, 5);
        SoundManager.Instance.SoundPlay("Ch3_Skill2", Skill2Audio);
        SkillObj.transform.DOScale(new Vector3(3, 3, 3), 1);
        yield return new WaitForSeconds(4);
        SoundManager.Instance.SoundPlay("Ch3_Skill1Off", Skill2OffAudio);
        SkillObj.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1);
    }
    void SkillOff() => SecondSkillObj.SetActive(false);
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
