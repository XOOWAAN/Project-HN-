// JudgeManager.cs
// ----------------------------
// InfoItem 두 개를 선택해 비교 후 결과를 출력하는 판단 스크립트
// 불일치가 발생할 경우 NotificationPanel을 통해 경고를 누적 관리

// 주요 메서드:
// - SelectItem(InfoItem): 클릭한 항목을 비교 대기 리스트에 추가
// - EvaluateContradiction(): 두 항목을 비교하여 일치/불일치 판단 메시지 반환
// - ShowResultUI(string): 결과 메시지를 결과 패널에 출력 후 자동 숨김 처리
// - HandleJudgement(bool, string): 불일치 발생 시 경고 알림 출력 및 경고 횟수 누적

// ⚠️현재는 '값의 완전 일치'만 판단하며, 매뉴얼 등 모호한 비교는 별도 확장 필요⚠️


using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class JudgeManager : MonoBehaviour // 이름 변경됨 (기존: DocumentJudgeManager)
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

    [Header("지적 버튼 연결")]
    public ContradictionButton contradictionButton; // 버튼에서 콜백 호출

    private List<InfoItem> selectedItems = new List<InfoItem>(); // 선택된 비교 항목 리스트

    private int errorCount = 0; // 누적 경고 수

    public static JudgeManager Instance; // 싱글톤 참조 추가

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 판단 모드 진입 시 초기화
    public void StartContradictionMode()
    {
        selectedItems.Clear();
    }

    // InfoItem 클릭 시 호출됨 (문서, 인물, 매뉴얼 공통)
    public void SelectItem(InfoItem item)
    {
        if (selectedItems.Count >= 2)
            selectedItems.Clear(); // 두 개 초과 선택되면 초기화

        selectedItems.Add(item);

        // 두 개 선택된 경우 ContradictionButton에 알림
        if (HasSelectedTwoItems() && contradictionButton != null)
        {
            contradictionButton.OnSecondItemClicked();
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