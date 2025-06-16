// 매뉴얼 관련 스크립트는 원하는 바가 다 구현되어 있음. 항목과 텍스트 채우는 거는 에디터에서 할 일임
// 매뉴얼 UI 클릭 후 비교는 판단 매니저와 InfoItemUI 스크립트 관할

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


// [Manual System 구조]

// ManualEntry: 개별 항목 내의 설명 (ID, 제목, 내용, 이미지, 연동 키 포함)
// DailyManualData: 특정 날짜의 항목 리스트 (예시 : N일에는 어떤 규칙들 삭제 추가)
// ManualDatabase: 모든 날짜별 항목의 데이터 전체 저장. 항목별 덮어쓰기나 병합도 담당
    // 같은 entryId는 덮어쓰기 → 최신 내용 유지
    // 누적하고 싶으면 별도 병합 로직 필요
// ManualManager: 현재 날짜 기준으로 유효 항목을 정리해 UI나 판단 매니저가 사용 가능하게 함

// [향후 구현 필요]
// - UI에 항목 목록 → 클릭 → 상세 페이지 → 뒤로가기 흐름
// - 누적 텍스트 병합 옵션
// - logicKey 기반 필터링 또는 연동 기능 추가