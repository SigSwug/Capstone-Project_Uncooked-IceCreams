using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    // Reference to the Image component to change the alpha
    public Image targetImage;

    // Reference to the Toggle component
    private Toggle toggle;

    // Reference to the AudioSource component for playing the sound
    public AudioSource audioSource;

    // Reference to the AudioClip for the button click sound
    public AudioClip buttonClickSound;

    void Start()
    {
        // Get the Toggle component attached to the same GameObject
        toggle = GetComponent<Toggle>();

        // Subscribe to the onValueChanged event
        toggle.onValueChanged.AddListener(OnToggleChanged);

        // Initialize the Image's alpha based on the Toggle's starting value
        SetImageAlpha(toggle.isOn);
    }

    // This method is called whenever the Toggle is switched on or off
    void OnToggleChanged(bool isOn)
    {
        // Set the Image's alpha depending on the toggle state
        SetImageAlpha(isOn);

        // Play the button click sound
        PlayButtonClickSound();
    }

    // Helper method to set the alpha value of the Image
    void SetImageAlpha(bool isOn)
    {
        Color color = targetImage.color;
        color.a = isOn ? 1f : 0f;  // Set alpha to 1 when on, 0 when off
        targetImage.color = color;
    }

    // Helper method to play the button click sound
    void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    // Optionally, you can unsubscribe from the event when this script is destroyed
    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}