using UnityEngine;
using System.Collections.Generic;

public class PersonGenerator : MonoBehaviour
{
    public GameObject personPrefab;
    public Transform personParent;

    public List<string> randomNames;
    public List<string> nationalities;
    public List<Sprite> faceSprites;
    public List<Sprite> bodySprites;

    public PersonData GenerateRandomPerson()
    {
        PersonData data = new PersonData {
            fullName = randomNames[Random.Range(0, randomNames.Count)],
            nationality = nationalities[Random.Range(0, nationalities.Count)],
            birthDate = RandomDate(),
            face = faceSprites[Random.Range(0, faceSprites.Count)],
            body = bodySprites[Random.Range(0, bodySprites.Count)]
        };

        GameObject personObj = Instantiate(personPrefab, personParent);
        var display = personObj.GetComponent<PersonDisplay>();
        display.InitializePerson(data);
        display.AnimateEnter();

        return data;
    }

    private string RandomDate()
    {
        int year = Random.Range(1960, 2005);
        int month = Random.Range(1, 13);
        int day = Random.Range(1, 28);
        return $"{year}.{month:00}.{day:00}";
    }
}