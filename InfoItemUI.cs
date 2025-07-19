// InfoItemUI.cs
// ------------------------------------
// InfoItem 데이터를 UI로 표시하고, 클릭 시 JudgeManager에 전달하는 통합 컴포넌트
// 문서·인물·매뉴얼 등 모든 항목을 InfoItem 데이터로 변환하여 이 스크립트를 통해 클릭 가능하게 함
// Initialize()를 통해 라벨/값/UI를 세팅하고 클릭 이벤트를 등록

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InfoItemUI : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI labelText;   // 항목 이름 텍스트
    [SerializeField] private TextMeshProUGUI valueText;   // 항목 값 텍스트
    [SerializeField] private Button button;               // (선택사항) 버튼을 통한 클릭

    private JudgeManager judgeManager;
    private JudgeManager.InfoItem item;

    // InfoItem 데이터를 받아 UI 구성 및 클릭 이벤트 연결
    public void Initialize(JudgeManager.InfoItem item, JudgeManager judgeManager)
    {
        this.item = item;
        this.judgeManager = judgeManager;

        if (labelText != null)
            labelText.text = item.label;

        if (valueText != null)
            valueText.text = item.value;

        // 버튼이 있을 경우에도 클릭 이벤트 연결
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnClick);
        }
    }

    // 버튼을 통한 클릭 처리
    private void OnClick()
    {
        HandleClick();
    }

    // IPointerClickHandler를 통한 클릭 처리
    public void OnPointerClick(PointerEventData eventData)
    {
        HandleClick();
    }

    // JudgeManager로 InfoItem 전달 및 비교 결과 출력
    private void HandleClick()
    {
        if (judgeManager == null)
        {
            judgeManager = JudgeManager.Instance; // 싱글톤 접근 (에디터에서 직접 세팅 안 한 경우)
        }

        if (judgeManager == null) return;

        judgeManager.SelectItem(item);

        if (judgeManager.HasSelectedTwoItems())
        {
            string result = judgeManager.EvaluateContradiction();
            judgeManager.ShowResultUI(result);
        }
    }
}