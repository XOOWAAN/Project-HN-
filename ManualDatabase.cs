// 날짜별로 구성된 여러 DailyManualData를 저장함
// 특정 날짜까지의 모든 매뉴얼 항목을 병합해서 반환하는 기능을 제공함
// 같은 entryId를 가진 항목은 새로운 내용으로 덮어씌움
// EX) 2일차의 내용은 1일차의 내용을 포함하도록 함

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManualDatabase", menuName = "Database/Manual Database")]
public class ManualDatabase : ScriptableObject
{
    public List<DailyManualData> dailyManuals;  // 날짜별로 저장된 매뉴얼 데이터 목록

    // 특정 날짜까지의 매뉴얼 항목을 병합하여 반환
    // entryId를 기준으로 이전 내용은 덮어쓰거나 누적
    public List<ManualEntry> GetMergedManualEntriesUpToDay(int day)
    {
        Dictionary<string, ManualEntry> entryDict = new();

        foreach (var daily in dailyManuals)
        {
            if (daily.day > day) continue;

            foreach (var entry in daily.manualEntries)
            {
                // 기존에 이미 있는 항목이라면 최신 내용으로 덮어쓰기
                if (entryDict.ContainsKey(entry.entryId))
                {
                    entryDict[entry.entryId].content = entry.content;
                    entryDict[entry.entryId].image = entry.image;
                }
                else
                {
                    // 새 항목일 경우 복사해서 추가
                    entryDict[entry.entryId] = new ManualEntry
                    {
                        entryId = entry.entryId,
                        title = entry.title,
                        content = entry.content,
                        image = entry.image,
                        logicKey = entry.logicKey
                    };
                }
            }
        }

        return new List<ManualEntry>(entryDict.Values);
    }
}