// 문서 드래그, 유효하지 않은 영역에 드롭하면 마지막 유효한 위치로 자동 복귀

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform; // 이 오브젝트의 RectTransform
    private Canvas canvas;               // 이 오브젝트가 소속된 캔버스
    private Vector2 originalPosition;    // 드래그 시작 시 위치
    private Vector2 lastValidPosition;   // 마지막으로 드롭된 유효한 위치

    [Header("드래그 허용 영역")]
    public RectTransform[] validAreas;   // 드래그가 허용된 영역들 (좌측 책상, 우측 책상, 인물 영역 등)

    void Awake()
    {
        // 컴포넌트 초기화
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // 드래그 시작 시 호출됨
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 현재 위치를 저장해둠 (필요 시 복귀용)
        originalPosition = rectTransform.anchoredPosition;
    }

    // 드래그 중 매 프레임마다 호출됨
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        // 드래그 위치를 캔버스 내부의 좌표계로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            null,
            out pos
        );

        // 오브젝트 위치 갱신
        rectTransform.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsInValidArea(rectTransform))
        {
            lastValidPosition = rectTransform.anchoredPosition;

            // 확대/축소 처리
            if (RectTransformUtility.RectangleContainsScreenPoint(validAreas[1], Input.mousePosition)) // 우측
                SetScale(1.2f);
            else if (RectTransformUtility.RectangleContainsScreenPoint(validAreas[0], Input.mousePosition)) // 좌측
                SetScale(0.7f);
        }
        else
        {
            rectTransform.anchoredPosition = lastValidPosition;
        }
    }

    private void SetScale(float scale) // 어느 영역에 문서가 있는지에 따라 확대 축소 표시
    {
        rectTransform.localScale = Vector3.one * scale;
    }

    // 현재 RectTransform이 어떤 유효 영역 안에 있는지 확인
    private bool IsInValidArea(RectTransform dragTarget)
    {
        foreach (var area in validAreas)
        {
            // 마우스가 유효 영역 중 하나 위에 있을 경우 true 반환
            if (RectTransformUtility.RectangleContainsScreenPoint(area, Input.mousePosition))
                return true;
        }
        return false; // 어느 영역에도 포함되지 않음
    }
}