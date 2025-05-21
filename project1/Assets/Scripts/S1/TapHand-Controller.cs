using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapToHideButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonToHide;

    private void OnEnable()
    {

        if (buttonToHide != null)
        {
            buttonToHide.SetActive(true);
        }
    }

    void Update()
    {
        // Si se hace tap/clic
        if (Input.GetMouseButtonDown(0))
        {
            // Si el tap NO fue sobre otro elemento UI
            buttonToHide.SetActive(false);

        }
    }
}
