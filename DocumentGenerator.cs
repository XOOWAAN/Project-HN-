// 문서를 무작위 또는 인물 기반으로 생성하고 Canvas에 분할된 UI로 문서를 띄움
// PersonData를 기반으로 문서를 생성함

// GenerateRandomDocuments() = 무작위 문서 생성 테스트/디버그 용도
// GenerateDocumentsForPerson = 실제 게임용으로. PersonData 입력 기반 문서 생성
// CreateAndPlaceDocumentRandom, CreateAndPlaceDocumentFromPerson = 문서 생성 로직

using UnityEngine;
using System.Collections.Generic;

public class DocumentGenerator : MonoBehaviour
{
    [Header("문서 UI 프리팹")]
    public GameObject splitDisplayPrefab;         // 확대 화면용 문서 프리팹
    public GameObject compactDocumentPrefab;      // 축소 화면용 (정보 없이 작게 표시되는 프리팹)

    [Header("문서 생성 위치")]
    public Transform expandedDocumentParent;      // 확대 화면 문서의 부모
    public Transform compactDocumentParent;       // 축소 화면 문서의 부모

    [Header("축소 화면 문서 위치 (총 4개 영역 중 3개 사용)")]
    public RectTransform[] compactPositions;      // 문서 종류 순서대로 위치 지정

    [Header("데이터 소스 (랜덤 생성용)")]
    public List<string> randomNames;
    public List<string> nationalities;
    public List<Sprite> photos;

    private List<DocumentData> currentDocuments = new List<DocumentData>();

    // 무작위 문서를 생성하는 테스트용 함수
    public void GenerateRandomDocuments()
    {
        currentDocuments.Clear();

        CreateAndPlaceDocumentRandom(DocumentType.IDCard, 0);
        CreateAndPlaceDocumentRandom(DocumentType.BusinessPermit, 1);
        CreateAndPlaceDocumentRandom(DocumentType.Pass, 2);
    }

    // 게임용: 인물(PersonData)을 기반으로 문서를 생성하는 함수
    public void GenerateDocumentsForPerson(PersonData data)
    {
        currentDocuments.Clear();

        CreateAndPlaceDocumentFromPerson(data, DocumentType.IDCard, 0);
        CreateAndPlaceDocumentFromPerson(data, DocumentType.BusinessPermit, 1);
        CreateAndPlaceDocumentFromPerson(data, DocumentType.Pass, 2);
    }

    // 랜덤 문서를 생성하고 배치하는 내부 함수
    private void CreateAndPlaceDocumentRandom(DocumentType type, int index)
    {
        DocumentData data = new DocumentData {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            dateOfBirth = RandomDate(),
            photo = photos[Random.Range(0, photos.Count)],
            documentType = type
        };

        switch (type)
        {
            case DocumentType.IDCard:
                data.gender = Random.Range(0, 2) == 0 ? "남" : "여";
                data.address = "서울특별시 중구";
                break;

            case DocumentType.BusinessPermit:
                data.gender = Random.Range(0, 2) == 0 ? "남" : "여";
                data.businessType = "식당업";
                break;

            case DocumentType.Pass:
                data.departure = "평양";
                data.destination = "서울";
                break;
        }

        CreateDocumentUI(data, index);
    }

    // 인물 정보를 기반으로 문서를 생성하고 배치하는 내부 함수
    private void CreateAndPlaceDocumentFromPerson(PersonData data, DocumentType type, int index)
    {
        DocumentData doc = new DocumentData {
            fullName = data.fullName,
            nationality = data.nationality,
            dateOfBirth = data.birthDate,
            photo = data.photo,
            documentType = type
        };

        switch (type)
        {
            case DocumentType.IDCard:
                doc.gender = data.gender;
                doc.address = data.address;
                break;

            case DocumentType.BusinessPermit:
                doc.gender = data.gender;
                doc.businessType = data.businessType;
                break;

            case DocumentType.Pass:
                doc.departure = data.departure;
                doc.destination = data.destination;
                break;
        }

        CreateDocumentUI(doc, index);
    }

    // 공통 UI 생성 로직 (문서 생성 + 애니메이션 처리)
    private void CreateDocumentUI(DocumentData data, int index)
    {
        currentDocuments.Add(data);

        // --- 축소 화면용 문서 생성 ---
        if (compactDocumentPrefab != null && compactDocumentParent != null)
        {
            GameObject compactDoc = Instantiate(compactDocumentPrefab, compactDocumentParent);
            RectTransform rect = compactDoc.GetComponent<RectTransform>();

            if (index < compactPositions.Length)
                rect.anchoredPosition = compactPositions[index].anchoredPosition;

            compactDoc.transform.localScale = Vector3.one * 0.4f;

            Animator animator = compactDoc.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("Appear");
        }

        // --- 확대 화면용 문서 생성 ---
        if (splitDisplayPrefab != null && expandedDocumentParent != null)
        {
            GameObject doc = Instantiate(splitDisplayPrefab, expandedDocumentParent);

            DocumentSplitDisplay splitDisplay = doc.GetComponent<DocumentSplitDisplay>();
            splitDisplay.orderPriority = index;

            splitDisplay.InitializeDocument(data);
            splitDisplay.AnimateGiveDocument();
        }
    }

    // 날짜를 무작위로 생성하는 도우미 함수
    private string RandomDate()
    {
        int year = Random.Range(1960, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        return $"{year}.{month:00}.{day:00}";
    }
} 