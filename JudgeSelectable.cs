// 지적 대상 항목에 붙이는 스크립트(문서, 인물, 매뉴얼 등)
// 클릭 시 JudgeManager에 InfoItem으로 전달됨

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