// 매뉴얼 UI 항목 리스트 → 상세보기 → 뒤로가기 흐름 구현

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManualUIController : MonoBehaviour
{
    [Header("탭 패널")]
    public GameObject listPanel;          // 항목 리스트 패널
    public GameObject detailPanel;        // 상세 설명 패널

    [Header("리스트 요소")] 
    public Transform entryListParent;     // 항목 리스트의 부모 (스크롤뷰 Content)
    public GameObject entryButtonPrefab;  // 항목 버튼 프리팹

    [Header("상세 요소")]
    public TMP_Text detailTitleText;
    public TMP_Text detailContentText;
    public Image detailImage;

    private List<ManualEntry> currentEntries;

    // 매뉴얼 항목을 받아와 UI로 출력
    public void InitializeManualUI(List<ManualEntry> entries)
    {
        currentEntries = entries;
        foreach (Transform child in entryListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in entries)
        {   
            // [버튼 프리팹 에디터에서 적용하는 법]
            // EntryButton (Button)
            // └── Text (TMP_Text) ← 버튼 텍스트
            GameObject buttonObj = Instantiate(entryButtonPrefab, entryListParent);
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            buttonText.text = entry.title;

            Button btn = buttonObj.GetComponent<Button>();
            btn.onClick.AddListener(() => ShowDetail(entry));
        }

        listPanel.SetActive(true);
        detailPanel.SetActive(false);
    }

    // 항목 클릭 시 상세 설명 UI 활성화
    private void ShowDetail(ManualEntry entry)
    {
        detailTitleText.text = entry.title;
        detailContentText.text = entry.content;
        detailImage.sprite = entry.image;
        detailImage.gameObject.SetActive(entry.image != null);

        listPanel.SetActive(false);
        detailPanel.SetActive(true);
    }

    // 뒤로가기 버튼에서 호출
    public void OnBackToList()
    {
        listPanel.SetActive(true);
        detailPanel.SetActive(false);
    }
}

[유니티 에디터에서의 요소 연결 방법]
// ManualUIController
// ├── ListPanel
// │   └── ScrollView
// │       └── Viewport
// │           └── Content  ← entryListParent 연결
// ├── DetailPanel
// │   ├── TitleText        ← detailTitleText 연결
// │   ├── ContentText      ← detailContentText 연결
// │   ├── Image            ← detailImage 연결
// │   └── BackButton       ← OnBackToList() 연결