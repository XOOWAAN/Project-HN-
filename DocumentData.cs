// 문서에 들어가는 데이터를 구조체처럼 보관. 한 사람의 정보를 하나의 단위로 다룸. 보기 좋고 유지보수 쉬움

using UnityEngine;

// 직렬화하면 인스펙터에서도 볼 수 있음
[System.Serializable]
public class DocumentData
{
    public string fullName;         // 이름 (예: "Bae Ki Gas")
    public string nationality;      // 국적 (예: "Korea")
    public string dateOfBirth;      // 생년월일 (예: "1980.01.15")
    public Sprite photo;            // 증명사진으로 사용할 이미지
}