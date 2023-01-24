using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject magGlass;
    [SerializeField] private GameObject magCam;

    void Start()
    {
        magGlass.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            magGlass.SetActive(true);
            magGlass.transform.position = Input.mousePosition;
            magCam.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } else
        {
            magGlass.SetActive(false);
        }
    }
}
