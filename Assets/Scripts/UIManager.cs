using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text[] prompts;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public float[] displayDurations;
    private Animator[] animators;

    private void Start()
    {
        animators = new Animator[prompts.Length];
        for (int i = 0; i < prompts.Length; i++)
        {
            animators[i] = prompts[i].GetComponent<Animator>();
        }
        StartCoroutine(DisplayPrompts());
    }

    private IEnumerator DisplayPrompts()
    {
        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].gameObject.SetActive(true); // Show the text prompt
            animators[i].SetTrigger("FadeIn"); // Trigger the fade-in animation
            audioSource.clip = audioClips[i];
            audioSource.Play();

            // Wait for the duration of the display plus the audio clip length
            yield return new WaitForSeconds(displayDurations[i] + audioClips[i].length);

            animators[i].SetTrigger("FadeOut"); // Trigger the fade-out animation

            // Wait for the fade-out animation to complete
            yield return new WaitForSeconds(1f); // Adjust this duration to match the fade-out animation length

            prompts[i].gameObject.SetActive(false); // Hide the text prompt
        }
    }
}
