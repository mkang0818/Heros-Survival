using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIScale : MonoBehaviour
{
    public Text textObject; // 텍스트 오브젝트를 가리키는 변수
    private Image imageComponent; // 이미지 UI 컴포넌트

    public float baseWidth = 100f; // 기본 이미지 너비
    public float widthPerCharacter = 10f; // 글자 당 너비 조절량

    // 스타트 함수에서 이미지 컴포넌트 참조
    void Start()
    {
        imageComponent = GetComponent<Image>(); // 스크립트를 가지고 있는 오브젝트의 이미지 컴포넌트 참조
        UpdateImageSize();
    }
    private void Update()
    {
        UpdateImageSize();
    }

    // 이미지 너비 업데이트 함수
    void UpdateImageSize()
    {
        if (textObject != null && imageComponent != null)
        {
            int characterCount = textObject.text.Length; // 텍스트 글자 수 계산
            float newWidth = baseWidth + (characterCount * widthPerCharacter); // 새로운 이미지 너비 계산
            imageComponent.rectTransform.sizeDelta = new Vector2(newWidth, imageComponent.rectTransform.sizeDelta.y); // 이미지 너비 업데이트
        }
    }
}
