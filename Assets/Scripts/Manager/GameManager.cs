using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    void Start()
    {
        LoadSettings(); // 설정 값 로드
        
        // 기본 화면 크기 설정
        Screen.SetResolution(1280, 720, false);
    }

    

    // 설정 값을 로드하는 메서드
    void LoadSettings()
    {
        IsAI = PlayerPrefs.GetInt("IsAI", 0) == 1; // 기본값을 true로 설정
        IsShake = PlayerPrefs.GetInt("IsShake", 0) == 1; // 기본값을 true로 설정
        IsRange = PlayerPrefs.GetInt("IsRange", 0) == 1; // 기본값을 true로 설정
        IsScreen = PlayerPrefs.GetInt("isFull", 1) == 0; // 기본값을 true로 설정
    }
}
