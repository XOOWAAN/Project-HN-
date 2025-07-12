// 문서 데이터를 UI에 시각적으로 표시하는 컨트롤러 스크립트

// 주요 기능:
// - SetData(DocumentData data): 문서 정보 설정 및 UI 텍스트/이미지 갱신
// - BringToFront(): 문서를 UI 계층 맨 앞으로 이동시켜 강조 표시
// - null 사진 대응: 사진이 없을 경우 기본 이미지로 대체 표시

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocumentUIController : MonoBehaviour
{
    [Header("UI References")]
    public Image photoImage;           // 사진
    public TMP_Text nameText;          // 이름
    public TMP_Text birthText;         // 생년월일
    public TMP_Text nationalityText;   // 국적

    public TMP_Text genderText;        // 성별
    public TMP_Text addressText;       // 주소
    public TMP_Text businessTypeText;  // 사업 종류
    public TMP_Text departureText;     // 출발지
    public TMP_Text destinationText;   // 목적지

    public Sprite defaultPhoto;        // null 대비 기본 사진 (옵션)
    public Canvas canvas;

    private void Start() {
        canvas = GetComponent<Canvas>();
    }

    public void SetData(DocumentData data)
    {
        photoImage.sprite = data.photo != null ? data.photo : defaultPhoto;

        nameText.text = data.fullName;
        birthText.text = data.dateOfBirth;
        nationalityText.text = data.nationality;

        genderText.text = "";
        addressText.text = "";
        businessTypeText.text = "";
        departureText.text = "";
        destinationText.text = "";

        switch (data.documentType)
        {
            case DocumentType.IDCard:
                genderText.text = data.gender;
                addressText.text = data.address;
                break;

            case DocumentType.BusinessPermit:
                genderText.text = data.gender;
                businessTypeText.text = data.businessType;
                break;

            case DocumentType.Pass:
                departureText.text = data.departure;
                destinationText.text = data.destination;
                break;
        }
    }

    public void BringToFront()
    {
        transform.SetAsLastSibling();
    }
}