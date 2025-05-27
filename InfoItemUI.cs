// InfoItem 데이터를 실제로 화면에 표시, 클릭할 수 있게 만드는 UI 컴포넌트
// 예: 주민등록증의 이름, 생년월일 등 개별 항목을 표시하고 선택 가능하게 함

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelText;   // 항목 이름 텍스트
    [SerializeField] private TextMeshProUGUI valueText;   // 항목 값 텍스트
    [SerializeField] private Button button;               // 항목 클릭 버튼

    private DocumentJudgeManager judgeManager;
    private DocumentJudgeManager.InfoItem item;           // 비교용 데이터 항목

    // InfoItem 데이터를 받아 UI 구성 및 클릭 이벤트 연결
    public void Initialize(DocumentJudgeManager.InfoItem item, DocumentJudgeManager judgeManager)
    {
        this.item = item;
        this.judgeManager = judgeManager;

        if (labelText != null)
            labelText.text = item.label;

        if (valueText != null)
            valueText.text = item.value;

        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // 중복 연결 방지
            button.onClick.AddListener(OnClick);
        }
    }

    // 버튼 클릭 시 항목 선택을 판단 매니저에 전달
    private void OnClick()
    {
        judgeManager.SelectItem(item);

        if (judgeManager.HasSelectedTwoItems())
        {
            string result = judgeManager.EvaluateContradiction();
            judgeManager.ShowResultUI(result);
        }
    }
}