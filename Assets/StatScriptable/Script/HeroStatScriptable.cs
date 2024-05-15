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


    [Header("����")]
    [SerializeField]
    private int level; //����
    public int Level { get { return level; } set { level = value; } }

    [SerializeField]
    private float MaxExp; //�ְ� ����ġ
    public float maxExp { get { return MaxExp; } set { MaxExp = value; } }

    [SerializeField]
    private float CurExp; //���� ����ġ
    public float curExp { get { return CurExp; } set { CurExp = value; } }


    [Header("ü��")]
    [SerializeField]
    private float MaxHp; //�ִ� ü��
    public float maxHp { get { return MaxHp; } set { MaxHp = value; } }

    [SerializeField]
    public float CurHp; //����ü��
    public float curHp { get { return CurHp; } set { CurHp = value; } }

    [SerializeField]
    private float HpRecovery; //ȸ����
    public float hpRecovery { get { return HpRecovery; } set { HpRecovery = value; } }

    [SerializeField]
    private float Absorption; //ü�����
    public float absorption { get { return Absorption; } set { Absorption = value; } }

    [Header("�Ѿ�")]
    [SerializeField]
    private int MaxbulletCount; // �ִ� źâ ��
    public int maxbulletCount { get { return MaxbulletCount; } set { MaxbulletCount = value; } }

    [SerializeField]
    private int CurbulletCount; //����źâ ��
    public int curbulletCount { get { return CurbulletCount; } set { CurbulletCount = value; } }

    [SerializeField]
    private float ReloadTime; //������ �ӵ�
    public float reloadTime { get { return ReloadTime; } set { ReloadTime = value; } }

    [SerializeField]
    private float ReloadCoolTime; //������ ��Ÿ��
    public float reloadCoolTime { get { return ReloadCoolTime; } set { ReloadCoolTime = value; } }

    [SerializeField]
    private int BulletcurHP; //�Ѿ� ����
    public int bulletcurHP { get { return BulletcurHP; } set { BulletcurHP = value; } }

    [Header("����")]
    [SerializeField]
    private float AttackSp; //���ݼӵ�
    public float attackSp { get { return AttackSp; } set { AttackSp = value; } }

    [SerializeField]
    private float AttackcoolTime; //���ݼӵ� ��Ÿ��
    public float attackcoolTime { get { return AttackcoolTime; } set { AttackcoolTime = value; } }

    [SerializeField]
    private float MoveSp; //�̵��ӵ�
    public float moveSp { get { return MoveSp; } set { MoveSp = value; } }

    [SerializeField]
    private float Damage; //���ݷ�
    public float damage { get { return Damage; } set { Damage = value; } }

    [SerializeField]
    private float SkillDamage; //��ų������
    public float skillDamage { get { return SkillDamage; } set { SkillDamage = value; } }

    [SerializeField]
    private float BombDamage; // ���ߵ�����
    public float bombDamage { get { return BombDamage; } set { BombDamage = value; } }

    [SerializeField]
    private float Science; //���ȭ
    public float science { get { return Science; } set { Science = value; } }

    [Header("���δɷ�")]
    [SerializeField]
    private float Critical; //ġ��Ÿ
    public float critical { get { return Critical; } set { Critical = value; } }

    [SerializeField]
    private float Accuracy; //���߷�
    public float accuracy { get { return Accuracy; } set { Accuracy = value; } }

    [SerializeField]
    private float Range; //������ ȹ�� ����
    public float range { get { return Range; } set { Range = value; } }

    [SerializeField]
    private float Defense; //����
    public float defense { get { return Defense; } set { Defense = value; } }

    [SerializeField]
    private float Harvesting; //��Ȯ
    public float harvesting { get { return Harvesting; } set { Harvesting = value; } }

    [SerializeField]
    private float Evasion; //ȸ����
    public float evasion { get { return Evasion; } set { Evasion = value; } }

    [SerializeField]
    private float HasExp; //����ġȹ��
    public float hasExp { get { return HasExp; } set { HasExp = value; } }

    [SerializeField]
    private float DiamondPer; //���� ȹ���
    public float diamondPer { get { return DiamondPer; } set { DiamondPer = value; } }


    [Header("��ų")]
    [SerializeField]
    private float skillMaxTime; //��ų ��Ÿ��
    public float skillmaxTime { get { return skillMaxTime; } set { skillMaxTime = value; } }

    [SerializeField]
    private float skillCurTime; //��ų ���� ��Ÿ��
    public float skillcurTime { get { return skillCurTime; } set { skillCurTime = value; } }

    [SerializeField]
    private float Second_skillMaxTime; //�ι�° ��ų ��Ÿ��
    public float second_skillmaxTime { get { return Second_skillMaxTime; } set { Second_skillMaxTime = value; } }

    [SerializeField]
    private float Second_skillCurTime; //�ι�° ��ų ���� ��Ÿ��
    public float second_skillcurTime { get { return Second_skillCurTime; } set { Second_skillCurTime = value; } }

    
    [Header("��������")]
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
