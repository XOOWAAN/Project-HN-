// 지적 버튼을 누른 후 두 항목 연결 시 판단 매니저에게 판단을 넘김

using UnityEngine;

public class ContradictionButton : MonoBehaviour
{
    public DocumentJudgeManager judgeManager; // 판단 매니저 연결

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
