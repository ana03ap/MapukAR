using UnityEngine;

public class S1UIManager : MonoBehaviour
{
    public GameObject mainPPL;
    public GameObject mainMenu;

    public void GoToMainMenu()
    {
        mainPPL.SetActive(false);
        mainMenu.SetActive(true);
    }
}
