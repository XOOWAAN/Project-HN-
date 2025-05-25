// 게임 설정 패널
// 설정 탭을 누르면 이 패널이 활성화되어 전체 화면을 덮음

using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("설정 패널 루트")]
    public GameObject panelRoot; // 패널 전체 루트 오브젝트

    [Header("닫기 버튼")]
    public Button closeButton; // 닫기 버튼

    private bool isOpen = false;

    private void Awake()
    {
        panelRoot.SetActive(false); // 시작 시 비활성화
        closeButton.onClick.AddListener(ClosePanel);
    }

    private void Update()
    {
        // 설정 창이 열려 있을 때 ESC 키 입력 감지
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        panelRoot.SetActive(true);
        isOpen = true;
    }

    public void ClosePanel()
    {
        panelRoot.SetActive(false);
        isOpen = false;
    }
}