// 컴퓨터 창 UI를 관장하는 전체 스크립트로, 내부의 탭 4개 스크립트를 연결하여 사용함

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComputerUIManager : MonoBehaviour
{
    [Header("컴퓨터 창 슬라이드 제어")]
    public RectTransform computerPanel; // 컴퓨터 전체 UI 패널
    public float slideDuration = 0.5f; // 슬라이드 시간
    public Vector2 hiddenPosition; // 숨겨진 anchoredPosition
    public Vector2 visiblePosition; // 보이는 anchoredPosition

    [Header("PC 버튼")]
    public Button pcButton; // 컴퓨터 창 안쪽의 PC 버튼

    [Header("탭 버튼")]
    public Button manualTabButton; 
    public Button recordingsTabButton;
    public Button notificationsTabButton;
    public Button settingsTabButton;

    [Header("탭 콘텐츠")]
    public GameObject manualPanel;
    public GameObject recordingsPanel;
    public GameObject notificationsPanel;

    [Header("설정 패널")]
    public SettingsPanel settingsOverlayPanel; // 오버레이 방식의 전체 설정 패널

    private bool isOpen = false;
    private Coroutine slideCoroutine;

    private void Start()
    {
        // PC 버튼으로 컴퓨터 창 열고 닫기
        pcButton.onClick.AddListener(ToggleComputerWindow);

        // 탭 버튼 리스너 연결
        manualTabButton.onClick.AddListener(() => ShowTab(manualPanel));
        recordingsTabButton.onClick.AddListener(() => ShowTab(recordingsPanel));
        notificationsTabButton.onClick.AddListener(() => ShowTab(notificationsPanel));

        // 설정 버튼은 별도 오버레이 패널을 여는 동작
        settingsTabButton.onClick.AddListener(OpenSettingsPanel);

        // 기본 탭 설정
        ShowTab(manualPanel);
        computerPanel.anchoredPosition = hiddenPosition;
    }

    // 컴퓨터 창 열기/닫기
    public void ToggleComputerWindow()
    {
        isOpen = !isOpen;

        if (slideCoroutine != null)
            StopCoroutine(slideCoroutine);

        slideCoroutine = StartCoroutine(SlidePanel(isOpen ? visiblePosition : hiddenPosition));
    }

    // 슬라이드 애니메이션
    private IEnumerator SlidePanel(Vector2 targetPos)
    {
        Vector2 startPos = computerPanel.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            computerPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsed / slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        computerPanel.anchoredPosition = targetPos;
    }

    // 일반 탭 열기
    private void ShowTab(GameObject activePanel)
    {
        manualPanel.SetActive(false);
        recordingsPanel.SetActive(false);
        notificationsPanel.SetActive(false);

        activePanel.SetActive(true);
    }

    // 설정 탭 클릭 시 설정 오버레이 열기
    private void OpenSettingsPanel()
    {
        settingsOverlayPanel.OpenPanel();
    }
}