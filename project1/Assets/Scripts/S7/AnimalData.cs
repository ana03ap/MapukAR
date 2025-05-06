using UnityEngine;

[CreateAssetMenu(menuName = "Museum/Animal Data")]
public class AnimalData : ScriptableObject
{
    public string animalName;           // “Ibis”
    [TextArea] public string description; // Texto sobre el ibis
    public GameObject prefab;           // El prefab 3D del ibis
}

