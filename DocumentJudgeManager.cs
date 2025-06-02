// 문서 판단을 담당하는 핵심 매니저 스크립트
// InfoItem 두 개를 선택해 비교하고 결과를 UI로 출력하며,
// 불일치 발생 시 NotificationPanel을 통해 경고 알림 생성

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DocumentJudgeManager : MonoBehaviour
{
    [System.Serializable]
    public class InfoItem
    {
        public string label; // 항목 이름
        public string value; // 항목 값

        public InfoItem(string label, string value)
        {
            this.label = label;
            this.value = value;
        }
    }

    [Header("UI 요소")]
    public GameObject resultPanel; // 비교 결과를 보여줄 판넬
    public TextMeshProUGUI resultText; // 결과 메시지 텍스트

    [Header("알림 패널 연동")]
    public NotificationPanel notificationPanel; // 경고 알림을 위한 참조

    private List<InfoItem> selectedItems = new List<InfoItem>(); // 선택된 비교 항목 리스트

    private int errorCount = 0; // 누적 경고 수

    // InfoItem 클릭 시 호출됨
    public void SelectItem(InfoItem item)
    {
        if (selectedItems.Count >= 2)
            selectedItems.Clear(); // 두 개 초과 선택되면 초기화

        selectedItems.Add(item);

        if (HasSelectedTwoItems())
        {
            string result = EvaluateContradiction();
            ShowResultUI(result);

            if (result.StartsWith("불일치"))
            {
                string reason = $"{selectedItems[0].label}과 {selectedItems[1].label}의 정보 불일치";
                HandleJudgement(false, reason);
            }
        }
    }

    // 두 항목이 선택되었는지 확인
    public bool HasSelectedTwoItems() => selectedItems.Count == 2;

    // 두 항목 값 비교 후 판단 결과 반환
    public string EvaluateContradiction()
    {
        if (selectedItems.Count != 2)
            return "항목 두 개를 선택해야 합니다.";

        if (selectedItems[0].value != selectedItems[1].value)
            return $"불일치 발견: {selectedItems[0].label} ≠ {selectedItems[1].label}";

        return "일치: 두 항목은 동일합니다.";
    }

    // 결과 메시지를 UI로 출력하고 자동으로 숨김 처리
    public void ShowResultUI(string resultMessage)
    {
        resultPanel.SetActive(true);
        resultText.text = resultMessage;
        StartCoroutine(HideResultAfterDelay(1.5f));
    }

    // 일정 시간 후 결과 판넬 숨기기
    private IEnumerator HideResultAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultPanel.SetActive(false);
    }

    // 판단 결과를 처리하는 함수 (불일치 시 경고 알림 추가)
    public void HandleJudgement(bool isCorrect, string errorDetail)
    {
        if (!isCorrect)
        {
            errorCount++;
            string fullMessage = errorDetail;

            // 경고 3회째부터 벌금 메시지 포함
            if (errorCount >= 3)
                fullMessage += $" (경고 {errorCount}회 — 벌금 부과됨)";

            notificationPanel.AddWarningNotification(fullMessage);
        }
    }
}