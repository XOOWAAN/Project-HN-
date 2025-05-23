using UnityEngine;
using System.Collections.Generic;

public class PersonGenerator : MonoBehaviour
{
    [Header("인물 UI 프리팹")]
    public GameObject personPrefab;             // 인물 UI 프리팹

    [Header("생성 위치")]
    public Transform personParent;              // 생성될 위치

    [Header("데이터 소스")]
    public List<string> randomNames;
    public List<string> nationalities;
    public List<Sprite> photos;

    public void GenerateRandomPerson()
    {
        GameObject personObj = Instantiate(personPrefab, personParent);
        PersonDisplay display = personObj.GetComponent<PersonDisplay>();

        PersonData data = new PersonData {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            dateOfBirth = RandomDate(),
            photo = photos[Random.Range(0, photos.Count)]
        };

        display.InitializePerson(data);
        display.AnimateEnter(); // 필요시 진입 애니메이션
    }

    private string RandomDate()
    {
        int year = Random.Range(1960, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        return $"{year}.{month:00}.{day:00}";
    }
}