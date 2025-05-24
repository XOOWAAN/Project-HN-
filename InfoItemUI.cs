// InfoItem 데이터를 실제로 화면에 표시, 클릭할 수 있게 만드는 UI 컴포넌트 스크립트
// 예로, DocumentData는 주민등록증 전체고, InfoItem은 민증의 항목 하나임. InfoItemUI는 항목 클릭 가능하게 함
//버튼이나 텍스트 클릭 시 InfoItemUI가 이를 감지하고 DocumentJudgeManager에게 알려주는 역할을 함

using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro를 사용하는 경우

public class InfoItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelText;   // 항목 이름 텍스트
    [SerializeField] private TextMeshProUGUI valueText;   // 항목 값 텍스트
    [SerializeField] private Button button;               // 클릭 버튼

    private DocumentJudgeManager judgeManager;
    private DocumentJudgeManager.InfoItem item;

    // 항목 정보를 세팅하고 버튼 클릭 연결
    public void Initialize(DocumentJudgeManager.InfoItem item, DocumentJudgeManager judgeManager)
    {
        this.item = item;
        this.judgeManager = judgeManager;

        if (labelText != null)
            labelText.text = item.label;

        if (valueText != null)
            valueText.text = item.value;

        if (button != null)
            button.onClick.AddListener(OnClick);
    }

    // 클릭 시 항목 선택 및 지적 판단 실행
    private void OnClick()
    {
        judgeManager.SelectItem(item);

        if (judgeManager.HasSelectedTwoItems())
        {
            string result = judgeManager.EvaluateContradiction();  // 두 항목 비교
            judgeManager.ShowResultUI(result);                     // 결과 UI 표시
        }
    }
}