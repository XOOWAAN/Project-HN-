// PersonData.cs
// ----------------------------
// 한 명의 인물 정보를 저장하는 데이터 클래스
// PersonGenerator에서 생성되어 문서 생성 및 인물 UI에 사용됨
// 문서 데이터와 연동되며, 성별/종족/사진/국적 등의 기본 정보 포함

// 주요 필드:
// - fullName, gender, birthDate, nationality: 기본 인적 정보
// - race: 종족 정보
// - photo: 인물 사진 (규칙에 따라 결정됨)
// - address, businessType, departure, destination: 문서 생성 시 활용되는 추가 필드

using UnityEngine;

[System.Serializable]
public class PersonData
{
    public string fullName;         // 이름
    public string birthDate;        // 생년월일
    public string nationality;      // 국적
    public string gender;           // 성별
    public string race;             // 종족 (오크, 인간, 엘프)
    public Sprite photo;            // 인물 사진

    public string address;          // 주소 (ID 카드용)
    public string businessType;     // 업종 (사업 허가증용)
    public string departure;        // 출발지 (통행증용)
    public string destination;      // 도착지 (통행증용)
}