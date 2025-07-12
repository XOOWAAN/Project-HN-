// 문서를 우측 책상에만 표시하고, 좌측은 시각 연출용으로 RenderTexture 사용
// 좌측 영역은 책상 축소 화면에 카메라를 추가해 스크립트와 연결해야 함

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocumentSplitDisplay : MonoBehaviour
{
    [Header("UI 프리팹")]
    public GameObject documentUIPrefab;

    [Header("마스크 및 영역")]
    public RectTransform rightMask;          // 우측 책상 마스크 (문서가 드래그 가능한 영역)
    public RectTransform personArea;         // 문서가 시작하는 위치 (인물 손 영역)

    [Header("드래그 금지 영역")]
    public RectTransform invalidArea2;       // 우측 금지 영역만 유효

    [Header("좌측 RenderTexture 표시용")]
    public RawImage leftPreviewImage;        // 확대 화면 좌측에 보여줄 이미지 (축소 화면의 일부)
    public RenderTexture miniDeskTexture;    // 축소 화면 카메라의 렌더 타겟

    private GameObject docUI;
    private DocumentData cachedData;

    // 정렬 우선순위 (낮을수록 위에 그려짐)
    public int orderPriority = 0;

    public void InitializeDocument(DocumentData data)
    {
        cachedData = data;

        // 문서 UI 생성 (오직 우측 책상 영역에만)
        docUI = Instantiate(documentUIPrefab, rightMask);
        DocumentUIController controller = docUI.GetComponent<DocumentUIController>();
        controller.SetData(data);
        controller.SetScale(0.7f); // 초기 축소 상태로 시작

        // 문서 외형 구성 (문서 종류별로 다르게 설정)
        switch (data.documentType)
        {
            case DocumentType.IDCard:
                controller.SetTitle("신분증");
                controller.SetField("이름", data.fullName);
                controller.SetField("생년월일", data.dateOfBirth);
                controller.SetField("성별", data.gender);
                break;

            case DocumentType.BusinessPermit:
                controller.SetTitle("사업 허가증");
                controller.SetField("이름", data.fullName);
                controller.SetField("주소", data.address);
                controller.SetField("업종", data.businessType);
                break;

            case DocumentType.Pass:
                controller.SetTitle("통행증");
                controller.SetField("이름", data.fullName);
                controller.SetField("출발지", data.departure);
                controller.SetField("도착지", data.destination);
                break;
        }

        // 드래그 설정
        UIDragHandler dragHandler = docUI.GetComponent<UIDragHandler>();
        if (dragHandler != null)
        {
            dragHandler.validAreas = new RectTransform[] { rightMask };
            dragHandler.invalidAreas = new RectTransform[] { invalidArea2 };
        }

        // 초기 위치를 인물 손 위치로 설정
        RectTransform docRect = docUI.GetComponent<RectTransform>();
        docRect.anchoredPosition = personArea.anchoredPosition;

        // 우선순위에 따라 문서 정렬 (낮을수록 위에 위치)
        docRect.SetSiblingIndex(orderPriority);

        // 문서 애니메이션 스크립트로 연출 위임
        DocumentAnimation animation = docUI.GetComponent<DocumentAnimation>();
        if (animation != null)
        {
            animation.targetRect = docRect;
            animation.AnimateDropToDesk(0.3f);
        }

        // 좌측에 축소 화면을 렌더링한 이미지 보여주기
        if (leftPreviewImage != null && miniDeskTexture != null)
        {
            leftPreviewImage.texture = miniDeskTexture; // RenderTexture 적용
        }
    }

    public void AnimateGiveDocument(float duration = 0.5f)
    {
        if (docUI == null) return;

        DocumentAnimation animation = docUI.GetComponent<DocumentAnimation>();
        if (animation != null)
        {
            animation.targetRect = docUI.GetComponent<RectTransform>();
            animation.AnimateGiveFrom(personArea.anchoredPosition, duration);
        }
    }
}