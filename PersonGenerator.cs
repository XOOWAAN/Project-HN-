// PersonGenerator.cs
// ----------------------------
// 게임에 등장할 인물 정보를 생성하고, 해당 데이터를 바탕으로 캐릭터 오브젝트를 생성함
// 종족, 성별, 연령대, 사진 규칙에 따라 무작위 인물 데이터를 구성
// 문서 생성 이전에 인물 정보를 먼저 생성하기 위한 핵심 스크립트

// 주요 메서드:
// - GeneratePersonData(): 인물 정보를 무작위로 생성해 PersonData로 반환 (UI, 오브젝트 없음)
// - SpawnCharacter(PersonData): 인물 정보를 바탕으로 GameObject를 화면에 생성
// - GetPhotoByRules(): 종족, 성별, 나이에 따라 적절한 얼굴 사진 선택
// - GenerateBirthDate(age): 입력된 나이로부터 생년월일 문자열 생성
// - GetRandomRace(): 오크, 인간, 엘프 중 무작위 종족 선택


using UnityEngine;
using System.Collections.Generic;

public class PersonGenerator : MonoBehaviour
{
    public GameObject personPrefab;
    public Transform personParent;

    public List<string> randomNames;
    public List<string> nationalities;

    public Dictionary<string, List<Sprite>> raceImageMap; // 종족별 이미지
    public List<Sprite> orcImages;
    public List<Sprite> humanImages;
    public List<Sprite> elfImages;

    // 💡 인물 정보만 생성 (GameObject 없이)
    public PersonData GeneratePersonData()
    {
        string race = GetRandomRace();
        string gender = Random.value < 0.5f ? "남" : "여";
        int age = Random.Range(20, 81);
        string birthDate = GenerateBirthDate(age);
        string name = randomNames[Random.Range(0, randomNames.Count)];
        string nationality = nationalities[Random.Range(0, nationalities.Count)];
        Sprite photo = GetPhotoByRules(race, gender, age);

        return new PersonData {
            fullName = name,
            gender = gender,
            nationality = nationality,
            birthDate = birthDate,
            race = race,
            photo = photo
        };
    }

    // 💡 생성된 인물 정보를 바탕으로 오브젝트 생성
    public GameObject SpawnCharacter(PersonData data, Transform parent = null)
    {
        Transform spawnParent = parent != null ? parent : personParent;
        GameObject personObj = Instantiate(personPrefab, spawnParent);
        var display = personObj.GetComponent<PersonDisplay>();
        display.InitializePerson(data);
        display.AnimateEnter();
        return personObj;
    }

    private string GetRandomRace()
    {
        string[] races = { "오크", "인간", "엘프" };
        return races[Random.Range(0, races.Length)];
    }

    private string GenerateBirthDate(int age)
    {
        int currentYear = 2025;
        int birthYear = currentYear - age;
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 29);
        return $"{birthYear}.{month:00}.{day:00}";
    }

    private Sprite GetPhotoByRules(string race, string gender, int age)
    {
        List<Sprite> candidateImages = new List<Sprite>();

        switch (race)
        {
            case "오크": candidateImages = orcImages; break;
            case "인간": candidateImages = humanImages; break;
            case "엘프": candidateImages = elfImages; break;
        }

        // 연령 조건 필터
        if (age >= 20 && age <= 40)
            candidateImages = candidateImages.FindAll(s => s.name == "image1" || s.name == "image3" || s.name == "image5" || s.name == "image8");
        else if (age <= 60)
            candidateImages = candidateImages.FindAll(s => s.name == "image2" || s.name == "image4" || s.name == "image7" || s.name == "image11");
        else
            candidateImages = candidateImages.FindAll(s => s.name == "image6" || s.name == "image9" || s.name == "image10");

        // 성별 조건 필터
        if (gender == "남")
            candidateImages = candidateImages.FindAll(s => s.name == "image1" || s.name == "image3" || s.name == "image5" || s.name == "image7" || s.name == "image9" || s.name == "image11");
        else
            candidateImages = candidateImages.FindAll(s => s.name == "image2" || s.name == "image4" || s.name == "image6" || s.name == "image8" || s.name == "image10");

        return candidateImages[Random.Range(0, candidateImages.Count)];
    }
}