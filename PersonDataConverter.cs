using System.Collections.Generic;
using UnityEngine;

public static class PersonDataConverter
{
    // PersonData → List<InfoItem>
    public static List<DocumentJudgeManager.InfoItem> ToInfoItems(DocumentJudgeManager.PersonData personData, string prefix = "Person")
    {
        List<DocumentJudgeManager.InfoItem> items = new List<DocumentJudgeManager.InfoItem>
        {
            new DocumentJudgeManager.InfoItem($"{prefix}_이름", personData.fullName),
            new DocumentJudgeManager.InfoItem($"{prefix}_국적", personData.nationality),
            new DocumentJudgeManager.InfoItem($"{prefix}_생년월일", personData.dateOfBirth),
            new DocumentJudgeManager.InfoItem($"{prefix}_사진", personData.photo != null ? personData.photo.name : "")
        };

        return items;
    }

    // List<InfoItem> → PersonData
    public static DocumentJudgeManager.PersonData ToPersonData(List<DocumentJudgeManager.InfoItem> items)
    {
        DocumentJudgeManager.PersonData personData = new DocumentJudgeManager.PersonData();

        foreach (var item in items)
        {
            if (item.label.Contains("이름"))
                personData.fullName = item.value;
            else if (item.label.Contains("국적"))
                personData.nationality = item.value;
            else if (item.label.Contains("생년월일"))
                personData.dateOfBirth = item.value;
            else if (item.label.Contains("사진"))
                personData.photo = Resources.Load<Sprite>(item.value); // 단순 예시: 이름으로 불러오기
        }

        return personData;
    }
}