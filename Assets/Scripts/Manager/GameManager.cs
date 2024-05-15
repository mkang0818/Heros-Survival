using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ��Ʈ : instance�� ����ϴ� ManagerŬ������ ���� �� �ִٸ� ���׸� �̱������� �����ּ���
    //public static GameManager instance;
    public int playerNum = -1;
    public int[] playerUpgrade = new int[14];

    public int Score;
    public bool IsAI;
    public bool IsShake;
    public bool IsRange;
    public bool IsScreen;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadSettings(); // ���� �� �ε�
        
        // �⺻ ȭ�� ũ�� ����
        Screen.SetResolution(1280, 720, false);
    }

    

    // ���� ���� �ε��ϴ� �޼���
    void LoadSettings()
    {
        // IsAI�� IsShake ���� PlayerPrefs���� �����ͼ� ����
        // ��Ʈ : PlayerPrefs�� get, set�ϴ� ��� ������Ƽ�� �̿��� ����ȭ�� �ϰų�
        // ��Ʈ : nameof�� Ȱ���ؼ� string ��Ÿ�� ���� ���ּ���
        /* ��Ʈ ex)
         * private int IsAi
            {
                get => PlayerPrefs.GetInt(nameof(IsAi));
                set => PlayerPrefs.SetInt(nameof(IsAi), value);
            }
         */
        IsAI = PlayerPrefs.GetInt("IsAI", 0) == 1; // �⺻���� true�� ����
        IsShake = PlayerPrefs.GetInt("IsShake", 0) == 1; // �⺻���� true�� ����
        IsRange = PlayerPrefs.GetInt("IsRange", 0) == 1; // �⺻���� true�� ����
        IsScreen = PlayerPrefs.GetInt("isFull", 1) == 0; // �⺻���� true�� ����
        //Debug.Log("IsShake: " + IsShake);
        //Debug.Log("IsAI: " + IsAI);
        //Debug.Log("IsScreen: " + IsScreen);
    }
}
