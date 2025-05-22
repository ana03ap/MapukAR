


//using UnityEngine;
//using TMPro;
//using DG.Tweening;

//public class AnimalDisplayController : MonoBehaviour
//{
//    [Header("Canvases de Sección")]
//    [SerializeField] private GameObject mainMenuCanvas;
//    [SerializeField] private GameObject animalCanvas;
//    [SerializeField] private CanvasGroup animalCanvasGroup;

//    [Header("Datos de los animales")]
//    [SerializeField] private AnimalData[] animals;

//    [Header("Elementos UI dentro de AnimalCanvas")]
//    [SerializeField] private TMP_Text titleText;
//    [SerializeField] private TMP_Text descriptionText;
//    [SerializeField] private GameObject btnBack;

//    private GameObject currentModel;

//    void Start()
//    {
//        mainMenuCanvas.SetActive(true);
//        animalCanvas.SetActive(false);
//        animalCanvasGroup.alpha = 0f;

//        btnBack.GetComponent<UnityEngine.UI.Button>()
//               .onClick.AddListener(HideAnimal);
//    }

//    public void ShowAnimal(int index)
//    {
//        if (index < 0 || index >= animals.Length) return;
//        StartCoroutine(ShowAnimalRoutine(index));
//    }

//    private System.Collections.IEnumerator ShowAnimalRoutine(int index)
//    {
//        // Activar canvas y ocultar menú
//        animalCanvas.SetActive(true);
//        animalCanvasGroup.alpha = 0f;
//        mainMenuCanvas.SetActive(false);

//        // Esperar a que el canvas se inicialice correctamente
//        yield return new WaitForEndOfFrame();

//        // Obtener datos del animal
//        var data = animals[index];
//        titleText.text = data.animalName;
//        descriptionText.text = data.description;

//        // Eliminar modelo anterior si existe
//        if (currentModel != null)
//        {
//            Destroy(currentModel);
//        }

//        // Instanciar modelo justo al frente de la cámara
//        Camera cam = Camera.main;
//        Vector3 spawnPosition = cam.transform.position + cam.transform.forward * 1.5f;
//        Quaternion lookRotation = Quaternion.LookRotation(-cam.transform.forward); // que mire hacia el usuario

//        currentModel = Instantiate(
//            data.prefab,
//            spawnPosition,
//            lookRotation
//        );

//        // Animar aparición del canvas
//        animalCanvasGroup.DOFade(1f, 0.5f);
//    }

//    public void HideAnimal()
//    {
//        animalCanvasGroup.alpha = 0f;
//        animalCanvas.SetActive(false);

//        if (currentModel != null)
//        {
//            Destroy(currentModel);
//            currentModel = null;
//        }

//        mainMenuCanvas.SetActive(true);
//    }
//}



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
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float minZoom = 0.3f;
    [SerializeField] private float maxZoom = 2.5f;

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
        HandleZoom();
    }

    private void HandleTouchRotation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotationAmount = touch.deltaPosition.x * rotationSpeed;
                currentModel.transform.Rotate(0f, -rotationAmount, 0f, Space.World);
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

    private void HandleZoom()
    {
        Vector3 direction = currentModel.transform.forward;

        // Zoom con dos dedos
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 prevT0 = t0.position - t0.deltaPosition;
            Vector2 prevT1 = t1.position - t1.deltaPosition;

            float prevDist = (prevT0 - prevT1).magnitude;
            float currentDist = (t0.position - t1.position).magnitude;

            float delta = (currentDist - prevDist) * zoomSpeed * Time.deltaTime;
            ApplyZoom(delta);
        }

        // Zoom con scroll en PC
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            ApplyZoom(scroll * zoomSpeed);
        }
    }

    private void ApplyZoom(float amount)
    {
        Vector3 direction = currentModel.transform.forward;
        Vector3 newPos = currentModel.transform.position + direction * amount;

        float distance = Vector3.Distance(Camera.main.transform.position, newPos);
        if (distance >= minZoom && distance <= maxZoom)
        {
            currentModel.transform.position = newPos;
        }
    }
}

