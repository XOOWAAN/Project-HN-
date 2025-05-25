// 특정 날짜(day)에 해당하는 모든 ManualEntry 항목을 저장함
// EX) '행정 기본 지침'의 일자별 항목을 전부 저장함
// ManualDatabase에 여러 개가 저장되어 날짜별 매뉴얼 갱신을 가능하게 함

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DailyManualData
{
    public int day;                         // 해당 데이터가 적용될 날짜 (예: 1, 2, 3...)
    public List<ManualEntry> manualEntries; // 그 날짜에 적용되는 매뉴얼 항목들 리스트
}