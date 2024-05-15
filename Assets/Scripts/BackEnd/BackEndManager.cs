using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks; // [변경] async 기능을 이용하기 위해서는 해당 namepsace가 필요합니다.

// 뒤끝 SDK namespace 추가
using BackEnd;

public class BackEndManager : MonoBehaviour
{
    public static BackEndManager _instance = null;

    public bool IsLogin = false;
    GameObject LoginUI;
    public InputField idInput;
    public TextMeshProUGUI NickNameText;
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생 
        }
        //Register();

        LoginUI = GameObject.Find("Login");
    }
    private void Update()
    {
        if (LoginUI != null)
        {
            if (IsLogin)
            {
                LoginUI.SetActive(false);
            }
        }
    }
    // =======================================================
    // [추가] 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
    // =======================================================
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // 뒤끝 로그인

            BackendGameData.Instance.GameDataInsert(); //[추가] 데이터 삽입 함수

            Debug.Log("테스트를 종료합니다.");
        });
    }*/
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // 뒤끝 로그인

            BackendGameData.Instance.GameDataGet(); //[추가] 데이터 불러오기 함수

            Debug.Log("테스트를 종료합니다.");
        });
    }*/
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // 뒤끝 로그인

            BackendGameData.Instance.GameDataGet(); // 데이터 삽입 함수

            // [추가] 서버에 불러온 데이터가 존재하지 않을 경우, 데이터를 새로 생성하여 삽입
            if (BackendGameData.userData == null)
            {
                BackendGameData.Instance.GameDataInsert();
            }

            BackendGameData.Instance.LevelUp(); // [추가] 로컬에 저장된 데이터를 변경

            BackendGameData.Instance.GameDataUpdate(); //[추가] 서버에 저장된 데이터를 덮어쓰기(변경된 부분만)

            Debug.Log("테스트를 종료합니다.");
        });
    }*/
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // [추가] 뒤끝 로그인

            BackendLogin.Instance.UpdateNickname("강민준"); // [추가] 닉네임 변겅
            Debug.Log("테스트를 종료합니다.");
        });
    }*/

    // 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // 뒤끝 로그인 함수

            BackendRank.Instance.RankInsert(100); // [추가] 랭킹 등록하기 함수

            Debug.Log("테스트를 종료합니다.");
        });
    }*/
    public void Login()
    {
        Login(idInput.text, "1234");
    }
    public async void Register(string id, string pw)
    {
        NickNameText.text = id;
        await Task.Run(() => {
            BackendLogin.Instance.CustomSignUp(id, pw); // [추가] 뒤끝 회원가입 함수
            BackendGameData.Instance.GameDataInsert(); //[추가] 데이터 삽입 함수
            BackendLogin.Instance.UpdateNickname(id);
            BackendRank.Instance.RankInsert(0, 3);
        });
    }
    async void Login(string id, string pw)
    {
        NickNameText.text = id;
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomLogin(id, pw); // 뒤끝 로그인 함수
        });
    }
    public async void Rank(int score, int charCode)
    {
        await Task.Run(() =>
        {
            print("닉네임:" + Backend.UserNickName + "현재최고기록:" + BackendRank.Instance.RankGet() + "캐릭터" + charCode + "번");

            int currentBestScore = BackendRank.Instance.RankGet();
            print("자신 최고기록"+ currentBestScore);
            if (score > currentBestScore)
            {
                print("최고기록 갱신");
                BackendRank.Instance.RankInsert(score, charCode); // 랭킹 등록하기 함수
            }
            else
            {
                print("최고기록 갱신실패");
            }
        });
    }
}