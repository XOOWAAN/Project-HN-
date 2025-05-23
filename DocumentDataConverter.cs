using System.Collections.Generic;
using UnityEngine;

public static class DocumentDataConverter
{
    // DocumentData → InfoItem 리스트
    public static List<InfoItem> ToInfoItems(DocumentData data, string source)
    {
        List<InfoItem> items = new List<InfoItem>();

        items.Add(new InfoItem("이름", data.fullName, source));
        items.Add(new InfoItem("국적", data.nationality, source));
        items.Add(new InfoItem("생년월일", data.dateOfBirth, source));
        items.Add(new InfoItem("사진", data.photo != null ? data.photo.name : "없음", source));

        return items;
    }

    // InfoItem 리스트 → DocumentData (역변환)
    public static DocumentData ToDocumentData(List<InfoItem> items)
    {
        DocumentData data = new DocumentData();

        foreach (var item in items)
        {
            switch (item.label)
            {
                case "이름":
                    data.fullName = item.value;
                    break;
                case "국적":
                    data.nationality = item.value;
                    break;
                case "생년월일":
                    data.dateOfBirth = item.value;
                    break;
                case "사진":
                    // 사진은 복원이 어려워서 실제 게임에서 사용한다면, 이 부분은 따로 처리
                    break;
            }
        }

        return data;
    }
}