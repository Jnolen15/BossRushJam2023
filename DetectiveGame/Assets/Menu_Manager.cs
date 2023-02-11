using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public GameObject AllMenus;
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;

    private GameObject CurrentMenu;
    private GameObject NextMenu;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(true);
        CurrentMenu = MainMenu;
    }

    public void startGame()
    {
        AllMenus.SetActive(false);
        // code here to animate looking up from desk
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // debug statement for in-editor functionality checking
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
