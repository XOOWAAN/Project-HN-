// GameFlowManager.cs
// ----------------------------
// 게임의 주 흐름을 관리하는 매니저 스크립트
// 인물 생성 → 문서 생성 → 대사 재생 → 플레이어 처리 → 퇴장까지의 루프 반복
// 문서 생성 및 애니메이션 흐름도 함께 제어하며, 매 인물에 대해 자동 순환 실행

// 주요 메서드:
// - Start(): 게임 루프 시작 (코루틴)
// - GameLoop(): 인물 처리 루프를 무한 반복
// - HandleNextPerson(): 인물 생성, 문서 생성, 애니메이션 재생, 처리 버튼 제어까지 담당
// - OnProcessComplete(): 플레이어가 처리 완료 버튼을 눌렀을 때 호출됨

// ⚠️현재 대사 출력 후 처리 버튼이 활성화되는데, 추후 구조 변경되면 이 부분도 바꿔야 함⚠️

using UnityEngine;
using System.Collections;
using TMPro;

public class GameFlowManager : MonoBehaviour
{
    public PersonGenerator personGenerator;
    public DocumentGenerator documentGenerator;

    public Transform characterParent;
    public DialoguePlayer dialoguePlayer;
    public ButtonManager buttonManager;

    private GameObject currentCharacter;
    private PersonData currentPerson;
    private DocPersonAnimation currentAnim;

    private bool isProcessing = false;

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            yield return StartCoroutine(HandleNextPerson());
        }
    }

    IEnumerator HandleNextPerson()
    {
        isProcessing = true;

        // 1. 인물 생성 및 등장
        currentPerson = personGenerator.GeneratePersonData();
        currentCharacter = personGenerator.SpawnCharacter(currentPerson, characterParent);

        // 2. 문서 생성
        GameObject docUI = documentGenerator.GenerateDocumentsForPerson(currentPerson);

        // 3. 애니메이션 할당 및 재생
        currentAnim = currentCharacter.GetComponent<DocPersonAnimation>();
        if (currentAnim != null && docUI != null)
        {
            currentAnim.documentRect = docUI.GetComponent<RectTransform>();
            currentAnim.personTransform = currentCharacter.transform;
            currentAnim.PlayEntrance();
        }

        // 4. 대사 재생
        yield return StartCoroutine(dialoguePlayer.PlayRandomDialogue(currentPerson));

        // 5. 처리 버튼 활성화
        buttonManager.SetProcessButtonInteractable(true);

        // 6. 완료 버튼 누를 때까지 대기
        while (isProcessing)
        {
            yield return null;
        }

        // 7. 퇴장 애니메이션 실행
        if (currentAnim != null)
        {
            currentAnim.PlayExit();
        }

        // 8. 처리 후 대기
        buttonManager.SetProcessButtonInteractable(false);
        yield return new WaitForSeconds(1f);
    }

    public void OnProcessComplete()
    {
        // 확대 화면에서 완료 버튼을 눌렀을 때 호출됨
        isProcessing = false;
    }
}