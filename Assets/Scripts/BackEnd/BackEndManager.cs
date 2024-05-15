using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks; // [����] async ����� �̿��ϱ� ���ؼ��� �ش� namepsace�� �ʿ��մϴ�.

// �ڳ� SDK namespace �߰�
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

        var bro = Backend.Initialize(true); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻� 
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
    // [�߰�] ���� �Լ��� �񵿱⿡�� ȣ���ϰ� ���ִ� �Լ�(����Ƽ UI ���� �Ұ�)
    // =======================================================
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // �ڳ� �α���

            BackendGameData.Instance.GameDataInsert(); //[�߰�] ������ ���� �Լ�

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }*/
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // �ڳ� �α���

            BackendGameData.Instance.GameDataGet(); //[�߰�] ������ �ҷ����� �Լ�

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }*/
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // �ڳ� �α���

            BackendGameData.Instance.GameDataGet(); // ������ ���� �Լ�

            // [�߰�] ������ �ҷ��� �����Ͱ� �������� ���� ���, �����͸� ���� �����Ͽ� ����
            if (BackendGameData.userData == null)
            {
                BackendGameData.Instance.GameDataInsert();
            }

            BackendGameData.Instance.LevelUp(); // [�߰�] ���ÿ� ����� �����͸� ����

            BackendGameData.Instance.GameDataUpdate(); //[�߰�] ������ ����� �����͸� �����(����� �κи�)

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }*/
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // [�߰�] �ڳ� �α���

            BackendLogin.Instance.UpdateNickname("������"); // [�߰�] �г��� ����
            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }*/

    // ���� �Լ��� �񵿱⿡�� ȣ���ϰ� ���ִ� �Լ�(����Ƽ UI ���� �Ұ�)
    /*async void Test()
    {
        await Task.Run(() => {
            BackendLogin.Instance.CustomLogin("user1", "1234"); // �ڳ� �α��� �Լ�

            BackendRank.Instance.RankInsert(100); // [�߰�] ��ŷ ����ϱ� �Լ�

            Debug.Log("�׽�Ʈ�� �����մϴ�.");
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
            BackendLogin.Instance.CustomSignUp(id, pw); // [�߰�] �ڳ� ȸ������ �Լ�
            BackendGameData.Instance.GameDataInsert(); //[�߰�] ������ ���� �Լ�
            BackendLogin.Instance.UpdateNickname(id);
            BackendRank.Instance.RankInsert(0, 3);
        });
    }
    async void Login(string id, string pw)
    {
        NickNameText.text = id;
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomLogin(id, pw); // �ڳ� �α��� �Լ�
        });
    }
    public async void Rank(int score, int charCode)
    {
        await Task.Run(() =>
        {
            print("�г���:" + Backend.UserNickName + "�����ְ���:" + BackendRank.Instance.RankGet() + "ĳ����" + charCode + "��");

            int currentBestScore = BackendRank.Instance.RankGet();
            print("�ڽ� �ְ���"+ currentBestScore);
            if (score > currentBestScore)
            {
                print("�ְ��� ����");
                BackendRank.Instance.RankInsert(score, charCode); // ��ŷ ����ϱ� �Լ�
            }
            else
            {
                print("�ְ��� ���Ž���");
            }
        });
    }
}