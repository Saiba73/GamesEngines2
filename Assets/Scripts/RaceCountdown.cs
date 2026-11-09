using System.Collections;
using UnityEngine;
using TMPro; // Remove this line if using legacy UnityEngine.UI.Text

public class RaceCountdown : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Root object for the countdown (will be hidden after GO!).")]
    [SerializeField] private GameObject countdownPanel;

    [Tooltip("The text that shows 3 / 2 / 1 / GO!.")]
    [SerializeField] private TMP_Text countdownText;

    [Header("Race")]
    [SerializeField] private RaceManager raceManager;

    [Header("Timing")]
    [Tooltip("Delay before '3' appears.")]
    [SerializeField] private float preDelay = 1f;

    [Tooltip("How long 'GO!' stays on screen.")]
    [SerializeField] private float goDisplayDuration = 1f;

    [Header("Audio (optional)")]
    [SerializeField] private AudioSource beepSource;
    
    [SerializeField] private AudioClip beepClip;
    [SerializeField] private AudioClip goClip;

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        // Freeze the cars (RaceManager.RaceStarted is false by default)
        if (countdownPanel != null) countdownPanel.SetActive(true);

        yield return new WaitForSeconds(preDelay);

        for (int i = 3; i >= 1; i--)
        {
            if (countdownText != null) countdownText.text = i.ToString();
            if (beepSource != null && beepClip != null) beepSource.PlayOneShot(beepClip);
            yield return new WaitForSeconds(1f);
        }

        // GO!
        if (countdownText != null) countdownText.text = "GO!";
        if (beepSource != null && goClip != null) beepSource.PlayOneShot(goClip);

        // Unlock the cars
        if (raceManager != null) raceManager.StartRace();

        yield return new WaitForSeconds(goDisplayDuration);

        if (countdownPanel != null) countdownPanel.SetActive(false);
    }
}