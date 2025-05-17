// 교화 버튼: 인물 체포 애니메이션 실행

using UnityEngine;

public class ReeducateButtonHandler : MonoBehaviour
{
    public Animator characterAnimator;

    public void OnReeducateClick()
    {
        if (characterAnimator != null)
        {
            characterAnimator.SetTrigger("Arrest");
        }
        else
        {
            Debug.LogWarning("Character Animator가 없습니다.");
        }
    }
}