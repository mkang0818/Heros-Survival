using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIScale : MonoBehaviour
{
    public Text textObject; // �ؽ�Ʈ ������Ʈ�� ����Ű�� ����
    private Image imageComponent; // �̹��� UI ������Ʈ

    public float baseWidth = 100f; // �⺻ �̹��� �ʺ�
    public float widthPerCharacter = 10f; // ���� �� �ʺ� ������

    // ��ŸƮ �Լ����� �̹��� ������Ʈ ����
    void Start()
    {
        imageComponent = GetComponent<Image>(); // ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� �̹��� ������Ʈ ����
        UpdateImageSize();
    }
    private void Update()
    {
        UpdateImageSize();
    }

    // �̹��� �ʺ� ������Ʈ �Լ�
    void UpdateImageSize()
    {
        if (textObject != null && imageComponent != null)
        {
            int characterCount = textObject.text.Length; // �ؽ�Ʈ ���� �� ���
            float newWidth = baseWidth + (characterCount * widthPerCharacter); // ���ο� �̹��� �ʺ� ���
            imageComponent.rectTransform.sizeDelta = new Vector2(newWidth, imageComponent.rectTransform.sizeDelta.y); // �̹��� �ʺ� ������Ʈ
        }
    }
}
