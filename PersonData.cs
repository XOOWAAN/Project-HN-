// 인물 정보 구조체
using UnityEngine;

[System.Serializable]
public class PersonData
{
    public string fullName;         // 이름
    public string birthDate;        // 생년월일
    public string nationality;      // 국적
    public Sprite faceSprite;       // 얼굴 이미지
    public Sprite bodySprite;       // 몸통 이미지

    public string gender;           // 성별
    public string address;          // 주소
    public string businessType;     // 업종
    public string departure;        // 출발지
    public string destination;      // 목적지
}