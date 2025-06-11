// 문서를 무작위로 생성하고 Canvas에 분할된 UI로 문서를 띄움

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
    // Unity 에디터에서 축소 화면 내 문서를 배치할 3개의 RectTransform을 등록(문서 순으로)

    [Header("데이터 소스")]
    public List<string> randomNames;
    public List<string> nationalities;
    public List<Sprite> photos;

    private List<DocumentData> currentDocuments = new List<DocumentData>();

    public void GenerateAllDocuments()
    {
        currentDocuments.Clear();

        // 문서 종류 순서대로 3개 생성
        CreateAndPlaceDocument(DocumentType.IDCard, 0);
        CreateAndPlaceDocument(DocumentType.BusinessPermit, 1);
        CreateAndPlaceDocument(DocumentType.Pass, 2);
    }

    private void CreateAndPlaceDocument(DocumentType type, int index)
    {
        DocumentData data = new DocumentData {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            dateOfBirth = RandomDate(),
            photo = photos[Random.Range(0, photos.Count)],
            documentType = type
        };
        currentDocuments.Add(data);

        // --- 축소 화면용 문서 생성 ---
        if (compactDocumentPrefab != null && compactDocumentParent != null)
        {
            GameObject compactDoc = Instantiate(compactDocumentPrefab, compactDocumentParent);
            RectTransform rect = compactDoc.GetComponent<RectTransform>();

            // 축소 화면 문서는 지정된 위치 배열(compactPositions)에서 각 인덱스 위치에 배치됨
            // 문서 종류만큼 유니티 에디터에 등록하기
            if (index < compactPositions.Length)
                rect.anchoredPosition = compactPositions[index].anchoredPosition;

            compactDoc.transform.localScale = Vector3.one * 0.4f; // 축소 크기로 조정

            Animator animator = compactDoc.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("Appear");
        }

        // --- 확대 화면용 문서 생성 ---
        if (splitDisplayPrefab != null && expandedDocumentParent != null)
        {
            GameObject doc = Instantiate(splitDisplayPrefab, expandedDocumentParent);

            // DocumentSplitDisplay의 orderPriority 값으로 우선순위를 관리

            DocumentSplitDisplay splitDisplay = doc.GetComponent<DocumentSplitDisplay>();

            // '문서 생성 시' orderPriority 값 할당(유니티 컴포넌트에서 하는 듯)
            // 이 값이 낮을수록 UI 계층에서 위로 올라가도록 DocumentSplitDisplay 스크립트가 관리함
            // 필요 시 이 시스템에 맞춰 문서 클릭 우선순위, 드래그 처리, 투명도 조절 같은 후속 기능도 연결
            splitDisplay.orderPriority = index;

            splitDisplay.InitializeDocument(data);
            splitDisplay.AnimateGiveDocument();
        }
    }

    private string RandomDate()
    {
        int year = Random.Range(1960, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        return $"{year}.{month:00}.{day:00}";
    }
}