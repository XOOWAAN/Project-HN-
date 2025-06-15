// 지적 버튼을 누른 후 두 항목 연결 시 판단 매니저에게 판단을 넘김

using UnityEngine;

public class ContradictionButton : MonoBehaviour
{
    public JudgeManager judgeManager; // 판단 매니저 연결

    private bool isJudging = false;

    // 지적 버튼을 눌렀을 때 판단 모드 시작
    public void OnClick()
    {
        isJudging = true;
        judgeManager.StartContradictionMode(); // 선택 초기화
    }

    // 항목 두 개가 클릭된 이후에 호출되는 메서드
    public void OnSecondItemClicked()
    {
        if (!isJudging) return;

        if (judgeManager.HasSelectedTwoItems())
        {
            string result = judgeManager.EvaluateContradiction();
            Debug.Log(result); // 결과 출력

            isJudging = false;
        }
    }
}

// [문서 판단 절차(25.6.15.)]
// 지적 버튼, 판단 매니저, 인포아이템UI 스크립트 사용

// 1. 플레이어가 "지적" 버튼 클릭
// → ContradictionButton.OnClick()
// → 판단 모드 시작 (DocumentJudgeManager.StartContradictionMode() 호출)

// 2. 두 개의 UI 항목(InfoItem)을 클릭
// → 각 항목의 UI에서 InfoItemUI.OnClick() 실행
// → 판단 매니저에 항목을 전달 (judgeManager.SelectItem(item))

// 3. 두 번째 항목 클릭 시
// → DocumentJudgeManager.SelectItem() 내부에서
// → ContradictionButton.OnSecondItemClicked() 호출됨
// → 내부에서 EvaluateContradiction() 실행
// → 판단 결과를 콘솔에 출력

// 4. 같은 시점에 InfoItemUI에서 직접 UI 결과 출력 요청
// → judgeManager.ShowResultUI(result) 실행
// → 결과 패널 활성화 + 메시지 표시
// → HideResultAfterDelay() 코루틴으로 1.5초 후 자동 숨김