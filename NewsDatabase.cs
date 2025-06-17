using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NewsDialogueUnit
{
    public string dialogue;     // 대사
    public Sprite image;        // 해당 대사에 대응하는 이미지
}

[System.Serializable]
public class NewsData
{
    public string summary;                       // 요약 문장 (알림용)
    public List<NewsDialogueUnit> dialogues;     // 여러 개의 대사 + 이미지 묶음
}

// 각 날짜에 대한 뉴스 데이터를 관리하는 스크립트
public class NewsDatabase : MonoBehaviour
{   
    public List<NewsDataPerDay> newsList; // 에디터에서 날짜별 뉴스 설정 가능

    // 일자에 해당하는 뉴스 반환
    public NewsData GetNewsDataForDay(int day)
    {
        foreach (var item in newsList)
        {
            if (item.day == day)
                return item.newsData;
        }
        return new NewsData
        {
            summary = "오늘은 특별한 뉴스가 없습니다.",
            dialogues = new List<NewsDialogueUnit>
            {
                new NewsDialogueUnit
                {
                    dialogue = "뉴스 없음",
                    image = null
                }
            }
        };
    }
}