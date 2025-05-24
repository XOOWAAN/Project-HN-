// 일과를 진행하고 다음 사람을 등장시키는 흐름 매니저

using UnityEngine;
using System.Collections;

public class GameFlowManager : MonoBehaviour
{
    [Header("기본 컴포넌트 연결")]
    public DocumentGenerator documentGenerator; // 문서 생성기
    public PersonSpawner personSpawner;          // 인물 생성기 (생성기들은 기존 스크립트를 활용하는 것으로 수정 필요)
    public Transform documentParent;             // 문서 부모 위치 (문서가 생성될 위치)
    
    [Header("UI 버튼")]
    public GameObject issueButton;               // 발급 버튼 (버튼 오브젝트)
    
    private bool isProcessing = false;           // 현재 처리 중인지 여부

    // 일과 시작을 외부에서 호출하면 여기에 로직이 실행됨
    public void StartWorkDay()
    {
        SpawnNextEntry();
    }

    // 다음 인물과 문서 생성
    public void SpawnNextEntry()
    {
        if (isProcessing) return;
        isProcessing = true;

        // 인물 생성
        if(personSpawner != null)
            personSpawner.SpawnRandomPerson();

        // 문서 생성
        documentGenerator.GenerateRandomDocument();

        // 발급 버튼 활성화
        if (issueButton != null)
            issueButton.SetActive(true);
    }

    // 발급 버튼 눌렀을 때 호출 (문서 자동 전달)
    public void OnIssueButtonPressed()
    {
        if (!isProcessing) return;

        // TODO: 문서 전달 애니메이션 처리하면 여기에 넣기

        // 발급 버튼 비활성화
        if (issueButton != null)
            issueButton.SetActive(false);

        EndProcessing();
    }

    // 문서를 다 돌려주면 거절 처리 (문서 반환 이벤트 등에서 호출)
    public void OnDocumentsReturned()
    {
        if (!isProcessing) return;

        // TODO: 거절 처리 애니메이션 넣을 수 있음

        // 발급 버튼 비활성화
        if (issueButton != null)
            issueButton.SetActive(false);

        EndProcessing();
    }

    // 한 인물 처리 완료 후 다음 인물 대기
    private void EndProcessing()
    {
        // 처리 완료 후 짧은 딜레이를 두고 다음 인물 등장
        StartCoroutine(DelayNextEntry());
    }

    private IEnumerator DelayNextEntry()
    {
        yield return new WaitForSeconds(1.5f);

        isProcessing = false;

        // 다음 인물과 문서 생성
        SpawnNextEntry();
    }
}