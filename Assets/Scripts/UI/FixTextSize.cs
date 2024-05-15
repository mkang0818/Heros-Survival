using UnityEngine;
using UnityEngine.UI;

public class FixTextSize : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 initialSize;
    private Vector2 initialPosition;

    void Start()
    {
        // �ؽ�Ʈ�� RectTransform ������Ʈ ��������
        rectTransform = GetComponent<RectTransform>();

        // �ʱ� ũ��� ��ġ ����
        initialSize = rectTransform.sizeDelta;
        initialPosition = rectTransform.anchoredPosition;
    }

    void LateUpdate()
    {
        // �θ� ������Ʈ�� ũ�� �� ��ġ ��������
        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();
        Vector2 parentSize = parentRectTransform.sizeDelta;
        Vector2 parentPosition = parentRectTransform.anchoredPosition;

        // �ؽ�Ʈ�� ũ�� �� ��ġ�� �ʱ� ������ ����
        rectTransform.sizeDelta = initialSize;
        rectTransform.anchoredPosition = initialPosition;
    }
}
