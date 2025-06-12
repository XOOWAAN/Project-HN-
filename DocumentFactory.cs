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
        DocumentData doc = new DocumentData();
        doc.documentType = type.ToString();

        // 모든 문서에 공통적으로 들어가는 필드 (Ex: 이름 등)
        doc.fullName = ShouldMakeError() ? GetRandomNameExcluding(baseInfo.fullName) : baseInfo.fullName;
        doc.birthDate = ShouldMakeError() ? GetRandomBirthDateExcluding(baseInfo.birthDate) : baseInfo.birthDate;

        switch (type)
        {
            case DocumentType.IDCard:
                doc.gender = ShouldMakeError() ? GetRandomGenderExcluding(baseInfo.gender) : baseInfo.gender;
                doc.address = ShouldMakeError() ? GetRandomAddressExcluding(baseInfo.address) : baseInfo.address;
                break;

            case DocumentType.BusinessPermit:
                doc.gender = ShouldMakeError() ? GetRandomGenderExcluding(baseInfo.gender) : baseInfo.gender;
                doc.businessType = ShouldMakeError() ? GetRandomBusinessType() : baseInfo.businessType;
                break;

            case DocumentType.Pass:
                doc.departure = ShouldMakeError() ? GetRandomLocationExcluding(baseInfo.departure) : baseInfo.departure;
                doc.destination = ShouldMakeError() ? GetRandomLocationExcluding(baseInfo.destination) : baseInfo.destination;
                break;
        }

        return doc;
    }

    // 30% 확률로 true 반환 (오류 발생 여부)
    private static bool ShouldMakeError()
    {
        return UnityEngine.Random.value < ErrorProbability;
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