// 문서를 생성하고 30% 확률로 오류 데이터를 삽입하는 문서 데이터 조합 팩토리
// GameFlowManager에서 요청하면 생성하고 DocumentData 객체로 만든 후,
// DocumentGenerator는 이 객체를 받아 문서 UI 생성

using System;
using System.Collections.Generic;
using UnityEngine;

public static class DocumentFactory
{
    // 오류가 발생할 확률 (0.3 = 30%)
    private const float ErrorProbability = 0.3f;

    // 문서 종류 열거형
    public enum DocumentType { IDCard, BusinessPermit, Pass }

    // 메인 생성 메서드
    public static DocumentData CreateDocument(DocumentType type, PersonData baseInfo)
    {
        DocumentData doc = new DocumentData
        {
            documentType = type.ToString(),
            fullName = GetPossiblyIncorrect(() => baseInfo.fullName, GetRandomNameExcluding),
            birthDate = GetPossiblyIncorrect(() => baseInfo.birthDate, GetRandomBirthDateExcluding)
        };

        switch (type)
        {
            case DocumentType.IDCard:
                doc.gender = GetPossiblyIncorrect(() => baseInfo.gender, GetRandomGenderExcluding);
                doc.address = GetPossiblyIncorrect(() => baseInfo.address, GetRandomAddressExcluding);
                break;

            case DocumentType.BusinessPermit:
                doc.gender = GetPossiblyIncorrect(() => baseInfo.gender, GetRandomGenderExcluding);
                doc.businessType = GetPossiblyIncorrect(() => baseInfo.businessType, _ => GetRandomBusinessType());
                break;

            case DocumentType.Pass:
                doc.departure = GetPossiblyIncorrect(() => baseInfo.departure, GetRandomLocationExcluding);
                doc.destination = GetPossiblyIncorrect(() => baseInfo.destination, GetRandomLocationExcluding);
                break;
        }

        return doc;
    }

    // 30% 확률로 오류 삽입 여부 결정
    private static bool ShouldMakeError()
    {
        return UnityEngine.Random.value < ErrorProbability;
    }

    // 오류 삽입 시 대체 값을 가져오는 헬퍼 메서드
    private static string GetPossiblyIncorrect(Func<string> correctValueGetter, Func<string, string> errorGenerator)
    {
        string correct = correctValueGetter();
        return ShouldMakeError() ? errorGenerator(correct) : correct;
    }

    // 오류용 랜덤 데이터 생성기 (정확한 Pool은 나중에 정리 가능)

    private static string GetRandomNameExcluding(string correct)
    {
        List<string> names = new List<string> { "John", "Anna", "Chris", "Dana", "Paul" };
        names.Remove(correct);
        return names[UnityEngine.Random.Range(0, names.Count)];
    }

    private static string GetRandomBirthDateExcluding(string correct)
    {
        List<string> dates = new List<string> { "1990-01-01", "1985-12-12", "2000-06-20", "1978-03-15" };
        dates.Remove(correct);
        return dates[UnityEngine.Random.Range(0, dates.Count)];
    }

    private static string GetRandomGenderExcluding(string correct)
    {
        return correct == "M" ? "F" : "M";
    }

    private static string GetRandomAddressExcluding(string correct)
    {
        List<string> addresses = new List<string> { "Arstotzka", "Kolechia", "Impor", "United Fed" };
        addresses.Remove(correct);
        return addresses[UnityEngine.Random.Range(0, addresses.Count)];
    }

    private static string GetRandomBusinessType()
    {
        List<string> types = new List<string> { "Bakery", "Pharmacy", "Workshop", "Cafe" };
        return types[UnityEngine.Random.Range(0, types.Count)];
    }

    private static string GetRandomLocationExcluding(string correct)
    {
        List<string> locations = new List<string> { "Grestin", "East Arstotzka", "Outer Grouse", "Vescillo" };
        locations.Remove(correct);
        return locations[UnityEngine.Random.Range(0, locations.Count)];
    }
}