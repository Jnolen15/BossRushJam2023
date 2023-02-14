using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    // public
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;
    public Transform CameraTable;
    public Transform CameraNormal;

    // private
    private GameObject CurrentMenu;
    private GameObject NextMenu;

    void Start()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(true);
        CurrentMenu = MainMenu;

        Camera.main.transform.position = CameraTable.position;
    }

    public void startGame()
    {
        this.gameObject.SetActive(false);
        // code here to animate looking up from desk

        Camera.main.transform.position = CameraNormal.position;
        Camera.main.transform.rotation = CameraNormal.transform.rotation;

        // load next scene when done
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // debug statement for in-editor functionality checking
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    IEnumerator CameraTransition()
    {
        var speed = 1f;
        while (Camera.main.transform.position != CameraNormal.position)
        {
            Camera.main.transform.position = Vector3.Lerp(CameraTable.position, CameraNormal.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        } 
        // load next scene when done
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
