using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject CreditsMenu;
    public bool moving = true;
    public Transform CameraWide;
    public Camera MainCam;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(true);

        TransitionLook(CameraWide);
    }

    public void startGame()
    {
        Debug.Log("Camera Transition");
        // this.gameObject.SetActive(false);
       
        TransitionLook(CameraWide);

    }

    public void QuitGame()
    {
        // debug statement for in-editor functionality checking
        Debug.Log("Quitting Game");
        Application.Quit();
    }


    IEnumerator TransitionLook(Transform lookto)
    {
        moving = true;
        float time = 0;
        float lookSpeed = 0.75f;

        Vector3 startPos = MainCam.transform.position;
        Quaternion startRot = MainCam.transform.rotation;

        while (time < lookSpeed)
        {
            float t = time / lookSpeed;
            t = t * t * (3f - 2f * t);

            MainCam.transform.position = Vector3.Lerp(startPos, lookto.position, t);
            MainCam.transform.rotation = Quaternion.Lerp(startRot, lookto.rotation, t);

            time += Time.deltaTime;
            yield return null;
        }

        moving = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
