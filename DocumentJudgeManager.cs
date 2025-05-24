using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DocumentJudgeManager : MonoBehaviour
{
    public GameObject resultPanel; // 비교 결과를 보여줄 UI 판넬

    public TextMeshProUGUI resultText; // 결과 텍스트 표시

    // 선택된 InfoItem들을 저장하는 리스트 (최대 2개까지 비교)
    private List<InfoItem> selectedItems = new List<InfoItem>();

    // InfoItemUI에서 항목을 클릭했을 때 호출됨
    // 최대 2개의 항목을 선택할 수 있고, 두 개가 선택되면 비교 후 결과를 UI로 표시함
    public void SelectItem(InfoItem item)
    {
        if (selectedItems.Count >= 2)
            selectedItems.Clear(); // 2개 이상 선택되면 초기화

        selectedItems.Add(item);

        if (HasSelectedTwoItems())
        {
            string result = EvaluateContradiction();
            ShowResultUI(result);
        }
    }

    // 선택된 항목이 2개인지 확인
    public bool HasSelectedTwoItems() => selectedItems.Count == 2;

    // 두 항목의 값을 비교하여 일치/불일치를 판단하는 함수
    public string EvaluateContradiction()
    {
        if (selectedItems.Count != 2)
            return "항목 두 개를 선택해야 합니다.";

        if (selectedItems[0].value != selectedItems[1].value)
            return $"불일치 발견: {selectedItems[0].label} ≠ {selectedItems[1].label}";

        return "일치: 두 항목은 동일합니다.";
    }

    // UI에 비교 결과를 보여주는 함수
    public void ShowResultUI(string resultMessage)
    {
        resultPanel.SetActive(true);
        resultText.text = resultMessage;
        StartCoroutine(HideResultAfterDelay(1.5f)); // 1.5초 뒤 자동으로 UI 비활성화
    }

    // 일정 시간 후 UI를 자동으로 숨기기 위한 코루틴
    private IEnumerator HideResultAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultPanel.SetActive(false);
    }
}