// 하나의 문서를 좌/우 책상에 각각 복제해 페이퍼플리즈처럼 경계를 넘은 부분만 확대 또는 축소
// 인물이 문서를 건네주는 연출까지 담당

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DocumentSplitDisplay : MonoBehaviour
{
    [Header("UI 프리팹")]
    public GameObject documentUIPrefab;

    [Header("마스크 영역")]
    public RectTransform leftMask;
    public RectTransform rightMask;
    public RectTransform personArea;

    [Header("드래그 금지 영역")]
    public RectTransform invalidArea1;
    public RectTransform invalidArea2;

    private GameObject leftDocUI;

    private DocumentData cachedData;

    public void InitializeDocument(DocumentData data)
    {
        cachedData = data;

        // 좌측 문서 UI만 생성
        leftDocUI = Instantiate(documentUIPrefab, leftMask);
        DocumentUIController controller = leftDocUI.GetComponent<DocumentUIController>();
        controller.SetData(data);
        controller.SetScale(0.7f); // 축소

        UIDragHandler dragHandler = leftDocUI.GetComponent<UIDragHandler>();
        if (dragHandler != null)
        {
            dragHandler.validAreas = new RectTransform[] { leftMask, rightMask, personArea };
            dragHandler.invalidAreas = new RectTransform[] { invalidArea1, invalidArea2 };
        }

        // 초기 위치를 숨긴 영역(인물 손)으로 설정
        RectTransform leftRect = leftDocUI.GetComponent<RectTransform>();
        leftRect.anchoredPosition = personArea.anchoredPosition;
    }

    public void AnimateGiveDocument(float duration = 0.5f)
    {
        StartCoroutine(AnimateMoveToDesk(duration));
    }

    private IEnumerator AnimateMoveToDesk(float duration)
    {
        RectTransform docRect = leftDocUI.GetComponent<RectTransform>();
        Vector2 from = personArea.anchoredPosition;
        Vector2 to = Vector2.zero;

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
}