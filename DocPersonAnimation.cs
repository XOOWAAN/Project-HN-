using UnityEngine;
using System.Collections;

public class DocPersonAnimation : MonoBehaviour
{
    public RectTransform documentRect;
    public Transform personTransform;

    // 생성 시: 문서 떨어뜨리기 + 인물 등장
    public void PlayEntrance(float duration = 0.5f)
    {
        if (documentRect != null)
            StartCoroutine(DropDocumentAnimation(duration));

        if (personTransform != null)
            StartCoroutine(MovePersonIn(duration));
    }

    // 퇴장 시: 문서 사라지기 + 인물 퇴장
    public void PlayExit(float duration = 0.5f)
    {
        if (documentRect != null)
            StartCoroutine(SlideDocumentOut(duration));

        if (personTransform != null)
            StartCoroutine(MovePersonOut(duration));
    }

    private IEnumerator DropDocumentAnimation(float duration)
    {
        Vector2 targetPos = Vector2.zero;
        Vector2 startPos = targetPos + new Vector2(0f, 300f);
        documentRect.anchoredPosition = startPos;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            documentRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        documentRect.anchoredPosition = targetPos;
    }

    private IEnumerator SlideDocumentOut(float duration)
    {
        Vector2 startPos = documentRect.anchoredPosition;
        Vector2 endPos = startPos + new Vector2(500f, 0f);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            documentRect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
    }

    private IEnumerator MovePersonIn(float duration)
    {
        Vector3 start = personTransform.position + new Vector3(-3f, 0, 0);
        Vector3 end = personTransform.position;
        personTransform.position = start;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            personTransform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        personTransform.position = end;
    }

    private IEnumerator MovePersonOut(float duration)
    {
        Vector3 start = personTransform.position;
        Vector3 end = start + new Vector3(3f, 0, 0);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, timer / duration);
            personTransform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        personTransform.position = end;
    }
}