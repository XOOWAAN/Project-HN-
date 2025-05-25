// 녹음본 탭 UI
// DialogueManager로부터 대사를 받아서 순서대로 UI에 표시함

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RecordingLogUI : MonoBehaviour
{
    [Header("UI 요소")]
    public RectTransform contentRoot; // 대사 로그의 부모 (스크롤 뷰 내부)
    public GameObject logLinePrefab; // 대사 한 줄용 프리팹

    private List<GameObject> logLines = new List<GameObject>();

    // 녹음본에 대사 한 줄 추가
    public void AddLine(string line)
    {
        GameObject newLine = Instantiate(logLinePrefab, contentRoot);
        newLine.GetComponent<Text>().text = line;
        logLines.Add(newLine);
    }

    // 녹음본 초기화
    public void ClearLog()
    {
        foreach (GameObject line in logLines)
        {
            Destroy(line);
        }
        logLines.Clear();
    }
}