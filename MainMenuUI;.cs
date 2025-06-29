using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("설정 패널")]
    public GameObject settingPanel; // 설정 UI 패널 (씬 위에 덮는 UI)

    // 게임 시작 버튼 클릭 시 호출됨
    public void OnStartGame()
    {
        SceneManager.LoadScene("예시 씬 이름");
    }

    // 불러오기 버튼 클릭 시 호출됨
    public void OnLoadGame()
    {
        SceneManager.LoadScene("LoadScene"); // 저장 데이터 관리 씬
    }

    // 게임 종료 버튼 클릭 시 호출됨
    public void OnQuitGame()
    {
        Application.Quit();

        // 에디터에서는 종료 안 되니까 종료처럼 보이게 하기 위한 코드(출시할 때 제거 안 해도 무방)
        UnityEditor.EditorApplication.isPlaying = false;

    }

    // 설정 버튼 클릭 시 호출됨
    public void OnOpenSettings()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true); // 설정 패널 활성화
        }
    }
}