// 현재 날짜를 기준으로 유효한 매뉴얼 항목 리스트를 가져오는 매니저 스크립트
// ManualDatabase에서 현재 날짜까지의 항목들을 병합하여 반환함

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
