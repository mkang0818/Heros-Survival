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

        Debug.Log("데이터 조회를 시도합니다.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("데이터 조회 중 문제가 발생했습니다 : " + bro);
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
                Debug.LogError("데이터 삽입 중 문제가 발생했습니다 : " + bro2);
                return;
            }

            rowInDate = bro2.GetInDate();
        }

        // 여러 데이터를 하나의 문자열로 결합하여 extraData에 저장
        string atk = $"{CountryNum}|{CharCode}";

        Param param = new Param();
        param.Add("level", score);
        param.Add("atk", atk);

        Debug.Log("Param data: " + atk);

        Debug.Log("랭킹 삽입을 시도합니다.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if (rankBro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 등록 중 오류가 발생했습니다. : " + rankBro);
            return;
        }

        Debug.Log("랭킹 삽입에 성공했습니다. : " + rankBro);
    }

    public void RankGet()
    {
        Debug.Log("점수 조회");
        string rankUUID = "5ca6bd20-e954-11ee-9d2b-6d64e7ab239c";
        var bro = Backend.URank.User.GetMyRank(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }

        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine("순위 : " + jsonData["rank"].ToString());
            info.AppendLine("닉네임 : " + jsonData["nickname"].ToString());
            info.AppendLine("점수 : " + jsonData["score"].ToString());

            if (jsonData.Keys.Contains("atk"))
            {
                string extraData = jsonData["atk"].ToString();
                string[] extraDataSplit = extraData.Split('|');

                if (extraDataSplit.Length >= 2)
                {
                    int charCode = int.Parse(extraDataSplit[0]);
                    int countryNum = int.Parse(extraDataSplit[1]);

                    info.AppendLine("캐릭터 코드 : " + charCode);
                    info.AppendLine("나라 번호 : " + countryNum);
                }
            }
            else
            {
                info.AppendLine("추가 데이터가 없습니다.");
            }

            info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
            info.AppendLine("정렬번호 : " + jsonData["index"].ToString());
            info.AppendLine();
            Debug.Log(info);
        }
    }
}
