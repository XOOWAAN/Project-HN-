// 문서를 드래그해서 이동할 수 있게 해주는 인터페이스 구현

using UnityEngine;
using UnityEngine.EventSystems; // UI 입력 이벤트 처리용

// 드래그 관련 이벤트 인터페이스 구현
public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform; // UI 위치 조작용
    private Canvas canvas;               // 스케일 보정용
    private CanvasGroup canvasGroup;     // 드래그 중에 raycast 무시용

    private void Awake() {
        // 이 UI 오브젝트에 붙은 RectTransform 가져오기
        rectTransform = GetComponent<RectTransform>();

        // 드래그 중 Raycast를 막을 수 있게 CanvasGroup 사용
        canvasGroup = GetComponent<CanvasGroup>();

        // 부모 Canvas 참조
        canvas = GetComponentInParent<Canvas>();
    }

    // 드래그 시작 시 호출됨
    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false; // 드래그 중 다른 UI와 충돌하지 않도록
        transform.SetAsLastSibling();       // 드래그된 문서를 가장 위로 보냄
    }

    // 드래그 중 계속 호출됨
    public void OnDrag(PointerEventData eventData) {
        // 스케일 보정 후 위치 업데이트
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // 드래그 끝났을 때 호출됨
    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true; // 다시 충돌 감지 허용
    }
}