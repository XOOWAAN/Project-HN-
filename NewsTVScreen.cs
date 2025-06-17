[System.Serializable]
// NewsDataPerDay는 뉴스 데이터를 날짜와 함께 저장
// Unity 에디터에서 날짜별 뉴스 항목을 직관적으로 구성하기 위함
public class NewsDataPerDay
{
    public int day;
    public NewsData newsData;
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 뉴스 연출을 위한 TV 스크린 제어 스크립트 예시
public class NewsTVScreen : MonoBehaviour
{
    public Image tvImageUI;           // TV 화면 이미지
    public Text dialogueTextUI;       // TV 화면 대사 텍스트
    public float typingSpeed = 0.05f; // 타이핑 속도
    public bool autoPlay = false;     // 자동 넘김 여부
    public float autoDelay = 1.0f;    // 자동 넘김 시 대사 간 대기 시간

    private bool skipTyping = false;  // 타이핑 건너뛰기 플래그

    // 뉴스 정보 출력 (코루틴으로 순차 재생)
    public IEnumerator ShowNews(NewsData data)
    {
        gameObject.SetActive(true);

        foreach (var unit in data.dialogues)
        {
            tvImageUI.sprite = unit.image;

            yield return StartCoroutine(TypeDialogue(unit.dialogue));

            if (autoPlay)
            {
                yield return new WaitForSeconds(autoDelay);
            }
            else
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0));
            }
        }

        gameObject.SetActive(false);
    }

    // 텍스트 타이핑 효과 출력
    private IEnumerator TypeDialogue(string text)
    {
        dialogueTextUI.text = "";
        skipTyping = false;

        foreach (char c in text)
        {
            dialogueTextUI.text += c;

            // 타이핑 효과 도중 입력 시 전체를 바로 출력
            // 대사는 지정된 키 혹은 버튼을 눌러 넘김(에디터에서 키 지정 필요)
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                skipTyping = true;
                break;
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        // skipTyping이 참이면 남은 전체 문장 바로 출력
        if (skipTyping)
        {
            dialogueTextUI.text = text;
            yield return null;
        }
    }
}