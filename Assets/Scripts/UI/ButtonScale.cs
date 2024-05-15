using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip[] ArrBtAudio;

    public Button[] SelectButtons;

    [SerializeField] public TextMeshProUGUI buttonText;
    [SerializeField] public Image buttonImage;

    public bool NoColor;

    private Vector3 originalScale;
    public bool isClick = false;
    public bool IsLockBt;
    public Color Btcolor;
    public Color Textcolor;


    public bool IsStore;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;

        buttonImage = GetComponent<Image>();
        Btcolor = buttonImage.color;

        if (GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            Textcolor = buttonText.color;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.SoundPlay("BtEnter", ArrBtAudio[0]);

        if (!NoColor)
        {
            if (SelectButtons.Length > 10)
            {
                // 버튼 색상 변경
                if (buttonText != null) buttonText.color = Color.black;
                if (buttonImage != null) buttonImage.color = Color.white;
                if (IsStore && transform.GetChild(1).gameObject != null) transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.black;
            }
            else
            {
                //if (buttonText != null) buttonText.color = Btcolor;
                //if (buttonImage != null) buttonImage.color = Textcolor;
                if (buttonText != null) buttonText.color = Color.black;
                if (buttonImage != null) buttonImage.color = Color.white;
                if (IsStore && transform.GetChild(1).gameObject != null) transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!NoColor)
        {
            // 버튼 색상 변경
            if (!isClick)
            {
                if (SelectButtons.Length > 10)
                {
                    // 버튼 색상 변경
                    if (buttonText != null) buttonText.color = Textcolor;
                    if (buttonImage != null) buttonImage.color = Btcolor;
                    if (IsStore && transform.GetChild(1).gameObject != null) transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    if (buttonText != null) buttonText.color = Textcolor;
                    if (buttonImage != null) buttonImage.color = Btcolor;
                }

            }

        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.SoundPlay("BtEnter", ArrBtAudio[1]);

        if (!NoColor)
        {
            if (SelectButtons.Length > 10 && !isClick)
            {
                foreach (Button bt in SelectButtons)
                {
                    bt.GetComponent<Image>().color = Btcolor;
                    bt.GetComponent<ButtonScale>().isClick = false;
                    if (IsStore && transform.GetChild(1).gameObject != null) bt.transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
                }
                // 버튼 색상 변경
                if (buttonText != null) buttonText.color = Color.black;
                if (buttonImage != null) buttonImage.color = Color.white;
                if (IsStore && transform.GetChild(1).gameObject != null) transform.GetChild(1).gameObject.GetComponent<Image>().color = Color.black;
                isClick = true;
            }
            else if (!isClick && IsLockBt)
            {
                isClick = true;
                print("잠금");
            }
            else if (isClick && IsLockBt)
            {
                isClick = false;
                print("잠금해제");
            }
            else
            {
                //print("색변경상태");
                // 버튼 색상 변경
                if (buttonText != null) buttonText.color = Textcolor;
                if (buttonImage != null) buttonImage.color = Btcolor;
            }
        }
    }
}