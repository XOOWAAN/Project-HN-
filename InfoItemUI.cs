// InfoItem 데이터를 실제로 화면에 표시, 클릭할 수 있게 만드는 UI 컴포넌트 스크립트
// 예로, DocumentData는 주민등록증 전체고, InfoItem은 민증의 항목 하나임. InfoItemUI는 항목 클릭 가능하게 함
//버튼이나 텍스트 클릭 시 InfoItemUI가 이를 감지하고 DocumentJudgeManager에게 알려주는 역할을 함

using UnityEngine;
using UnityEngine.UI;

public class InfoItemUI : MonoBehaviour
{
    public Text labelText;
    public Text valueText;

    private DocumentJudgeManager judgeManager;
    private DocumentJudgeManager.InfoItem infoItem;

    private Button button;

    public void Initialize(DocumentJudgeManager.InfoItem item, DocumentJudgeManager manager)
    {
        infoItem = item;
        judgeManager = manager;

        if (labelText != null) labelText.text = item.label;
        if (valueText != null) valueText.text = item.value;
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClickItem);
        }
    }

    private void OnClickItem()
    {
        if (judgeManager != null)
        {
            judgeManager.SelectItem(infoItem);

            if (judgeManager.HasSelectedTwoItems())
            {
                string result = judgeManager.EvaluateContradiction();
                Debug.Log(result);
            }
        }
    }
}