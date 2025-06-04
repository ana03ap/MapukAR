using UnityEngine;
using TMPro;
using DG.Tweening;

public class AnimalDisplayController : MonoBehaviour
{
    [Header("Canvases de Sección")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject animalCanvas;
    [SerializeField] private CanvasGroup animalCanvasGroup;

    [Header("Datos de los animales")]
    [SerializeField] private AnimalData[] animals;

    [Header("Elementos UI dentro de AnimalCanvas")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject btnBack;

    [Header("Interacción con el modelo")]
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float zoomSpeed = 0.08f;
    [SerializeField] private float minZoom = 0.3f;
    [SerializeField] private float maxZoom = 1.0f;

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
        animalCanvas.SetActive(true);
        animalCanvasGroup.alpha = 0f;
        mainMenuCanvas.SetActive(false);

        yield return new WaitForEndOfFrame();

        var data = animals[index];
        titleText.text = data.animalName;
        descriptionText.text = data.description;

        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        Camera cam = Camera.main;
        Transform camTransform = Camera.main.transform;
        Vector3 spawnPosition = camTransform.position + camTransform.forward * 1.5f;
        Quaternion lookRotation = Quaternion.LookRotation(-camTransform.forward);

       

        currentModel = Instantiate(
            data.prefab,
            spawnPosition,
            lookRotation
        );

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

    void Update()
    {
        if (!animalCanvas.activeInHierarchy || currentModel == null) return;

        HandleTouchRotation();
        HandleMouseRotation();
        HandleKeyboardRotation();
      
    }

    private void HandleTouchRotation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                
                float rotationX = touch.deltaPosition.y * rotationSpeed;   
                float rotationY = -touch.deltaPosition.x * rotationSpeed; 
                currentModel.transform.Rotate(rotationX, rotationY, 0f, Space.World);
            }
        }
    }

    private void HandleMouseRotation()
    {
        if (Input.GetMouseButton(0))
        {
            float rotationAmount = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
            currentModel.transform.Rotate(0f, -rotationAmount, 0f, Space.World);
        }
    }

    private void HandleKeyboardRotation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        currentModel.transform.Rotate(0f, horizontal * 100f * Time.deltaTime, 0f, Space.World);
    }
}

