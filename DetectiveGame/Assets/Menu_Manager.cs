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
    public GameObject CreditsMenu2;
    public bool moving = true;
    public Transform CameraWide;
    public Camera MainCam;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        MainMenu.SetActive(true);
        SettingsMenu.SetActive(true);
        CreditsMenu.SetActive(true);
        CreditsMenu2.SetActive(true);

        // TransitionLook(CameraWide);
    }

    public void StartGame()
    {       
        StartCoroutine(TransitionLook(CameraWide));

    }

    public void QuitGame()
    {
        // debug statement for in-editor functionality checking
        Debug.Log("Quitting Game");
        Application.Quit();
    }


    IEnumerator TransitionLook(Transform lookto)
    {
        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < 6; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        moving = true;
        float time = 0;
        float lookSpeed = 0.75f;

        Vector3 startPos = MainCam.transform.position;
        Quaternion startRot = MainCam.transform.rotation;


        yield return new WaitForSeconds(0.2f);
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
