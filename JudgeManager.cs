// JudgeManager.cs
// ----------------------------
// InfoItem 또는 매뉴얼 규칙 중 두 개를 선택해 비교 후 결과를 출력하는 판단 스크립트
// InfoItem 간 값 일치 비교 로직과 매뉴얼 규칙 일치 검사 로직으로 구성됨
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
using System.Linq; // LINQ 사용

public class JudgeManager : MonoBehaviour
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
    public GameObject resultPanel; // 비교 결과 패널
    public TextMeshProUGUI resultText; // 비교 결과 텍스트

    [Header("알림 패널 연동")]
    public NotificationPanel notificationPanel;

    [Header("지적 버튼 연결")]
    public ContradictionButton contradictionButton;

    [Header("매뉴얼 연동")]
    public ManualManager manualManager; // 오늘 날짜 규칙을 가져오기 위해 연결

    private List<InfoItem> selectedItems = new List<InfoItem>();
    private int errorCount = 0;

    public static JudgeManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // InfoItem 비교 로직(인물 or 문서의 UI 두 개 클릭 시 값 일치 여부 판단)
    public void StartContradictionMode()
    {
        selectedItems.Clear();
    }

    public void SelectItem(InfoItem item)
    {
        if (selectedItems.Count >= 2)
            selectedItems.Clear();

        selectedItems.Add(item);

        if (HasSelectedTwoItems() && contradictionButton != null)
        {
            contradictionButton.OnSecondItemClicked();
        }
    }

    public bool HasSelectedTwoItems() => selectedItems.Count == 2;

    public string EvaluateContradiction()
    {
        if (selectedItems.Count != 2)
            return "항목 두 개를 선택해야 합니다.";

        if (selectedItems[0].value != selectedItems[1].value)
            return $"불일치 발견: {selectedItems[0].label} ≠ {selectedItems[1].label}";

        return "일치: 두 항목은 동일합니다.";
    }

    public void ShowResultUI(string resultMessage)
    {
        resultPanel.SetActive(true);
        resultText.text = resultMessage;
        StartCoroutine(HideResultAfterDelay(1.5f));
    }

    private IEnumerator HideResultAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultPanel.SetActive(false);
    }

    public void HandleJudgement(bool isCorrect, string errorDetail)
    {
        if (!isCorrect)
        {
            errorCount++;
            string fullMessage = errorDetail;

            if (errorCount >= 3)
                fullMessage += $" (경고 {errorCount}회 — 벌금 부과됨)";

            notificationPanel.AddWarningNotification(fullMessage);
        }
    }
   
    //  매뉴얼 기반 검사 로직(매뉴얼 규칙과 인물 or 문서가 일치하는지 검사)
    public void CheckForbiddenDocuments(List<DocumentData> submittedDocuments)
    {
        if (manualManager == null)
        {
            Debug.LogWarning("ManualManager가 연결되지 않았습니다.");
            return;
        }

        // 오늘 날짜 기준 활성화된 매뉴얼 항목들
        List<ManualEntry> entries = manualManager.GetTodayManualEntries();

        // 오늘 금지된 문서가 있는지 찾기
        bool isBusinessPermitForbidden = entries.Any(e => e.logicKey == "business_forbidden");

        if (isBusinessPermitForbidden)
        {
            // 제출된 문서 중 BusinessPermit이 있는지 검사
            bool hasBusinessPermit = submittedDocuments.Any(d => d.documentType == DocumentType.BusinessPermit);

            if (hasBusinessPermit)
            {
                // 규칙 위반! 경고 처리
                HandleJudgement(false, "오늘은 사업허가증 제출이 금지되었습니다!");
            }
        }
    }
}