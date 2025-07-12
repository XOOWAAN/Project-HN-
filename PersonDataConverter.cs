// PersonDataConverter.cs
// ----------------------------
// PersonData의 비교를 위해 InfoItem으로 보내 UI화함.
// 인물 정보(이름, 생년월일, 성별 등)를 클릭 가능한 항목으로 바꾸어 비교 기능에 활용
// 문서와 마찬가지로 일치 여부 판단에 사용됨

// 주요 메서드:
// - ToInfoItems(PersonData, source): 인물 데이터를 항목별 InfoItem 리스트로 변환
// → 모든 핵심 필드 포함: 이름, 생년월일, 성별, 종족, 국적, 사진 등

using System.Collections.Generic;
using UnityEngine;

public static class PersonDataConverter
{
    public static List<InfoItem> ToInfoItems(PersonData data, string source)
    {
        List<InfoItem> items = new List<InfoItem>
        {
            new InfoItem("이름", data.fullName, source),
            new InfoItem("국적", data.nationality, source),
            new InfoItem("생년월일", data.birthDate, source),
            new InfoItem("성별", data.gender, source),
            new InfoItem("종족", data.race, source),
            new InfoItem("사진", data.photo != null ? data.photo.name : "없음", source)
        };

        // 추가 정보도 포함 (문서에서 사용하는 필드)
        if (!string.IsNullOrEmpty(data.address))
            items.Add(new InfoItem("주소", data.address, source));
        if (!string.IsNullOrEmpty(data.businessType))
            items.Add(new InfoItem("업종", data.businessType, source));
        if (!string.IsNullOrEmpty(data.departure))
            items.Add(new InfoItem("출발지", data.departure, source));
        if (!string.IsNullOrEmpty(data.destination))
            items.Add(new InfoItem("도착지", data.destination, source));

        return items;
    }
}