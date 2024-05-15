using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StatInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InGameManager IngameManager;

    public GameObject ImageUIPrefab; // 생성할 이미지 UI 프리팹

    string ValueStr;
    float Value;
    public bool IsPer; // 퍼센트인지 int형인지
    public int StatIndex; // 스탯 번호
    public string Name; // 스탯 이름
    public string detail1; //스탯 문장1
    public string detail2; //스탯 문장2

    void InitStat()
    {
        Value = IngameManager.UpgradeStat[StatIndex];
        if (IsPer)
        {
            float Percent = Mathf.Floor(Value * 10) * 0.1f;
            print(Percent);
            if (Percent > 0) { ValueStr = "<color=#00FF00>" + " +" + Percent.ToString() + "%" + "</color>"; print(1); }
            else if (Percent < 0) { ValueStr = "<color=#FF0000>" + " +" + Percent.ToString() + "%" + "</color>"; print(2); }
            else { ValueStr = "0%"; print(3); }
        }
        else
        {
            if (Value > 0) ValueStr = "<color=#00FF00>" + ((int)Value).ToString() + "</color>";
            else if (Value < 0) ValueStr = "<color=#FF0000>" + ((int)Value).ToString() + "</color>";
            else ValueStr = "0";
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //InitStat();

        ImageUIPrefab.SetActive(true);

        ImageUIPrefab.transform.GetChild(0).gameObject.GetComponent<LocalizeScript>().textKey = Name;
        ImageUIPrefab.transform.GetChild(1).gameObject.GetComponent<LocalizeScript>().StatValue = IngameManager.UpgradeStat[StatIndex];

        if (IsPer) ImageUIPrefab.transform.GetChild(1).gameObject.GetComponent<LocalizeScript>().IsPer = IsPer;
        if (detail1.Length > 1)
        {
            ImageUIPrefab.transform.GetChild(1).gameObject.GetComponent<LocalizeScript>().textKey = detail1;
        }
        if (detail2.Length > 1)
        {
            ImageUIPrefab.transform.GetChild(1).gameObject.GetComponent<LocalizeScript>().textKey2 = detail2;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 이미지 UI가 생성된 경우 삭제
        if (ImageUIPrefab != null)
        {
            ImageUIPrefab.SetActive(false);
        }
    }
}
