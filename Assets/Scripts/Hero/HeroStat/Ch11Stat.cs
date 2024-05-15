using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch11Stat : HeroStat
{
    public AudioClip[] Skill1Audio;
    public AudioClip Skill2Audio;

    Animator anim;

    public GameObject Skill1Obj;
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
        // 아군부대
        if (herodata.second_skillcurTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(SecondSkill());
                herodata.second_skillcurTime = herodata.second_skillmaxTime;
            }
        }
        //1자 폭탄
        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameObject LineShot = Instantiate(Skill1Obj, transform.position + transform.forward * 2, Quaternion.Euler(0, 90, 0));
                StartCoroutine(SkillSound());
                Destroy(LineShot, 1.8f);

                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    IEnumerator SecondSkill()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.SoundPlay("Ch11_Skill2", Skill2Audio);
        for (int i = 0; i < 10; i++)
        {
            float xPos = Random.Range(-2, 2);
            float zPos = Random.Range(-2, 2);
            Vector3 SpawnPos = new Vector3(transform.position.x + xPos, 0, transform.position.z + zPos);

            GameObject MiniObj = Instantiate(SecondSkillObj, SpawnPos, Quaternion.identity);
            MiniObj.GetComponent<Ch11SkillController>().player = transform;
            MiniObj.GetComponent<Ch11SkillController>().damage = herodata.damage * 2;
        }
    }
    IEnumerator SkillSound()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < 9; i++)
        {
            int index = Random.Range(0,3);
            print(index);
            SoundManager.Instance.SoundPlay("Ch11_Skill1", Skill1Audio[index]);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
