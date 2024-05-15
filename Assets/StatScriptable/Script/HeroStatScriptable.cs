using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroStat",menuName = "ScriptableObject/HeroStat", order =int.MaxValue)]
public class HeroStatScriptable : ScriptableObject
{
    [SerializeField]
    public int CharCode;

    [SerializeField]
    public int CharUpgrade;
    public int charUpgrade { get { return CharUpgrade; } set { CharUpgrade = value; } }


    [Header("레벨")]
    [SerializeField]
    private int level; //레벨
    public int Level { get { return level; } set { level = value; } }

    [SerializeField]
    private float MaxExp; //최고 경험치
    public float maxExp { get { return MaxExp; } set { MaxExp = value; } }

    [SerializeField]
    private float CurExp; //현재 경험치
    public float curExp { get { return CurExp; } set { CurExp = value; } }


    [Header("체력")]
    [SerializeField]
    private float MaxHp; //최대 체력
    public float maxHp { get { return MaxHp; } set { MaxHp = value; } }

    [SerializeField]
    public float CurHp; //현재체력
    public float curHp { get { return CurHp; } set { CurHp = value; } }

    [SerializeField]
    private float HpRecovery; //회복력
    public float hpRecovery { get { return HpRecovery; } set { HpRecovery = value; } }

    [SerializeField]
    private float Absorption; //체력흡수
    public float absorption { get { return Absorption; } set { Absorption = value; } }

    [Header("총알")]
    [SerializeField]
    private int MaxbulletCount; // 최대 탄창 수
    public int maxbulletCount { get { return MaxbulletCount; } set { MaxbulletCount = value; } }

    [SerializeField]
    private int CurbulletCount; //현재탄창 수
    public int curbulletCount { get { return CurbulletCount; } set { CurbulletCount = value; } }

    [SerializeField]
    private float ReloadTime; //재장전 속도
    public float reloadTime { get { return ReloadTime; } set { ReloadTime = value; } }

    [SerializeField]
    private float ReloadCoolTime; //재장전 쿨타임
    public float reloadCoolTime { get { return ReloadCoolTime; } set { ReloadCoolTime = value; } }

    [SerializeField]
    private int BulletcurHP; //총알 관통
    public int bulletcurHP { get { return BulletcurHP; } set { BulletcurHP = value; } }

    [Header("공격")]
    [SerializeField]
    private float AttackSp; //공격속도
    public float attackSp { get { return AttackSp; } set { AttackSp = value; } }

    [SerializeField]
    private float AttackcoolTime; //공격속도 쿨타임
    public float attackcoolTime { get { return AttackcoolTime; } set { AttackcoolTime = value; } }

    [SerializeField]
    private float MoveSp; //이동속도
    public float moveSp { get { return MoveSp; } set { MoveSp = value; } }

    [SerializeField]
    private float Damage; //공격력
    public float damage { get { return Damage; } set { Damage = value; } }

    [SerializeField]
    private float SkillDamage; //스킬데미지
    public float skillDamage { get { return SkillDamage; } set { SkillDamage = value; } }

    [SerializeField]
    private float BombDamage; // 폭발데미지
    public float bombDamage { get { return BombDamage; } set { BombDamage = value; } }

    [SerializeField]
    private float Science; //기계화
    public float science { get { return Science; } set { Science = value; } }

    [Header("세부능력")]
    [SerializeField]
    private float Critical; //치명타
    public float critical { get { return Critical; } set { Critical = value; } }

    [SerializeField]
    private float Accuracy; //명중률
    public float accuracy { get { return Accuracy; } set { Accuracy = value; } }

    [SerializeField]
    private float Range; //아이템 획득 범위
    public float range { get { return Range; } set { Range = value; } }

    [SerializeField]
    private float Defense; //방어력
    public float defense { get { return Defense; } set { Defense = value; } }

    [SerializeField]
    private float Harvesting; //수확
    public float harvesting { get { return Harvesting; } set { Harvesting = value; } }

    [SerializeField]
    private float Evasion; //회피율
    public float evasion { get { return Evasion; } set { Evasion = value; } }

    [SerializeField]
    private float HasExp; //경험치획득
    public float hasExp { get { return HasExp; } set { HasExp = value; } }

    [SerializeField]
    private float DiamondPer; //보석 획득률
    public float diamondPer { get { return DiamondPer; } set { DiamondPer = value; } }


    [Header("스킬")]
    [SerializeField]
    private float skillMaxTime; //스킬 쿨타임
    public float skillmaxTime { get { return skillMaxTime; } set { skillMaxTime = value; } }

    [SerializeField]
    private float skillCurTime; //스킬 현재 쿨타임
    public float skillcurTime { get { return skillCurTime; } set { skillCurTime = value; } }

    [SerializeField]
    private float Second_skillMaxTime; //두번째 스킬 쿨타임
    public float second_skillmaxTime { get { return Second_skillMaxTime; } set { Second_skillMaxTime = value; } }

    [SerializeField]
    private float Second_skillCurTime; //두번째 스킬 현재 쿨타임
    public float second_skillcurTime { get { return Second_skillCurTime; } set { Second_skillCurTime = value; } }

    
    [Header("근접공격")]
    [SerializeField]
    private float MeleeAt;
    public float meleeAt { get { return MeleeAt; } set { MeleeAt = value; } }

    [SerializeField]
    private float MeleeAtMaxTime;
    public float meleeAtMaxTime { get { return MeleeAtMaxTime; } set { MeleeAtMaxTime = value; } }

    [SerializeField]
    private float MeleeAtCurTime;
    public float meleeAtcurTime { get { return MeleeAtCurTime; } set { MeleeAtCurTime = value; } }
}
