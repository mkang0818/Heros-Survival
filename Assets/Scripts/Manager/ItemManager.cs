using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public InGameManager inGameManager;
    Data data;
    //////heel turret//////
    public GameObject HeelTurretPrefab;
    static bool isHeelTurret = false;
    static int HeelTurretCount = 0;

    //////basic turret//////
    public GameObject DefaultTurretPrefab;
    static bool isDefaultTurret = false;
    static int DefaultTurretCount = 0;

    //////bomb turret/////
    public GameObject BombTurretPrefab;
    static bool isBombTurret = false;
    static int BombTurretCount = 0;

    //////slow turret/////
    public GameObject SlowTurretPrefab;
    static bool isSlowTurret = false;
    static int SlowTurretCount = 0;


    static bool isLandMine;
    static bool isLvUp;
    static bool IsrandomBox;

    static bool IsSpinBullet;
    public GameObject SpinBullet;

    static bool IsSale = false;
    static bool StoreTicket = false;
    static bool IsCurHpItem = false;
    static bool IsReborn = false;
    private void Start()
    {
        ItemInit();
    }
    void ItemInit()
    {
        isHeelTurret = false;
        HeelTurretCount = 0;


        isDefaultTurret = false;
        DefaultTurretCount = 0;


        isBombTurret = false;
        BombTurretCount = 0;


        isSlowTurret = false;
        SlowTurretCount = 0;


        isLandMine = false;
        isLvUp = false;
        IsrandomBox = false;
        IsSpinBullet = false;
        IsSale = false;
        StoreTicket = false;
        IsCurHpItem = false;
        IsReborn = false;
    }
    public enum ItemType { StatItem, InstItem, SystemItem }
    public enum StatType
    {
        MaxHp, CurHp, HpRecovery, MaxbulletCount, ReloadTime, AttackSp, MoveSp, Damage, SkillDamage, BombDamage, Accuracy, Critical, Absorption,
        Defense, Evasion, HasExp, Science, Range, DiamondPer, skillCoolTime, BulletcurHP,  MapScale, RandomBoxMaxCoolTime
                            , BasicTurret, HeelTurret, BombTurret, SlowTurret, EmyBomb, MultiBullet, SpinBullet, LandMine  // InstItem
                          , LvUp, FirstRerollFree, RerollSale, ItemSale, RandomBoxHPItem, RandomBoxCoinPerItem, TenPerHasMoney, randomItem, Reborn  //System Item
    }

    public class StatData
    {
        public StatType statType; //���� ���� ����
        public ItemType itemType; //���� ���� ����
        public float statValue; //���� ���� ��
        public int PlusMinus; //���� ���� ��

        public StatData(ItemType itemType, StatType statType, float statValue, int PlusMinus)
        {
            this.itemType = itemType;
            this.statType = statType;
            this.statValue = statValue;
            this.PlusMinus = PlusMinus;
        }
    }
    public class Item
    {
        public int index; //������ ��ȣ
        public StatData[] statDatas; //������ ���� �� ������ ���� ����

        public Item(params StatData[] statDatas)
        {
            this.statDatas = statDatas;
        }
    }
    readonly Item[] items = new Item[]
    {
        // Item 1
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 3, 1),
            new StatData(ItemType.StatItem, StatType.BombDamage, -1, -1)
        }),
        // Item 2
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.HpRecovery, 5, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -1, -1)
        }),
        // Item 3
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 6, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 1.03f, -1)
        }),
        // Item 4
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.ReloadTime, 0.94f,1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 1.02f, -1)
        }),
        // Item 5
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.93f,1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.97f, -1)
        }),
        // Item 6
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 3,1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 1.1f, -1)
        }),
        // Item 7
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MoveSp, 1.05f, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 1.03f, -1)
        }),
        // Item 8
        new Item(new StatData[]{ new StatData(ItemType.StatItem, StatType.AttackSp, 0.94f, 1) }),
        
        // Item 9
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.SkillDamage, 3, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 1.05f, -1)
        }),
        // Item 10
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.93f, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 0.97f, -1)
        }),
        // Item 11
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Defense, 4, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -1, -1)
        }),
        // Item 12
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Critical, 1.08f, 1),
            new StatData(ItemType.StatItem, StatType.Range, 0.93f, -1)
        }),
        // Item 13
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Evasion, 1.08f, 1),
            new StatData(ItemType.StatItem, StatType.Defense, -1, -1)
        }),
        // Item 14
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Science, 3, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 1.03f, -1)
        }),
        // Item 15
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Range, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 1.03f, -1)
        }),
        // Item 16
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.DiamondPer, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, 1, 1),
            new StatData(ItemType.StatItem, StatType.Range, 0.91f, -1)
        }),
        // Item 17
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.BombDamage, 3, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 1.04f, -1)
        }),
        // Item 18
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 0.92f, 1),
            new StatData(ItemType.StatItem, StatType.Science, -1, -1)
        }),
        
        // Item 19
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 1, 1),
            new StatData(ItemType.StatItem, StatType.Defense, 2, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.98f, -1)
        }),
        
        // Item 20
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Science, 1, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.Science, -1, -1)
        }),
        
        // Item 21
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.SkillDamage, 2, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 1.02f, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 1.02f, -1)
        }),
        
        // Item 22
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Defense, 2, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, 1, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, -2, -1)
        }),
        
        // Item 23
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.96f, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 1.03f, 1),
            new StatData(ItemType.StatItem, StatType.Range, 0.95f, -1)
        }),
        
        // Item 24
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.BombDamage, 2, 1),
            new StatData(ItemType.StatItem, StatType.Range, 1.07f, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 0.95f, -1)
        }),
        
        // Item 25
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Critical, 1.04f, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.97f, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 1.05f, -1)
        }),
        
        
        // Item 26
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Absorption, 1.03f, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, 2, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -1, -1)
        }),

        // Item 27
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Absorption, 1.08f, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, -3, -1)
        }),
        
        // Item 28
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Absorption, 1.04f, 1),
        }),
        // Item 29
        new Item(new StatData[]{
            new StatData(ItemType.InstItem, StatType.BasicTurret, 1, 1),
        }),
        // Item 30
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.TenPerHasMoney, 1, 1) }),
        
        // Item 31
        new Item(new StatData[]{
             new StatData(ItemType.StatItem, StatType.CurHp, 1, 1),
             new StatData(ItemType.StatItem, StatType.MaxHp, 1.3f, 1)
        }),
        
        // Item 32
        new Item(new StatData[]{ new StatData(ItemType.StatItem, StatType.MapScale, 2, 1) }),
        
        // Item 33
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.RandomBoxCoinPerItem, 1, 1) }),
        
        // Item 34
        new Item(new StatData[]{ new StatData(ItemType.StatItem, StatType.RandomBoxMaxCoolTime, 1, 1) }),
        // Item 35
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 3, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.97f, -1)
        }),
        // Item 36
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.9f, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 6, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 0.97f, -1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -1, -1)
        }),
        // Item 37
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 4, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, 6, 1),
            new StatData(ItemType.StatItem, StatType.Absorption, 1.05f, 1),
            new StatData(ItemType.StatItem, StatType.Evasion, 0.9f, -1)
        }),
        // Item 38
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MoveSp, 1.05f, 1),
            new StatData(ItemType.StatItem, StatType.HasExp, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.Range, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.Damage, -2, -1)
        }),
        // Item 39
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 6, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 0.92f, 1),
            new StatData(ItemType.StatItem, StatType.Science, 4, 1),
            new StatData(ItemType.StatItem, StatType.Defense, -2, -1)
        }),
        // Item 40
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 5, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 6, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, -2, -1)
        }),
        // Item 41
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.9f, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 1.07f, 1),
            new StatData(ItemType.StatItem, StatType.Damage, 2, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 1.02f, -1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 1.05f, -1)
        }),
        // Item 42
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.9f, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.Absorption, 0.95f, -1)
        }),
        // Item 43
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Evasion, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.Damage, 4, 1),
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, -3, -1)
        }),
        // Item 44
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 4, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, 4, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.95f, -1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 1.07f, -1)
        }),
        
        // Item 45
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Science, 7, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, 5, 1),
            new StatData(ItemType.StatItem, StatType.BombDamage, 5, 1),
            new StatData(ItemType.StatItem, StatType.Evasion, 0.97f, -1)
        }),
        // Item 46
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.BombDamage, 5, 1),
            new StatData(ItemType.StatItem, StatType.Range, 1.2f, 1),
            new StatData(ItemType.StatItem, StatType.Damage, -2, -1)
        }),
        
        // Item 47
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.DiamondPer, 1.15f, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.97f, 1),
            new StatData(ItemType.StatItem, StatType.Range, 0.8f, -1)
        }),
        // Item 48
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.HasExp, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.93f, 1),
            new StatData(ItemType.StatItem, StatType.Range, 0.95f, -1)
        }),
        // Item 49
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.SkillDamage, 7, 1),
            new StatData(ItemType.StatItem, StatType.Damage, 4, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 0.97f, -1)
        }),
        // Item 50
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 3, 1),
            new StatData(ItemType.StatItem, StatType.Damage, 5, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 1.07f, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -1, -1)
        }),
        // Item 51
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Defense, 5, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, 5, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, -2, -1)
        }),
        // Item 52
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Absorption, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.Damage, 3, 1),
            new StatData(ItemType.StatItem, StatType.Defense, -2, -1)
        }),
        // Item 53
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 0.93f, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, 2, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, -3, -1)
        }),
        // Item 54
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 2, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 0.97f, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 1.03f, -1)
        }),
        // Item 55
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Absorption, 1.07f, 1),
            new StatData(ItemType.StatItem, StatType.Evasion, 1.08f, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -2, -1)
        }),
        // Item 56
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 3, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 1.03f, 1),
            new StatData(ItemType.StatItem, StatType.Science, 3, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 1.07f, -1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.96f, -1)
        }),
        // Item 57
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.SkillDamage, 3, 1),
            new StatData(ItemType.StatItem, StatType.Defense, 3, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 0.96f, -1)
        }),
        // Item 58
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 3, 1),
            new StatData(ItemType.StatItem, StatType.Science, 3, 1),
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 6, 1),
            new StatData(ItemType.StatItem, StatType.Defense, -5, -1)
        }),
        // Item 59
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Absorption, 1.07f, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, 3, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, 7, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 1.03f, -1)
        }),
        // Item 60
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.EmyBomb, 1, 1) }),
        
        // Item 61
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.HeelTurret, 1, 1) }),
        
        // Item 62
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.SlowTurret, 1, 1) }),
        
        // Item 63
        new Item(new StatData[]{ new StatData(ItemType.StatItem, StatType.BulletcurHP, 1, 1) }),        
        
        // Item 64
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.MultiBullet, 1, 1) }), 
        
        // Item 65
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.FirstRerollFree, 1, 1) }),
        
        // Item 66
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.RerollSale, 1, 1) }),
        
        // Item 67
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.ItemSale, 1, 1) }),

        // Item 68
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.randomItem, 1, 1) }),

        // Item 69
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 10, 1),
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 12, 1),
            new StatData(ItemType.StatItem, StatType.Range, 1.15f, 1),
            new StatData(ItemType.StatItem, StatType.Damage, -3, -1)
        }),

        // Item 70
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 15, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.9f, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 1.05f, -1)
        }),

        // Item 71
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.9f, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, 7, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 0.85f, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -2, -1)
        }),

        // Item 72
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Defense, 8, 1),
            new StatData(ItemType.StatItem, StatType.HasExp, 1.15f, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.95f, -1)
        }),

        // Item 73
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 12, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 0.85f, 1),
            new StatData(ItemType.StatItem, StatType.Science, 7, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 1.07f, -1)
        }),

        // Item 74
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 7, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 1.15f, 1),
            new StatData(ItemType.StatItem, StatType.Evasion, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.95f, -1)
        }),
        // Item 75
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MoveSp, 1.07f, 1),
            new StatData(ItemType.StatItem, StatType.Damage, 4, 1),
            new StatData(ItemType.StatItem, StatType.Evasion, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.Absorption, 0.92f, -1)
        }),
        // Item 76
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.DiamondPer, 1.2f, 1),
            new StatData(ItemType.StatItem, StatType.Range, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.HasExp, 1.2f, 1),
            new StatData(ItemType.StatItem, StatType.MaxHp, -2, -1)
        }),
        // Item 77
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.SkillDamage, 7, 1),
            new StatData(ItemType.StatItem, StatType.Science, 6, 1),
            new StatData(ItemType.StatItem, StatType.BombDamage, 5, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 1.07f, -1)
        }),
        
        // Item 78
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 7, 1),
            new StatData(ItemType.StatItem, StatType.Defense, 8, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, 10, 1),
            new StatData(ItemType.StatItem, StatType.Absorption, 1.08f, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, -2, -1)
        }),
        
        // Item 79
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 12, 1),
            new StatData(ItemType.StatItem, StatType.Damage, 3, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.97f, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 0.97f, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 0.97f, 1),
            new StatData(ItemType.StatItem, StatType.Defense, 8, 1),
        }),
        
        // Item 80
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.MaxHp, 10, 1),
            new StatData(ItemType.StatItem, StatType.Absorption, 1.13f, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.92f, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, -5, -1)
        }),
        // Item 81
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 5, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.9f, 1),
            new StatData(ItemType.StatItem, StatType.ReloadTime, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.Defense, -3, -1)
        }),
        // Item 82
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Critical, 1.1f, 1),
            new StatData(ItemType.StatItem, StatType.AttackSp, 0.95f, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.93f, 1),
            new StatData(ItemType.StatItem, StatType.MoveSp, 0.93f, -1)
        }),
        // Item 83
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Damage, 8, 1),
            new StatData(ItemType.StatItem, StatType.Absorption, 1.12f, 1),
            new StatData(ItemType.StatItem, StatType.Critical, 0.95f, -1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 1.07f, -1)
        }),
        // Item 84
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.BombDamage, 6, 1),
            new StatData(ItemType.StatItem, StatType.Accuracy, 0.9f, 1),
            new StatData(ItemType.StatItem, StatType.MaxbulletCount, 12, 1),
            new StatData(ItemType.StatItem, StatType.skillCoolTime, 1.1f, -1)
        }),
        // Item 85
        new Item(new StatData[]{
            new StatData(ItemType.StatItem, StatType.Science, 7, 1),
            new StatData(ItemType.StatItem, StatType.HpRecovery, 10, 1),
            new StatData(ItemType.StatItem, StatType.SkillDamage, 8, 1),
            new StatData(ItemType.StatItem, StatType.Defense, -3, -1),
            new StatData(ItemType.StatItem, StatType.Evasion, 0.96f, -1)
        }),
        // Item 86
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.Reborn, 1, 1) }),
        // Item 87
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.BombTurret, 1, 1) }),
        
        // Item 88
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.SpinBullet, 1, 1) }),
        
        // Item 89
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.LvUp, 1, 1) }),          
        
        // Item 90
        new Item(new StatData[]{ new StatData(ItemType.InstItem, StatType.LandMine, 1, 1) }), 
        
        // Item 91
        new Item(new StatData[]{ new StatData(ItemType.SystemItem, StatType.RandomBoxHPItem, 1, 1) })
    };
    public Item GetItem(int index)
    {
        return items[index];
    }
    GameObject[] newInfo = new GameObject[30];
    void ItemText(GameObject newInfo, float Value, string text, int colorNum)
    {
        GameObject InfoTextObj = newInfo.transform.GetChild(0).gameObject;
        GameObject StatValueTextObj = newInfo.transform.GetChild(1).gameObject;

        float percentage = 0;

        if (Value < 1) //0.97일 때
        {
            percentage = 100 - (100 * Value);
            if (colorNum > 0)
            {
                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=#00FF00>" + "+" + (int)percentage + "% " + "</color>";
            }
            else if (colorNum < 0) StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>" + -(int)percentage + "% " + "</color>";
        }
        else if (Value > 1)  //1.1일 때
        {
            percentage = (100 * Value) - 100;
            if (colorNum > 0)
            {
                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=#00FF00>" + "+" + (int)percentage + "% " + "</color>";
            }
            else if (colorNum < 0) StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>" + -(int)percentage + "% " + "</color>";
        }

        InfoTextObj.GetComponent<LocalizeScript>().textKey = text;
    }
    public void AddItemInfo(PlayerController Player, Item item, GameObject ItemInfoPrefab, Transform ItemInfoContentTransform)
    {
        string ItemInfo;
        for (int i = 0; i < item.statDatas.Length; i++)
        {
            newInfo[i] = Instantiate(ItemInfoPrefab, ItemInfoContentTransform);
            GameObject InfoTextObj = newInfo[i].transform.GetChild(0).gameObject;
            GameObject StatValueTextObj = newInfo[i].transform.GetChild(1).gameObject;

            ItemInfoContentTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(ItemInfoContentTransform.GetComponent<RectTransform>().sizeDelta.x, ItemInfoContentTransform.childCount * 100f); // 이미지의 높이를 100이라고 가정

            switch (item.statDatas[i].itemType)
            {
                case ItemType.StatItem:
                    switch (item.statDatas[i].statType)
                    {
                        // MaxHp, CurHp, HpRecovery, MaxbulletCount, ReloadTime, AttackSp, MoveSp, Damage, SkillDamage, BombDamage, Accuracy, Critical, 
                        // Defense, Harvesting, Evasion, HasExp, Science, Range, DiamondPer, skillCoolTime, MeleeAt, BulletcurHP, Timer, RandomBoxMaxCoolTime
                        case StatType.MaxHp:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "MaxHP";
                            }
                            else
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "MaxHP";
                            }
                            break;
                        case StatType.CurHp:
                            StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=red>" + "1" + "</color>";
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "CurHP";
                            break;
                        case StatType.HpRecovery:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "HpRecovery";
                            }
                            else
                            {
                                StatValueTextObj.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "HpRecovery";
                            }
                            break;
                        case StatType.MaxbulletCount:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Bullet";
                            }
                            else
                            {
                                StatValueTextObj.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Bullet";
                            }
                            break;
                        case StatType.ReloadTime:
                            ItemInfo = "Reload";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.AttackSp:
                            ItemInfo = "AttackSpeed";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.MoveSp:
                            ItemInfo = "Move";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.Damage:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.gameObject.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Damage";
                            }
                            else
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Damage";
                            }
                            break;
                        case StatType.SkillDamage:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Skill";
                            }
                            else
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Skill";
                            }
                            break;
                        case StatType.BombDamage:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Bomb";
                            }
                            else
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Bomb";
                            }
                            break;
                        case StatType.Accuracy:
                            ItemInfo = "Accuracy";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.Critical:
                            ItemInfo = "Critical";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.Defense:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Defense";
                            }
                            else
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Defense";
                            }
                            break;
                        case StatType.Evasion:
                            ItemInfo = "Evasion";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.HasExp:
                            ItemInfo = "GetExp";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.Science:
                            if (item.statDatas[i].PlusMinus > 0)
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=green>" + "+" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Science";
                            }
                            else
                            {
                                StatValueTextObj.GetComponent<TextMeshProUGUI>().text = "<color=red>" + (int)(item.statDatas[i].statValue) + "</color>";
                                InfoTextObj.GetComponent<LocalizeScript>().textKey = "Science";
                            }
                            break;
                        case StatType.Range:
                            ItemInfo = "Range";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.DiamondPer:
                            ItemInfo = "Gem Drop Rate";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.skillCoolTime:
                            ItemInfo = "SkillCoolTime";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.Absorption:
                            ItemInfo = "Absorption";
                            ItemText(newInfo[i], item.statDatas[i].statValue, ItemInfo, item.statDatas[i].PlusMinus);
                            break;
                        case StatType.BulletcurHP:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Bullet Penetration +1";
                            break;
                        case StatType.MapScale:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "+2 wider field of view";
                            break;
                        case StatType.RandomBoxMaxCoolTime:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Reduced box creation time";
                            break;
                    }
                    break;
                case ItemType.InstItem:
                    switch (item.statDatas[i].statType)
                    {
                        // BasicTurret, HeelTurret, BombTurret, SlowTurret, EmyBomb, RandomBullet, SpinBullet, LandMine
                        case StatType.BasicTurret:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Normal turret";
                            break;
                        case StatType.HeelTurret:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "treatment turret";
                            break;
                        case StatType.BombTurret:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "bomb turret";
                            break;
                        case StatType.SlowTurret:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Slow turret";
                            break;
                        case StatType.EmyBomb:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "50% Explodes When Monster Dies";
                            break;
                        case StatType.MultiBullet:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Bullets everywhere";
                            break;
                        case StatType.SpinBullet:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "rotating bullet";
                            break;
                        case StatType.LandMine:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Random mine placement";
                            break;
                        case StatType.Reborn:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "When you die, you are reborn.";
                            break;
                    }
                    break;
                case ItemType.SystemItem:
                    switch (item.statDatas[i].statType)
                    {
                        // LvUp , FirstRerollFree, RerollSale, ItemSale , RandomBoxHPItem, RandomBoxCoinPerItem, TenPerHasMoney, randomItem
                        case StatType.LvUp:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "level increase";
                            break;
                        case StatType.FirstRerollFree:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "1 free refresh";
                            break;
                        case StatType.RerollSale:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "refresh discount";
                            break;
                        case StatType.ItemSale:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "5% discount on item price";
                            break;
                        case StatType.RandomBoxHPItem:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Defeat boxes at once";
                            break;
                        case StatType.RandomBoxCoinPerItem:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Increased goods acquisition rate";
                            break;
                        case StatType.TenPerHasMoney:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Obtain 10% of goods at the end of the game";
                            break;
                        case StatType.randomItem:
                            InfoTextObj.GetComponent<LocalizeScript>().textKey = "Random item acquisition";
                            break;
                    }
                    break;
            }
        }
    }
    public class Data
    {
        public PlayerController Player;
        public InGameManager ingameManager;
        public int level;
        public float MaxExp;
        public float CurExp;

        public void ApplyItem(Item item, GameObject ItemInfoPrefab, Transform ItemInfoContentTransform)
        {
            for (int i = 0; i < item.statDatas.Length; i++)
            {
                switch (item.statDatas[i].itemType)
                {
                    case ItemType.StatItem:
                        switch (item.statDatas[i].statType)
                        {
                            // MaxHp, CurHp, HpRecovery, MaxbulletCount, ReloadTime, AttackSp, MoveSp, Damage, SkillDamage, BombDamage, Accuracy, Critical, 
                            // Defense, Harvesting, Evasion, HasExp, Science, Range, DiamondPer, skillCoolTime, MeleeAt, BulletcurHP, Timer, RandomBoxMaxCoolTime
                            case StatType.MaxHp:
                                print("MaxHp up");
                                print(ingameManager.UpgradeStat[0]);
                                ingameManager.UpgradeStat[0] += item.statDatas[i].statValue;
                                print(ingameManager.UpgradeStat[0]);
                                Player.herodata.maxHp += item.statDatas[i].statValue;
                                break;
                            case StatType.CurHp:
                                print("CurHp up");
                                Player.herodata.CurHp = 1;
                                ingameManager.UpgradeStat[0] += (int)Player.herodata.maxHp * 0.3f;
                                IsCurHpItem = true;
                                break;
                            case StatType.HpRecovery:
                                print("HpRecovery up");
                                ingameManager.UpgradeStat[1] += item.statDatas[i].statValue;
                                Player.herodata.hpRecovery += (int)item.statDatas[i].statValue;
                                break;
                            case StatType.Absorption:
                                print("Absorption up");
                                ingameManager.UpgradeStat[2] += (int)(item.statDatas[i].statValue * 100) - 100;
                                Player.herodata.absorption *= item.statDatas[i].statValue;
                                break;
                            case StatType.Damage:
                                print("전" + Player.herodata.damage);
                                ingameManager.UpgradeStat[3] += item.statDatas[i].statValue;
                                Player.herodata.damage += item.statDatas[i].statValue;
                                break;
                            case StatType.BombDamage:
                                print("전" + Player.herodata.bombDamage);
                                ingameManager.UpgradeStat[4] += item.statDatas[i].statValue;
                                Player.herodata.bombDamage += item.statDatas[i].statValue;
                                break;
                            case StatType.SkillDamage:
                                print("전" + Player.herodata.skillDamage);
                                ingameManager.UpgradeStat[5] += item.statDatas[i].statValue;
                                Player.herodata.skillDamage += item.statDatas[i].statValue;
                                break;
                            case StatType.Science:
                                print("전" + Player.herodata.science);
                                ingameManager.UpgradeStat[6] += item.statDatas[i].statValue;
                                Player.herodata.science += item.statDatas[i].statValue;
                                break;
                            case StatType.MaxbulletCount:
                                print("MaxbulletCount up");
                                ingameManager.UpgradeStat[7] += item.statDatas[i].statValue;
                                Player.herodata.maxbulletCount += (int)item.statDatas[i].statValue;
                                break;
                            case StatType.AttackSp:
                                print("전" + Player.herodata.attackSp);
                                ingameManager.UpgradeStat[8] += 100 - (int)(item.statDatas[i].statValue * 100);
                                Player.herodata.attackSp *= item.statDatas[i].statValue;
                                break;
                            case StatType.MoveSp:
                                print("전" + Player.herodata.moveSp);
                                ingameManager.UpgradeStat[9] += (int)(item.statDatas[i].statValue * 100) - 100;
                                Player.herodata.moveSp *= item.statDatas[i].statValue;
                                break;
                            case StatType.ReloadTime:
                                print("전" + Player.herodata.reloadTime);
                                ingameManager.UpgradeStat[10] += 100 - (int)(item.statDatas[i].statValue * 100);
                                Player.herodata.reloadTime *= item.statDatas[i].statValue;
                                break;
                            case StatType.skillCoolTime:
                                print("skillCoolTime up");
                                ingameManager.UpgradeStat[11] += 100 - (int)(item.statDatas[i].statValue * 100);
                                Player.herodata.skillmaxTime *= item.statDatas[i].statValue;
                                break;
                            case StatType.HasExp:
                                print("전" + Player.herodata.hasExp);
                                ingameManager.UpgradeStat[12] += (int)(item.statDatas[i].statValue * 100) - 100;
                                Player.herodata.hasExp *= item.statDatas[i].statValue;
                                break;
                            case StatType.Defense:
                                print("전" + Player.herodata.defense);
                                ingameManager.UpgradeStat[13] += item.statDatas[i].statValue;
                                Player.herodata.defense += item.statDatas[i].statValue;
                                break;
                            case StatType.Critical:
                                print("전" + Player.herodata.critical);
                                ingameManager.UpgradeStat[14] += (int)(item.statDatas[i].statValue * 100) - 100;
                                Player.herodata.critical += item.statDatas[i].statValue;
                                break;
                            case StatType.Accuracy:
                                print("전" + Player.herodata.accuracy);
                                ingameManager.UpgradeStat[15] += 100 - (int)(item.statDatas[i].statValue * 100);
                                Player.herodata.accuracy *= item.statDatas[i].statValue;
                                break;
                            case StatType.Range:
                                print("Range up");
                                print((int)(item.statDatas[i].statValue * 100)); //95
                                print((int)(item.statDatas[i].statValue * 100) - 100); //-5
                                ingameManager.UpgradeStat[16] += (int)(item.statDatas[i].statValue * 100) - 100;
                                Player.herodata.range *= item.statDatas[i].statValue;
                                break;
                            case StatType.Evasion:
                                print("전" + Player.herodata.evasion);
                                ingameManager.UpgradeStat[17] += (int)(item.statDatas[i].statValue * 100) - 100;
                                Player.herodata.evasion *= item.statDatas[i].statValue;
                                break;
                            case StatType.DiamondPer:
                                print("DiamondPer up");
                                Player.herodata.diamondPer *= item.statDatas[i].statValue;
                                break;
                            case StatType.BulletcurHP:
                                print("BulletcurHP up");
                                Player.herodata.bulletcurHP += 1;
                                break;
                            case StatType.MapScale:
                                print("+2 wider field of view");
                                ingameManager.InGameUIGroup.InData.InGameCam.gameObject.GetComponent<CameraContorller>().offset.y += item.statDatas[i].statValue;
                                ingameManager.InGameUIGroup.InData.InGameCam.gameObject.GetComponent<CameraContorller>().offset.z += -2.5f;
                                break;
                            case StatType.RandomBoxMaxCoolTime:
                                print("RandomBoxMaxCoolTime up");
                                ingameManager.RandomBoxMaxCoolTime *= item.statDatas[i].statValue;
                                break;
                        }
                        break;
                    case ItemType.InstItem:
                        switch (item.statDatas[i].statType)
                        {
                            // BasicTurret, HeelTurret, BombTurret, SlowTurret, EmyBomb, RandomBullet, SpinBullet, LandMine
                            case StatType.BasicTurret:
                                print("BasicTurret");
                                isDefaultTurret = true;
                                DefaultTurretCount += (int)item.statDatas[i].statValue;
                                break;
                            case StatType.HeelTurret:
                                print("HeelTurret");
                                isHeelTurret = true;
                                HeelTurretCount += (int)item.statDatas[i].statValue;
                                print("HeelTurret" + HeelTurretCount + "+" + (int)item.statDatas[i].statValue);
                                break;
                            case StatType.BombTurret:
                                print("BombTurret");
                                isBombTurret = true;
                                BombTurretCount += (int)item.statDatas[i].statValue;
                                break;
                            case StatType.SlowTurret:
                                print("SlowTurret");
                                isSlowTurret = true;
                                SlowTurretCount += (int)item.statDatas[i].statValue;
                                break;
                            case StatType.EmyBomb:
                                print("EmyBomb");
                                ingameManager.EmyBombItem = true;
                                break;
                            case StatType.MultiBullet:
                                ingameManager.player.GetComponent<PlayerController>().isMultiBullet = true;
                                print("multiBullet");
                                break;
                            case StatType.SpinBullet:
                                print("111111111");
                                IsSpinBullet = true;
                                print("SpinBullet");
                                break;
                            case StatType.LandMine:
                                print("LandMine");
                                isLandMine = true;
                                ingameManager.LandMineCount += (int)item.statDatas[i].statValue;
                                break;
                            case StatType.Reborn:
                                print("LifePlus");
                                IsReborn = true;
                                break;
                        }
                        break;
                    case ItemType.SystemItem:
                        switch (item.statDatas[i].statType)
                        {
                            // LvUp , FirstRerollFree, RerollSale, ItemSale , RandomBoxHPItem, RandomBoxCoinPerItem, TenPerHasMoney, randomItem
                            case StatType.LvUp:
                                print("LvUp");
                                Player.herodata.curExp = Player.herodata.maxExp;
                                break;
                            case StatType.FirstRerollFree:
                                print("FirstRerollFree");
                                ingameManager.StoreTicket = true;
                                break;
                            case StatType.RerollSale:
                                print("RerollSale");
                                if (ingameManager.CurRerollMoney > 2) ingameManager.CurRerollMoney -= (int)item.statDatas[i].statValue;
                                break;
                            case StatType.ItemSale:
                                int[] itemprice = ingameManager.InGameUIGroup.InData.ItemPriceArr;
                                for (int j = 0; j < itemprice.Length; j++)
                                {
                                    itemprice[j] -= (int)(itemprice[j] * 0.05f);
                                    print(itemprice[j]);
                                }
                                print("ItemSale");
                                break;
                            case StatType.RandomBoxHPItem:
                                print("RandomBoxHPItem");
                                ingameManager.RandomBoxHPItem = true;
                                break;
                            case StatType.RandomBoxCoinPerItem:
                                print("RandomBoxCoinPerItem");
                                ingameManager.RandomBoxCoinPerItem = true;
                                break;
                            case StatType.TenPerHasMoney:
                                print("TenPerHasMoney");
                                ingameManager.TenPerHasMoney = true;
                                break;
                            case StatType.randomItem:
                                print("randomItem");
                                ingameManager.GetRandomItem = true;
                                break;
                        }
                        break;
                }

            }
        }
    }
    public void DoItem()
    {
        PlayerController Player = inGameManager.player.GetComponent<PlayerController>();

        // 힐포탑
        if (isHeelTurret)
        {
            for (int i = 0; i < HeelTurretCount; i++)
            {
                //print("힐포탑생성");
                float randX = Random.Range(-10, 10);
                float randZ = Random.Range(-10, 10);
                Vector3 RandomPos = new Vector3(randX, 0, randZ);
                Instantiate(HeelTurretPrefab, RandomPos, Quaternion.identity);
            }
        }
        // 기본 포탑
        if (isDefaultTurret)
        {
            for (int i = 0; i < DefaultTurretCount; i++)
            {
                print("기본포탑생성");
                float randX = Random.Range(-10, 10);
                float randZ = Random.Range(-10, 10);
                Vector3 RandomPos = new Vector3(randX, 0, randZ);
                var turret = Instantiate(DefaultTurretPrefab, RandomPos, Quaternion.identity);
                turret.GetComponent<TurretController>().BulletText = "TurretDefaultBullet";
            }
        }

        // 폭탄 포탑
        if (isBombTurret)
        {
            for (int i = 0; i < BombTurretCount; i++)
            {
                print("폭탄포탑생성");
                float randX = Random.Range(-10, 10);
                float randZ = Random.Range(-10, 10);
                Vector3 RandomPos = new Vector3(randX, 0, randZ);
                var turret = Instantiate(BombTurretPrefab, RandomPos, Quaternion.identity);
                turret.GetComponent<TurretController>().BulletText = "TurretBombBullet";
            }
        }

        // 슬로우 포탑
        if (isSlowTurret)
        {
            for (int i = 0; i < SlowTurretCount; i++)
            {
                print("슬로우포탑생성");
                float randX = Random.Range(-10, 10);
                float randZ = Random.Range(-10, 10);
                Vector3 RandomPos = new Vector3(randX, 0, randZ);
                var turret = Instantiate(SlowTurretPrefab, RandomPos, Quaternion.identity);
                turret.GetComponent<TurretController>().BulletText = "TurretSlowBullet";
            }
        }

        if (IsCurHpItem)
        {
            IsCurHpItem = false;
        }
        else if (!IsCurHpItem)
        {
            Player.herodata.CurHp = Player.herodata.maxHp;
        }

        // 랜덤지뢰
        if (isLandMine) inGameManager.isLandMine = true;

        // 레벨업
        if (isLvUp) Player.herodata.curExp = Player.herodata.maxExp;

        // 랜덤박스 쿨타임 감소
        if (IsrandomBox) inGameManager.RandomBoxMaxCoolTime = inGameManager.RandomBoxMaxCoolTime * 0.9f;

        // 스핀 총알 
        if (IsSpinBullet) inGameManager.player.GetComponent<PlayerController>().isSpinBullet = true;

        // 상점 리롤 1회 무료
        if (StoreTicket) inGameManager.StoreTicket = true;

        //1회 다시 태어남
        if (IsReborn)
        {
            print(Player.gameObject);
            Player.IsReborn = true;
            print(Player.IsReborn+"아이템 적용");
        }

        // 아이템 할인
        if (IsSale)
        {
            for (int i = 0; i < 70; i++)
            {
                inGameManager.InGameUIGroup.InData.ItemPriceArr[i] -= Mathf.RoundToInt(inGameManager.InGameUIGroup.InData.ItemPriceArr[i] * 0.5f);
            }
        }
    }
    public void ItemInfo(int index, PlayerController player, GameObject ItemInfoPrefab, Transform ItemInfoContentTransform)
    {
        AddItemInfo(player, GetItem(index), ItemInfoPrefab, ItemInfoContentTransform);
    }
    public void HasItem(int index, GameObject ItemInfoPrefab, Transform ItemInfoContentTransform)
    {
        //print(index);
        data = new Data();

        data.Player = inGameManager.player.GetComponent<PlayerController>();
        data.ingameManager = inGameManager;
        data.ApplyItem(GetItem(index), ItemInfoPrefab, ItemInfoContentTransform);
    }
}