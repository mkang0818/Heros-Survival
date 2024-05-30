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

    [SerializeField] Texture2D defaultImg; // 바꿀 커서 이미지
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
    private int ItemActiveNum = 34; //일반 34,  레어 68, 희귀 90
    private int LvUpStatNum = -1;
    private int[] ItemMulti = { 21, 23, 24, 31, 45, 49, 50, 51, 52, 66, 69, 90 }; //중복 방지 아이템
    private int[] randItem = { -1, -1, -1, -1 }; //각 버튼 아이템 번호
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
    public int MaxRerollMoney = 1; //리롤 재화
    public int CurRerollMoney = 1; //리롤 재화
    public int PlusRerollMoney = 0; // 리롤 시 추가되는 재화


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


    // ------------------게임시스템------------------

    // 재화
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

    // 스테이지 시간
    void TimeManager()
    {
        //print("현재시간"+CurTimer);

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
        else if (InGameUIGroup.InRankModeUI.BossStage && IsBossDead && !isStore) //보스스테이지, 보스죽었을 때
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

    // 레벨업, 상점 UI 이동
    void SelectUI()
    {
        if (InGameUIGroup.InRankModeUI.BossStage && IsBossDead)
        {
            print("랭크모드 시작");
            OnRankModeUI();
        }
        else if (lvUpCount >= 0) //레벨업 ui on
        {
            print("레벨업ui");
            CurRerollMoney = MaxRerollMoney; // 스테이지에 맞는 리롤 재화 초기화
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
                InGameUIGroup.InLvUpUI.LvUpContentTransform[i].GetComponent<RectTransform>().sizeDelta = new Vector2(InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta.x, InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount * 100f); // 이미지의 높이를 100이라고 가정
            }

            ////////////////////리롤재화 추가 임시로 상점재화로
            InGameUIGroup.InLvUpUI.LvUpRerollPriceText.text = CurRerollMoney.ToString();


            for (int i = 0; i < InGameUIGroup.InLvUpUI.LvUpItemNameText.Length; i++)
            {
                Localizezation(InGameUIGroup.InLvUpUI.LvUpItemNameText[i], InGameUIGroup.InData.LvUpItemLocalCode[LvUpItemNum[i]].ToString());
            }
            InGameUIGroup.InPlayUI.PlayUI.SetActive(false);
            InGameUIGroup.InLvUpUI.LvUpUI.SetActive(true);
        }
        else if (lvUpCount <= -1) //상점 ui on
        {
            print("상점ui");
            CurRerollMoney = MaxRerollMoney; // 스테이지에 맞는 리롤 재화 초기화
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

    // 스테이지 종료 후 오브젝트 모두 삭제
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


    //다음 스테이지 시작_버튼
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
        //print("현재 리롤금액" + CurRerollMoney);
        //print("최대 리롤금액" + MaxRerollMoney);
    }

    // 스테이지 시작 시 스폰오브젝트 활성화
    void Spawn()
    {
        //print("현재 스테이지 : "+ StageNum);
        InGameUIGroup.InPlayUI.PlayUI.SetActive(true);
        //스테이지가 늘어날수록 낮은레벨 몬스터는 적게 높은레벨 몬스터는 많게
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

            //시간 무제한으로 변경
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
            //print(i+"스폰 활성화");
            InGameUIGroup.InData.SpawnObj[i].SetActive(true);
            InGameUIGroup.InData.SpawnObj[i].GetComponent<SpawnManager>().StartCoroutine("spawnMosnter");
            InGameUIGroup.InData.SpawnObj[i].GetComponent<SpawnManager>().target = player.gameObject;
            InGameUIGroup.InData.SpawnObj[i].GetComponent<SpawnManager>().level *= 0.93f;
        }
    }
    // 레벨업 버튼 기능
    public void LvUpBt(int index)
    {
        lvUpCount -= 1;
        LvUpStatNum = LvUpItemNum[index];
        LvUpStatItem();
        InGameUIGroup.InLvUpUI.LvUpUI.SetActive(false);
        SelectUI();
        CurRerollMoney = MaxRerollMoney;
    }

    // 게임 오버 시
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
    // ------------------상점------------------

    // 아이템 중복 방지 기능
    private List<int> usedNumbers = new List<int>();
    int GetUniqueRandomNumber()
    {
        int randomNumber;
        do
        {
            // 0부터 ItemActiveNum까지의 랜덤 번호 생성
            randomNumber = Random.Range(0, ItemActiveNum);
        } while (usedNumbers.Contains(randomNumber) || CheckIfNumberExistsInArray(randomNumber)); // 이미 사용된 번호인지 확인

        // 생성된 번호를 사용된 리스트에 추가
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
                    //print("중복됨");
                    return true;
                }
            }
        }
        //print("중복 x");
        return false;
    }

    // 상점 UI 업데이트
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


        //아이템 정보 초기화
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

    // 상점 리롤 기능
    public void Reroll()
    {
        if (StoreTicket)
        {
            int temp = CurRerollMoney;
            CurRerollMoney = 0;
            for (int i = 0; i < 4; i++) InGameUIGroup.InStoreUI.ItemArr[i].SetActive(true);

            // 1. 2. 3. 4. 아이템 랜덤으로 생성
            for (int i = 0; i < randItem.Length; i++)
            {
                if (!ItemLock[i])
                {
                    randItem[i] = GetUniqueRandomNumber();
                }
            }


            //text에 나타내기
            StoreUIUpdate();

            StoreTicket = false;
            CurRerollMoney = temp;
        }
        else if (money >= CurRerollMoney)
        {
            money -= CurRerollMoney;
            print("현재 리롤금액" + CurRerollMoney);
            CurRerollMoney += PlusRerollMoney; // 리롤 시 리롤 재화 증가
            print("현재 리롤금액" + CurRerollMoney);

            SoundManager.Instance.SoundPlay("IngameBt", InGameUIGroup.InSound.BuyBtSound);

            for (int i = 0; i < 4; i++) InGameUIGroup.InStoreUI.ItemArr[i].SetActive(true);

            // 1. 2. 3. 4. 아이템 랜덤으로 생성
            for (int i = 0; i < randItem.Length; i++)
            {
                if (!ItemLock[i])
                {
                    randItem[i] = GetUniqueRandomNumber();
                }
            }

            //text에 나타내기
            StoreUIUpdate();
        }

        usedNumbers.Clear();
    }

    // 레벨업 리롤기능
    public void LvUpReroll()
    {
        if (money >= CurRerollMoney)
        {
            money -= CurRerollMoney;
            CurRerollMoney += PlusRerollMoney; // 리롤 시 리롤 재화 증가
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
                InGameUIGroup.InLvUpUI.LvUpContentTransform[i].GetComponent<RectTransform>().sizeDelta = new Vector2(InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta.x, InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount * 100f); // 이미지의 높이를 100이라고 가정
            }

            ////////////////////리롤재화 추가 임시로 상점재화로
            InGameUIGroup.InLvUpUI.LvUpRerollPriceText.text = CurRerollMoney.ToString();

            for (int i = 0; i < InGameUIGroup.InLvUpUI.LvUpItemNameText.Length; i++)
            {
                Localizezation(InGameUIGroup.InLvUpUI.LvUpItemNameText[i], InGameUIGroup.InData.LvUpItemLocalCode[LvUpItemNum[i]].ToString());
            }
        }
    }

    // 아이템 구매 버튼 기능
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

            InGameUIGroup.InData.ItemPriceArr[randItem] += (int)(InGameUIGroup.InData.ItemPriceArr[randItem] * 1.5f); // 가격 증가

            InGameUIGroup.InStoreUI.ItemArr[index].SetActive(false); // 
            ItemNum = randItem;
            InGameUIGroup.InData.itemManager.HasItem(ItemNum, InGameUIGroup.InStoreUI.ItemInfoPrefab, InGameUIGroup.InStoreUI.ItemInfoContentTransform[index]);

            // 아이템 정보 UI 업데이트
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
        // 잠금상태가 아닐때
        if (!IsLock[index])
        {
            SoundManager.Instance.SoundPlay("LockBt", InGameUIGroup.InSound.LockBtSound);
            IsLock[index] = true;
            ItemLock[index] = true;
        }
        else if (IsLock[index]) // 잠금상태일때
        {
            SoundManager.Instance.SoundPlay("LockBt", InGameUIGroup.InSound.LockBtSound);
            IsLock[index] = false;
            ItemLock[index] = false;
        }

    }

    // 아이템 이미지 UI 기능
    GameObject AddItemImg(int Itemindex)
    {
        GameObject newSlot = Instantiate(InGameUIGroup.InStoreUI.ItemSlotPrefab, InGameUIGroup.InStoreUI.ItemImgContentTransform);

        switch (ItemGrades[Itemindex])
        {
            case ItemGrade.Basic:
                print("기본아이템");
                newSlot.GetComponent<Image>().color = new Color(116 / 255f, 116 / 255f, 116 / 255f);
                break;
            case ItemGrade.Rare:
                print("레어아이템");
                newSlot.GetComponent<Image>().color = new Color(57 / 255f, 82 / 255f, 98 / 255f);
                break;
            case ItemGrade.Epic:
                print("에픽아이템");
                newSlot.GetComponent<Image>().color = new Color(80 / 255f, 59 / 255f, 100 / 255f);
                break;
        }

        newSlot.transform.GetChild(0).GetComponent<Image>().sprite = InGameUIGroup.InData.ItemImgArr[Itemindex];
        newSlot.name = "Image_" + InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount;

        InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta = new Vector2(InGameUIGroup.InStoreUI.ItemImgContentTransform.GetComponent<RectTransform>().sizeDelta.x, InGameUIGroup.InStoreUI.ItemImgContentTransform.childCount * 100f); // 이미지의 높이를 100이라고 가정
        return newSlot;
    }


    // 스탯 뷰 UI 업데이트
    string Value;
    float Num;
    void statView()
    {
        // UpgradeStat[i] => 0 체력
        for (int i = 0; i < InGameUIGroup.InStoreUI.StatArr.Length; i++)
        {
            Value = "";
            Num = 0;
            switch (i)
            {
                //몇퍼센트 증가 변수 : 공격속도, 이동속도, 재장전속도, 스킬쿨타임, 체력흡수, 치명타율, 정확도, 회피율
                case 0: // 최대체력 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 1: // 체력 재생 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 2: // 체력흡수 +3
                    Value = ((int)UpgradeStat[i]).ToString() + "%";
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 3: // 공격력 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 4: // 폭발력 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 5: // 스킬력 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 6: // 기술력 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 7: // 탄약 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 8: // 공격속도 +3%
                    float AtSp = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = AtSp.ToString() + "%";
                    Num = AtSp;
                    break;
                case 9: // 이동속도 +3%
                    float MoveSp = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = MoveSp.ToString() + "%";
                    Num = MoveSp;
                    break;
                case 10: // 재장전 속도 +3%
                    float Reload = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Reload.ToString() + "%";
                    Num = Reload;
                    break;
                case 11: // 스킬쿨타임 +3%
                    float SkillCooltime = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = SkillCooltime.ToString() + "%";
                    Num = SkillCooltime;
                    break;
                case 12: // 경험치 획득량 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 13: //방어력 +3
                    Value = ((int)UpgradeStat[i]).ToString();
                    Num = ((int)UpgradeStat[i]);
                    break;
                case 14: // 치명타 +3%
                    float Critical = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Critical.ToString() + "%";
                    Num = Critical;
                    break;
                case 15: // 명중률 +3%
                    float Accuracy = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Accuracy.ToString() + "%";
                    Num = Accuracy;
                    break;
                case 16: // 범위 +3%
                    float Range = Mathf.Floor(UpgradeStat[i] * 10) / 10;
                    Value = Range.ToString() + "%";
                    Num = Range;
                    break;
                case 17: // 회피율 +3%
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



    // ------------------아이템 기능------------------

    // 박스 생성
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

    // 아이템 활성화 기능모음
    void ItemActive()
    {
        //////랜덤 지뢰생성//////
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

    // 아이템 활성화
    void EmyItem()
    {
        if (EmyBombItem)
        {
            InGameUIGroup.InData.SpawnObj[0].GetComponent<SpawnManager>().IsBomb = true;
        }
    }


    // 레벨업 기능 구현
    void LvUpStatItem()
    {
        HeroStatScriptable Playerstat = player.herodata;
        switch (LvUpStatNum)
        {
            case 0:
                print("0");
                //체력 증가
                UpgradeStat[0] += 2;
                Playerstat.maxHp += 2;
                break;
            case 1:
                //체력재생 증가
                print("1");
                UpgradeStat[1] += 3;
                Playerstat.hpRecovery += 3;
                break;
            case 2:
                print("2");
                //체력흡수 증가
                UpgradeStat[2] += 5;
                Playerstat.absorption *= 1.05f;
                break;
            case 3:
                print("3");
                //탄약 증가
                UpgradeStat[8] += 6;
                Playerstat.maxbulletCount += 6;
                break;
            case 4:
                print("4");
                //공격력 증가
                UpgradeStat[3] += 2;
                Playerstat.damage += 2;
                break;
            case 5:
                print("5");
                //스킬력 증가
                UpgradeStat[6] += 2;
                Playerstat.skillDamage += 2;
                break;
            case 6:
                print("6");
                //이동속도 증가
                UpgradeStat[10] += 3;
                Playerstat.moveSp *= 1.03f;
                break;
            case 7:
                print("7");
                //공격속도 증가
                UpgradeStat[9] += 3;
                Playerstat.attackSp *= 0.97f;
                break;
            case 8:
                print("8");
                //스킬쿨타임 감소
                UpgradeStat[12] += 3;
                Playerstat.skillcurTime *= 0.97f;
                Playerstat.second_skillcurTime *= 0.97f;
                break;
            case 9:
                print("9");
                //명중률 증가
                UpgradeStat[16] += 5;
                Playerstat.accuracy *= 0.95f;
                break;
            case 10:
                print("10");
                //범위 증가
                UpgradeStat[17] += 7;
                Playerstat.range *= 1.07f;
                break;
            case 11:
                print("11");
                //방어력 증가
                UpgradeStat[14] += 3;
                Playerstat.defense += 3;
                break;
            case 12:
                print("12");
                //치명타 증가
                UpgradeStat[15] += 5;
                Playerstat.critical += 0.5f;
                break;
            case 13:
                print("13");
                //회피율 증가
                UpgradeStat[18] += 5;
                Playerstat.evasion *= 1.05f;
                break;
            case 14:
                print("14");
                //경험치 획득률 증가
                UpgradeStat[13] += 3;
                Playerstat.hasExp *= 1.03f;
                break;
            case 15:
                print("15");
                //경험치 즉시 30% 획득
                Playerstat.curExp = Playerstat.curExp + Playerstat.curExp * 0.3f;
                break;
            case 16:
                print("16");
                // 기술 증가
                UpgradeStat[7] += 3;
                Playerstat.science += 3;
                break;
            case 17:
                print("17");
                // 폭발력 증가
                UpgradeStat[5] += 3;
                Playerstat.bombDamage += 3;
                break;
            case 18:
                print("18");
                // 재장전시간 감소
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

