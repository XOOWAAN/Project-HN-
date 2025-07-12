// DocumentUIController.cs
// ----------------------------
// DocumentData를 기반으로 문서 UI에 실제 데이터를 표시하는 컨트롤러
// 사진, 이름, 생년월일, 국적 등 문서 종류에 따라 필요한 항목만 UI에 출력함
// DocumentGenerator 또는 DocumentSplitDisplay에서 호출되어 UI 구성

// 주요 메서드:
// - SetData(DocumentData): 전달된 문서 데이터를 받아 각 UI 요소에 매핑
// - BringToFront(): 해당 문서를 다른 UI 위로 올려 강조 표시

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

    private void Start()
    {
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