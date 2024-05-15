using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI ItemName;
    public Image ItemImg;
    public Image ItemBackgroundImg;

    public Transform ContentTransform;
    public GameObject infoTextObj;

    public GameObject itemInfoUI; // 아이템 정보 UI 인스턴스

    public bool IsCoin;
    public GameObject CoinInfoText;

    // 마우스를 가져다 대면 호출되는 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
    }
    // 마우스를 떼면 호출되는 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemInfoUI!= null)
        {
            itemInfoUI.SetActive(false);
        }
    }
}