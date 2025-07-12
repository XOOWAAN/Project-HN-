// 게임의 메인 루프를 담당하는 매니저
// 1. 일과 시작 시 GameLoop 코루틴으로 무한 루프 시작
// 2. 각 루프에서 한 명의 인물 처리 (HandleNextPerson)

// HandleNextPerson 작동 절차:
// - PersonGenerator를 통해 인물 생성 후 스폰
// - 이 데이터를 DocumentGenerator에 전달해 문서 3종 생성
// - 인물 등장 후 랜덤 대사 재생
// - 처리 시 버튼 활성화, 처리 후에는 비활성화

// 주요 연결 컴포넌트:
// - PersonGenerator: 인물 정보 및 오브젝트 생성
// - DocumentGenerator: 인물 기반 문서 생성
// - DialoguePlayer: 랜덤 대사 출력
// - ButtonManager: 수락/거절 버튼 UI 제어

// ⚠️ 인물 생성과 문서 생성을 분리해 제어 가능하도록 설계됨
// ⚠️ GameLoop 내에서 전체 심사 흐름이 반복 실행됨

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
        currentPerson = personGenerator.GeneratePerson(); // PersonData 생성
        currentCharacter = personGenerator.SpawnCharacter(currentPerson, characterParent); // 실제 오브젝트 생성

        // 2. 문서 생성 (인물에 맞게 생성하도록 수정됨)
        documentGenerator.GenerateDocumentsForPerson(currentPerson); // ← 여기서 데이터 전달됨

        // 3. 대사 재생
        yield return StartCoroutine(dialoguePlayer.PlayRandomDialogue(currentPerson));

        // 4. 처리 버튼 활성화 (문서가 도착한 뒤에만 가능)
        buttonManager.SetProcessButtonInteractable(true);

        // 5. 확대 화면에서 완료 버튼 누를 때까지 대기
        while (isProcessing)
        {
            yield return null;
        }

        // 6. 문서 돌려주는 애니메이션 후 축소 화면으로 전환 등 처리 가능
        buttonManager.SetProcessButtonInteractable(false);

        yield return new WaitForSeconds(1f); // 다음 인물 등장 전 잠시 대기
    }

    public void OnProcessComplete()
    {
        // 확대 화면에서 완료 버튼을 눌렀을 때 호출됨
        isProcessing = false;
    }
}