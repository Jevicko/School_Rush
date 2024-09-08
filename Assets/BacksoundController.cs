using UnityEngine;
using UnityEngine.UI;

public class BacksoundController : MonoBehaviour
{
    public AudioSource audioSource;  // Drag Audio Source object here
    public Slider volumeSlider;      // Drag Slider object here

    void Start()
    {
        // Set the slider's value to match the current audio source volume
        volumeSlider.value = audioSource.volume;

        // Add listener to call the method when slider value changes
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void ChangeVolume(float value)
    {
        // Set the audio source volume to the slider value
        audioSource.volume = value;
    }
}
