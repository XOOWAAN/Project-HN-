// 문서를 무작위로 생성하고 Canvas에 분할된 UI로 문서를 띄움

using UnityEngine;
using System.Collections.Generic;

public class DocumentGenerator : MonoBehaviour
{
    [Header("문서 UI 프리팹")]
    public GameObject splitDisplayPrefab;         // 확대 화면용
    public GameObject compactDocumentPrefab;      // 축소 화면용 (정보 없이 작게 표시)

    [Header("문서 생성 위치")]
    public Transform expandedDocumentParent;      // 확대 화면 문서 부모
    public Transform compactDocumentParent;       // 축소 화면 문서 부모 (책상 위)

    [Header("데이터 소스")]
    public List<string> randomNames;
    public List<string> nationalities;
    public List<Sprite> photos;

    public void GenerateRandomDocument()
    {
        // 1. 확대 화면 문서 생성 (정보 포함)
        GameObject doc = Instantiate(splitDisplayPrefab, expandedDocumentParent);
        DocumentSplitDisplay splitDisplay = doc.GetComponent<DocumentSplitDisplay>();

        DocumentData data = new DocumentData {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            dateOfBirth = RandomDate(),
            photo = photos[Random.Range(0, photos.Count)]
        };

        splitDisplay.InitializeDocument(data);
        splitDisplay.AnimateGiveDocument();

        // 2. 축소 화면 문서 생성 (정보 없이 단순 표시)
        if (compactDocumentPrefab != null && compactDocumentParent != null)
        {
            GameObject compactDoc = Instantiate(compactDocumentPrefab, compactDocumentParent);

            // 위치/크기 조정 또는 간단한 연출
            compactDoc.transform.localScale = Vector3.one * 0.4f;

            // 애니메이션 예시: 책상 위로 슬라이드 등장
            Animator animator = compactDoc.GetComponent<Animator>();
            if (animator != null)
                animator.SetTrigger("Appear"); // Animator에 해당 트리거 추가 필요
        }
    }

    // 문서 생성 범위
    private string RandomDate()
    {
        int year = Random.Range(1960, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        return $"{year}.{month:00}.{day:00}";
    }
}