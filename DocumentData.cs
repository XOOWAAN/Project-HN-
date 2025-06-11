// 문서에 들어가는 데이터를 구조체처럼 보관. 한 사람의 정보를 하나의 단위로 다룸
// 데이터 구조로써 문서 생성, 인물 정보 매칭, 판단, 저장 모두 여기에 의존함
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
    public string fullName;
    public string nationality;
    public string dateOfBirth;
    public Sprite photo;
    public DocumentType documentType; // 문서 종류 필드 추가
}