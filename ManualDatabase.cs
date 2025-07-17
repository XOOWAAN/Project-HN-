// ManualDatabase.cs
// ----------------------------
// 날짜별로 구성된 매뉴얼 항목(DailyManualData 리스트)을 관리하는 데이터베이스
// 특정 날짜까지의 모든 매뉴얼 항목을 병합하여 반환하는 기능을 제공
// 같은 entryId를 가진 항목은 나중 날짜의 항목으로 덮어써서 최신 정보만 유지
// ScriptableObject로 관리되며 에디터에서 항목 입력 및 편집 가능

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManualDatabase", menuName = "Database/Manual Database")]
public class ManualDatabase : ScriptableObject
{
    public List<DailyManualData> dailyManuals;  // 날짜별로 저장된 매뉴얼 데이터 목록

    public List<ManualEntry> GetMergedManualEntriesUpToDay(int day)
    {
        Dictionary<string, ManualEntry> entryDict = new();

        foreach (var daily in dailyManuals)
        {
            if (daily.day > day) continue;

            foreach (var entry in daily.manualEntries)
            {
                if (entryDict.ContainsKey(entry.entryId))
                {
                    entryDict[entry.entryId].content += "\n" + entry.content;
                    if (entry.image != null)
                        entryDict[entry.entryId].image = entry.image;
                    entryDict[entry.entryId].logicKey = entry.logicKey;
                }
                else
                {
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