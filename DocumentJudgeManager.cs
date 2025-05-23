using UnityEngine;
using System.Collections.Generic;

public class DocumentJudgeManager : MonoBehaviour
{
    // 판단 결과 타입
    public enum JudgmentResult
    { 
        Pass,       // 합격
        Fail,       // 불합격
        Mismatch    // 항목 불일치 (지적 가능)
    }

    // InfoItem은 UI 조작을 위해 추가로 만들어진 표현 수단임. DocumentData를 UI클릭용으로 분해해서 쓰는 것.
    [System.Serializable]
    public class InfoItem
    {
        public string label;  // 항목 이름 예: "이름", "국적"
        public string value;  // 항목 값 예: "김철수", "대한민국"

        public InfoItem(string label, string value)
        {
            this.label = label;
            this.value = value;
        }
    }

    // 인물 데이터
    [System.Serializable]
    public struct PersonData
    {
        public string fullName;
        public string nationality;
        public string dateOfBirth;
        public Sprite photo;
    }

    // 문서 데이터
    [System.Serializable]
    public struct DocumentData
    {
        public string fullName;
        public string nationality;
        public string dateOfBirth;
        public Sprite photo;
    }

    // 판단 함수
    public JudgmentResult Judge(PersonData person, DocumentData document, out string mismatchReason)
    {
        List<string> mismatches = new List<string>();

        if (person.fullName != document.fullName)
            mismatches.Add("이름이 일치하지 않음");
        if (person.nationality != document.nationality)
            mismatches.Add("국적이 일치하지 않음");
        if (person.dateOfBirth != document.dateOfBirth)
            mismatches.Add("생년월일이 일치하지 않음");
        if (person.photo != document.photo)
            mismatches.Add("사진이 일치하지 않음");

        if (mismatches.Count == 0)
        {
            mismatchReason = "";
            return JudgmentResult.Pass;
        }
        else if (mismatches.Count == 1)
        {
            mismatchReason = mismatches[0];
            return JudgmentResult.Mismatch;
        }
        else
        {
            mismatchReason = string.Join(", ", mismatches);
            return JudgmentResult.Fail;
        }
    }

    // ------------------------------
    // 지적 모드를 위한 항목 선택 로직
    // ------------------------------

    private List<InfoItem> selectedItems = new List<InfoItem>();

    public void StartContradictionMode()
    {
        selectedItems.Clear();
    }

    public void SelectItem(InfoItem item)
    {
        if (selectedItems.Count < 2)
        {
            selectedItems.Add(item);
        }
    }

    public bool HasSelectedTwoItems()
    {
        return selectedItems.Count == 2;
    }

    public string EvaluateContradiction()
    {
        if (selectedItems.Count != 2)
            return "항목 두 개를 선택해야 합니다.";

        if (selectedItems[0].value != selectedItems[1].value)
        {
            return $"불일치 발견: {selectedItems[0].label} ≠ {selectedItems[1].label}";
        }

        return "일치: 두 항목은 동일합니다.";
    }
}