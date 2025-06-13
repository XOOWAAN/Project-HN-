// 문서에 들어가는 데이터를 구조체처럼 보관. 한 사람의 정보를 하나의 단위로 다룸
// 데이터를 저장하여 문서 생성, 인물 정보 매칭, 판단, 저장에 모두 활용
// DocumentFactory에서 데이터를 받아 저장하고 DocumentGenerator에게 전달
// InfoItem과는 다름. InfoLtem은 UI 클릭을 위해 만들어진 표현 포맷임

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
    public string dateOfBirth;          // 생년월일 (소문자로 수정)
    public Sprite photo;                // 사진
    public DocumentType documentType;   // 문서 종류

    // 문서별 추가 필드
    public string gender;               // 성별 (IDCard, BusinessPermit)
    public string address;              // 주소 (IDCard)
    public string businessType;         // 업종 (BusinessPermit)
    public string departure;            // 출발지 (Pass)
    public string destination;          // 목적지 (Pass)
}