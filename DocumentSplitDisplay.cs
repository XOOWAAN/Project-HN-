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
    public RectTransform leftMask;    // 좌측 책상
    public RectTransform rightMask;   // 우측 책상
    public RectTransform personArea;  // 인물이 문서를 건네주는 위치

    private GameObject leftDocUI;
    private GameObject rightDocUI;

    public void InitializeDocument(DocumentData data)
    {
        // 좌측 문서 UI 생성
        leftDocUI = Instantiate(documentUIPrefab, leftMask);
        DocumentUIController leftController = leftDocUI.GetComponent<DocumentUIController>();
        leftController.SetData(data);
        leftController.SetScale(0.7f); // 축소된 상태

        // 우측 문서 UI 생성
        rightDocUI = Instantiate(documentUIPrefab, rightMask);
        DocumentUIController rightController = rightDocUI.GetComponent<DocumentUIController>();
        rightController.SetData(data);
        rightController.SetScale(1.2f); // 확대된 상태

        // 드래그 허용 영역 설정
        UIDragHandler leftDrag = leftDocUI.GetComponent<UIDragHandler>();
        UIDragHandler rightDrag = rightDocUI.GetComponent<UIDragHandler>();

        if (leftDrag != null)
        {
            leftDrag.validAreas = new RectTransform[] { leftMask, rightMask, personArea };
        }

        if (rightDrag != null)
        {
            rightDrag.validAreas = new RectTransform[] { leftMask, rightMask, personArea };
        }

        // 시작 애니메이션 실행
        StartCoroutine(AnimateGiveDocument(from: personArea.anchoredPosition, to: leftMask.anchoredPosition));
    }

    // 문서를 인물 위치에서 책상 위치로 이동시키는 애니메이션
    public IEnumerator AnimateGiveDocument(Vector2 from, Vector2 to, float duration = 0.5f)
    {
        RectTransform leftRect = leftDocUI.GetComponent<RectTransform>();
        RectTransform rightRect = rightDocUI.GetComponent<RectTransform>();

        // 처음 위치 설정
        leftRect.anchoredPosition = from;
        rightRect.anchoredPosition = from;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            leftRect.anchoredPosition = Vector2.Lerp(from, to, t);
            rightRect.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }

        // 최종 위치 고정
        leftRect.anchoredPosition = to;
        rightRect.anchoredPosition = to;
    }
}