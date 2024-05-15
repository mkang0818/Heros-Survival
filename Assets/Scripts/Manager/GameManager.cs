using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 멘트 : instance를 사용하는 Manager클래스가 여러 개 있다면 제네릭 싱글톤으로 묶어주세요
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
        LoadSettings(); // 설정 값 로드
        
        // 기본 화면 크기 설정
        Screen.SetResolution(1280, 720, false);
    }

    

    // 설정 값을 로드하는 메서드
    void LoadSettings()
    {
        // IsAI와 IsShake 값을 PlayerPrefs에서 가져와서 설정
        // 멘트 : PlayerPrefs에 get, set하는 경우 프로퍼티를 이용한 변수화를 하거나
        // 멘트 : nameof를 활용해서 string 오타가 없게 해주세요
        /* 멘트 ex)
         * private int IsAi
            {
                get => PlayerPrefs.GetInt(nameof(IsAi));
                set => PlayerPrefs.SetInt(nameof(IsAi), value);
            }
         */
        IsAI = PlayerPrefs.GetInt("IsAI", 0) == 1; // 기본값을 true로 설정
        IsShake = PlayerPrefs.GetInt("IsShake", 0) == 1; // 기본값을 true로 설정
        IsRange = PlayerPrefs.GetInt("IsRange", 0) == 1; // 기본값을 true로 설정
        IsScreen = PlayerPrefs.GetInt("isFull", 1) == 0; // 기본값을 true로 설정
        //Debug.Log("IsShake: " + IsShake);
        //Debug.Log("IsAI: " + IsAI);
        //Debug.Log("IsScreen: " + IsScreen);
    }
}
