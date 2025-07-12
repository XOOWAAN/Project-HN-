// DocumentData.cs
// ----------------------------
// 문서 한 장에 들어가는 정보를 저장하는 데이터 클래스
// PersonData로부터 파생되며, 문서 종류에 따라 필요한 항목만 포함됨
// 문서 생성, 표시, 비교판별, 저장 등에 사용됨

// 주요 필드:
// - fullName, nationality, dateOfBirth, photo: 공통 문서 정보
// - documentType: 문서 종류
// - gender, address, businessType, departure, destination: 문서별 세부 정보

using UnityEngine;

public enum DocumentType
{
    IDCard,         // 신분증
    BusinessPermit, // 사업허가증
    Pass            // 통행증
}

[System.Serializable]
public class DocumentData
{
    public string fullName;             // 이름
    public string nationality;          // 국적
    public string dateOfBirth;          // 생년월일
    public Sprite photo;                // 사진
    public DocumentType documentType;   // 문서 종류

    public string gender;               // 성별 (IDCard, BusinessPermit)
    public string address;              // 주소 (IDCard)
    public string businessType;         // 업종 (BusinessPermit)
    public string departure;            // 출발지 (Pass)
    public string destination;          // 목적지 (Pass)
}