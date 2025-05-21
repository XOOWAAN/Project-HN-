// 문서를 무작위로 생성하고 Canvas에 분할된 UI로 문서를 띄움

using UnityEngine;
using System.Collections.Generic;

public class DocumentGenerator : MonoBehaviour
{
    [Header("문서 UI 프리팹")]
    public GameObject splitDisplayPrefab;

    [Header("문서 생성 위치")]
    public Transform documentParent;

    [Header("데이터 소스")]
    public List<string> randomNames;
    public List<string> nationalities;
    public List<Sprite> photos;

    public void GenerateRandomDocument()
    {   
        GameObject doc = Instantiate(splitDisplayPrefab, documentParent);
        DocumentSplitDisplay splitDisplay = doc.GetComponent<DocumentSplitDisplay>();

        DocumentData data = new DocumentData {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            dateOfBirth = RandomDate(),
            photo = photos[Random.Range(0, photos.Count)]
        };

        splitDisplay.InitializeDocument(data);
        splitDisplay.AnimateGiveDocument(); // 이름 통일: PlayPassDocumentAnimation → AnimateGiveDocument
    }

    private string RandomDate()
    {
        int year = Random.Range(1960, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        return $"{year}.{month:00}.{day:00}";
    }
}