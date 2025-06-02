// 책상 확대 화면과 축소 화면을 전환하는 버튼을 조작하는 스크립트
// 처리 버튼 : 문서 작업을 할 수 있는 '문서 확대 화면' 오브젝트로 전환
// 완료 버튼 : 문서 처리 완료 후 책상 확대 화면에서 책상 축소 화면으로 전환
// 책상 확대 화면의 좌측, 우측 책상에서 문서 전달 애니메이션 재생 후 축소 화면으로 돌아옴.

using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public GameObject collapsedView;  // 책상 축소 화면
    public GameObject expandedView;   // 책상 확대 화면

    // 처리 버튼을 눌렀을 때 호출
    public void SwitchToExpandedView()
    {
        collapsedView.SetActive(false);
        expandedView.SetActive(true);
    }

    // 완료 버튼을 눌렀을 때 호출
    public void SwitchToCollapsedView()
    {
        expandedView.SetActive(false);
        collapsedView.SetActive(true);
    }
}