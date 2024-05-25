using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BackEnd;

public class BackendRank
{
    private static BackendRank _instance = null;

    public static BackendRank Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendRank();
            }

            return _instance;
        }
    }

    public void RankInsert(int score, int CharCode, int CountryNum)
    {
        string rankUUID = "5ca6bd20-e954-11ee-9d2b-6d64e7ab239c";

        string tableName = "RankingTable";
        string rowInDate = string.Empty;

        Debug.Log("������ ��ȸ�� �õ��մϴ�.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("������ ��ȸ �� ������ �߻��߽��ϴ� : " + bro);
            return;
        }

        if (bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            var bro2 = Backend.GameData.Insert(tableName);

            if (bro2.IsSuccess() == false)
            {
                Debug.LogError("������ ���� �� ������ �߻��߽��ϴ� : " + bro2);
                return;
            }

            rowInDate = bro2.GetInDate();
        }

        // ���� �����͸� �ϳ��� ���ڿ��� �����Ͽ� extraData�� ����
        string atk = $"{CountryNum}|{CharCode}";

        Param param = new Param();
        param.Add("level", score);
        param.Add("atk", atk);

        Debug.Log("Param data: " + atk);

        Debug.Log("��ŷ ������ �õ��մϴ�.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if (rankBro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��� �� ������ �߻��߽��ϴ�. : " + rankBro);
            return;
        }

        Debug.Log("��ŷ ���Կ� �����߽��ϴ�. : " + rankBro);
    }

    public void RankGet()
    {
        Debug.Log("���� ��ȸ");
        string rankUUID = "5ca6bd20-e954-11ee-9d2b-6d64e7ab239c";
        var bro = Backend.URank.User.GetMyRank(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��ȸ�� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }

        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine("���� : " + jsonData["rank"].ToString());
            info.AppendLine("�г��� : " + jsonData["nickname"].ToString());
            info.AppendLine("���� : " + jsonData["score"].ToString());

            if (jsonData.Keys.Contains("atk"))
            {
                string extraData = jsonData["atk"].ToString();
                string[] extraDataSplit = extraData.Split('|');

                if (extraDataSplit.Length >= 2)
                {
                    int charCode = int.Parse(extraDataSplit[0]);
                    int countryNum = int.Parse(extraDataSplit[1]);

                    info.AppendLine("ĳ���� �ڵ� : " + charCode);
                    info.AppendLine("���� ��ȣ : " + countryNum);
                }
            }
            else
            {
                info.AppendLine("�߰� �����Ͱ� �����ϴ�.");
            }

            info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
            info.AppendLine("���Ĺ�ȣ : " + jsonData["index"].ToString());
            info.AppendLine();
            Debug.Log(info);
        }
    }
}
