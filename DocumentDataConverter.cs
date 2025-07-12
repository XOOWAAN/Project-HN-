// DocumentDataConverter.cs
// ----------------------------
// DocumentData의 비교를 위해 InfoItem으로 보내 UI화함.
// DocumentUIController는 단순 표시용으로 이것과 성격이 다름
// 문서 정보 → 지적 시스템에서 사용할 수 있도록 클릭 가능한 항목으로 변환함
// UI 출력/판별 전용이며, 원본 문서 데이터를 손상시키지 않음

// 주요 메서드:
// - ToInfoItems(DocumentData, source): 문서 데이터를 항목별 InfoItem 리스트로 변환
//  → 문서 종류에 따라 성별/주소/업종/출발지/도착지도 포함

using System.Collections.Generic;
using UnityEngine;

public static class DocumentDataConverter
{
    // DocumentData → InfoItem 리스트로 UI화
    public static List<InfoItem> ToInfoItems(DocumentData data, string source)
    {
        List<InfoItem> items = new List<InfoItem>
        {
            new InfoItem("이름", data.fullName, source),
            new InfoItem("국적", data.nationality, source),
            new InfoItem("생년월일", data.dateOfBirth, source),
            new InfoItem("사진", data.photo != null ? data.photo.name : "없음", source)
        };

        // 추후 문서 종류별 항목을 추가해야 함
        switch (data.documentType)
        {
            case DocumentType.IDCard:
                items.Add(new InfoItem("성별", data.gender, source));
                items.Add(new InfoItem("주소", data.address, source));
                break;

            case DocumentType.BusinessPermit:
                items.Add(new InfoItem("성별", data.gender, source));
                items.Add(new InfoItem("업종", data.businessType, source));
                break;

            case DocumentType.Pass:
                items.Add(new InfoItem("출발지", data.departure, source));
                items.Add(new InfoItem("도착지", data.destination, source));
                break;
        }

        return items;
    }
}