// 일과가 시작되면 인물과 문서를 생성하고, 발급 또는 거절 처리 후 다음 인물로 진행함
// 페이퍼플리즈처럼 반복되는 인물 처리 루프를 담당

using UnityEngine;
using System.Collections;

public class GameFlowManager : MonoBehaviour
{
    [Header("기본 컴포넌트 연결")]
    public DocumentGenerator documentGenerator;     // 문서 생성기
    public PersonGenerator personGenerator;         // 인물 생성기
    public Transform documentParent;                // 문서 부모 오브젝트 (문서가 생성될 위치)

    [Header("UI 버튼")]
    public GameObject issueButton;                  // 발급 버튼 오브젝트 (IssueDocumentButton 스크립트가 붙어 있음)

    private bool isProcessing = false;              // 현재 인물 처리 중인지 여부 (중복 처리 방지용)

    // 외부에서 호출되어 일과 시작 (예: 뉴스 이벤트 종료 후 호출됨)
    public void StartWorkDay()
    {
        SpawnNextEntry();
    }

    // 다음 인물과 문서를 생성하는 루틴
    public void SpawnNextEntry()
    {
        if (isProcessing) return;
        isProcessing = true;

        // 인물 생성
        if (personGenerator != null)
            personGenerator.GenerateRandomPerson();

        // 문서 생성
        if (documentGenerator != null)
            documentGenerator.GenerateRandomDocument();

        // 발급 버튼 활성화
        if (issueButton != null)
            issueButton.SetActive(true);
    }

    // 발급 버튼이 눌렸을 때 호출됨
    // 문서가 자동으로 인물에게 전달된 것으로 간주
    public void OnIssueButtonPressed()
    {
        if (!isProcessing) return;

        // 발급 버튼 비활성화
        if (issueButton != null)
            issueButton.SetActive(false);

        // 다음 인물로 넘어가기 위한 처리 종료
        EndProcessing();
    }

    // 플레이어가 문서를 모두 반환한 경우 거절 처리로 간주
    public void OnDocumentsReturned()
    {
        if (!isProcessing) return;

        // 발급 버튼 비활성화 (혹시 눌리지 않았더라도 처리)
        if (issueButton != null)
            issueButton.SetActive(false);

        // 다음 인물로 넘어가기 위한 처리 종료
        EndProcessing();
    }

    // 인물 하나에 대한 처리 완료 후 다음 인물 등장까지 딜레이
    private void EndProcessing()
    {
        StartCoroutine(DelayNextEntry());
    }

    // 약간의 딜레이 후 다음 인물과 문서를 생성
    private IEnumerator DelayNextEntry()
    {
        yield return new WaitForSeconds(1.5f); // 처리 후 대기 시간

        isProcessing = false;

        // 다음 처리 시작
        SpawnNextEntry();
    }
}