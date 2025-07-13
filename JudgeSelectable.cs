// JudgeSelectable.cs
// ----------------------------
// 비교 대상이 되는 UI 항목(문서, 인물, 매뉴얼 등)에 부착하는 클릭 처리 스크립트
// 항목을 클릭하면 JudgeManager에 InfoItem으로 전달되어 비교 판단 대상이 됨
// label에는 항목 이름(예: "이름"), value에는 해당 값(예: "김철수")을 입력

// 유니티 에디터에서 비교 대상 오브젝트에 붙이면 됨

using UnityEngine;
using UnityEngine.EventSystems;

public class JudgeSelectable : MonoBehaviour, IPointerClickHandler
{
    public string label; // 항목 이름
    public string value; // 항목 값

    public void OnPointerClick(PointerEventData eventData)
    {
        // InfoItem 객체를 생성하여 JudgeManager에 전달
        var item = new JudgeManager.InfoItem(label, value);
        JudgeManager.Instance.SelectItem(item);
    }
}