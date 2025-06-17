using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NewsData
{
    public string summary;       // 요약 문장 (알림용)
    public string dialogue;      // TV 대사 출력용
    public Sprite newsImage;     // TV 이미지 출력용
}

// 각 날짜에 대한 뉴스 데이터를 관리하는 스크립트
public class NewsDatabase : MonoBehaviour
{   
    // NewsDataPerDay는 날짜와 뉴스 데이터 쌍을 묶음
    public List<NewsDataPerDay> newsList = new List<NewsDataPerDay>();

    private void Awake()
    {
        // 1일차: 금속공장 폭발
        newsList.Add(new NewsDataPerDay
        {
            day = 1,
            newsData = new NewsData
            {
                summary = "공장에서 폭발 사고 발생. 사망자 다수.",
                dialogue = "금속공장이 폭발했습니다\n많은 사람이 죽었습니다",
                newsImage = Resources.Load<Sprite>("Images/factory")
            }
        });

        // 2일차: 체포 관련
        newsList.Add(new NewsDataPerDay
        {
            day = 2,
            newsData = new NewsData
            {
                summary = "공장주 체포됨. 안전 부실 혐의.",
                dialogue = "공장주를 체포했습니다\n안전 관리를 부실하게 한 혐의입니다",
                newsImage = Resources.Load<Sprite>("Images/arrest")
            }
        });
    }
}