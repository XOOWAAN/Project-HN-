// 문서에 들어가는 데이터를 구조체처럼 보관. 한 사람의 정보를 하나의 단위로 다룸
// 데이터 구조로써 문서 생성, 인물 정보 매칭, 판단, 저장 모두 여기에 의존함
// InfoItem과는 다름. InfoLtem은 UI 클릭을 위해 만들어진 표현 포맷임

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