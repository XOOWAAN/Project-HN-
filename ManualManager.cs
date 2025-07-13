// ManualManager.cs
// ----------------------------
// ManualDatabase와 연결되어, 현재 날짜에 따라 유효한 매뉴얼 항목을 반환하는 관리 클래스
// 게임 내에서 날짜를 기준으로 어떤 규칙(ManualEntry)이 활성 상태인지 판단할 수 있도록 도와줌
// 판단 시스템, UI 출력, 규칙 비교 등에서 이 매니저를 통해 오늘의 규칙 목록을 획득

using System.Collections.Generic;
using UnityEngine;

public class ManualManager : MonoBehaviour
{
    [Header("매뉴얼 데이터베이스")]
    public ManualDatabase manualDatabase; // 연결된 매뉴얼 데이터베이스 (ScriptableObject)

    [Header("현재 일자")]
    public int currentDay = 1; // 현재 날짜 (게임 진행 상황에 따라 갱신됨)

    // 현재 날짜까지 유효한 매뉴얼 항목들을 병합해서 가져옴
    public List<ManualEntry> GetTodayManualEntries()
    {
        if (manualDatabase == null)
        {
            Debug.LogError("ManualDatabase가 연결되지 않았습니다.");
            return new List<ManualEntry>();
        }

        return manualDatabase.GetMergedManualEntriesUpToDay(currentDay);
    }

    // 날짜 설정용 메서드 (게임 시스템과 연동 예정)
    public void SetDay(int day)
    {
        currentDay = day;
    }
}