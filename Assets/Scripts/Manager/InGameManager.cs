using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;


[System.Serializable]
public class InPlayUI
{
    public GameObject PlayUI;

    public TextMeshProUGUI[] TxtStage = new TextMeshProUGUI[2];
    public TextMeshProUGUI TxtTime;
    public TextMeshProUGUI TxtCoin;
    public TextMeshProUGUI TxtDia;

    public TextMeshProUGUI HPText;
    public TextMeshProUGUI ScoreText;
    public Slider hpBar;
    public Slider ExpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI BulletCountText;
    public GameObject ReloadGauge;

    public Image Skill1Img;
    public Slider Skill1Slider;
    public TextMeshProUGUI Skill1CoolTime;

    public Image Skill2Img;
    public Slider Skill2Slider;
    public TextMeshProUGUI Skill2CoolTime;
}

[System.Serializable]
public class InLvUpUI
{
    public GameObject LvUpUI;

    public GameObject[] LvUpSlotArr = new GameObject[4];
    public GameObject LvUpSlotPrefab;
    public Transform[] LvUpContentTransform;

    public TextMeshProUGUI[] LvUpStatTextArr = new TextMeshProUGUI[14];
    public TextMeshProUGUI[] LvUpItemNameText;
    public TextMeshProUGUI LvUpRerollPriceText;
    public TextMeshProUGUI LvUpCoinTxt;
    public Button[] LvUpButton;

    public string[] LvUpStat = new string[20];
}

[System.Serializable]
public class InStoreUI
{
    public GameObject StoreUI;

    public GameObject[] ItemArr = new GameObject[4];
    public GameObject ItemSlotPrefab;
    public GameObject ItemInfoPrefab;
    public Transform ItemImgContentTransform;
    public Transform[] ItemInfoContentTransform;

    public Image[] ItemImg;
    public Button[] storeButton;
    public Button[] ItemLockBts = new Button[4];
    public TextMeshProUGUI[] StatArr = new TextMeshProUGUI[14];
    public TextMeshProUGUI[] ItemPriceTextArr = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] ItemNameTxtArr = new TextMeshProUGUI[4];
    public TextMeshProUGUI ItemCountText;
    public TextMeshProUGUI RerollTxt;
    public TextMeshProUGUI StoreCoinTxt;
}

[System.Serializable]
public class InOptionUI
{
    public GameObject OptionUI;
    public TextMeshProUGUI[] OptionStatArr = new TextMeshProUGUI[19];
}

[System.Serializable]
public class InOverUI
{
    public GameObject GameOverUI;
    public TextMeshProUGUI[] GameOverStatArr = new TextMeshProUGUI[19];
}
[System.Serializable]
public class InRankModeUI
{
    public GameObject RankModeUI;
    public TextMeshProUGUI[] RankModeStatArr = new TextMeshProUGUI[19];

    public bool BossStage = false;
}

[System.Serializable]
public class InData
{
    public Camera InGameCam;
    public ItemManager itemManager;

    public GameObject RandomBox;
    public GameObject LandMinePrefab;
    public GameObject[] CharPrefabs = new GameObject[10];
    public GameObject[] SpawnObj = new GameObject[7];
    public GameObject[] WeaponImg = new GameObject[14];

    [SerializeField] Texture2D defaultImg; // �ٲ� Ŀ�� �̹���
    public Sprite[] ItemImgArr = new Sprite[91];

    public int[] ItemPriceArr = new int[70];
    public string[] LvUpItemLocalCode = new string[20];
    public string[] ItemLocalCode = new string[91];
}

[System.Serializable]
public class InEmptyData
{
}

[System.Serializable]
public class InSound
{
    public AudioSource MusicAudio;
    public AudioClip GameMusic;
    public AudioClip BuyBtSound;
    public AudioClip LockBtSound;
    public AudioClip NoBtSound;
}

[System.Serializable]
public class InGameUIGroup
{
    public InPlayUI InPlayUI;
    public InLvUpUI InLvUpUI;
    public InStoreUI InStoreUI;
    public InOptionUI InOptionUI;
    public InOverUI InOverUI;
    public InRankModeUI InRankModeUI;
    public InData InData;
    public InSound InSound;
}

public class InGameManager : MonoBehaviour
{
    public InGameUIGroup InGameUIGroup;

    private int CharCode;
    [HideInInspector] public PlayerController player;
    [HideInInspector] public int charUpgradeNum;


    public int StageNum = 15;
    public float MaxTimer = 20;
    public float CurTimer = 20;
    public int money;
    public int Nextmoney;
    public int diamond;
    public int Score = 0;

    [Header("Item")]
    private bool[] IsLock = { false, false, false, false };
    private bool[] ItemLock = new bool[4];
    private int ItemCount = 0;
    private int ItemActiveNum = 34; //�Ϲ� 34,  ���� 68, ��� 90
    private int LvUpStatNum = -1;
    private int[] ItemMulti = { 21, 23, 24, 31, 45, 49, 50, 51, 52, 66, 69, 90 }; //�ߺ� ���� ������
    private int[] randItem = { -1, -1, -1, -1 }; //�� ��ư ������ ��ȣ
    private int ItemNum = -1;
    private int[] LvUpItemNum = new int[4] { -1, -1, -1, -1 };
    [HideInInspector] public bool[] HasItem = new bool[91];
    [HideInInspector] public float[] UpgradeStat = new float[20];
    [HideInInspector] public int lvUpCount = -1;
    [HideInInspector] public bool isStore = false;
    [HideInInspector] public bool IsOption = false;
    [HideInInspector] public bool IsBossDead = false;



    [Header("Item")]
    [HideInInspector] public bool RandomBoxHPItem = false;
    [HideInInspector] public bool RandomBoxCoinPerItem = false;
    [HideInInspector] public bool TenPerHasMoney = false;
    [HideInInspector] public bool StoreTicket = false;
    [HideInInspector] public bool EmyBombItem = false;
    [HideInInspector] public bool GetRandomItem = false;
    [HideInInspector] public int GetRandomItemNum;
    [HideInInspector] public int RandomItemArrNum;
    public bool isLandMine = false;
    public int LandMineCount = 1;
    float LandMaxCoolTime = 5;
    float LandCurCoolTime = 0;
    public float RandomBoxMaxCoolTime = 13;
    public float RandomBoxCurCooltime = 13;
    public int MaxRerollMoney = 1; //���� ��ȭ
    public int CurRerollMoney = 1; //���� ��ȭ
    public int PlusRerollMoney = 0; // ���� �� �߰��Ǵ� ��ȭ


    ItemGrade[] ItemGrades = new ItemGrade[91];
    public enum ItemGrade
    {
        Basic, Rare, Epic
    }


    void Start()
    {
        SoundInit();
        ItemInit();
        PlayerSpawn();
        AddButton();
    }

    void SoundInit()
    {
        SoundManager.Instance.MusicAudio = InGameUIGroup.InSound.MusicAudio;
        InGameUIGroup.InSound.MusicAudio.volume = SoundManager.Instance.MusicVolume;
    }
    void ItemInit()
    {
        for (int i = 0; i < 91; i++)
        {
            if (i <= 33) ItemGrades[i] = ItemGrade.Basic;
            else if (i <= 68) ItemGrades[i] = ItemGrade.Rare;
            else if (i <= 91) ItemGrades[i] = ItemGrade.Epic;
        }
    }
    void PlayerSpawn()
    {
        CurTimer = MaxTimer;
        charUpgradeNum = GameManager.Instance.playerUpgrade[GameManager.Instance.playerNum];

        player = Instantiate(InGameUIGroup.InData.CharPrefabs[GameManager.Instance.playerNum], Vector3.zero, Quaternion.identity).GetComponent<PlayerController>();
        player.GetComponent<PlayerController>().enabled = true;
        player.isAI = GameManager.Instance.IsAI;
        player.isRange = GameManager.Instance.IsRange;
        CharCode = player.herodata.CharCode;
        InGameUIGroup.InData.WeaponImg[CharCode - 1].SetActive(true);
        Invoke("Spawn", 2);
    }
    void AddButton()
    {
        for (int i = 0; i < InGameUIGroup.InStoreUI.storeButton.Length; i++)
        {
            int index = i;
            InGameUIGroup.InStoreUI.storeButton[i].onClick.AddListener(() => StoreBt(index, randItem));
            InGameUIGroup.InLvUpUI.LvUpButton[i].onClick.AddListener(() => LvUpBt(index));
            InGameUIGroup.InStoreUI.ItemLockBts[i].onClick.AddListener(() => StoreLockBt(index));
        }

        for (int i = 0; i < HasItem.Length; i++)
        {
            HasItem[i] = false;
        }

        TenPerHasMoney = false;
    }

    void Update()
    {
        InGameStart();
    }

    void InGameStart()
    {
        if (player.isStart && !IsOption)
        {
            TimeManager();
            UIUpdate();

            statView();
            randomBoxCreate();

            ItemActive();
            GameOver();
        }
        OnOption();
    }


    // ------------------���ӽý���------------------

    // ��ȭ
    void UIUpdate()
    {
        InGameUIGroup.InPlayUI.TxtCoin.text = ((int)money).ToString() + "<color=green>" + "<size=32>" + " (" + Nextmoney.ToString() + ")" + "</size>" + "</color>";
        InGameUIGroup.InStoreUI.StoreCoinTxt.text = money.ToString();
        InGameUIGroup.InLvUpUI.LvUpCoinTxt.text = money.ToString();
        InGameUIGroup.InPlayUI.TxtDia.text = diamond.ToString();
        InGameUIGroup.InStoreUI.ItemCountText.text = "( " + ItemCount.ToString() + " )";

        for (int i = 0; i < InGameUIGroup.InPlayUI.TxtStage.Length; i++) InGameUIGroup.InPlayUI.TxtStage[i].text = StageNum.ToString();

        // Score Text
        InGameUIGroup.InPlayUI.ScoreText.text = GameManager.Instance.Score.ToString();
        // Hp text
        InGameUIGroup.InPlayUI.HPText.text = (int)player.herodata.CurHp + " / " + (int)player.herodata.maxHp;
    }

    // �������� �ð�
    void TimeManager()
    {
        //print("����ð�"+CurTimer);

        if (CurTimer >= 0 && !player.isStore) CurTimer -= Time.deltaTime;

        if (CurTimer <= 5) InGameUIGroup.InPlayUI.TxtTime.color = Color.red;
        else InGameUIGroup.InPlayUI.TxtTime.color = Color.white;

        for (int i = 0; i < InGameUIGroup.InPlayUI.TxtStage.Length; i++) InGameUIGroup.InPlayUI.TxtStage[i].text = StageNum.ToString();

        InGameUIGroup.InPlayUI.TxtTime.text = ((int)CurTimer).ToString();

        if (CurTimer <= 0 && !isStore && !InGameUIGroup.InRankModeUI.BossStage)
        {
            isStore = true;
            StartCoroutine(StageFinshUI());
        }
        else if (InGameUIGroup.InRankModeUI.BossStage && IsBossDead && !isStore) //������������, �����׾��� ��
        {
            isStore = true;
            StartCoroutine(StageFinshUI());
        }
    }
    IEnumerator StageFinshUI()
    {
        player.isStore = true;
        player.anim.SetBool("Walk", false);
        ClearEmy();
        yield return new WaitForSeconds(1);
        ClearDrop();
        yield return new WaitForSeconds(2);
        SelectUI();

        if (TenPerHasMoney)
        {
            money += money / 10;
        }
        if (MaxTimer <= 55) MaxTimer += 5;
        player.gameObject.transform.position = Vector3.zero;
        yield return null;
    }

    // ������, ���� UI �̵�
    void SelectUI()
    {
        if (InGameUIGroup.InRankModeUI.BossStage && IsBossDead)
        {
            print("��ũ��� ����");
            OnRankModeUI();
        }
        else if (lvUpCount >= 0) //������ ui on
        {
            print("������ui");
            CurRerollMoney = MaxRerollMoney; // ���������� �´� ���� ��ȭ �ʱ�ȭ
            if (InGameUIGroup.InLvUpUI.LvUpSlotArr != null) for (int i = 0; i < InGameUIGroup.InLvUpUI.LvUpSlotArr.Length; i++) Destroy(InGameUIGroup.InLvUpUI.LvUpSlotArr[i]);

            for (int i = 0; i < LvUpItemNum.Length; i++)
            {
                //print(i);
                LvUpItemNum[i] = Random.Range(0, 20);
                InGameUIGroup.InLvUpUI.LvUpSlotArr[i] = Instantiate(InGameUIGroup.InLvUpUI.LvUpSlotPrefab, InGameUIGroup.InLvUpUI.LvUpContentTransform[i]);
                string statText = "<color=#00FF00>" + InGameUIGroup.InLvUpUI.LvUpStat[LvUpItemNum[i]] + "</color>";
                InGameUIGroup.InLvUpUI.LvUpSlotArr[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = statText;
                InGameUIGroup.InLvUpUI.LvUpSlotArr[i].transform.GetChild(1).gameObject.GetComponent<LocalizeScript>().enabled = true;
                Localizezation(InGameUIGroup.InLvUpUI.LvUpSlotArr[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(), InGameUIGroup.InData.LvUpItemLocalCode[LvUpItemNum[i]]);
                InGameUIGroup.InLvUpUI.LvUpContentTransform[i].GetComponent<RectTransform>().sizeDelta = new Vector2(InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta.x, InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount * 100f); // �̹����� ���̸� 100�̶�� ����
            }

            ////////////////////������ȭ �߰� �ӽ÷� ������ȭ��
            InGameUIGroup.InLvUpUI.LvUpRerollPriceText.text = CurRerollMoney.ToString();


            for (int i = 0; i < InGameUIGroup.InLvUpUI.LvUpItemNameText.Length; i++)
            {
                Localizezation(InGameUIGroup.InLvUpUI.LvUpItemNameText[i], InGameUIGroup.InData.LvUpItemLocalCode[LvUpItemNum[i]].ToString());
            }
            InGameUIGroup.InPlayUI.PlayUI.SetActive(false);
            InGameUIGroup.InLvUpUI.LvUpUI.SetActive(true);
        }
        else if (lvUpCount <= -1) //���� ui on
        {
            print("����ui");
            CurRerollMoney = MaxRerollMoney; // ���������� �´� ���� ��ȭ �ʱ�ȭ
            //player.SetActive(false);
            for (int i = 0; i < 4; i++) InGameUIGroup.InStoreUI.ItemArr[i].SetActive(true);

            for (int i = 0; i < randItem.Length; i++)
            {
                if (!ItemLock[i])
                {
                    randItem[i] = GetUniqueRandomNumber();
                }
            }

            usedNumbers.Clear();
            StoreUIUpdate();

            InGameUIGroup.InPlayUI.PlayUI.SetActive(false);
            InGameUIGroup.InStoreUI.StoreUI.SetActive(true);
        }
    }

    // �������� ���� �� ������Ʈ ��� ����
    void ClearEmy()
    {
        for (int i = 0; i < 6; i++) InGameUIGroup.InData.SpawnObj[i].SetActive(false);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemies.Concat(GameObject.FindGameObjectsWithTag("Turret")).ToArray();
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<EnemyController>() != null) enemy.GetComponent<EnemyController>().ReleaseObject();
            else Destroy(enemy);
        }
    }
    void ClearDrop()
    {
        GameObject[] DropObj = GameObject.FindGameObjectsWithTag("Drop");
        foreach (GameObject Drop in DropObj)
        {
            int coinLayer = LayerMask.NameToLayer("Coin");
            if (Drop.gameObject.layer == coinLayer)
            {
                Drop.gameObject.tag = "NextCoin";
                Drop.gameObject.layer = 3;
            }
            Drop.GetComponent<DropController>().isFollow = true;
            Drop.GetComponent<DropController>().InGameManager = gameObject.GetComponent<InGameManager>();
        }

        GameObject[] randomBox = GameObject.FindGameObjectsWithTag("RandomBox");
        foreach (GameObject randBox in randomBox)
        {
            Destroy(randBox);
        }
    }


    //���� �������� ����_��ư
    public void nextStage()
    {
        isStore = false;
        StageNum += 1;
        //player.SetActive(true);
        InGameUIGroup.InPlayUI.ReloadGauge.SetActive(false);
        player.isStore = false;

        Invoke("Spawn", 1);
        InGameUIGroup.InStoreUI.StoreUI.SetActive(false);
        InGameUIGroup.InPlayUI.PlayUI.SetActive(true);

        InGameUIGroup.InData.itemManager.DoItem();
        money += Nextmoney * 2;
        Nextmoney = 0;
        CurTimer = MaxTimer;
        //print("���� ���ѱݾ�" + CurRerollMoney);
        //print("�ִ� ���ѱݾ�" + MaxRerollMoney);
    }

    // �������� ���� �� ����������Ʈ Ȱ��ȭ
    void Spawn()
    {
        //print("���� �������� : "+ StageNum);
        InGameUIGroup.InPlayUI.PlayUI.SetActive(true);
        //���������� �þ���� �������� ���ʹ� ���� �������� ���ʹ� ����
        if (StageNum >= 21)
        {
            MaxRerollMoney++;
            PlusRerollMoney = 7;

            SpawnManagerActive(6);
            EmyItem();
        }
        else if (StageNum >= 20)
        {
            InGameUIGroup.InRankModeUI.BossStage = true;
            MaxRerollMoney++;
            PlusRerollMoney = 6;
            InGameUIGroup.InData.SpawnObj[6].SetActive(true);

            //�ð� ���������� ����
            EmyItem();
        }
        else if (StageNum >= 16)
        {
            MaxRerollMoney++;
            PlusRerollMoney = 5;

            SpawnManagerActive(6);
            EmyItem();
        }
        else if (StageNum >= 14)
        {
            MaxRerollMoney++;
            PlusRerollMoney = 4;

            ItemActiveNum = 91;
            SpawnManagerActive(5);
            EmyItem();
        }
        else if (StageNum >= 11)
        {
            MaxRerollMoney++;
            PlusRerollMoney = 3;

            SpawnManagerActive(4);
            EmyItem();
        }
        else if (StageNum >= 7)
        {
            MaxRerollMoney++;
            PlusRerollMoney = 3;

            ItemActiveNum = 67;
            SpawnManagerActive(3);
            EmyItem();
        }
        else if (StageNum >= 3)
        {
            MaxRerollMoney++;
            PlusRerollMoney = 2;

            SpawnManagerActive(2);
            EmyItem();
        }
        else if (StageNum >= 0)
        {
            PlusRerollMoney = 1;

            ItemActiveNum = 33;
            SpawnManagerActive(1);


            //InGameUIGroup.InData.SpawnObj[6].SetActive(true);
            //InGameUIGroup.InData.SpawnObj[2].SetActive(true);
            //InGameUIGroup.InData.SpawnObj[2].GetComponent<SpawnManager>().StartCoroutine("spawnMosnter");
            EmyItem();
        }
    }
    void SpawnManagerActive(int index)
    {
        for (int i = 0; i < index; i++)
        {
            //print(i+"���� Ȱ��ȭ");
            InGameUIGroup.InData.SpawnObj[i].SetActive(true);
            InGameUIGroup.InData.SpawnObj[i].GetComponent<SpawnManager>().StartCoroutine("spawnMosnter");
            InGameUIGroup.InData.SpawnObj[i].GetComponent<SpawnManager>().target = player.gameObject;
            InGameUIGroup.InData.SpawnObj[i].GetComponent<SpawnManager>().level *= 0.93f;
        }
    }
    // ������ ��ư ���
    public void LvUpBt(int index)
    {
        lvUpCount -= 1;
        LvUpStatNum = LvUpItemNum[index];
        LvUpStatItem();
        InGameUIGroup.InLvUpUI.LvUpUI.SetActive(false);
        SelectUI();
        CurRerollMoney = MaxRerollMoney;
    }

    // ���� ���� ��
    void GameOver()
    {
        if (player.IsDead)
        {
            ClearEmy();
            ClearDrop();
            print(GameManager.Instance.Score);
            SteamManager.instance.Rank(GameManager.Instance.Score, player.herodata.CharCode);
            GameManager.Instance.Score = 0;
            player.isStart = false;
            player.IsDead = false;
            StartCoroutine("GameOverCoroutine");
            DataManager.Instance.nowPlayer.Diamond += diamond;
            DataManager.Instance.SaveData();
        }
    }
    IEnumerator GameOverCoroutine()
    {
        ClearEmy();
        ClearDrop();
        InGameUIGroup.InPlayUI.PlayUI.SetActive(false);
        player.gameObject.GetComponent<Animator>().SetTrigger("Dead");
        yield return new WaitForSeconds(2f);
        InGameUIGroup.InOverUI.GameOverUI.SetActive(true);
    }
    public void MainMenu()
    {
        IsOption = false;
        Time.timeScale = 1;
        player.isStart = false;
        player.IsDead = false;
        DataManager.Instance.nowPlayer.Diamond += diamond;
        DataManager.Instance.SaveData();
        SceneManager.LoadScene("MainScene");
    }
    private void OnOption()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsOption)
        {
            IsOption = true;
            Time.timeScale = 0;
            InGameUIGroup.InOptionUI.OptionUI.SetActive(true);
            InGameUIGroup.InPlayUI.PlayUI.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsOption)
        {
            IsOption = false;
            Time.timeScale = 1;
            InGameUIGroup.InOptionUI.OptionUI.SetActive(false);
            InGameUIGroup.InPlayUI.PlayUI.SetActive(true);
        }
    }
    public void OffOption()
    {
        if (IsOption)
        {
            IsOption = false;
            Time.timeScale = 1;
            InGameUIGroup.InOptionUI.OptionUI.SetActive(false);
            InGameUIGroup.InPlayUI.PlayUI.SetActive(true);
        }
    }
    // ------------------����------------------

    // ������ �ߺ� ���� ���
    private List<int> usedNumbers = new List<int>();
    int GetUniqueRandomNumber()
    {
        int randomNumber;
        do
        {
            // 0���� ItemActiveNum������ ���� ��ȣ ����
            randomNumber = Random.Range(0, ItemActiveNum);
        } while (usedNumbers.Contains(randomNumber) || CheckIfNumberExistsInArray(randomNumber)); // �̹� ���� ��ȣ���� Ȯ��

        // ������ ��ȣ�� ���� ����Ʈ�� �߰�
        usedNumbers.Add(randomNumber);

        return randomNumber;
    }
    bool CheckIfNumberExistsInArray(int number)
    {
        for (int i = 0; i < ItemMulti.Length; i++)
        {
            if (ItemMulti[i] == number)
            {
                if (HasItem[number])
                {
                    //print("�ߺ���");
                    return true;
                }
            }
        }
        //print("�ߺ� x");
        return false;
    }

    // ���� UI ������Ʈ
    void StoreUIUpdate()
    {
        for (int i = 0; i < InGameUIGroup.InStoreUI.ItemNameTxtArr.Length; i++)
        {
            InGameUIGroup.InStoreUI.ItemNameTxtArr[i].GetComponent<LocalizeScript>().textKey = InGameUIGroup.InData.ItemLocalCode[randItem[i]];

            InGameUIGroup.InStoreUI.ItemNameTxtArr[i].GetComponent<LocalizeScript>().LocalizeChanged();

            InGameUIGroup.InStoreUI.ItemImg[i].sprite = InGameUIGroup.InData.ItemImgArr[randItem[i]];

            switch (ItemGrades[randItem[i]])
            {
                case ItemGrade.Basic:
                    //ItemImg[i].transform.parent.GetComponent<Image>().color = new Color(116 / 255f, 116 / 255f, 116 / 255f);
                    InGameUIGroup.InStoreUI.ItemImg[i].transform.parent.transform.parent.GetComponent<Outline>().effectColor = new Color(0 / 255f, 0 / 255f, 0 / 255f);
                    break;
                case ItemGrade.Rare:
                    //ItemImg[i].transform.parent.GetComponent<Image>().color = new Color(57 / 255f, 82 / 255f, 98 / 255f);
                    InGameUIGroup.InStoreUI.ItemImg[i].transform.parent.transform.parent.GetComponent<Outline>().effectColor = new Color(57 / 255f, 82 / 255f, 98 / 255f);
                    break;
                case ItemGrade.Epic:
                    //ItemImg[i].transform.parent.GetComponent<Image>().color = new Color(80 / 255f, 59 / 255f, 100 / 255f);
                    InGameUIGroup.InStoreUI.ItemImg[i].transform.parent.transform.parent.GetComponent<Outline>().effectColor = new Color(80 / 255f, 59 / 255f, 100 / 255f);
                    break;
            }
        }

        InGameUIGroup.InStoreUI.RerollTxt.text = "-" + CurRerollMoney.ToString();


        //������ ���� �ʱ�ȭ
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < InGameUIGroup.InStoreUI.ItemInfoContentTransform[i].childCount; j++)
            {
                Destroy(InGameUIGroup.InStoreUI.ItemInfoContentTransform[i].GetChild(j).gameObject);
            }
        }

        for (int i = 0; i < randItem.Length; i++) InGameUIGroup.InData.itemManager.ItemInfo(randItem[i], player, InGameUIGroup.InStoreUI.ItemInfoPrefab, InGameUIGroup.InStoreUI.ItemInfoContentTransform[i]);

        for (int i = 0; i < randItem.Length; i++) InGameUIGroup.InStoreUI.ItemPriceTextArr[i].text = InGameUIGroup.InData.ItemPriceArr[randItem[i]].ToString();


    }

    void OnRankModeUI()
    {
        InGameUIGroup.InRankModeUI.RankModeUI.SetActive(true);
    }
    public void RankModeStartBt()
    {
        InGameUIGroup.InRankModeUI.RankModeUI.SetActive(false);

        InGameUIGroup.InRankModeUI.BossStage = false;
        IsBossDead = false;
        StartCoroutine(StageFinshUI());
    }

    // ���� ���� ���
    public void Reroll()
    {
        if (StoreTicket)
        {
            int temp = CurRerollMoney;
            CurRerollMoney = 0;
            for (int i = 0; i < 4; i++) InGameUIGroup.InStoreUI.ItemArr[i].SetActive(true);

            // 1. 2. 3. 4. ������ �������� ����
            for (int i = 0; i < randItem.Length; i++)
            {
                if (!ItemLock[i])
                {
                    randItem[i] = GetUniqueRandomNumber();
                }
            }


            //text�� ��Ÿ����
            StoreUIUpdate();

            StoreTicket = false;
            CurRerollMoney = temp;
        }
        else if (money >= CurRerollMoney)
        {
            money -= CurRerollMoney;
            print("���� ���ѱݾ�" + CurRerollMoney);
            CurRerollMoney += PlusRerollMoney; // ���� �� ���� ��ȭ ����
            print("���� ���ѱݾ�" + CurRerollMoney);

            SoundManager.Instance.SoundPlay("IngameBt", InGameUIGroup.InSound.BuyBtSound);

            for (int i = 0; i < 4; i++) InGameUIGroup.InStoreUI.ItemArr[i].SetActive(true);

            // 1. 2. 3. 4. ������ �������� ����
            for (int i = 0; i < randItem.Length; i++)
            {
                if (!ItemLock[i])
                {
                    randItem[i] = GetUniqueRandomNumber();
                }
            }

            //text�� ��Ÿ����
            StoreUIUpdate();
        }

        usedNumbers.Clear();
    }

    // ������ ���ѱ��
    public void LvUpReroll()
    {
        if (money >= CurRerollMoney)
        {
            money -= CurRerollMoney;
            CurRerollMoney += PlusRerollMoney; // ���� �� ���� ��ȭ ����
            SoundManager.Instance.SoundPlay("IngameBt", InGameUIGroup.InSound.BuyBtSound);

            if (InGameUIGroup.InLvUpUI.LvUpSlotArr != null) for (int i = 0; i < InGameUIGroup.InLvUpUI.LvUpSlotArr.Length; i++) Destroy(InGameUIGroup.InLvUpUI.LvUpSlotArr[i]);

            for (int i = 0; i < LvUpItemNum.Length; i++)
            {
                LvUpItemNum[i] = Random.Range(0, 20);
                print(InGameUIGroup.InLvUpUI.LvUpContentTransform[i].position);
                InGameUIGroup.InLvUpUI.LvUpSlotArr[i] = Instantiate(InGameUIGroup.InLvUpUI.LvUpSlotPrefab, InGameUIGroup.InLvUpUI.LvUpContentTransform[i]);

                string statText = "<color=#00FF00>" + InGameUIGroup.InLvUpUI.LvUpStat[LvUpItemNum[i]] + "</color>";
                InGameUIGroup.InLvUpUI.LvUpSlotArr[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = statText;

                InGameUIGroup.InLvUpUI.LvUpSlotArr[i].transform.GetChild(1).gameObject.GetComponent<LocalizeScript>().enabled = true;
                Localizezation(InGameUIGroup.InLvUpUI.LvUpSlotArr[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(), InGameUIGroup.InData.LvUpItemLocalCode[LvUpItemNum[i]]);
                InGameUIGroup.InLvUpUI.LvUpContentTransform[i].GetComponent<RectTransform>().sizeDelta = new Vector2(InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta.x, InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount * 100f); // �̹����� ���̸� 100�̶�� ����
            }

            ////////////////////������ȭ �߰� �ӽ÷� ������ȭ��
            InGameUIGroup.InLvUpUI.LvUpRerollPriceText.text = CurRerollMoney.ToString();

            for (int i = 0; i < InGameUIGroup.InLvUpUI.LvUpItemNameText.Length; i++)
            {
                Localizezation(InGameUIGroup.InLvUpUI.LvUpItemNameText[i], InGameUIGroup.InData.LvUpItemLocalCode[LvUpItemNum[i]].ToString());
            }
        }
    }

    // ������ ���� ��ư ���
    public void StoreBt(int index, int[] randItemArr)
    {
        int randItem = index switch
        {
            0 => randItemArr[0],
            1 => randItemArr[1],
            2 => randItemArr[2],
            3 => randItemArr[3],
            _ => randItemArr[0]
        };

        if (money >= InGameUIGroup.InData.ItemPriceArr[randItem])
        {
            RandomItemArrNum = index;

            ItemCount++;
            money -= InGameUIGroup.InData.ItemPriceArr[randItem];
            SoundManager.Instance.SoundPlay("IngameBt", InGameUIGroup.InSound.BuyBtSound);

            InGameUIGroup.InData.ItemPriceArr[randItem] += (int)(InGameUIGroup.InData.ItemPriceArr[randItem] * 1.5f); // ���� ����

            InGameUIGroup.InStoreUI.ItemArr[index].SetActive(false); // 
            ItemNum = randItem;
            InGameUIGroup.InData.itemManager.HasItem(ItemNum, InGameUIGroup.InStoreUI.ItemInfoPrefab, InGameUIGroup.InStoreUI.ItemInfoContentTransform[index]);

            // ������ ���� UI ������Ʈ
            GameObject ItemInfo = AddItemImg(ItemNum);
            //print(ItemInfo);
            ItemInfo iteminfoScript = ItemInfo.GetComponent<ItemInfo>();
            iteminfoScript.ItemName.gameObject.GetComponent<LocalizeScript>().textKey = InGameUIGroup.InData.ItemLocalCode[ItemNum];
            iteminfoScript.ItemImg.sprite = InGameUIGroup.InData.ItemImgArr[ItemNum];


            InGameUIGroup.InData.itemManager.ItemInfo(ItemNum, player, iteminfoScript.infoTextObj, iteminfoScript.ContentTransform);

            IsLock[index] = false;
            ItemLock[index] = false;

            InGameUIGroup.InStoreUI.ItemLockBts[index].GetComponent<ButtonScale>().isClick = false;
            InGameUIGroup.InStoreUI.ItemLockBts[index].GetComponent<ButtonScale>().buttonText.color = InGameUIGroup.InStoreUI.ItemLockBts[index].GetComponent<ButtonScale>().Textcolor;
            InGameUIGroup.InStoreUI.ItemLockBts[index].GetComponent<ButtonScale>().buttonImage.color = InGameUIGroup.InStoreUI.ItemLockBts[index].GetComponent<ButtonScale>().Btcolor;

            HasItem[ItemNum] = true;


        }
        else
        {
            SoundManager.Instance.SoundPlay("NoBt", InGameUIGroup.InSound.NoBtSound);
            print("No Money");
        }
    }

    void StoreLockBt(int index)
    {
        // ��ݻ��°� �ƴҶ�
        if (!IsLock[index])
        {
            SoundManager.Instance.SoundPlay("LockBt", InGameUIGroup.InSound.LockBtSound);
            IsLock[index] = true;
            ItemLock[index] = true;
        }
        else if (IsLock[index]) // ��ݻ����϶�
        {
            SoundManager.Instance.SoundPlay("LockBt", InGameUIGroup.InSound.LockBtSound);
            IsLock[index] = false;
            ItemLock[index] = false;
        }

    }

    // ������ �̹��� UI ���
    GameObject AddItemImg(int Itemindex)
    {
        GameObject newSlot = Instantiate(InGameUIGroup.InStoreUI.ItemSlotPrefab, InGameUIGroup.InStoreUI.ItemImgContentTransform);

        switch (ItemGrades[Itemindex])
        {
            case ItemGrade.Basic:
                print("�⺻������");
                newSlot.GetComponent<Image>().color = new Color(116 / 255f, 116 / 255f, 116 / 255f);
                break;
            case ItemGrade.Rare:
                print("���������");
                newSlot.GetComponent<Image>().color = new Color(57 / 255f, 82 / 255f, 98 / 255f);
                break;
            case ItemGrade.Epic:
                print("���Ⱦ�����");
                newSlot.GetComponent<Image>().color = new Color(80 / 255f, 59 / 255f, 100 / 255f);
                break;
        }

        newSlot.transform.GetChild(0).GetComponent<Image>().sprite = InGameUIGroup.InData.ItemImgArr[Itemindex];
        newSlot.name = "Image_" + InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount;

        InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta.x, InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount * 100f); // �̹����� ���̸� 100�̶�� ����
        return newSlot;
    }


    // ���� �� UI ������Ʈ
    string Value;
    float Num;
    void statView()
    {
        // UpgradeStat[i] => 0 ü��
        for (int i = 0; i < InGameUIGroup.InStoreUI.StatArr.Length; i++)
        {
            Value = "";
            Num = 0;
            switch (i)
            {
                //���ۼ�Ʈ ���� ���� : ���ݼӵ�, �̵��ӵ�, �������ӵ�, ��ų��Ÿ��, ü�����, ġ��Ÿ��, ��Ȯ��, ȸ����
                case 0: // �ִ�ü�� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 1: // ü�� ��� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 2: // ü����� +3
                    Value = ((int)UpgradeStat[i]).ToString() + "%";
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 3: // ���ݷ� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 4: // ���߷� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 5: // ��ų�� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 6: // ����� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 7: // ź�� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 8: // ���ݼӵ� +3%
                    float AtSp = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = AtSp.ToString() + "%";
                    Num = AtSp;
                    break;
                case 9: // �̵��ӵ� +3%
                    float MoveSp = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = MoveSp.ToString() + "%";
                    Num = MoveSp;
                    break;
                case 10: // ������ �ӵ� +3%
                    float Reload = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Reload.ToString() + "%";
                    Num = Reload;
                    break;
                case 11: // ��ų��Ÿ�� +3%
                    float SkillCooltime = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = SkillCooltime.ToString() + "%";
                    Num = SkillCooltime;
                    break;
                case 12: // ����ġ ȹ�淮 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 13: //���� +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 14: // ġ��Ÿ +3%
                    float Critical = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Critical.ToString() + "%";
                    Num = Critical;
                    break;
                case 15: // ���߷� +3%
                    float Accuracy = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Accuracy.ToString() + "%";
                    Num = Accuracy;
                    break;
                case 16: // ���� +3%
                    float Range = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Range.ToString() + "%";
                    Num = Range;
                    break;
                case 17: // ȸ���� +3%
                    float Evasion = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Evasion.ToString() + "%";
                    Num = Evasion;
                    break;
            }
            if (Num > 0)
            {
                InGameUIGroup.InStoreUI.StatArr[i].text = "<color=#00FF00>" + Value + "</color>";
                InGameUIGroup.InLvUpUI.LvUpStatTextArr[i].text = "<color=#00FF00>" + Value + "</color>";
                InGameUIGroup.InOverUI.GameOverStatArr[i].text = "<color=#00FF00>" + Value + "</color>";
                InGameUIGroup.InOptionUI.OptionStatArr[i].text = "<color=#00FF00>" + Value + "</color>";
                InGameUIGroup.InRankModeUI.RankModeStatArr[i].text = "<color=#00FF00>" + Value + "</color>";
            }
            else if (Num < 0)
            {
                InGameUIGroup.InStoreUI.StatArr[i].text = "<color=#FF0000>" + Value + "</color>";
                InGameUIGroup.InLvUpUI.LvUpStatTextArr[i].text = "<color=#FF0000>" + Value + "</color>";
                InGameUIGroup.InOverUI.GameOverStatArr[i].text = "<color=#FF0000>" + Value + "</color>";
                InGameUIGroup.InOptionUI.OptionStatArr[i].text = "<color=#FF0000>" + Value + "</color>";
                InGameUIGroup.InRankModeUI.RankModeStatArr[i].text = "<color=#FF0000>" + Value + "</color>";
            }
            else if (Num == 0)
            {

                InGameUIGroup.InStoreUI.StatArr[i].text = "<color=#FFFFFF>" + Value + "</color>";
                InGameUIGroup.InLvUpUI.LvUpStatTextArr[i].text = "<color=#FFFFFF>" + Value + "</color>";
                InGameUIGroup.InOverUI.GameOverStatArr[i].text = "<color=#FFFFFF>" + Value + "</color>";
                InGameUIGroup.InOptionUI.OptionStatArr[i].text = "<color=#FFFFFF>" + Value + "</color>";
                InGameUIGroup.InRankModeUI.RankModeStatArr[i].text = "<color=#FFFFFF>" + Value + "</color>";
            }

        }
    }



    // ------------------������ ���------------------

    // �ڽ� ����
    void randomBoxCreate()
    {
        if (!player.isStore) RandomBoxCurCooltime -= Time.deltaTime;

        if (RandomBoxCurCooltime <= 0)
        {
            int xPox = Random.Range(10, -10);
            int yPox = Random.Range(10, -10);
            Vector3 BoxPos = new Vector3(xPox, 0, yPox);

            GameObject randomBox = Instantiate(InGameUIGroup.InData.RandomBox, BoxPos, Quaternion.identity);
            randomBox.GetComponent<RandomBoxController>().target = player.gameObject;

            if (RandomBoxHPItem) randomBox.GetComponent<RandomBoxController>().curHP = 1;
            else randomBox.GetComponent<RandomBoxController>().curHP = Random.Range(2, 5);

            if (RandomBoxCoinPerItem) randomBox.GetComponent<RandomBoxController>().CoinPer += 3;

            RandomBoxCurCooltime = RandomBoxMaxCoolTime;
        }
    }

    // ������ Ȱ��ȭ ��ɸ���
    void ItemActive()
    {
        //////���� ���ڻ���//////
        if (isLandMine && !isStore)
        {
            LandCurCoolTime -= Time.deltaTime;

            if (LandCurCoolTime <= 0)
            {
                for (int i = 0; i < LandMineCount; i++)
                {
                    float randX = Random.Range(-10, 10);
                    float randZ = Random.Range(-10, 10);
                    Vector3 RandomPos = new Vector3(randX, 0, randZ);
                    Instantiate(InGameUIGroup.InData.LandMinePrefab, RandomPos, Quaternion.identity);
                }
                LandCurCoolTime = LandMaxCoolTime;
            }
        }
        if (GetRandomItem)
        {
            GetRandomItemNum = Random.Range(0, 70);
            InGameUIGroup.InData.ItemPriceArr[GetRandomItemNum] += (int)(InGameUIGroup.InData.ItemPriceArr[GetRandomItemNum] * 1.5f);

            InGameUIGroup.InStoreUI.ItemArr[RandomItemArrNum].SetActive(false);
            InGameUIGroup.InData.itemManager.HasItem(GetRandomItemNum, InGameUIGroup.InStoreUI.ItemInfoPrefab, InGameUIGroup.InStoreUI.ItemInfoContentTransform[RandomItemArrNum]);

            HasItem[GetRandomItemNum] = true;


            GameObject ItemInfo = AddItemImg(GetRandomItemNum);
            //print(ItemInfo);
            ItemInfo iteminfoScript = ItemInfo.GetComponent<ItemInfo>();
            iteminfoScript.ItemName.gameObject.GetComponent<LocalizeScript>().textKey = InGameUIGroup.InData.ItemLocalCode[GetRandomItemNum];
            iteminfoScript.ItemImg.sprite = InGameUIGroup.InData.ItemImgArr[GetRandomItemNum];


            InGameUIGroup.InData.itemManager.ItemInfo(GetRandomItemNum, player, iteminfoScript.infoTextObj, iteminfoScript.ContentTransform);


            GetRandomItem = false;
        }
    }

    // ������ Ȱ��ȭ
    void EmyItem()
    {
        if (EmyBombItem)
        {
            InGameUIGroup.InData.SpawnObj[0].GetComponent<SpawnManager>().IsBomb = true;
        }
    }


    // ������ ��� ����
    void LvUpStatItem()
    {
        HeroStatScriptable Playerstat = player.herodata;
        switch (LvUpStatNum)
        {
            case 0:
                print("0");
                //ü�� ����
                UpgradeStat[0] += 2;
                Playerstat.maxHp += 2;
                break;
            case 1:
                //ü����� ����
                print("1");
                UpgradeStat[1] += 3;
                Playerstat.hpRecovery += 3;
                break;
            case 2:
                print("2");
                //ü����� ����
                UpgradeStat[2] += 5;
                Playerstat.absorption *= 1.05f;
                break;
            case 3:
                print("3");
                //ź�� ����
                UpgradeStat[8] += 6;
                Playerstat.maxbulletCount += 6;
                break;
            case 4:
                print("4");
                //���ݷ� ����
                UpgradeStat[3] += 2;
                Playerstat.damage += 2;
                break;
            case 5:
                print("5");
                //��ų�� ����
                UpgradeStat[6] += 2;
                Playerstat.skillDamage += 2;
                break;
            case 6:
                print("6");
                //�̵��ӵ� ����
                UpgradeStat[10] += 3;
                Playerstat.moveSp *= 1.03f;
                break;
            case 7:
                print("7");
                //���ݼӵ� ����
                UpgradeStat[9] += 3;
                Playerstat.attackSp *= 0.97f;
                break;
            case 8:
                print("8");
                //��ų��Ÿ�� ����
                UpgradeStat[12] += 3;
                Playerstat.skillcurTime *= 0.97f;
                Playerstat.second_skillcurTime *= 0.97f;
                break;
            case 9:
                print("9");
                //���߷� ����
                UpgradeStat[16] += 5;
                Playerstat.accuracy *= 0.95f;
                break;
            case 10:
                print("10");
                //���� ����
                UpgradeStat[17] += 7;
                Playerstat.range *= 1.07f;
                break;
            case 11:
                print("11");
                //���� ����
                UpgradeStat[14] += 3;
                Playerstat.defense += 3;
                break;
            case 12:
                print("12");
                //ġ��Ÿ ����
                UpgradeStat[15] += 5;
                Playerstat.critical += 0.5f;
                break;
            case 13:
                print("13");
                //ȸ���� ����
                UpgradeStat[18] += 5;
                Playerstat.evasion *= 1.05f;
                break;
            case 14:
                print("14");
                //����ġ ȹ��� ����
                UpgradeStat[13] += 3;
                Playerstat.hasExp *= 1.03f;
                break;
            case 15:
                print("15");
                //����ġ ��� 30% ȹ��
                Playerstat.curExp = Playerstat.curExp + Playerstat.curExp * 0.3f;
                break;
            case 16:
                print("16");
                // ��� ����
                UpgradeStat[7] += 3;
                Playerstat.science += 3;
                break;
            case 17:
                print("17");
                // ���߷� ����
                UpgradeStat[5] += 3;
                Playerstat.bombDamage += 3;
                break;
            case 18:
                print("18");
                // �������ð� ����
                UpgradeStat[11] += 5;
                Playerstat.reloadTime *= 0.95f;
                break;
        }
    }
    void Localizezation(TextMeshProUGUI text, string str)
    {
        //print(str);
        text.GetComponent<LocalizeScript>().textKey = str;
        text.GetComponent<LocalizeScript>().DynamicLocal();
    }
}

