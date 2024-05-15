using UnityEngine;
using UnityEngine.UI;

public class FixTextSize : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 initialSize;
    private Vector2 initialPosition;

    void Start()
    {
        // 텍스트의 RectTransform 컴포넌트 가져오기
        rectTransform = GetComponent<RectTransform>();

        // 초기 크기와 위치 저장
        initialSize = rectTransform.sizeDelta;
        initialPosition = rectTransform.anchoredPosition;
    }

    void LateUpdate()
    {
        // 부모 오브젝트의 크기 및 위치 가져오기
        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
        Vector2 parentSize = parentRectTransform.sizeDelta;
        Vector2 parentPosition = parentRectTransform.anchoredPosition;

        // 텍스트의 크기 및 위치를 초기 값으로 고정
        rectTransform.sizeDelta = initialSize;
        rectTransform.anchoredPosition = initialPosition;
    }
}
