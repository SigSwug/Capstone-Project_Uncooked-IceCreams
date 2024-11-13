using UnityEngine;

public class VRMenuController : MonoBehaviour
{
    public Canvas menuCanvas;           // Reference to the Canvas
    public GameObject leftHandModel;    // Reference to the Left Hand Model
    public GameObject rightHandModel;   // Reference to the Right Hand Model

    void Start()
    {
        // Ensure the canvas and hand models are inactive initially
        menuCanvas.gameObject.SetActive(false);
        if (leftHandModel != null) leftHandModel.SetActive(false);
        if (rightHandModel != null) rightHandModel.SetActive(false);
    }

    void Update()
    {
        // Check for primary index trigger to activate the left hand model
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))  // For Oculus
        {
            ActivateMenu(leftHandModel);
        }
        // Check for secondary index trigger to activate the right hand model
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))  // For Oculus
        {
            ActivateMenu(rightHandModel);
        }

        if (Input.GetKeyDown(KeyCode.M))  // M key on the keyboard (activates both models for testing)
        {
            ActivateMenu(leftHandModel, rightHandModel);
        }
    }

    void ActivateMenu(GameObject handModelToActivate = null, GameObject optionalHandModel = null)
    {
        // Deactivate both hand models first to ensure only specified ones activate
        if (leftHandModel != null) leftHandModel.SetActive(false);
        if (rightHandModel != null) rightHandModel.SetActive(false);

        // Activate the menu and only the specified hand model(s)
        menuCanvas.gameObject.SetActive(true);

        // Activate only the relevant hand models
        if (handModelToActivate != null) handModelToActivate.SetActive(true);
        if (optionalHandModel != null) optionalHandModel.SetActive(true);
    }

    public void CloseMenu()
    {
        // Deactivate the menu and both hand models
        menuCanvas.gameObject.SetActive(false);
        if (leftHandModel != null) leftHandModel.SetActive(false);
        if (rightHandModel != null) rightHandModel.SetActive(false);
    }
}
