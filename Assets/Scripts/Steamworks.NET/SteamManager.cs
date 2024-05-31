// The SteamManager is designed to work with Steamworks.NET
// This file is released into the public domain.
// Where that dedication is not recognized you are granted a perpetual,
// irrevocable license to copy and modify this file as you see fit.
//
// Version: 1.0.13

#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

using UnityEngine;
#if !DISABLESTEAMWORKS
using System.Collections;
using Steamworks;
using BackEnd;
using TMPro;
using System.Threading.Tasks; // [����] async ����� �̿��ϱ� ���ؼ��� �ش� namepsace�� �ʿ��մϴ�.
using System;
#endif

//
// The SteamManager provides a base implementation of Steamworks.NET on which you can build upon.
// It handles the basics of starting up and shutting down the SteamAPI for use.
//
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
    public static SteamManager instance = null;

    string idNickName = "";

    public GameObject NickObj;
    public TextMeshProUGUI NickNameText;
    public int IsLogin = 0;


    private byte[] m_Ticket;
    private uint m_pcbTicket;
    private HAuthTicket m_HAuthTicket;

    string sessionTicket = string.Empty;

    public int CountryNum = 99;

    protected Callback<GetAuthSessionTicketResponse_t> m_GetAuthSessionTicketResponse;

    void OnGetAuthSessionTicketResponse(GetAuthSessionTicketResponse_t pCallback)
    {
        //Resize to buffer of 1024
        System.Array.Resize(ref m_Ticket, (int)m_pcbTicket);

        //format as Hex
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (byte b in m_Ticket) sb.AppendFormat("{0:x2}", b);

        sessionTicket = sb.ToString();
        Debug.Log("Hex encoded ticket: " + sb.ToString());
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this; 
            print("����");
            FirstLogin();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            print("�̹� ����");

            if (NickObj!=null) NickObj.SetActive(false);
            //NickNameText.text = DataManager.Instance.nowPlayer.nickName;
            Destroy(this.gameObject);
        }
    }
    void FirstLogin()
    {
        if (SteamManager.Initialized)
        {
            m_GetAuthSessionTicketResponse = Callback<GetAuthSessionTicketResponse_t>.Create(OnGetAuthSessionTicketResponse);

            m_Ticket = new byte[1024];
            m_HAuthTicket = SteamUser.GetAuthSessionTicket(m_Ticket, 1024, out m_pcbTicket);

        }

        Backend.Initialize(true);
        BackendReturnObject bro = Backend.BMember.AuthorizeFederation(sessionTicket, FederationType.Steam);

        if (bro.IsSuccess())
        {
            Debug.Log("Steam �α��� ����" + bro);
            //���� ó��
        }
        else
        {
            if (bro.IsClientRequestFailError())
            {
                Debug.Log("��Ʈ��ũ�� �Ͻ������� �������� ���");
            }
            Debug.LogError("Steam �α��� ����" + bro);
            //���� ó��
        }

        Debug.Log("���� ���̵� : " + SteamUser.GetSteamID()); // ������ȣ - ���� ����
        Debug.Log("����� �г��� : " + SteamFriends.GetPersonaName()); // �г��� - �г��� (ex : Backend01 )
        Debug.Log("����� ���� ���� : " + SteamUtils.GetIPCountry()); // ���� ���� ���� - KR
        CountryCode(SteamUtils.GetIPCountry());


        //�� ���󺰷� ������ȣ �ִ� �޼��� �ۼ�

        idNickName = SteamFriends.GetPersonaName().ToString();
        DataManager.Instance.nowPlayer.nickName = idNickName;
        DataManager.Instance.SaveData();
        Login(idNickName, idNickName);
    }
    enum CountryName
    {
        BR, CN, CZ, DE, US, GB, ES,FR,GR,IN,IT,JP,KR,PL,PT,RO,RU,SE,TH,TR,TW
        // �ʿ��� ��ŭ �߰� ����
    }

    void CountryCode(string countryName)
    {
        // ���ڿ��� enum���� ��ȯ�Ͽ� ����ġ ������ ó���մϴ�.
        CountryName name;
        if (Enum.TryParse(countryName, out name))
        {
            switch (name)
            {
                case CountryName.BR:
                    CountryNum = 1;
                    // �ѱ��� ���� ó��
                    Debug.Log("������� �ڵ�� 1�Դϴ�.");
                    break;
                case CountryName.CN:
                    CountryNum = 2;
                    // �̱��� ���� ó��
                    Debug.Log("�̱��� �ڵ�� 1�Դϴ�.");
                    break;
                case CountryName.CZ:
                    CountryNum = 3;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�Ϻ��� �ڵ�� 1�Դϴ�.");
                    break;
                case CountryName.DE:
                    CountryNum = 4;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�Ϻ��� �ڵ�� 1�Դϴ�.");
                    break;
                case CountryName.US:
                    CountryNum = 5;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�Ϻ��� �ڵ�� 1�Դϴ�.");
                    break;
                case CountryName.GB:
                    CountryNum = 6;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�Ϻ��� �ڵ�� 1�Դϴ�.");
                    break;
                case CountryName.ES:
                    CountryNum = 7;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�������� �ڵ�� 7�Դϴ�.");
                    break;
                case CountryName.FR:
                    CountryNum = 8;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�������� �ڵ�� 8�Դϴ�.");
                    break;
                case CountryName.GR:
                    CountryNum = 9;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�׸����� �ڵ�� 9�Դϴ�.");
                    break;
                case CountryName.IN:
                    CountryNum = 10;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�ε��� �ڵ�� 10�Դϴ�.");
                    break;
                case CountryName.IT:
                    CountryNum = 11;
                    // �Ϻ��� ���� ó��
                    Debug.Log("��Ż������ �ڵ�� 11�Դϴ�.");
                    break;
                case CountryName.JP:
                    CountryNum = 12;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�Ϻ��� �ڵ�� 12�Դϴ�.");
                    break;
                case CountryName.KR:
                    CountryNum = 13;
                    // ���ѹα��� ���� ó��
                    Debug.Log("���ѹα��� �ڵ�� 13�Դϴ�.");
                    break;
                case CountryName.PL:
                    CountryNum = 14;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�������� �ڵ�� 14�Դϴ�.");
                    break;
                case CountryName.PT:
                    CountryNum = 15;
                    // �Ϻ��� ���� ó��
                    Debug.Log("���������� �ڵ�� 15�Դϴ�.");
                    break;
                case CountryName.RO:
                    CountryNum = 16;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�縶�Ͼ��� �ڵ�� 16�Դϴ�.");
                    break;
                case CountryName.RU:
                    CountryNum = 17;
                    // �Ϻ��� ���� ó��
                    Debug.Log("���þ��� �ڵ�� 17�Դϴ�.");
                    break;
                case CountryName.SE:
                    CountryNum = 18;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�������� �ڵ�� 18�Դϴ�.");
                    break;
                case CountryName.TH:
                    CountryNum = 19;
                    // �Ϻ��� ���� ó��
                    Debug.Log("�±��� �ڵ�� 19�Դϴ�.");
                    break;
                case CountryName.TR:
                    CountryNum = 20;
                    // �Ϻ��� ���� ó��
                    Debug.Log("��Ű�� �ڵ�� 20�Դϴ�.");
                    break;
                case CountryName.TW:
                    CountryNum = 21;
                    // �Ϻ��� ���� ó��
                    Debug.Log("Ÿ�̿��� �ڵ�� 21�Դϴ�.");
                    break;
                // �ʿ��� ��ŭ �߰� ����
                default:
                    // ���� ó�� �Ǵ� �⺻ ����
                    CountryNum = 0;
                    Debug.LogWarning("�ش� ���� �ڵ尡 ���ǵ��� �ʾҽ��ϴ�.");
                    break;
            }
        }
        else
        {
            // ���� ó��
            CountryNum = 0;
            Debug.LogError("�ùٸ� ���� �ڵ尡 �ƴմϴ�.");
        }
    }
    public async void Register(string id, string pw)
    {
        NickNameText.text = id;
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomSignUp(id, pw); // [�߰�] �ڳ� ȸ������ �Լ�
            BackendGameData.Instance.GameDataInsert(); //[�߰�] ������ ���� �Լ�
            BackendLogin.Instance.UpdateNickname(id);
            BackendRank.Instance.RankInsert(0, 1, CountryNum);
        });
    }
    async void Login(string id, string pw)
    {
        NickNameText.text = id;
        StartCoroutine(OffNickName());
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomLogin(id, pw); // �ڳ� �α��� �Լ�
        });
    }
    IEnumerator OffNickName()
    {
        yield return new WaitForSeconds(3);
        if(NickObj!=null) NickObj.SetActive(false);
    }
    public async void Rank(int score, int charCode)
    {
        await Task.Run(() =>
        {
            //print("�г���:" + Backend.UserNickName + "�����ְ���:" + BackendRank.Instance.RankGet() + "ĳ����" + charCode + "��");

            int currentBestScore = 0;//BackendRank.Instance.RankGet();
            print("�ڽ� �ְ���" + currentBestScore);
            if (score > currentBestScore)
            {
                print("�ְ��� ����");
                BackendRank.Instance.RankInsert(score, charCode, CountryNum); // ��ŷ ����ϱ� �Լ�
            }
            else
            {
                print("�ְ��� ���Ž���");
            }
        });
    }



#if !DISABLESTEAMWORKS
    protected static bool s_EverInitialized = false;

    protected static SteamManager s_instance;
    protected static SteamManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                return new GameObject("SteamManager").AddComponent<SteamManager>();
            }
            else
            {
                return s_instance;
            }
        }
    }

    protected bool m_bInitialized = false;
    public static bool Initialized
    {
        get
        {
            return Instance.m_bInitialized;
        }
    }

    protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

    [AOT.MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
    protected static void SteamAPIDebugTextHook(int nSeverity, System.Text.StringBuilder pchDebugText)
    {
        Debug.LogWarning(pchDebugText);
    }

#if UNITY_2019_3_OR_NEWER
    // In case of disabled Domain Reload, reset static members before entering Play Mode.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitOnPlayMode()
    {
        s_EverInitialized = false;
        s_instance = null;
    }
#endif

    protected virtual void Awake()
    {
        // Only one instance of SteamManager at a time!
        if (s_instance != null)
        {
            //Destroy(gameObject);
            return;
        }
        s_instance = this;

        if (s_EverInitialized)
        {
            // This is almost always an error.
            // The most common case where this happens is when SteamManager gets destroyed because of Application.Quit(),
            // and then some Steamworks code in some other OnDestroy gets called afterwards, creating a new SteamManager.
            // You should never call Steamworks functions in OnDestroy, always prefer OnDisable if possible.
            throw new System.Exception("Tried to Initialize the SteamAPI twice in one session!");
        }

        // We want our SteamManager Instance to persist across scenes.
        //DontDestroyOnLoad(gameObject);

        if (!Packsize.Test())
        {
            Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
        }

        if (!DllCheck.Test())
        {
            Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
        }

        try
        {
            // If Steam is not running or the game wasn't started through Steam, SteamAPI_RestartAppIfNecessary starts the
            // Steam client and also launches this game again if the User owns it. This can act as a rudimentary form of DRM.
            // Note that this will run which ever version you have installed in steam. Which may not be the precise executable
            // we were currently running.

            // Once you get a Steam AppID assigned by Valve, you need to replace AppId_t.Invalid with it and
            // remove steam_appid.txt from the game depot. eg: "(AppId_t)480" or "new AppId_t(480)".
            // See the Valve documentation for more information: https://partner.steamgames.com/doc/sdk/api#initialization_and_shutdown
            if (SteamAPI.RestartAppIfNecessary(AppId_t.Invalid))
            {
                Debug.Log("[Steamworks.NET] Shutting down because RestartAppIfNecessary returned true. Steam will restart the application.");

                Application.Quit();
                return;
            }
        }
        catch (System.DllNotFoundException e)
        { // We catch this exception here, as it will be the first occurrence of it.
            Debug.LogError("[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n" + e, this);

            Application.Quit();
            return;
        }

        // Initializes the Steamworks API.
        // If this returns false then this indicates one of the following conditions:
        // [*] The Steam client isn't running. A running Steam client is required to provide implementations of the various Steamworks interfaces.
        // [*] The Steam client couldn't determine the App ID of game. If you're running your application from the executable or debugger directly then you must have a [code-inline]steam_appid.txt[/code-inline] in your game directory next to the executable, with your app ID in it and nothing else. Steam will look for this file in the current working directory. If you are running your executable from a different directory you may need to relocate the [code-inline]steam_appid.txt[/code-inline] file.
        // [*] Your application is not running under the same OS user context as the Steam client, such as a different user or administration access level.
        // [*] Ensure that you own a license for the App ID on the currently active Steam account. Your game must show up in your Steam library.
        // [*] Your App ID is not completely set up, i.e. in Release State: Unavailable, or it's missing default packages.
        // Valve's documentation for this is located here:
        // https://partner.steamgames.com/doc/sdk/api#initialization_and_shutdown
        m_bInitialized = SteamAPI.Init();
        if (!m_bInitialized)
        {
            Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);

            return;
        }

        s_EverInitialized = true;
    }

    // This should only ever get called on first load and after an Assembly reload, You should never Disable the Steamworks Manager yourself.
    protected virtual void OnEnable()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }

        if (!m_bInitialized)
        {
            return;
        }

        if (m_SteamAPIWarningMessageHook == null)
        {
            // Set up our callback to receive warning messages from Steam.
            // You must launch with "-debug_steamapi" in the launch args to receive warnings.
            m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamAPIDebugTextHook);
            SteamClient.SetWarningMessageHook(m_SteamAPIWarningMessageHook);
        }
    }

    // OnApplicationQuit gets called too early to shutdown the SteamAPI.
    // Because the SteamManager should be persistent and never disabled or destroyed we can shutdown the SteamAPI here.
    // Thus it is not recommended to perform any Steamworks work in other OnDestroy functions as the order of execution can not be garenteed upon Shutdown. Prefer OnDisable().
    protected virtual void OnDestroy()
    {
        if (s_instance != this)
        {
            return;
        }

        s_instance = null;

        if (!m_bInitialized)
        {
            return;
        }

        SteamAPI.Shutdown();
    }

    protected virtual void Update()
    {
        if (!m_bInitialized)
        {
            return;
        }

        // Run Steam client callbacks
        SteamAPI.RunCallbacks();
    }
#else
	public static bool Initialized {
		get {
			return false;
		}
	}
#endif // !DISABLESTEAMWORKS
}
