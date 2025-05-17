// 발급 버튼 클릭 시 문서 출력 + 인물 전달 애니메이션을 재생

using UnityEngine;

public class IssueDocumentButton : MonoBehaviour
{
    public Animator printerAnimator;      // 인쇄기 애니메이터
    public Animator characterAnimator;    // 인물이 문서를 받는 애니메이션
    public GameObject documentObject;     // 출력될 문서 오브젝트 (애니메이션용)

    // 발급 버튼 클릭 시 호출되는 함수
    public void OnIssueButtonClick()
    {
        // 문서 UI 활성화
        documentObject.SetActive(true);

        // 인쇄기 출력 애니메이션 재생
        printerAnimator.SetTrigger("Print");

        // 일정 시간 후 인물에게 전달 애니메이션 재생
        Invoke(nameof(DeliverToCharacter), 1.5f);  // 타이밍은 인쇄기 연출에 따라 조절
    }

    // 인물에게 문서가 전달되는 애니메이션 처리
    private void DeliverToCharacter()
    {
        characterAnimator.SetTrigger("ReceiveDoc");

        // 이후 문서 비활성화 처리도 가능 (애니메이션에 따라)
        // documentObject.SetActive(false);
    }
}