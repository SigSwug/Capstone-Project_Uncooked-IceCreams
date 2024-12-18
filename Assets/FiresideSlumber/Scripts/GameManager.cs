﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class GameManager : MonoBehaviour
{
    [Header("Fade Settings")]
    public Material fadeMaterial; // Material to use for fading
    public float fadeDuration = 1.0f;

    [Header("Game Settings")]
    public float gameDuration = 10.0f; // Default game duration in minutes

    [SerializeField, Tooltip("Time remaining in seconds, visible in the Inspector.")]
    private float timeRemaining; // Now serialized so we can view it in the Inspector

    private bool isFading = false;
    private bool gameStarted = false;

    public GameObject menuScene; // Reference to the Menu Scene GameObject
    public GameObject gameScene; // Reference to the Game Scene GameObject
    public GameObject menuToggleController; // Reference to the MenuToggleController GameObject
    public GameObject vrMenuControllerObject; // GameObject that contains VRMenuController script

    private VRMenuController vrMenuController; // Reference to the VRMenuController script

    [Header("Audio")]
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip buttonClickSound; // Clip to play when buttons are clicked

    private void Start()
    {
        // Ensure menuScene is active at the beginning
        menuScene.SetActive(true);
        gameScene.SetActive(false); // Deactivate gameScene at the start

        // Ensure MenuToggleController is inactive initially
        if (menuToggleController != null)
        {
            menuToggleController.SetActive(false);
        }

        // Get the VRMenuController component if the object is assigned
        if (vrMenuControllerObject != null)
        {
            vrMenuController = vrMenuControllerObject.GetComponent<VRMenuController>();
            vrMenuController.enabled = false; // Disable VRMenuController at the start
        }
    }

    private void Update()
    {
        if (gameStarted)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                StartCoroutine(FadeAndReturnToMenu());
                gameStarted = false;
            }
        }
    }

    // Call this when a UI button is pressed to set the time and start the game
    public void SetGameDurationAndStart(float durationInMinutes)
    {
        gameDuration = durationInMinutes;
        timeRemaining = gameDuration * 60; // Convert minutes to seconds
        StartCoroutine(FadeAndStartGame());
    }

    // Coroutine that waits for fade-out, switches to the game, and then fades in
    private IEnumerator FadeAndStartGame()
    {
        yield return StartCoroutine(FadeOut()); // Wait for fade-out to complete
        menuScene.SetActive(false); // Deactivate the menuScene after fade-out
        gameScene.SetActive(true);  // Activate the gameScene
        gameStarted = true;         // Start the game
        yield return StartCoroutine(FadeIn());  // Wait for fade-in to complete

        // Wait 0.5 second before activating the MenuToggleController and VRMenuController
        yield return new WaitForSeconds(0.5f);
        if (menuToggleController != null)
        {
            menuToggleController.SetActive(true);
        }

        if (vrMenuController != null)
        {
            vrMenuController.enabled = true; // Enable the VRMenuController script
        }
    }

    // Coroutine that waits for fade-out, returns to the menu, and then fades in
    private IEnumerator FadeAndReturnToMenu()
    {
        yield return StartCoroutine(FadeOut()); // Wait for fade-out to complete
        gameScene.SetActive(false); // Deactivate the gameScene after fade-out
        menuScene.SetActive(true);  // Activate the menuScene
        yield return StartCoroutine(FadeIn());  // Wait for fade-in to complete
    }

    // Fade out function
    private IEnumerator FadeOut()
    {
        if (fadeMaterial != null && !isFading)
        {
            isFading = true;
            float timer = 0f;
            Color color = fadeMaterial.color;
            color.a = 0f; // Ensure starting alpha is 0

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration); // Fade alpha from 0 to 1
                fadeMaterial.color = color;
                yield return null;
            }

            color.a = 1f; // Ensure alpha is fully opaque at the end
            fadeMaterial.color = color;
            isFading = false;
        }
    }

    // Fade in function
    private IEnumerator FadeIn()
    {
        if (fadeMaterial != null && !isFading)
        {
            isFading = true;
            float timer = 0f;
            Color color = fadeMaterial.color;
            color.a = 1f; // Ensure starting alpha is 1

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // Fade alpha from 1 to 0
                fadeMaterial.color = color;
                yield return null;
            }

            color.a = 0f; // Ensure alpha is fully transparent at the end
            fadeMaterial.color = color;
            isFading = false;
        }
    }

    // Call this function to quit the game and return to the menu
    public void EndGameAndReturnToMenu()
    {
        var fader = ScreenFader.Instance;
        fader.FadeToBlack();
        fader.WaitUntilFadeComplete();
        ExperienceApp.End();
        StartCoroutine(FadeAndReturnToMenu());
    }

    // Helper function to play button click sound
    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    // Button click handlers for 10 minutes, 20 minutes, and 30 minutes
    public void OnTenMinuteButtonClick()
    {
        PlayButtonClickSound();
        SetGameDurationAndStart(10);
    }

    public void OnTwentyMinuteButtonClick()
    {
        PlayButtonClickSound();
        SetGameDurationAndStart(20);
    }

    public void OnThirtyMinuteButtonClick()
    {
        PlayButtonClickSound();
        SetGameDurationAndStart(30);
    }
}
