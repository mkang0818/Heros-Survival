using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Text;
using BackEnd;


[System.Serializable]
public class MainUI
{
    public GameObject MainCam;

    public GameObject MainPopUp;
    public GameObject BackGroundObj;
    public GameObject[] MainCharactorPrefab = new GameObject[14];

    public AudioSource MusicAudio;
    public Texture2D defaultImg; // �ٲ� Ŀ�� �̹���
}
[System.Serializable]
public class CharSelectUI
{
    public GameObject CHarSelectPopup;
    public GameObject[] SelectCharBt = new GameObject[14];
    public GameObject[] selectCharactorPrefab = new GameObject[14];
    public GameObject[] CharactorInfoObj;
    public GameObject SelectStars;

    public TextMeshProUGUI SelectUICharName;
    public GameObject selectObj;
}
[System.Serializable]
public class StoreUI
{
    public GameObject StorePopup;
    public GameObject[] UpGradeCHaractorButtons = new GameObject[14];
    public GameObject CharInfoUI;
    public GameObject CharBuyUI;
    public GameObject[] InfoStars;
    
    public GameObject StoreSelectUI;
    public GameObject CharListUI;
    public GameObject ItemListUI;

    public Image InfoUIImg;
    public TextMeshProUGUI InfoUIName;
    public TextMeshProUGUI InfoUICharInfo;
    public Image BuyUIImg;
    public TextMeshProUGUI BuyUIName;
    public TextMeshProUGUI BuyUIPrice;
    public TextMeshProUGUI[] DiaText = new TextMeshProUGUI[2];
}
[System.Serializable]
public class UpGradeUI
{
    public GameObject UpgradePopup;
    public GameObject RightPopup;
    public GameObject ResultPopup;
    public GameObject UpgradeStage;
    public GameObject[] GradeStars;
    public GameObject[] PopupStars;
    public GameObject[] GauseEffect_GameObject;
    public GameObject[] ResultEffect;
    public GameObject[] FireObject;
    public Transform[] GaugeEffect;


    public TextMeshProUGUI UpgradeCharNameText;
    public TextMeshProUGUI UpgradePerText;
    public TextMeshProUGUI UpgradePriceText;
    public TextMeshProUGUI ResultText;

    public Sprite[] CharImgSprite;
    public Image CharImg;

    [HideInInspector] public bool IsUpgradeBt = false;
}
[System.Serializable]
public class RankUI
{
    public GameObject RankPopup;

    public TextMeshProUGUI myRank;
    public TextMeshProUGUI myRankScore;
    public TextMeshProUGUI myNickName;
    public Image myCharImg;
    public Image[] RankTopCharImg = new Image[10];
    public Sprite[] CharFaceImg = new Sprite[14];
    public TextMeshProUGUI[] RankTopScore = new TextMeshProUGUI[10];
    public TextMeshProUGUI[] RankTopNickName = new TextMeshProUGUI[10];

    public Image MyRankCountryImg;
    public Image[] RankListCountryImg;
    public Sprite[] CountryIcon;
}
[System.Serializable]
public class OptionUI
{
    public GameObject OptionPopup;

    public Toggle isAIBt;
    public Slider MusicSoundSlider;
    public Slider EffectSoundSlider;
    public TextMeshProUGUI MusicValueText;
    public TextMeshProUGUI EffectAudioValueText;
}
[System.Serializable]
public class Data
{
    public int[] CharPrice;
    public int[] GradePrice = new int[10];
    public int[] GradePer = new int[5];
    public string[] CharNameLocalCode = new string[14];
    public string[] CharInfoArr = new string[14];
}
[System.Serializable]
public class MainSceneUI
{
    public MainUI Main;
    public CharSelectUI CharSelect;
    public StoreUI Store;
    public UpGradeUI Upgrade;
    public RankUI Rank;
    public OptionUI Option;
    public Data data;
}

public class UIManager : MonoBehaviour
{
    public MainSceneUI MainUIGroup;

    float MusicSoundValue;
    float EffectSoundValue;
    GameObject selectChar;
    GameObject UpgradeChar;

    bool IsUpgrade = false;
    bool isSelect = false;
    int SelectCharNum;
    int UpgradeCHarNum = -1;

    Button GetComponentBt;
    ButtonScale ButtonScaleScript;
    LocalizeScript LocalScript;
    // Start is called before the first frame update
    void Start()
    {
        Setting();
        Component();
    }
    private void Component()
    {
        GetComponentBt = GetComponent<Button>();
        ButtonScaleScript = GetComponent<ButtonScale>();
        LocalScript = GetComponent<LocalizeScript>();
    }
    private void Setting()
    {
        Cursor.SetCursor(MainUIGroup.Main.defaultImg, Vector2.zero, CursorMode.ForceSoftware);
        MainUIGroup.Option.isAIBt.isOn = GameManager.Instance.IsAI;

        SoundManager.Instance.MusicAudio = MainUIGroup.Main.MusicAudio;
        MainUIGroup.Main.MusicAudio.volume = SoundManager.Instance.MusicVolume;
        MainUIGroup.Option.MusicSoundSlider.value = SoundManager.Instance.MusicVolume;
        MainUIGroup.Option.EffectSoundSlider.value = SoundManager.Instance.EffectVolume;
    }
    private void Update()
    {
        ESCBack();
        MainTextUI();
    }
    void ESCBack()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsUpgrade)
        {
            OnMainUI();
            DataManager.Instance.SaveData();
        }
    }
    public void InOptionUI() { MainUIGroup.Option.OptionPopup.SetActive(true); MainUIGroup.Main.MainPopUp.SetActive(false); }
    public void OffOptionUI() { MainUIGroup.Option.OptionPopup.SetActive(false);MainUIGroup.Main.MainPopUp.SetActive(true); }
    public void SelectStart() { MainUIGroup.CharSelect.CHarSelectPopup.SetActive(true); MainUIGroup.Main.MainPopUp.SetActive(false); MainUIGroup.CharSelect.SelectUICharName.text = ""; SelecthasUpdate(); }
    public void InRankUI() { MainUIGroup.Rank.RankPopup.SetActive(true); MainUIGroup.Main.MainPopUp.SetActive(false); RankUpdate(); MyRank(); }

    public void BtCharListOn()
    {
        MainUIGroup.Store.StoreSelectUI.SetActive(false);
        MainUIGroup.Store.CharListUI.SetActive(true);
        MainUIGroup.Store.ItemListUI.SetActive(false);
    }
    public void BtItemListOn()
    {
        MainUIGroup.Store.StoreSelectUI.SetActive(false);
        MainUIGroup.Store.CharListUI.SetActive(false);
        MainUIGroup.Store.ItemListUI.SetActive(true);
    }
    public void OnMainUI()
    {
        PlayerPrefs.Save();
        DataManager.Instance.SaveData();
        IsUpgrade = false;
        MainUIGroup.Main.MainPopUp.SetActive(true); MainUIGroup.CharSelect.CHarSelectPopup.SetActive(false);
        MainUIGroup.Store.StoreSelectUI.SetActive(true); MainUIGroup.Store.ItemListUI.SetActive(false); MainUIGroup.Store.CharListUI.SetActive(false);
        MainUIGroup.Upgrade.UpgradePopup.SetActive(false); MainUIGroup.Store.StorePopup.SetActive(false); MainUIGroup.Option.OptionPopup.SetActive(false); MainUIGroup.Store.StorePopup.SetActive(false); MainUIGroup.Rank.RankPopup.SetActive(false);
        //Destroy(UpgradeChar);
        MainUIGroup.Main.BackGroundObj.SetActive(true);
        MainUIGroup.Upgrade.UpgradeStage.SetActive(false);

        OffStatView();
        if (selectChar != null) Destroy(selectChar);
        MainUIGroup.Main.MainCam.transform.DOMoveY(1.94f, 1f);
    }

    public void InStoretUI() { MainUIGroup.Store.StorePopup.SetActive(true); MainUIGroup.Main.MainPopUp.SetActive(false); CharUIhasUpdate(); }
    public void UpgradeBackBt()
    {
        IsUpgrade = false;
        MainUIGroup.Main.MainCam.transform.DOMoveY(1.94f, 1f);
        MainUIGroup.Upgrade.ResultPopup.SetActive(false);
        MainUIGroup.Upgrade.RightPopup.SetActive(true);
        MainUIGroup.Upgrade.UpgradePopup.SetActive(false); MainUIGroup.Store.StorePopup.SetActive(true);
        if (UpgradeChar != null) Destroy(UpgradeChar); 
        MainUIGroup.Upgrade.UpgradeStage.SetActive(false);

        for (int i = 0; i < MainUIGroup.Upgrade.FireObject.Length; i++) MainUIGroup.Upgrade.FireObject[i].SetActive(false);
    }

    void MyRank()
    {
        //Debug.Log("���� ��ȸ");
        string rankUUID = "5ca6bd20-e954-11ee-9d2b-6d64e7ab239c"; // ���� : "4088f640-693e-11ed-ad29-ad8f0c3d4c70"
        var bro = Backend.URank.User.GetMyRank(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��ȸ�� ������ �߻��߽��ϴ�. : " + bro);
        }
        else
        {
            //Debug.Log("��ŷ ��ȸ�� �����߽��ϴ�. : " + bro);

            //Debug.Log("�� ��ŷ ��� ���� �� : " + bro.GetFlattenJSON()["totalCount"].ToString());

            foreach (LitJson.JsonData jsonData in bro.FlattenRows())
            {
                StringBuilder info = new StringBuilder();

                info.AppendLine("���� : " + jsonData["rank"].ToString());
                info.AppendLine("�г��� : " + jsonData["nickname"].ToString());
                info.AppendLine("���� : " + jsonData["score"].ToString());
                info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
                info.AppendLine("���Ĺ�ȣ : " + jsonData["index"].ToString());

                string[] extraData = jsonData["atk"].ToString().Split("|");
                CountryNum = extraData[0];
                CharNum = extraData[1];

                info.AppendLine("�����ȣ : " + CountryNum);
                info.AppendLine("������ȣ : " + CharNum);
                info.AppendLine();
                //Debug.Log(info);
                MainUIGroup.Rank.myCharImg.sprite = MainUIGroup.Rank.CharFaceImg[int.Parse(CharNum) - 1];
                MainUIGroup.Rank.MyRankCountryImg.sprite = MainUIGroup.Rank.CountryIcon[SteamManager.instance.CountryNum];

                MainUIGroup.Rank.myRank.text = ((int)jsonData["rank"]).ToString();
                MainUIGroup.Rank.myNickName.text = jsonData["nickname"].ToString();
                MainUIGroup.Rank.myRankScore.text = jsonData["score"].ToString();
            }
        }
    }
    string CountryNum;
    string CharNum;
    public void RankUpdate()
    {
        Debug.Log("��ŷ ��ȸ");

        string rankUUID = "5ca6bd20-e954-11ee-9d2b-6d64e7ab239c"; // ��ŷ�� UUID �� ����
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (!bro.IsSuccess())
        {
            Debug.LogError("��ŷ ��ȸ�� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        int count = 0; // ��µ� ��ŷ ���� ���� ���� ����

        // ��ŷ ����Ʈ ��� (10������)
        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            if (count >= 10)
                break; // 10���� ����� �� ������ �����մϴ�.

            StringBuilder info = new StringBuilder();

            info.AppendLine("���� : " + jsonData["rank"].ToString());
            info.AppendLine("�г��� : " + jsonData["nickname"].ToString());
            info.AppendLine("���� : " + jsonData["score"].ToString());
            info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
            info.AppendLine("���Ĺ�ȣ : " + jsonData["index"].ToString());

            string[] extraData = jsonData["atk"].ToString().Split("|");
            CountryNum = extraData[0];
            CharNum = extraData[1];


            info.AppendLine("�����ȣ : " + CountryNum);
            info.AppendLine("������ȣ : " + CharNum);
            info.AppendLine();

            Debug.Log(info);
            MainUIGroup.Rank.RankTopScore[count].text = jsonData["score"].ToString();
            MainUIGroup.Rank.RankTopNickName[count].text = jsonData["nickname"].ToString();
            //print((int)jsonData["atk"]-1);
            MainUIGroup.Rank.RankTopCharImg[count].sprite = MainUIGroup.Rank.CharFaceImg[int.Parse(CharNum) - 1];

            MainUIGroup.Rank.RankListCountryImg[count].sprite = MainUIGroup.Rank.CountryIcon[SteamManager.instance.CountryNum];
            count++; // ��ŷ �� ����
        }
    }
    void SelecthasUpdate()
    {
        MainUIGroup.Main.BackGroundObj.SetActive(false);
        // ��Ʈ : �ν��Ͻ� ��Ī�� �����ϴ°� �����ϴ�
        bool[] IsChar = DataManager.Instance.nowPlayer.IsChar;

        for (int i = 0; i < IsChar.Length; i++)
        {
            if (IsChar[i]) // ���� ����
            {
                // ��Ʈ : �ϳ��� ��ü�� �����Ǵ� ���� Ŭ������ �����ؼ� ����ϴ°� �����ϴ�
                // ��Ʈ : Ư�� GetChild ����� �����ϴ°� �����ϴ�. �ֳ��ϸ� �ν����� ������ �ٲ�� ��� ������ ���� ������ �����ϴ�.
                // ��Ʈ : GetComponent ����� �ִ��� �����ϴ� �������� �����ϴ°� �����ϴ� GetComponent�� �������� ȣ�� ����� ���� �Լ��Դϴ�
                MainUIGroup.CharSelect.SelectCharBt[i].transform.GetChild(0).gameObject.SetActive(true);
                MainUIGroup.CharSelect.SelectCharBt[i].GetComponent<ButtonScale>().enabled = true;
                MainUIGroup.CharSelect.SelectCharBt[i].GetComponent<Button>().enabled = true;
                //print(i);
            }
            else // �̺��� ����
            {
                MainUIGroup.CharSelect.SelectCharBt[i].transform.GetChild(0).gameObject.SetActive(false);
                MainUIGroup.CharSelect.SelectCharBt[i].GetComponent<ButtonScale>().enabled = false;
                MainUIGroup.CharSelect.SelectCharBt[i].GetComponent<Button>().enabled = false;
                //print(i + "��Ȱ��ȭ");
            }
        }
    }
    // �����ͷκ��� ������ ĳ���� UI Ȱ��ȭ
    void CharUIhasUpdate()
    {
        MainUIGroup.Main.BackGroundObj.SetActive(false);
        bool[] IsChar = DataManager.Instance.nowPlayer.IsChar;

        for (int i = 3; i < MainUIGroup.Store.UpGradeCHaractorButtons.Length; i++)
        {
            MainUIGroup.Store.UpGradeCHaractorButtons[i].transform.GetChild(0).gameObject.SetActive(false);
            MainUIGroup.Store.UpGradeCHaractorButtons[i].transform.GetChild(1).gameObject.SetActive(false);
        }

        for (int i = 3; i < IsChar.Length; i++)
        {
            if (IsChar[i])
            {
                MainUIGroup.Store.UpGradeCHaractorButtons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                MainUIGroup.Store.UpGradeCHaractorButtons[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    public void UpgradeCharSelectBt(int CharNum)
    {
        SelectCharNum = CharNum;

        if (DataManager.Instance.nowPlayer.IsChar[SelectCharNum])
        {
            MainUIGroup.Store.CharBuyUI.SetActive(false);
            MainUIGroup.Store.CharInfoUI.SetActive(true);
            UpgradeStars(MainUIGroup.Store.InfoStars, DataManager.Instance.nowPlayer.CharGrade[CharNum]);

            print("������");
            MainUIGroup.Store.InfoUIImg.sprite = MainUIGroup.Rank.CharFaceImg[SelectCharNum];
            MainUIGroup.Store.InfoUIName.GetComponent<LocalizeScript>().textKey = MainUIGroup.data.CharNameLocalCode[SelectCharNum];
            MainUIGroup.Store.InfoUIName.GetComponent<LocalizeScript>().LocalizeChanged();
            MainUIGroup.Store.InfoUICharInfo.GetComponent<LocalizeScript>().textKey = MainUIGroup.data.CharInfoArr[SelectCharNum];

        }
        else
        {
            MainUIGroup.Store.CharInfoUI.SetActive(false);
            MainUIGroup.Store.CharBuyUI.SetActive(true);
            print("�̺�����");
            MainUIGroup.Store.BuyUIImg.sprite = MainUIGroup.Rank.CharFaceImg[SelectCharNum];
            MainUIGroup.Store.BuyUIName.GetComponent<LocalizeScript>().textKey = MainUIGroup.data.CharNameLocalCode[SelectCharNum];
            MainUIGroup.Store.BuyUIName.GetComponent<LocalizeScript>().LocalizeChanged();
            MainUIGroup.Store.BuyUIPrice.text = MainUIGroup.data.CharPrice[SelectCharNum].ToString();
        }
    }
    // ��ȭ�� ĳ���� ����
    public void InUpgradeUI()
    {
        MainUIGroup.Main.MainCam.transform.DOMoveY(3.22f,1f);

        MainUIGroup.Upgrade.UpgradePopup.SetActive(true);
        MainUIGroup.Upgrade.RightPopup.SetActive(true);
        MainUIGroup.Upgrade.ResultPopup.SetActive(false);
        MainUIGroup.Store.StorePopup.SetActive(false);
        MainUIGroup.Upgrade.UpgradeStage.SetActive(true);
        MainUIGroup.Upgrade.CharImg.sprite = MainUIGroup.Upgrade.CharImgSprite[SelectCharNum];

        MainUIGroup.Upgrade.UpgradeCharNameText.GetComponent<LocalizeScript>().textKey = MainUIGroup.data.CharNameLocalCode[SelectCharNum];
        MainUIGroup.Upgrade.UpgradeCharNameText.GetComponent<LocalizeScript>().DynamicLocal();


        for (int i = 0; i < MainUIGroup.Upgrade.ResultEffect.Length; i++) MainUIGroup.Upgrade.ResultEffect[i].SetActive(false);
        for (int i = 0; i < MainUIGroup.Upgrade.FireObject.Length; i++) MainUIGroup.Upgrade.FireObject[i].SetActive(false);

        if (DataManager.Instance.nowPlayer.IsChar[SelectCharNum] && !IsUpgrade)
        { 
            // ��Ʈ : ��ü �ı��� ���� ���ٴ� �����͸� ���Ƴ���� ������ �����ϴ°� �����ϴ�
            // ��Ʈ : ��Ȱ������ ������ �������� ���� ���Դϴ�
            //if (UpgradeChar != null) Destroy(UpgradeChar);
            UpgradeCHarNum = SelectCharNum;

            // ��Ʈ : Vector3�� ���� ������ ����� �Է��ϱ� ���ٴ� � �ǹ̸� ��� �������� �����ؼ� ����ϴ°� �������� �����ϴ�
            // ��Ʈ : ex) private readonly Vector3 SpawnPos = new Vector3(0, 3.35f, 2.52f);

            //MainUIGroup.CharSelect.selectCharactorPrefab[SelectCharNum].SetActive(true);
            Vector3 SpawnPos = new Vector3(0, 1.33f, 4.36f);
            UpgradeChar = Instantiate(MainUIGroup.CharSelect.selectCharactorPrefab[SelectCharNum], SpawnPos, Quaternion.Euler(0f, 194f, 0f));
            UpgradeText(DataManager.Instance.nowPlayer.CharGrade[SelectCharNum], SelectCharNum);
        }
    }
    void UpgradeText(int Grade, int charnum)
    {
        print("���� ��ȭ�ܰ�" + DataManager.Instance.nowPlayer.CharGrade[charnum]);
        UpgradeStars(MainUIGroup.Upgrade.PopupStars, DataManager.Instance.nowPlayer.CharGrade[charnum]);

        var CharGradeNum = DataManager.Instance.nowPlayer.CharGrade[charnum] - 1;
        MainUIGroup.Upgrade.UpgradePerText.text = " : " + MainUIGroup.data.GradePer[CharGradeNum].ToString() + "%";
        MainUIGroup.Upgrade.UpgradePriceText.text = " : $" + MainUIGroup.data.GradePrice[CharGradeNum].ToString();
    }
    //��ȭ ��ư
    public void UpgradeBt()
    {
        if (!MainUIGroup.Upgrade.IsUpgradeBt)
        {
            MainUIGroup.Upgrade.IsUpgradeBt = true;
            IsUpgrade = true;

            MainUIGroup.Upgrade.RightPopup.SetActive(false);
            int grade = DataManager.Instance.nowPlayer.CharGrade[UpgradeCHarNum];

            int GradeIndex = grade >= 1 && grade <= 9 ? grade : 10;
            StartCoroutine(UpgradeResult(GradeIndex, UpgradeCHarNum));
        }
        else
        {
            print("��ȭ��ư xx");
        }
    }
    IEnumerator UpgradeResult(int Grade, int CharNum)
    {
        MainUIGroup.Upgrade.RightPopup.SetActive(false);
        MainUIGroup.Upgrade.IsUpgradeBt = false;

        //RightUI �����

        //if (GradePrice[Grade - 1] <= DataManager.Instance.nowPlayer.Diamond)
        if (true)
        {
            // ��Ʈ : if�� ��� �� else�� ���ǿ� ���� else�� ���� �� �����ϴ� �������� ���� if������ ����ϴ°� ����մϴ�
            /* 
             * ��Ʈ : ex) 
             * - ���� else��
             * if (Grade >= 10)
             * {
             *    print("�� ����");
             *    yield break;
             * }
             * 
             * - ���� if�� ���� ���
             */
            if (Grade < 10)
            {
                DataManager.Instance.nowPlayer.Diamond -= MainUIGroup.data.GradePrice[Grade - 1];

                int per;
                per = Random.Range(0, 100);
                if (per <= MainUIGroup.data.GradePer[Grade - 1])
                {
                    StartCoroutine(UpgradeEffect(true, DataManager.Instance.nowPlayer.CharGrade[CharNum] + 1));
                    yield return new WaitForSeconds(3);

                    MainUIGroup.Upgrade.ResultPopup.SetActive(true);
                    DataManager.Instance.nowPlayer.CharGrade[CharNum] += 1;
                    UpgradeChar.GetComponent<Animator>().SetTrigger("Success");
                    MainUIGroup.Upgrade.ResultText.GetComponent<LocalizeScript>().textKey = "Success";
                    MainUIGroup.Upgrade.ResultText.GetComponent<LocalizeScript>().DynamicLocal();

                    if (Grade < 10)
                    {
                        UpgradeText(Grade, CharNum);
                    }
                    else
                    {
                        MainUIGroup.Upgrade.UpgradePerText.text = "";
                        MainUIGroup.Upgrade.UpgradePriceText.text = "";
                    }
                }
                else
                {
                    StartCoroutine(UpgradeEffect(false, DataManager.Instance.nowPlayer.CharGrade[CharNum] - 1));
                    yield return new WaitForSeconds(3);

                    MainUIGroup.Upgrade.ResultPopup.SetActive(true);
                    UpgradeText(Grade, CharNum);
                    DataManager.Instance.nowPlayer.CharGrade[CharNum] -= 1;
                    UpgradeChar.GetComponent<Animator>().SetTrigger("Fail");
                    MainUIGroup.Upgrade.ResultText.GetComponent<LocalizeScript>().textKey = "fail";
                    MainUIGroup.Upgrade.ResultText.GetComponent<LocalizeScript>().DynamicLocal();
                    print(MainUIGroup.Upgrade.ResultText.text);
                    print(DataManager.Instance.nowPlayer.CharGrade[CharNum] + "����");
                }

                DataManager.Instance.SaveData();
                //UpgradeUIStar();
            }
            else
            {
                MainUIGroup.Upgrade.UpgradePerText.text = "";
                MainUIGroup.Upgrade.UpgradePriceText.text = "";
                print("�ְ���");
            }
            for (int i = 0; i < DataManager.Instance.nowPlayer.CharGrade.Length; i++)
            {
                GameManager.Instance.playerUpgrade[i] = DataManager.Instance.nowPlayer.CharGrade[i];
            }
        }
        else
        {
            print("�� ����");
        }
        UpgradeStars(MainUIGroup.Store.InfoStars, DataManager.Instance.nowPlayer.CharGrade[CharNum]);
        yield return new WaitForSeconds(1);
    }
    IEnumerator UpgradeEffect(bool IsResult, int grade)
    {
        yield return null;
        Vector3 EffectMaxScale = new Vector3(1, 1, 1);
        Vector3 EffectMinScale = new Vector3(0.1f, 0.1f, 0.1f);

        for (int i = 0; i < MainUIGroup.Upgrade.GauseEffect_GameObject.Length; i++)
        {
            MainUIGroup.Upgrade.GauseEffect_GameObject[i].SetActive(true);
            MainUIGroup.Upgrade.GauseEffect_GameObject[i].GetComponent<ParticleSystem>().Play();
        }

        print("������ ����Ʈ Ȱ��ȭ");
        for (int i = 0; i < MainUIGroup.Upgrade.GaugeEffect.Length; i++)
        {
            MainUIGroup.Upgrade.GaugeEffect[i].DOScale(EffectMaxScale, 1f);
        }
        yield return new WaitForSeconds(2f);


        for (int i = 0; i < MainUIGroup.Upgrade.GaugeEffect.Length; i++)
        {
            MainUIGroup.Upgrade.GaugeEffect[i].DOScale(EffectMinScale, 1f);
        }
        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < MainUIGroup.Upgrade.GauseEffect_GameObject.Length; i++) MainUIGroup.Upgrade.GauseEffect_GameObject[i].SetActive(false);
        for (int i = 0; i < MainUIGroup.Upgrade.ResultEffect.Length; i++) MainUIGroup.Upgrade.ResultEffect[i].SetActive(false);
        print("������ ����Ʈ Ȱ��ȭ");

        if (IsResult)
        {
            //����
            MainUIGroup.Upgrade.ResultEffect[0].SetActive(true);
            MainUIGroup.Upgrade.ResultEffect[0].GetComponent<ParticleSystem>().Play();
            MainUIGroup.Upgrade.ResultText.GetComponent<LocalizeScript>().textKey = "Success";
            for (int i = 2; i < MainUIGroup.Upgrade.FireObject.Length; i++) MainUIGroup.Upgrade.FireObject[i].SetActive(true);

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 2; i++) MainUIGroup.Upgrade.FireObject[i].SetActive(true);

            UpgradeStars(MainUIGroup.Upgrade.GradeStars, grade);

        }
        else
        {
            //����
            MainUIGroup.Upgrade.ResultEffect[1].SetActive(true);
            MainUIGroup.Upgrade.ResultEffect[1].GetComponent<ParticleSystem>().Play();
            MainUIGroup.Upgrade.ResultText.GetComponent<LocalizeScript>().textKey = "Fail";
            UpgradeStars(MainUIGroup.Upgrade.GradeStars, grade);
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < MainUIGroup.Upgrade.GauseEffect_GameObject.Length; i++) MainUIGroup.Upgrade.GauseEffect_GameObject[i].SetActive(false);
        for (int i = 0; i < MainUIGroup.Upgrade.ResultEffect.Length; i++) MainUIGroup.Upgrade.ResultEffect[i].SetActive(false);


        MainUIGroup.Upgrade.ResultPopup.SetActive(true);
    }
    void UpgradeStars(GameObject[] Stars, int grade)
    {
        for (int i = 0; i < Stars.Length; i++)
        {
            Stars[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
            Stars[i].SetActive(false);
        }
        if (grade <= 5)
        {
            for (int i = 0; i < grade; i++)
            {
                Stars[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < grade - 5; i++)
            {
                Stars[i].SetActive(true);
                Stars[i].GetComponent<Image>().color = new Color(255, 0, 213, 255);
            }
        }
    }

    // ĳ���� ����
    public void CharactorBuy()
    {
        print("ĳ���� ����" + MainUIGroup.data.CharPrice[SelectCharNum]);
        print(DataManager.Instance.nowPlayer.Diamond);
        print(DataManager.Instance.nowPlayer.IsChar[SelectCharNum]);

        if (DataManager.Instance.nowPlayer.Diamond <= MainUIGroup.data.CharPrice[SelectCharNum] && !DataManager.Instance.nowPlayer.IsChar[SelectCharNum])
        {
            print(SelectCharNum + "ĳ���� ����");

            MainUIGroup.Store.UpGradeCHaractorButtons[SelectCharNum].transform.GetChild(1).gameObject.SetActive(false);
            MainUIGroup.Store.UpGradeCHaractorButtons[SelectCharNum].transform.GetChild(0).gameObject.SetActive(true);

            // ������ ����
            DataManager.Instance.nowPlayer.Diamond -= MainUIGroup.data.CharPrice[SelectCharNum];
            DataManager.Instance.nowPlayer.IsChar[SelectCharNum] = true;
            DataManager.Instance.SaveData();

            MainUIGroup.Store.CharBuyUI.SetActive(false);
            MainUIGroup.Store.CharInfoUI.SetActive(false);
        }
    }

    // ĳ���� ����â ĳ���� ����
    public void CharacterSelect(int charnum)
    {
        //print(DataManager.Instance.nowPlayer.CharGrade[charnum]);
        MainUIGroup.CharSelect.SelectStars.SetActive(true);
        int Grade = DataManager.Instance.nowPlayer.CharGrade[charnum];
        for (int i = 0; i < 5; i++)
        {
            GameObject Star = MainUIGroup.CharSelect.SelectStars.transform.GetChild(i).transform.GetChild(0).gameObject;
            Star.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            Star.SetActive(false);
        }
        if (Grade <= 5)
        {
            for (int i = 0; i < Grade; i++)
            {
                GameObject Star = MainUIGroup.CharSelect.SelectStars.transform.GetChild(i).transform.GetChild(0).gameObject;
                Star.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                Star.SetActive(true);
            }
        }
        else if (Grade > 5)
        {
            for (int i = 0; i < Grade - 5; i++)
            {
                //print(i + 6);
                GameObject Star = MainUIGroup.CharSelect.SelectStars.transform.GetChild(i).transform.GetChild(0).gameObject;
                Star.GetComponent<Image>().color = new Color(255, 0, 213, 255);
                Star.SetActive(true);
            }
        }

        OffStatView();

        MainUIGroup.CharSelect.CharactorInfoObj[charnum].SetActive(true);
        RectTransform rectTransform = MainUIGroup.CharSelect.CharactorInfoObj[charnum].GetComponent<RectTransform>();
        // ��Ʈ : DoTween ��� �� ������ ��� ���� ���ط��� �ִ°� �����ϴ�
        rectTransform.DOAnchorPosX(517f, 1.5f).SetEase(Ease.OutBounce);    // ���� Ƣ�� ������ ���� Ease ����

        isSelect = true;
        GameManager.Instance.playerNum = charnum;
        // ĳ���� �̸� ���ö���¡
        MainUIGroup.CharSelect.SelectUICharName.GetComponent<LocalizeScript>().textKey = MainUIGroup.data.CharNameLocalCode[charnum];
        MainUIGroup.CharSelect.SelectUICharName.GetComponent<LocalizeScript>().DynamicLocal();

        if (selectChar != null) Destroy(selectChar);

        //selectChar = Instantiate(MainUIGroup.CharSelect.selectCharactorPrefab[charnum], new Vector3(0.04f, 2.11f, -2.28f), Quaternion.Euler(0f, 205f, 0f));
        selectChar = Instantiate(MainUIGroup.CharSelect.selectCharactorPrefab[charnum], new Vector3(0, -0.5f, 3), Quaternion.Euler(0f, 205f, 0f));
    }

    // ����â ĳ���� ����â Ȱ��ȭ
    void OffStatView()
    {
        for (int i = 0; i < MainUIGroup.CharSelect.CharactorInfoObj.Length; i++)
        {
            MainUIGroup.CharSelect.CharactorInfoObj[i].SetActive(false);

            RectTransform AllrectTransform = MainUIGroup.CharSelect.CharactorInfoObj[i].GetComponent<RectTransform>();

            Vector2 newPosition = new Vector2(756.49f, AllrectTransform.anchoredPosition.y);
            AllrectTransform.anchoredPosition = newPosition;
        }
    }

    //���� ����
    public void GameStart()
    {
        if (isSelect)
        {
            SceneManager.LoadScene("LoadScene");
            DataManager.Instance.SaveData();
        }
        else
        {
            MainUIGroup.CharSelect.selectObj.SetActive(true);
            Invoke("Off", 1f);
            print("ĳ���͸� �������ּ���.");
        }
    }
    void Off()
    {
        MainUIGroup.CharSelect.selectObj.SetActive(false);
    }

    //���� ����
    public void Quit()
    {
        PlayerPrefs.Save();
        DataManager.Instance.SaveData();
        Application.Quit();
    }

    // �����ؽ�Ʈ
    void MainTextUI()
    {
        for (int i = 0; i < MainUIGroup.Store.DiaText.Length; i++) MainUIGroup.Store.DiaText[i].text = DataManager.Instance.nowPlayer.Diamond.ToString();

        // ��Ʈ : ������ int���� ����ȭ ��Ű�� ������ + �Ǵ� string ������ ���ڿ� ����($)�� �̿��ϴ°� ����մϴ�
        // ��Ʈ : �� ������ string.Format()�� ����ϴ� ���� �����մϴ�
        MainUIGroup.Option.MusicValueText.text = ((int)(SoundManager.Instance.MusicAudio.volume * 100)).ToString() + "%";
        MainUIGroup.Option.EffectAudioValueText.text = ((int)(SoundManager.Instance.EffectVolume * 100)).ToString() + "%";
    }
    public void CommunityBt()
    {
        Application.OpenURL("https://discord.gg/nskzytFU");
    }
    public void SetMusicVolume(float Volume)
    {
        SoundManager.Instance.MusicAudio.volume = Volume;
        SoundManager.Instance.MusicVolume = Volume;

        PlayerPrefs.SetFloat("MusicSound", Volume);
    }
    public void SetEffectVolume(float Volume)
    {
        SoundManager.Instance.EffectVolume = Volume;
        PlayerPrefs.SetFloat("EffectSound", Volume);
    }

    // IsAI ���� �޼���
    public void AISetting(bool isFull)
    {
        GameManager.Instance.IsAI = isFull;
        PlayerPrefs.SetInt("IsAI", isFull ? 1 : 0); // true�� 1, false�� 0 ����
        //print("AI" + isFull);
        PlayerPrefs.Save(); // ����� ���� ����
    }

    // IsShake ���� �޼���
    public void ShakeSetting(bool isFull)
    {
        GameManager.Instance.IsShake = isFull;
        PlayerPrefs.SetInt("IsShake", isFull ? 1 : 0); // true�� 1, false�� 0 ����
        //print("SHAKE" + isFull);
        PlayerPrefs.Save(); // ����� ���� ����
    }

    // IsRange ���� �޼���
    public void RangeSetting(bool isFull)
    {
        GameManager.Instance.IsRange = isFull;
        PlayerPrefs.SetInt("IsRange", isFull ? 1 : 0); // true�� 1, false�� 0 ����
        //print("SHAKE" + isFull);
        PlayerPrefs.Save(); // ����� ���� ����
    }
}
