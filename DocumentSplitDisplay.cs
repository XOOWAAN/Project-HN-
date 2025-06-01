// 하나의 문서를 우측 책상에만 표시하고, 좌측은 시각 연출용으로만 활용
// 인물이 문서를 건네주는 연출도 포함

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DocumentSplitDisplay : MonoBehaviour
{
    [Header("UI 프리팹")]
    public GameObject documentUIPrefab;

    [Header("마스크 및 영역")]
    public RectTransform rightMask;          // 우측 책상 마스크 (문서가 드래그 가능한 영역)
    public RectTransform personArea;         // 문서가 시작하는 위치 (인물 손 영역)

    [Header("드래그 금지 영역")]
    public RectTransform invalidArea2;       // 우측 금지 영역만 유효

    private GameObject docUI;
    private DocumentData cachedData;

    public void InitializeDocument(DocumentData data)
    {
        cachedData = data;

        // 문서 UI 생성 (오직 우측 책상 영역에만)
        docUI = Instantiate(documentUIPrefab, rightMask);
        DocumentUIController controller = docUI.GetComponent<DocumentUIController>();
        controller.SetData(data);
        controller.SetScale(0.7f); // 초기 축소 상태로 시작

        // 드래그 설정
        UIDragHandler dragHandler = docUI.GetComponent<UIDragHandler>();
        if (dragHandler != null)
        {
            // 우측 책상 내에서만 드래그 가능하도록 설정
            dragHandler.validAreas = new RectTransform[] { rightMask };
            dragHandler.invalidAreas = new RectTransform[] { invalidArea2 };
        }

        // 초기 위치를 인물 손 위치로 설정
        RectTransform docRect = docUI.GetComponent<RectTransform>();
        docRect.anchoredPosition = personArea.anchoredPosition;

        // 문서를 우측 책상 중앙으로 떨어뜨리는 애니메이션 실행
        AnimateDropToDesk(0.3f);
    }

    public void AnimateGiveDocument(float duration = 0.5f)
    {
        StartCoroutine(AnimateMoveToDesk(duration));
    }

    private IEnumerator AnimateMoveToDesk(float duration)
    {
        RectTransform docRect = docUI.GetComponent<RectTransform>();
        Vector2 from = personArea.anchoredPosition;
        Vector2 to = Vector2.zero; // 우측 책상의 중심으로 이동

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            docRect.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }

        docRect.anchoredPosition = to;
    }

    // 문서를 위쪽에서 아래로 떨어뜨리는 연출용 애니메이션
    public void AnimateDropToDesk(float duration = 0.3f)
    {
        StartCoroutine(DropToDeskAnimation(duration));
    }

    private IEnumerator DropToDeskAnimation(float duration)
    {
        RectTransform docRect = docUI.GetComponent<RectTransform>();

        Vector2 targetPos = Vector2.zero; // 중앙 정렬 기준
        Vector2 startPos = targetPos + new Vector2(0f, 300f); // 위에서 출발

        docRect.anchoredPosition = startPos;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            docRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        docRect.anchoredPosition = targetPos;
    }
}