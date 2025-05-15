// 문서를 무작위로 생성하고 Canvas에 문서 UI 프리팹을 생성

using UnityEngine;
using System.Collections.Generic;

public class DocumentGenerator : MonoBehaviour
{
    // 문서 UI 프리팹 (DocumentPanel 오브젝트)
    public GameObject documentPrefab;

    // 생성된 문서들을 담을 부모 오브젝트 (Canvas의 빈 Panel 등)
    public Transform documentParent;

    [Header("데이터 소스")]
    public List<string> randomNames;      // 무작위 이름 목록
    public List<string> nationalities;    // 무작위 국적 목록
    public List<Sprite> photos;           // 무작위로 쓸 수 있는 증명사진 목록

    // 외부에서 호출하면 문서를 무작위로 생성함
    public void GenerateRandomDocument() {
        // 프리팹 인스턴스 생성 (Canvas 자식으로 붙임)
        GameObject doc = Instantiate(documentPrefab, documentParent);

        // 문서 UI 조작용 컴포넌트를 가져옴
        DocumentUIController controller = doc.GetComponent<DocumentUIController>();

        // 무작위 데이터 구성
        DocumentData data = new DocumentData {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            dateOfBirth = RandomDate(), // 랜덤 생년월일 생성 함수 사용
            photo = photos[Random.Range(0, photos.Count)]
        };

        // UI에 데이터 적용
        controller.SetData(data);
        controller.BringToFront(); // 문서를 가장 위로 가져옴
    }

    // 무작위 날짜를 yyyy.MM.dd 형식으로 반환하는 함수
    private string RandomDate() {
        int year = Random.Range(1960, 2005);    // 출생년도 범위
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);          // 간단하게 28일까지로 제한
        return $"{year}.{month:00}.{day:00}";
    }
}