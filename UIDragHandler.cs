// 문서 드래그, 유효하지 않은 영역에 드롭하면 마지막 유효한 위치로 자동 복귀

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private RectTransform rectTransform; // 드래그 대상의 RectTransform
    private Canvas canvas;               // 드래그가 이루어지는 부모 캔버스

    private Vector2 offset;              // 클릭 위치와 문서 중심 사이의 거리
    private RectTransform validArea;     // 문서가 이동 가능한 유일한 영역 (RightDesk)

    [Header("드래그 허용 영역 (RightDesk만)")]
    public RectTransform rightDeskArea;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // 유효한 드래그 영역으로 우측 책상(RightDesk) 설정
        validArea = rightDeskArea;
    }

    // 클릭 시 문서를 가장 위(UI 상단)로 올림
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling(); // 클릭만으로도 문서가 최상단에 오도록 처리
    }

    // 드래그 시작 시 호출됨
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling(); // 드래그 시작 시에도 문서를 최상단으로 이동

        // 클릭한 마우스 위치를 캔버스 로컬 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            null,
            out var localPoint
        );

        // 드래그 시작 시 클릭 지점과 오브젝트 중심의 거리 계산
        offset = rectTransform.anchoredPosition - localPoint;
    }

    // 드래그 중 계속 호출됨
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        // 현재 마우스 위치를 캔버스 로컬 좌표로 변환
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            null,
            out localPoint))
        {
            // 마우스 위치 + offset = 목표 위치
            Vector2 targetPosition = localPoint + offset;

            // 목표 위치를 유효 영역 안으로 제한
            Vector2 clampedPosition = ClampToArea(targetPosition, validArea);
            rectTransform.anchoredPosition = clampedPosition;
        }
    }

    // 드래그 종료 시 호출됨 (필요 시 후처리 추가 가능)
    public void OnEndDrag(PointerEventData eventData)
    {
        // 현재는 드래그 종료 시 특별한 처리 없음
    }

    // 지정된 영역 내부로 위치를 제한 (Clamp)
    private Vector2 ClampToArea(Vector2 targetPos, RectTransform area)
    {
        Vector3[] corners = new Vector3[4];
        area.GetWorldCorners(corners); // 영역의 월드 코너 좌표 받아오기

        // 좌상단(corners[0]) ~ 우하단(corners[2]) 범위 계산
        Vector2 min = WorldToAnchored(corners[0]);
        Vector2 max = WorldToAnchored(corners[2]);

        // x, y 좌표를 min-max 사이로 제한
        float clampedX = Mathf.Clamp(targetPos.x, min.x, max.x);
        float clampedY = Mathf.Clamp(targetPos.y, min.y, max.y);

        return new Vector2(clampedX, clampedY);
    }

    // 월드 좌표 → 캔버스 기준의 로컬 좌표로 변환
    private Vector2 WorldToAnchored(Vector3 worldPos)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            RectTransformUtility.WorldToScreenPoint(null, worldPos),
            null,
            out localPoint
        );
        return localPoint;
    }
}