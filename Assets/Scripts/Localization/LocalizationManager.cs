using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class Lang
{
    // lang - 첫번째줄, langLocalize - 두번째줄
    public string lang, langLocalize;

    // 번역하는 개수들 3번째 줄부터 쭈욱
    public List<string> value = new List<string>();
}

public class LocalizationManager : MonoBehaviour
{
    const string langURL = "https://docs.google.com/spreadsheets/d/1vJjp2DzNYLyiMNFIESnvS7F4YkVMZxDCuT-MmukBO6w/export?format=tsv";
    public event System.Action LocalizeChanged = () => { };
    public int curLangIndex;
    public List<Lang> Langs;

    public Image NowFlagImg;
    public TMP_Dropdown dropdown;

    #region 싱글톤
    static public LocalizationManager LocalizationInstance;
    void Awake()
    {
        if (null == LocalizationInstance)
        {
            LocalizationInstance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);

        InitLang();
    }
    #endregion


    void Start()
    {
        if (dropdown.options.Count != Langs.Count) SetLangOption();
        dropdown.onValueChanged.AddListener((d) => SetLangOption());

    }
    void SetLangOption()
    {
        List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < Langs.Count; i++) optionDatas.Add(new TMP_Dropdown.OptionData() { text = Langs[i].langLocalize });
        dropdown.options = optionDatas;
    }
    void InitLang()
    {
        int langIndex = PlayerPrefs.GetInt("LangIndex", -1);
        int systemIndex = Langs.FindIndex(x => x.lang.ToLower() == Application.systemLanguage.ToString().ToLower());
        if (systemIndex == -1) systemIndex = 0;
        int index = langIndex == -1 ? systemIndex : langIndex;

        SetLangIndex(index);
    }

    public void SetLangIndex(int index)
    {
        curLangIndex = index;
        PlayerPrefs.SetInt("LangIndex", curLangIndex);
        LocalizeChanged();
    }


    [ContextMenu("언어 가져오기")]
    void GetLang()
    {
        StartCoroutine(GetLangCo());
    }
    IEnumerator GetLangCo()
    {
        UnityWebRequest www = UnityWebRequest.Get(langURL);
        yield return www.SendWebRequest();
        print(www.downloadHandler.text);
        SetLangList(www.downloadHandler.text);
    }
    void SetLangList(string tsv)
    {
        // 이차원 배열
        string[] row = tsv.Split("\n"); //열 - 세로줄
        int rowSize = row.Length;
        int columnSize = row[0].Split("\t").Length; //행 - 가로줄
        string[,] Sentence = new string[rowSize, columnSize];

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split("\t");
            for (int j = 0; j < columnSize; j++) Sentence[i, j] = column[j];
        }

        // 클래스 리스트
        Langs = new List<Lang>();
        for (int i = 0; i < columnSize; i++)
        {
            Lang lang = new Lang();
            lang.lang = Sentence[0, i];          // 첫번째 줄
            lang.langLocalize = Sentence[1, i]; // 두번째 줄

            for (int j = 0; j < rowSize; j++) lang.value.Add(Sentence[j, i]);
            Langs.Add(lang);
        }
    }

    public void LanguageBt(int index)
    {
        SetLangIndex(index);
    }
}
