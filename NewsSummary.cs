using UnityEngine;
using System.Collections.Generic;

// 각 날짜에 대한 뉴스 요약 데이터를 관리하는 스크립트
public class NewsSummary : MonoBehaviour
{   
    // 일자별 뉴스 요약 저장
    private Dictionary<int, string> newsByDay = new Dictionary<int, string>() 
    {
        { 1, "관문 인근에서 폭발 사건 발생!" },
        { 2, "심사 기준 강화 예정." },
        { 3, "국경 통과자 수 증가 예상." },
        { 4, "불법 입국 시도 증가 주의." }
    };

    // 일자에 해당하는 뉴스 반환
    public string GetNewsForDay(int day)
    {
        if (newsByDay.ContainsKey(day))
            return newsByDay[day];
        else
            return "오늘은 특별한 뉴스가 없습니다.";
    }
}