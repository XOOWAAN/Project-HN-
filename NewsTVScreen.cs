[System.Serializable]
// NewsDataPerDay는 뉴스 데이터를 날짜와 함께 저장
// Unity 에디터에서 날짜별 뉴스 항목을 직관적으로 구성하기 위함
public class NewsDataPerDay
{
    public int day;
    public NewsData newsData;
}

// 뉴스 연출을 위한 TV 스크린 제어 스크립트 예시
public class NewsTVScreen : MonoBehaviour
{
    public UnityEngine.UI.Image tvImageUI;       // TV 화면 이미지
    public UnityEngine.UI.Text dialogueTextUI;   // TV 화면 대사 텍스트

    // 뉴스 정보 출력
    public void ShowNews(NewsData data)
    {
        tvImageUI.sprite = data.newsImage;
        dialogueTextUI.text = data.dialogue;
        gameObject.SetActive(true); // 화면 켜기
    }

    // 이후 자동 종료하거나 애니메이션으로 끄는 것도 가능
    public void HideNews()
    {
        gameObject.SetActive(false);
    }
}