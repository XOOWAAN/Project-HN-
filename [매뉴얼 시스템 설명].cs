[매뉴얼 시스템 설명]

이 매뉴얼 시스템은 날짜에 따라 변화하는 규칙들을 수집하고 UI로 표시하며,
유저가 이를 근거로 인물 정보나 문서 정보와 비교해 지적할 수 있도록 돕는다.
또한 판단 시스템에서는 logicKey를 기준으로 현재 유효한 규칙을 불러와 비교 판단을 내린다.


[스크립트 소개:]

(ManualEntry)|
매뉴얼 항목 하나를 정의하는 데이터 구조다.
제목, 설명, 이미지, 판단 시스템 연동용 키(logicKey)를 포함한다.

(ManualDatabase)
날짜별로 매뉴얼 항목들을 저장하는 ScriptableObject이다.
특정 날짜까지의 항목들을 병합해 오늘의 규칙 목록을 반환하는 기능을 갖는다.

(ManualManager)
현재 날짜를 기준으로 유효한 매뉴얼 항목들을 가져오는 관리 스크립트다.
UI와 판단 시스템이 사용할 오늘의 규칙 리스트를 제공한다.

(ManualUIController)
매뉴얼 항목들을 UI로 표시하고, 상세 보기 및 뒤로가기 기능을 제공한다.
유저는 이 UI를 통해 오늘 적용되는 규칙을 확인할 수 있다.

이 매뉴얼 시스템은 날짜에 따라 변화하는 규칙들을 수집하고 UI로 표시하며,
유저가 이를 근거로 인물 정보나 문서 정보와 비교해 지적할 수 있도록 돕는다.
또한 판단 시스템에서는 logicKey를 기준으로 현재 유효한 규칙을 불러와 비교 판단을 내린다.


[작동 흐름 설명]
게임 시작 또는 날짜 경과 시, 시스템은 다음과 같은 순서로 작동한다:

ManualManager가 현재 날짜(currentDay)를 설정한다.
→ 이 날짜는 게임의 흐름에 따라 외부(GameFlowManager 등)에서 SetDay(int)로 호출된다.

ManualManager는 ManualDatabase에 요청하여 오늘까지의 매뉴얼 항목을 병합해서 가져온다.
→ ManualManager.GetTodayManualEntries()
→ 내부적으로 ManualDatabase.GetMergedManualEntriesUpToDay(int day) 호출됨
→ entryId 기준으로 항목을 덮어쓰기/누적하며 병합

UI에서는 ManualUIController가 이 매뉴얼 리스트를 받아 목록으로 출력한다.
→ ManualUIController.InitializeManualUI(List<ManualEntry>)
→ 매뉴얼 항목 버튼을 동적으로 생성하고 제목을 출력
→ 클릭 시 상세 설명과 이미지를 ShowDetail(entry)로 표시

플레이어는 UI를 통해 매뉴얼 내용을 읽고 판단 근거로 활용할 수 있다.
→ UI 요소에 JudgeSelectable이 붙어 있으면 클릭 지적 대상이 된다.
→ 클릭 시 JudgeManager.SelectItem(InfoItem)으로 전달되어 비교 대기 항목이 된다.

판단 시스템(JudgeManager 등)은 현재 유효한 규칙의 logicKey 리스트를 필요로 한다.
→ 이때 다시 ManualManager.GetTodayManualEntries()를 호출
→ 반환된 ManualEntry 리스트에서 entry.logicKey만 추출하여 규칙 키 리스트 생성

문서나 인물 데이터를 검사할 때 이 logicKey와 비교하여 조건을 적용한다.

예시:
if (activeKeys.Contains("business_required") && string.IsNullOrEmpty(person.businessType))
{
    규칙 위반: 사업자가 필요한데 정보가 없음
}

이 모든 흐름은 날짜 변화에 따라 재실행된다.
→ 날짜가 바뀌면 ManualManager.SetDay(n) 호출 → UI 갱신 → 규칙 변경 반영


[스크립트별 핵심 메서드 연결 요약]

ManualManager	
SetDay(int)	날짜 설정 (외부 호출)
GetTodayManualEntries()	현재 날짜까지의 규칙 반환 (UI, 판단 시스템이 사용)

ManualDatabase
GetMergedManualEntriesUpToDay(int) 날짜별 규칙 병합 처리

ManualUIController
InitializeManualUI(List<ManualEntry>) 규칙 목록 UI 출력
ShowDetail(ManualEntry)	상세 내용 표시

JudgeSelectable
OnPointerClick()
InfoItem 생성 후 JudgeManager로 전달

JudgeManager
SelectItem(InfoItem) 비교 항목 등록
EvaluateContradiction()	두 항목 비교 후 결과 반환

날짜에 따라 규칙이 변화하고, 그 규칙이 UI에 반영되며, 유저는 이를 클릭하거나 비교 기준으로 활용하고,
판단 시스템은 logicKey를 기준으로 규칙 위반 여부를 판별하는 흐름으로 구성되어 있다.