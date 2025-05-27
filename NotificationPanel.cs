// 알림 패널에서 알림 박스를 생성, 삭제, 정렬 관리
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NotificationPanel : MonoBehaviour
{
    public Transform notificationContainer; // 알림 박스가 들어갈 부모 오브젝트
    public GameObject notificationPrefab; // 알림 박스 프리팹
    private List<GameObject> activeNotifications = new List<GameObject>();
    private const int maxNotifications = 6;

    // 경고 알림 추가
    public void AddWarningNotification(string message)
    {
        AddNotification(message);
    }

    // 뉴스 알림 추가
    public void AddNewsNotification(string message)
    {
        AddNotification("[뉴스] " + message);
    }

    // 알림 박스 생성 및 정렬 처리
    private void AddNotification(string message)
    {
        // 초과 시 가장 오래된 알림 제거
        if (activeNotifications.Count >= maxNotifications)
        {
            GameObject oldest = activeNotifications[activeNotifications.Count - 1];
            activeNotifications.RemoveAt(activeNotifications.Count - 1);
            Destroy(oldest);
        }

        // 새로운 알림 생성
        GameObject notification = Instantiate(notificationPrefab, notificationContainer);
        notification.transform.SetAsFirstSibling(); // 가장 위로 정렬
        notification.GetComponentInChildren<Text>().text = message;

        // 삭제 버튼 처리 연결
        Button deleteButton = notification.transform.Find("DeleteButton")?.GetComponent<Button>();
        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(() => RemoveNotification(notification));
        }

        activeNotifications.Insert(0, notification);
    }

    // 수동 삭제
    public void RemoveNotification(GameObject notification)
    {
        if (activeNotifications.Contains(notification))
        {
            activeNotifications.Remove(notification);
            Destroy(notification);
        }
    }
}