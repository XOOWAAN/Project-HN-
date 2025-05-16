// 문서를 무작위로 생성하고 Canvas에 분할된 확대/축소 UI로 문서를 띄움

using UnityEngine;
using System.Collections.Generic;

public class DocumentGenerator : MonoBehaviour
{
    [Header("문서 UI 프리팹")]
    public GameObject splitDisplayPrefab;     // 좌우 분할된 문서를 관리하는 프리팹

    [Header("문서 생성 위치")]
    public Transform documentParent;          // DocumentSplitDisplay가 붙을 부모 (보통 Canvas)

    [Header("데이터 소스")]
    public List<string> randomNames;          // 무작위 이름 목록
    public List<string> nationalities;        // 무작위 국적 목록
    public List<Sprite> photos;               // 무작위로 쓸 수 있는 증명사진 목록

    // 외부 호출 시 문서를 무작위 생성하는 함수
    public void GenerateRandomDocument()
    {
        // DocumentSplitDisplay 프리팹 인스턴스 생성 (Canvas 자식으로 붙임)
        GameObject docSplitObj = Instantiate(splitDisplayPrefab, documentParent);

        // 컴포넌트 참조
        DocumentSplitDisplay splitDisplay = docSplitObj.GetComponent<DocumentSplitDisplay>();

        // 무작위 데이터 구성
        DocumentData data = new DocumentData
        {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            dateOfBirth = RandomDate(), // 랜덤 생년월일 생성 함수 사용
            photo = photos[Random.Range(0, photos.Count)]
        };

        // UI에 데이터 적용
        splitDisplay.InitializeDocument(data);
    }

    // 무작위 날짜를 yyyy.MM.dd 형식으로 반환하는 함수
    private string RandomDate()
    {
        int year = Random.Range(1960, 2005);    // 출생년도 범위
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);          // 간단하게 28일까지로 제한
        return $"{year}.{month:00}.{day:00}";
    }
}