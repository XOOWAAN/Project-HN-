// 문서를 생성하고 30% 확률로 오류 데이터를 삽입하는 문서 데이터 조합 팩토리
// GameFlowManager에서 요청하면 생성하고 DocumentData 객체로 만든 후,
// DocumentGenerator는 이 객체를 받아 문서 UI 생성

using System;
using System.Collections.Generic;
using UnityEngine;

public static class DocumentFactory
{
    private const float ErrorProbability = 0.3f;

    public static DocumentData CreateDocument(DocumentType type, PersonData baseInfo)
    {
        DocumentData doc = new DocumentData
        {
            documentType = type, // enum 그대로 저장
            fullName = GetPossiblyIncorrect(() => baseInfo.fullName, GetRandomNameExcluding),
            dateOfBirth = GetPossiblyIncorrect(() => baseInfo.dateOfBirth, GetRandomDateOfBirthExcluding),
            nationality = baseInfo.address // 예시: 국적은 주소에서 파생
        };

        switch (type)
        {
            case DocumentType.IDCard:
                doc.gender = GetPossiblyIncorrect(() => baseInfo.gender, GetRandomGenderExcluding);
                doc.address = GetPossiblyIncorrect(() => baseInfo.address, GetRandomAddressExcluding);
                break;

            case DocumentType.BusinessPermit:
                doc.gender = GetPossiblyIncorrect(() => baseInfo.gender, GetRandomGenderExcluding);
                doc.businessType = GetPossiblyIncorrect(() => baseInfo.businessType, GetRandomBusinessTypeExcluding);
                break;

            case DocumentType.Pass:
                doc.departure = GetPossiblyIncorrect(() => baseInfo.departure, GetRandomLocationExcluding);
                doc.destination = GetPossiblyIncorrect(() => baseInfo.destination, GetRandomLocationExcluding);
                break;
        }

        return doc;
    }

    private static bool ShouldMakeError()
    {
        return UnityEngine.Random.value < ErrorProbability;
    }

    private static string GetPossiblyIncorrect(Func<string> correctValueGetter, Func<string, string> errorGenerator)
    {
        string correct = correctValueGetter();
        return ShouldMakeError() ? errorGenerator(correct) : correct;
    }

    private static string GetRandomNameExcluding(string correct)
    {
        List<string> names = new List<string> { "John", "Anna", "Chris", "Dana", "Paul" };
        names.Remove(correct);
        return names[UnityEngine.Random.Range(0, names.Count)];
    }

    private static string GetRandomDateOfBirthExcluding(string correct)
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

    private static string GetRandomBusinessTypeExcluding(string correct)
    {
        List<string> types = new List<string> { "Bakery", "Pharmacy", "Workshop", "Cafe" };
        types.Remove(correct);
        return types[UnityEngine.Random.Range(0, types.Count)];
    }

    private static string GetRandomLocationExcluding(string correct)
    {
        List<string> locations = new List<string> { "Grestin", "East Arstotzka", "Outer Grouse", "Vescillo" };
        locations.Remove(correct);
        return locations[UnityEngine.Random.Range(0, locations.Count)];
    }
}