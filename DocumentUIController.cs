// 문서 정보를 UI로 표현하는 컨트롤러 스크립트

using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 시 필요

public class DocumentUIController : MonoBehaviour
{
    [Header("UI References")]
    public Image photoImage;             // 증명사진을 보여줄 Image 컴포넌트
    public TMP_Text nameText;            // 이름을 출력할 TextMeshPro
    public TMP_Text birthText;           // 생년월일 출력용
    public TMP_Text nationalityText;     // 국적 출력용

    public Canvas canvas;                // 필요시 sortingOrder 등 접근용

    private void Start() {
        // 현재 오브젝트에 붙은 Canvas 컴포넌트를 가져온다
        canvas = GetComponent<Canvas>();
    }

    // 외부에서 데이터를 받아와서 UI에 반영하는 함수
    public void SetData(DocumentData data) {
        photoImage.sprite = data.photo;
        nameText.text = data.fullName;
        birthText.text = data.dateOfBirth;
        nationalityText.text = data.nationality;
    }

    // 이 문서를 UI 상단으로 올리기 (다른 문서 위에 보이게 하기)
    public void BringToFront() {
        transform.SetAsLastSibling(); // 부모 아래에서 가장 마지막 자식으로 배치됨 → 가장 위
    }
}