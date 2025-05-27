// 하루의 시작과 끝을 관리하는 매니저
// 추후 돈 정산, 인물 수 제한, 일일 목표, 게임오버 조건을 넣을 수 있음

public class DayManager : MonoBehaviour
{
    public NotificationPanel notificationPanel;
    public NewsSummaryDatabase newsDatabase;

    public void StartDay(int day)
    {
        string news = newsDatabase.GetNewsForDay(day);
        notificationPanel.AddNewsNotification(news);
    }
}