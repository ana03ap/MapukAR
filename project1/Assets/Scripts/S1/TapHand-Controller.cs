using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TapToHideButton : MonoBehaviour
{
    [SerializeField] private GameObject buttonToHide;

    private void OnEnable(){
        if (buttonToHide != null){
            buttonToHide.SetActive(true);
        }
    }

    void Update(){
        if (Input.GetMouseButtonDown(0)){
            buttonToHide.SetActive(false);
        }
    }
}
