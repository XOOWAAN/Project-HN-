// ManualUIController.cs
// ----------------------------
// 매뉴얼 항목을 UI에 표시하고, 상세 정보 보기 및 뒤로가기 기능을 제공하는 컨트롤러
// 리스트 형태로 모든 규칙 제목을 보여주며, 클릭 시 상세 설명과 이미지가 출력됨
// ManualManager에서 가져온 매뉴얼 데이터를 시각적으로 유저에게 노출하는 역할
// 스크롤뷰, 버튼, 텍스트 UI 요소를 연결하여 구성됨

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