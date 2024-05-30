using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocalizationManager;
using UnityEngine.UI;
using TMPro;

public class LocalizeScript : MonoBehaviour
{
    public string textKey;

    public bool IsPer = false;
    public bool IsStatInfo = false;
    public string textKey2;
    public float StatValue = 0;

    public bool IsTextInfo = false;

    public bool IsNameText = false;

    public bool IsItemInfo = false;
    public GameObject ParentObj;
    // Start is called before the first frame update
    void Start()
    {
        if (textKey != null)
        {
            LocalizeChanged();
        }
    }

    void OnDestroy()
    {
        LocalizationInstance.LocalizeChanged -= LocalizeChanged;
    }
    public void DynamicLocal()
    {
        //print("새롭게 번역");
        LocalizeChanged();

        LocalizationInstance.LocalizeChanged += LocalizeChanged;
    }
    string Localize(string key)
    {
        int keyindex = LocalizationInstance.Langs[0].value.FindIndex(x => x.ToLower() == key.ToLower());
        if (IsItemInfo && !IsTextInfo)
        {
            //print("아이템정보");
            return ItemInfoText(LocalizationInstance.Langs[LocalizationInstance.curLangIndex].value[keyindex], LocalizationInstance.curLangIndex);
        }
        else if(!IsItemInfo && IsTextInfo)
        {
            //print("캐릭터 설명창"); 
            return FormatLongText(LocalizationInstance.Langs[LocalizationInstance.curLangIndex].value[keyindex], LocalizationInstance.curLangIndex);
        }
        else if(!IsItemInfo && !IsTextInfo)
        {
            //print("일반");
            return LocalizationInstance.Langs[LocalizationInstance.curLangIndex].value[keyindex];
        }
        else
        {
            print("error");
            return "ERROR";
        }
    }

    public void LocalizeChanged()
    {
        if (textKey2 == "")
        {
            textKey2 = ".";
        }

        if (IsStatInfo)
        {
            if (IsPer)
            {
                //print(11);
                if (StatValue > 0)
                {
                    //print(11);
                    if (textKey.Length > 1) GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "<color=#00FF00>" + " +" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                    else if (textKey2.Length > 1) GetComponent<TextMeshProUGUI>().text = "<color=#00FF00>" + " +" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                    else GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "<color=#00FF00>" + " +" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                }
                else if (StatValue < 0)
                {
                    //print(11);
                    if (textKey.Length > 1) GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FF0000>" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                    else if (textKey2.Length > 1) GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                    else GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FF0000>" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                }
                else
                {
                    //print(11);
                    if (textKey.Length > 1) GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FFFFFF>" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                    else if (textKey2.Length > 1) GetComponent<TextMeshProUGUI>().text = "<color=#FFFFFF>" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                    else GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FFFFFF>" + StatValue + "%" + "</color>" + "  " + Localize(textKey2);
                }
            }
            else
            {
                //print(1);
                if (StatValue > 0)
                {
                    //print(11);
                    if (textKey.Length > 1) GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "<color=#00FF00>" + " +" + StatValue + "</color>" + "  " + Localize(textKey2);
                    else if (textKey2.Length > 1) GetComponent<TextMeshProUGUI>().text = "<color=#00FF00>" + " +" + StatValue + "</color>" + "  " + Localize(textKey2);
                    else GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "<color=#00FF00>" + " +" + StatValue + "</color>" + "  " + Localize(textKey2);
                }
                else if (StatValue < 0)
                {
                    //print(11);
                    if (textKey.Length > 1) GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FF0000>" + StatValue + "</color>" + "  " + Localize(textKey2);
                    else if (textKey2.Length > 1) GetComponent<TextMeshProUGUI>().text = "<color=#FF0000>" + StatValue + "</color>" + "  " + Localize(textKey2);
                    else GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FF0000>" + StatValue + "</color>" + "  " + Localize(textKey2);
                }
                else
                {
                    //print(11);
                    if (textKey.Length > 1) GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FFFFFF>" + StatValue + "</color>" + "  " + Localize(textKey2);
                    else if (textKey2.Length > 1) GetComponent<TextMeshProUGUI>().text = "<color=#FFFFFF>" + StatValue + "</color>" + "  " + Localize(textKey2);
                    else GetComponent<TextMeshProUGUI>().text = Localize(textKey) + "  " + "<color=#FFFFFF>" + StatValue + "</color>" + "  " + Localize(textKey2);
                }
            }
            
        }
        else if (GetComponent<TextMeshProUGUI>() != null && !IsStatInfo)
        {
            //print(11);
            GetComponent<TextMeshProUGUI>().text = Localize(textKey);
        }
        LocalizationInstance.LocalizeChanged += LocalizeChanged;
    }

    string ItemInfoText(string text, int curLangIndex)
    {
        bool LongStr = false;
        int SplitNum = curLangIndex switch
        {
            0 => 5,
            1 => 5,
            2 => 5,
            3 => 5,
            4 => 5,
            _ => 5
        };
        if (curLangIndex != 3 && curLangIndex != 2 && curLangIndex != 4 || !IsNameText)
        {
            string[] words = text.Split(' ');
            string formattedText = "";
            int wordCount = 0;

            int spaceCount = 0;
            foreach (string word in words)
            {
                wordCount++;

                if (word.Length > 3)
                {
                    //print(word);
                    wordCount++;
                    if (wordCount >= SplitNum)
                    {
                        formattedText += "\n";
                        spaceCount++;
                        wordCount = 0;
                        formattedText += word + " ";
                        if(spaceCount>=2) LongStr = true;
                    }
                    else
                    {
                        formattedText += word + " ";
                    }
                }
                else
                {
                    formattedText += word + " ";
                    if (wordCount >= SplitNum)
                    {
                        formattedText += "\n";
                        spaceCount++;
                        wordCount = 0;
                        if (spaceCount >= 2) LongStr = true;
                    }
                }
            }
            if (LongStr)
            {
                // 부모 오브젝트의 RectTransform 컴포넌트 가져오기
                RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();

                // 부모 오브젝트의 높이(세로 크기)를 2배로 늘리기
                parentRectTransform.sizeDelta = new Vector2(parentRectTransform.sizeDelta.x, parentRectTransform.sizeDelta.y * 3f);
                print(transform.parent);
            }
            return formattedText.Trim();
        }
        else
        {
            string[] words = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                words[i] = text[i].ToString();
            }
            string formattedText = "";
            int wordCount = 0;

            foreach (string word in words)
            {
                wordCount++;

                if (wordCount >= SplitNum)
                {
                    formattedText += "\n";
                    wordCount = 0;
                    formattedText += word;
                    LongStr = true;
                }

                formattedText += word;

            }
            if (LongStr)
            {
                // ParentObj의 RectTransform 컴포넌트 가져오기
                RectTransform parentRectTransform = ParentObj.GetComponent<RectTransform>();

                // ParentObj의 높이(세로 크기)를 2배로 늘리기
                parentRectTransform.sizeDelta = new Vector2(parentRectTransform.sizeDelta.x, parentRectTransform.sizeDelta.y * 2f);
                print(transform.parent);
            }
            return formattedText.Trim();
        }
    }
    string FormatLongText(string text, int curLangIndex)
    {
        int SplitNum = curLangIndex switch
        {
            0 => 4,
            1 => 4,
            2 => 4,
            3 => 14,
            4 => 4,
            _ => 4
        };
        if (curLangIndex != 3)
        {
            string[] words = text.Split(' ');
            string formattedText = "";
            int wordCount = 0;
            foreach (string word in words)
            {
                wordCount++;
                if (word.Length >= 10)
                {
                    wordCount++;
                    if (wordCount > SplitNum)
                    {
                        formattedText += "\n";
                        wordCount = 1;
                        formattedText += word + " ";
                    }
                    else
                    {
                        formattedText += word + " ";
                    }
                }
                else
                {

                    if (wordCount >= SplitNum)
                    {
                        formattedText += "\n";
                        wordCount = 0;
                        formattedText += word + " ";
                    }
                    else
                    {
                        formattedText += word + " ";
                    }
                }
            }
            return formattedText.Trim();
        }
        else
        {
            string[] words = new string[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                words[i] = text[i].ToString();
            }
            string formattedText = "";
            int wordCount = 0;

            foreach (string word in words)
            {
                wordCount++;

                if (wordCount >= SplitNum)
                {
                    formattedText += "\n";
                    wordCount = 0;
                    formattedText += word;
                }

                formattedText += word;
            }
            return formattedText.Trim();
        }

    }
}
