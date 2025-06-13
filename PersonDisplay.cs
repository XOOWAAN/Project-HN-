// 인물 정보를 UI에 표시하는 스크립트
// 얼굴 이미지와 몸통 이미지를 나누어 구성
// 추후 인물 등장 시 애니메이션 재생을 여기서 해야 하나?

using UnityEngine;
using UnityEngine.UI;

public class PersonDisplay : MonoBehaviour
{
    [Header("UI 연결 컴포넌트")]
    public Image faceImage;     // 인물 얼굴 이미지
    public Image bodyImage;     // 인물 몸통 이미지

    private PersonData personData;

    // 인물 데이터를 받아서 UI에 반영하는 초기화 함수
    public void InitializePerson(PersonData data)
    {
        personData = data;

        // 얼굴과 몸통을 각각의 이미지로 설정
        faceImage.sprite = data.faceSprite;
        bodyImage.sprite = data.bodySprite;
    }

    // 인물이 등장하는 애니메이션
    public void AnimateEnter()
    {
        // TODO: 애니메이션 트리거나 연출 추가
        // 예: transform.DOMove 또는 Animator 활용
    }

    // 인물 데이터를 외부에서 가져올 수 있게 반환
    public PersonData GetPersonData()
    {
        return personData;
    }
}