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

    public GameObject itemInfoUI; // ������ ���� UI �ν��Ͻ�

    public bool IsCoin;
    public GameObject CoinInfoText;

    // ���콺�� ������ ��� ȣ��Ǵ� �Լ�
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
    }
    // ���콺�� ���� ȣ��Ǵ� �Լ�
    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemInfoUI!= null)
        {
            itemInfoUI.SetActive(false);
        }
    }
}