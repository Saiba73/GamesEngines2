using TMPro;
using UnityEngine;

public class RacePositionUI : MonoBehaviour
{
    [SerializeField] private RaceManager raceManager;
    [SerializeField] private int racerIndex;
    [SerializeField] private TMP_Text label;

    private void Update()
    {
        int position = raceManager.GetPosition(racerIndex);
        if (position <= 0) return;

        string text = Ordinal(position);

        if (raceManager.IsFinished(racerIndex))
            text += "\nFinished!";
        else
            text += $"\nVuelta {raceManager.GetLap(racerIndex)}/{raceManager.TotalLaps}";

        label.text = text;
    }

    private static string Ordinal(int n)
    {
        switch (n)
        {
            case 1: return "1st";
            case 2: return "2nd";
            case 3: return "3rd";
            default: return n + "th";
        }
    }
}