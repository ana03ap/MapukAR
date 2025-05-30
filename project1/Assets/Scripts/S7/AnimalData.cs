using UnityEngine;

[CreateAssetMenu(menuName = "Museum/Animal Data")]
public class AnimalData : ScriptableObject
{
    public string animalName;           
    [TextArea] public string description; 
    public GameObject prefab;           
}

