// *향후 로직키를 바탕으로 한 검사 조건 리스트, 규칙 위반 파악 구조를 마련해야 함*

// ManualEntry.cs
// ----------------------------
// 게임 내 매뉴얼의 개별 항목 데이터를 나타내는 클래스
// 하나의 규칙이나 안내 항목(예: 통행증 요구, 출입 제한 등)을 구성하며,
// title, content, image, logicKey 등 UI 표시와 판단 시스템 연동에 필요한 정보를 포함함
// 날짜가 달라져도 entryId가 같으면 해당 항목으로 덮어쓰기 됨

using UnityEngine;

[System.Serializable]
public class ManualEntry
{
    public string entryId; // 항목 고유 ID (예: "rule_01" 또는 "map_01")
    public string title;   // UI에 표시할 항목 제목
    [TextArea]
    public string content; // 항목 설명
    public Sprite image;   // 항목 이미지
    public string logicKey; // 지적 시스템과 연동할 키 (ex: "business_forbidden")
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