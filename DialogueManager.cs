// 인물의 대사를 출력하고 녹음본에 전달하는 매니저
// 말풍선 없이 UI에만 전달되며, 인물 퇴장 시 초기화

using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("대사 세트")]
    public List<DialogueSet> dialogueSets; // 여러 대사 세트 (ScriptableObject 등으로 구성 가능)

    private DialogueSet currentSet;
    private int currentLineIndex = 0;

    private RecordingLogUI recordingLogUI;

    public void Init(RecordingLogUI logUI)
    {
        recordingLogUI = logUI;
    }


    // 대사 시작 (무작위 세트 선택)

    public void StartDialogue()
    {
        currentSet = dialogueSets[Random.Range(0, dialogueSets.Count)];
        currentLineIndex = 0;
        DisplayNextLine();
    }

    // 다음 대사 출력 및 녹음본 기록
    public void DisplayNextLine()
    {
        if (currentSet == null || currentLineIndex >= currentSet.lines.Count)
            return;

        string line = currentSet.lines[currentLineIndex];
        recordingLogUI.AddLine(line);
        currentLineIndex++;
    }

    // 인물 퇴장 시 초기화
    public void ResetDialogue()
    {
        recordingLogUI.ClearLog();
        currentSet = null;
        currentLineIndex = 0;
    }
}

[System.Serializable]
public class DialogueSet
{
    public List<string> lines;
}