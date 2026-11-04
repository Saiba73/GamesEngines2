using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceEndController : MonoBehaviour
{
    [SerializeField] private RaceManager raceManager;

    private void OnEnable()
    {
        // Subscribe to the event when a racer finishes
        raceManager.RacerFinished += HandleRacerFinished;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        raceManager.RacerFinished -= HandleRacerFinished;
    }

    private void HandleRacerFinished(RaceManager.Racer winner)
    {
        // Check if this is the FIRST racer to cross the finish line
        if (winner.finishOrder == 1)
        {
            Debug.Log($"{winner.displayName} won the race!");

            // Save the winner's display name to pass to the next scene
            PlayerPrefs.SetString("Winner", winner.displayName);
            PlayerPrefs.Save();

            // Load your results scene
            SceneManager.LoadScene("WinScene");
        }
    }
}