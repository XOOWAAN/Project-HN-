// 하루의 시작과 끝을 관리하는 매니저
// 추후 돈 정산, 인물 수 제한, 일일 목표, 게임오버 조건을 넣을 수 있음

public class DayManager : MonoBehaviour
{
    public NotificationPanel notificationPanel;
    public NewsSummary newsDatabase;
    public GameFlowManager gameFlowManager;

    public void StartDay(int day)
    {
        StartCoroutine(HandleDayStartSequence(day));
    }

    private IEnumerator HandleDayStartSequence(int day)
    {
        // 뉴스 또는 대화 이벤트
        string news = newsDatabase.GetNewsForDay(day);
        notificationPanel.AddNewsNotification(news);

        // 대사/이벤트 연출 또는 TV 영상 재생 등 (추가 가능)
        yield return new WaitForSeconds(3f); // 예: 뉴스 보여주는 시간

        // 일과 시작 (GameFlowManager)
        gameFlowManager.StartWorkDay();
    }
}