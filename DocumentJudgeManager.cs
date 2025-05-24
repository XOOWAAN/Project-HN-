using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DocumentJudgeManager : MonoBehaviour
{
    public GameObject resultPanel; // 비교 결과를 보여줄 UI 판넬
    public TextMeshProUGUI resultText; // 결과 텍스트 표시

    private List<InfoItem> selectedItems = new List<InfoItem>();

    // 항목 선택 (InfoItemUI에서 호출)
    public void SelectItem(InfoItem item)
    {
        if (selectedItems.Count >= 2)
            selectedItems.Clear();

        selectedItems.Add(item);

        if (HasSelectedTwoItems())
        {
            string result = EvaluateContradiction();
            ShowResultUI(result);
        }
    }

    public bool HasSelectedTwoItems() => selectedItems.Count == 2;

    public string EvaluateContradiction()
    {
        if (selectedItems.Count != 2)
            return "항목 두 개를 선택해야 합니다.";

        if (selectedItems[0].value != selectedItems[1].value)
            return $"불일치 발견: {selectedItems[0].label} ≠ {selectedItems[1].label}";

        return "일치: 두 항목은 동일합니다.";
    }

    public void ShowResultUI(string resultMessage)
    {
        resultPanel.SetActive(true);
        resultText.text = resultMessage;
        StartCoroutine(HideResultAfterDelay(1.5f)); // 비교 결과가 1.5초 동안 유지 후 사라짐
    }

    private IEnumerator HideResultAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resultPanel.SetActive(false);
    }
}