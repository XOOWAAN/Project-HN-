// 하루의 시작과 끝을 관리하는 매니저
// 추후 돈 정산, 인물 수 제한, 일일 목표, 게임오버 조건을 넣을 수 있음

using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour
{
    public NotificationPanel notificationPanel;
    public NewsDatabase newsDatabase; // NewsSummary → NewsDatabase로 변경
    public GameFlowManager gameFlowManager;
    public NewsTVScreen newsTVScreen; // 뉴스 화면 연출용 오브젝트 연결 (TV 이미지와 대사 출력 포함)

    public void StartDay(int day)
    {
        StartCoroutine(HandleDayStartSequence(day));
    }

    private IEnumerator HandleDayStartSequence(int day)
    {
        // 뉴스 전체 데이터 가져오기(뉴스 텍스트 + 이미지)
        // newsList에서 해당 day의 newsData를 가져옴
        NewsData todayNews = newsDatabase.GetNewsDataForDay(day); 

        // 뉴스 TV 화면 보여주기
        newsTVScreen.ShowNews(todayNews); // TV 화면에 뉴스 대사와 이미지 출력

        yield return new WaitForSeconds(5f); // 뉴스 연출 시간

        // 컴퓨터 창 알림 탭에 요약본 추가
        // 요약문만 알림으로 추가
        notificationPanel.AddNewsNotification(todayNews.summary); 
        
        // 일과 시작 (GameFlowManager)
        gameFlowManager.StartWorkDay();
    }
}