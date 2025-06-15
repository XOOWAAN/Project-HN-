// *향후 로직키를 바탕으로 한 검사 조건 리스트, 규칙 위반 파악 구조를 마련해야 함*

// 하나의 매뉴얼 항목 데이터를 표현, 매뉴얼 각 항목 내용의 기본 단위로, 날짜별로 내용이 달라질 수 있음

using UnityEngine;

[System.Serializable]
public class ManualEntry
{
    public string entryId; // 항목 고유 ID (예: "rule_01" 또는 "map_01") - 날짜 간 갱신 판단 기준
    public string title; // UI에 표시할 항목 제목 (예: "행정 기본 지침", "지도")
    
    [TextArea]
    public string content; // 항목의 설명 내용 (텍스트)
    
    public Sprite image; // 항목 내용에 포함될 이미지 (UI에 함께 표시됨)

    public string logicKey; // 지적 시스템 등 다른 시스템과 연동할 키
}

// logicKey는 지적 시스템과 연동되는 식별자 역할
// 이 매뉴얼 항목이 어떤 검사 조건과 관련되는가? 를 담당

// 1. 오늘 날짜 기준으로 활성화된 규칙들을 가져온다
// List<ManualEntry> activeEntries = manualManager.GetTodayManualEntries();

// 2. 각 항목에서 logicKey만 뽑는다
// List<string> activeRules = activeEntries .Select(entry => entry.logicKey) .ToList();

// 3. 판단 시스템에서 그 규칙들을 참고하여 문서를 검사한다
// if (activeRules.Contains("business_required"))
// {
    // if (personData.businessType == "") // 사업이 없음
    // {
        // result = JudgmentResult.Fail;
        // reason = "사업 허가증이 필요한데 없음";
    // }
// }