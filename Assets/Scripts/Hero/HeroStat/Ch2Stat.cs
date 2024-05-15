using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ch2Stat : HeroStat
{
    public AudioClip Skill1Audio;
    Animator anim;

    [SerializeField] GameObject MoveSkillObj;
    public GameObject SecondSkillObj;

    [HideInInspector] public bool IsRun;
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
        //고유스킬 재화 몇배로


        //달리기 - H13
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            herodata.moveSp *= 1.2f;

            MoveSkillObj.SetActive(true);
            StartCoroutine(RunEffect());
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            herodata.moveSp /= 1.2f;
            MoveSkillObj.SetActive(false);
            StopCoroutine(RunEffect());
        }

        if (herodata.skillcurTime <= 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (herodata.CurHp + (herodata.maxHp * 0.7f) < herodata.maxHp)
                {
                    herodata.CurHp += herodata.maxHp * 0.7f;
                }
                else herodata.CurHp = herodata.maxHp;

                SecondSkillObj.SetActive(true);
                SoundManager.Instance.SoundPlay("Ch2_Skill1", Skill1Audio);
                Invoke("SkillOff", 1);
                herodata.skillcurTime = herodata.skillmaxTime;
            }
        }
    }
    public IEnumerator RunEffect()
    {
        while (true)
        {
            Vector3 Pos = transform.position;
            yield return new WaitForSeconds(0.2f);
            GameObject RunEffectObj = Instantiate(MoveSkillObj, Pos, Quaternion.identity);
            Destroy(RunEffectObj,1f);
            MoveSkillObj.GetComponent<ParticleSystem>().Play();
        }
    }
    void SkillOff() => SecondSkillObj.SetActive(false);
    public override void Move(GameObject player, Animator anim)
    {
        base.Move(player, anim);
    }
}
