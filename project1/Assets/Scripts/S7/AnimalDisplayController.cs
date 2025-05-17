//using UnityEngine;
//using TMPro;

//public class AnimalDisplayController : MonoBehaviour
//{
//    [Header("Canvases de Secci�n")]
//    [SerializeField] private GameObject mainMenuCanvas;  // tu MainMenu2
//    [SerializeField] private GameObject animalCanvas;    // tu AnimalCanvas gen�rico

//    [Header("Datos de los animales")]
//    [SerializeField] private AnimalData[] animals;       // array de ScriptableObjects

//    [Header("Elementos UI dentro de AnimalCanvas")]
//    [SerializeField] private TMP_Text titleText;
//    [SerializeField] private TMP_Text descriptionText;
//    [SerializeField] private GameObject btnBack;         // el bot�n �Atr�s�

//    [Header("Contenedor AR para instanciar modelos")]
//    [SerializeField] private Transform spawnContainer;

//    private GameObject currentModel;  // referencia al modelo instanciado

//    void Start()
//    {
//        // Al inicio mostramos solo el men�
//        mainMenuCanvas.SetActive(true);
//        animalCanvas.SetActive(false);

//        // Configuramos el bot�n Atr�s
//        btnBack.GetComponent<UnityEngine.UI.Button>()
//               .onClick.AddListener(HideAnimal);
//    }

//    /// <summary>
//    /// Llamar desde cada bot�n del MainMenu2, pasando el �ndice del animal
//    /// </summary>
//    public void ShowAnimal(int index)
//    {
//        if (index < 0 || index >= animals.Length) return;

//        // 1) Oculta el men�
//        mainMenuCanvas.SetActive(false);

//        // 2) Actualiza UI (texto)
//        var data = animals[index];
//        titleText.text = data.animalName;
//        descriptionText.text = data.description;

//        // 3) Instancia el prefab 3D
//        if (currentModel != null)
//            Destroy(currentModel);

//        currentModel = Instantiate(
//            data.prefab,
//            spawnContainer.position,
//            Quaternion.identity,
//            spawnContainer
//        );

//        // 4) Muestra el canvas de animal
//        animalCanvas.SetActive(true);
//    }

//    /// <summary>
//    /// Llamar desde el bot�n �Atr�s� para volver al MainMenu2
//    /// </summary>
//    public void HideAnimal()
//    {
//        // 1) Oculta la vista de animal
//        animalCanvas.SetActive(false);

//        // 2) Destruye el modelo instanciado
//        if (currentModel != null)
//        {
//            Destroy(currentModel);
//            currentModel = null;
//        }

//        // 3) Vuelve a mostrar el men�
//        mainMenuCanvas.SetActive(true);
//    }
//}



using UnityEngine;
using TMPro;
using DG.Tweening;

public class AnimalDisplayController : MonoBehaviour
{
    [Header("Canvases de Secci�n")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject animalCanvas;
    [SerializeField] private CanvasGroup animalCanvasGroup;

    [Header("Datos de los animales")]
    [SerializeField] private AnimalData[] animals;

    [Header("Elementos UI dentro de AnimalCanvas")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject btnBack;

    private GameObject currentModel;

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        animalCanvas.SetActive(false);
        animalCanvasGroup.alpha = 0f;

        btnBack.GetComponent<UnityEngine.UI.Button>()
               .onClick.AddListener(HideAnimal);
    }

    public void ShowAnimal(int index)
    {
        if (index < 0 || index >= animals.Length) return;
        StartCoroutine(ShowAnimalRoutine(index));
    }

    private System.Collections.IEnumerator ShowAnimalRoutine(int index)
    {
        // Activar canvas y ocultar men�
        animalCanvas.SetActive(true);
        animalCanvasGroup.alpha = 0f;
        mainMenuCanvas.SetActive(false);

        // Esperar a que el canvas se inicialice correctamente
        yield return new WaitForEndOfFrame();

        // Obtener datos del animal
        var data = animals[index];
        titleText.text = data.animalName;
        descriptionText.text = data.description;

        // Eliminar modelo anterior si existe
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // Instanciar modelo justo al frente de la c�mara
        Camera cam = Camera.main;
        Vector3 spawnPosition = cam.transform.position + cam.transform.forward * 1.5f;
        Quaternion lookRotation = Quaternion.LookRotation(-cam.transform.forward); // que mire hacia el usuario

        currentModel = Instantiate(
            data.prefab,
            spawnPosition,
            lookRotation
        );

        // Animar aparici�n del canvas
        animalCanvasGroup.DOFade(1f, 0.5f);
    }

    public void HideAnimal()
    {
        animalCanvasGroup.alpha = 0f;
        animalCanvas.SetActive(false);

        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

        mainMenuCanvas.SetActive(true);
    }
}
