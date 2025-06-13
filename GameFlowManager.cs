// 일과가 시작되면 인물과 문서를 생성하고, 발급 또는 거절 처리 후 다음 인물로 진행함
// 페이퍼플리즈처럼 반복되는 인물 처리 루프를 담당
// documentGenerator.GenerateDocumentsForPerson(currentPerson);
// 을 통해 게임 내에서 실시간 인물 기반 문서 생성
// documentGenerator와 연동되어 인물 등장→인물 기반 문서 생성→문서 전달 및 판별 흐름 구현

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
        currentPerson = personGenerator.GeneratePerson();
        currentCharacter = personGenerator.SpawnCharacter(currentPerson, characterParent);

        // 2. 문서 생성 (인물에 맞게 생성하도록 수정됨)
        documentGenerator.GenerateDocumentsForPerson(currentPerson);

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