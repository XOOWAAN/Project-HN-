// 하나의 매뉴얼 항목 데이터를 표현하는 클래스로, 매뉴얼 각 항목 내용의 기본 단위
// 이 항목은 날짜별로 내용이 달라질 수 있으며, 지적 시스템과도 연동될 수 있음

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
