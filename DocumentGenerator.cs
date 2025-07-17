// DocumentGenerator.cs
// ----------------------------
// 게임 내 문서를 생성하고 UI에 표시하는 제너레이터 스크립트
// PersonData 또는 무작위 데이터를 기반으로 문서 3종(IDCard, BusinessPermit, Pass)을 생성
// 생성된 문서는 확대/축소 UI로 표시되며, GameFlowManager에서 호출됨

// 주요 메서드:
// - GenerateDocumentsForPerson(PersonData): 인물과 일자별 생성 가능 문서 기반으로 문서 생성 (게임용)
// - GenerateRandomDocuments(): 무작위 문서 3종을 생성 (디버그용)
// - CreateDocumentUI(DocumentData, int): UI 프리팹을 생성하고 화면에 배치
// - CreateAndPlaceDocumentFromPerson(): PersonData로 DocumentData 생성 후 UI 생성
// - CreateAndPlaceDocumentRandom(): 랜덤 데이터로 문서 생성 후 UI 생성

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

    [Header("게임 진행 상태")]
    public int currentDay = 1; // GameFlowManager나 ManualManager에서 받아올 수 있음

    private List<DocumentData> currentDocuments = new List<DocumentData>();

    // 무작위 문서를 생성하는 테스트용 함수
    public void GenerateRandomDocuments()
    {
        currentDocuments.Clear();

        CreateAndPlaceDocumentRandom(DocumentType.IDCard, 0);
        CreateAndPlaceDocumentRandom(DocumentType.BusinessPermit, 1);
        CreateAndPlaceDocumentRandom(DocumentType.Pass, 2);
    }

    // 게임용: 인물과 일자별 생성 가능 문서 기반으로 문서 생성
    public GameObject GenerateDocumentsForPerson(PersonData data)
    {
        currentDocuments.Clear();
        GameObject firstDoc = null;

        // IDCard는 1일차부터
        if (currentDay >= 1)
            firstDoc = CreateAndPlaceDocumentFromPerson(data, DocumentType.IDCard, 0);

        // 2일차부터 BusinessPermit도 생성 가능
        if (currentDay >= 2)
            CreateAndPlaceDocumentFromPerson(data, DocumentType.BusinessPermit, 1);

        // 3일차부터 Pass도 생성 가능
        if (currentDay >= 3)
            CreateAndPlaceDocumentFromPerson(data, DocumentType.Pass, 2);

        return firstDoc;
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
    private GameObject CreateAndPlaceDocumentFromPerson(PersonData data, DocumentType type, int index)
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

        return CreateDocumentUI(doc, index);
    }

    // 공통 UI 생성 로직 (문서 생성 + 애니메이션 처리)
    private GameObject CreateDocumentUI(DocumentData data, int index)
    {
        currentDocuments.Add(data);

        GameObject doc = null;

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
            doc = Instantiate(splitDisplayPrefab, expandedDocumentParent);

            DocumentUIController ui = doc.GetComponent<DocumentUIController>();
            if (ui != null)
                ui.SetData(data);

            DocumentSplitDisplay splitDisplay = doc.GetComponent<DocumentSplitDisplay>();
            if (splitDisplay != null)
            {
                splitDisplay.orderPriority = index;
                splitDisplay.AnimateGiveDocument();
            }
        }

        return doc;
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