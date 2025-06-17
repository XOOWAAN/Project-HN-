// 하루의 시작과 끝을 관리하는 매니저
// 추후 돈 정산, 인물 수 제한, 일일 목표, 게임오버 조건을 넣을 수 있음

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayManager : MonoBehaviour
{
    public NotificationPanel notificationPanel;
    public NewsDatabase newsDatabase; // NewsSummary → NewsDatabase로 변경
    public GameFlowManager gameFlowManager;
    public NewsTVScreen newsTVScreen; // 뉴스 화면 연출용 오브젝트 연결 (TV 이미지와 대사 출력 포함)

    private void Start()
    {
        newsTVScreen.autoPlay = true;       // 대사 다 출력 후 자동 넘김 활성화
        newsTVScreen.autoDelay = 1.0f;      // 대사 간 1초 대기
    }

    public void StartDay(int day)
    {
        StartCoroutine(HandleDayStartSequence(day));
    }

    private IEnumerator HandleDayStartSequence(int day)
    {
        // 뉴스 전체 데이터 가져오기
        NewsData todayNews = newsDatabase.GetNewsDataForDay(day); // 뉴스 텍스트 + 이미지 포함 객체 반환

        // 뉴스 TV 화면 보여주기 (다 끝날 때까지 대기)
        yield return StartCoroutine(newsTVScreen.ShowNews(todayNews));

        // 컴퓨터 창 알림 탭에 요약본 추가
        notificationPanel.AddNewsNotification(todayNews.summary); // 요약문만 알림으로 추가

        // 일과 시작 (GameFlowManager)
        gameFlowManager.StartWorkDay();
    }
}