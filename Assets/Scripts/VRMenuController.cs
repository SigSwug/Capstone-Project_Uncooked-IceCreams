﻿using UnityEngine;

public class VRMenuController : MonoBehaviour
{
    public Canvas menuCanvas;  // Reference to the Canvas

    void Start()
    {
        // Ensure the canvas is inactive initially
        menuCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        // Always activate the menu on input
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))  // For Oculus
        {
            ActivateMenu();
        }

        if (Input.GetMouseButtonDown(0))  // 0 is the left mouse button
        {
            ActivateMenu();
        }
    }

    void ActivateMenu()
    {
        // Always set the canvas as active
        menuCanvas.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        // Method to close the menu from within the menu
        menuCanvas.gameObject.SetActive(false);
    }
}
