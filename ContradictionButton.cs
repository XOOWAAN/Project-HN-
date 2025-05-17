// 지적 버튼: 두 UI 정보 요소 선택 후 불일치 판단

using UnityEngine;
using UnityEngine.UI;

public class ContradictionButton : MonoBehaviour
{
    // 검사 결과를 출력할 UI 텍스트
    public Text resultText;

    // 현재 선택된 두 개의 정보 요소
    private IInfoSelectable firstSelection;
    private IInfoSelectable secondSelection;

    // 현재 지적 모드가 활성화되어 있는지를 나타내는 플래그
    private bool isActive = false;

    // 지적 버튼 클릭 시 호출되는 함수
    public void OnContradictButtonClick()
    {
        isActive = true;  // 지적 모드 활성화
        resultText.text = "불일치 검사 모드: 두 항목을 선택하세요.";
    }

    // 문서나 인물에서 정보 항목을 선택했을 때 호출되는 함수
    public void OnInfoSelected(IInfoSelectable selected)
    {
        if (!isActive) return; // 지적 모드가 아니면 무시

        // 첫 번째 선택 저장
        if (firstSelection == null)
        {
            firstSelection = selected;
        }
        // 두 번째 선택 저장 후 바로 비교
        else if (secondSelection == null)
        {
            secondSelection = selected;
            CheckContradiction(); // 두 선택 항목 비교
        }
    }

    // 두 선택된 정보를 비교하여 불일치 메시지 출력
    void CheckContradiction()
    {
        // 인터페이스에서 반환된 문자열 비교
        if (firstSelection.GetInfo() != secondSelection.GetInfo())
        {
            resultText.text = "불일치 발견: 정보가 다릅니다.";
        }
        else
        {
            resultText.text = "불일치 없음: 정보가 동일합니다.";
        }

        ResetSelections(); // 다음 검사 위해 초기화
    }

    // 선택 상태 초기화
    void ResetSelections()
    {
        firstSelection = null;
        secondSelection = null;
        isActive = false; // 지적 모드 비활성화
    }
}