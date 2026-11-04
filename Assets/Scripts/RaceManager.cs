using System;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [Serializable]
    public class Racer
    {
        public string displayName = "Player";
        [Tooltip("The car object that has the Rigidbody.")]
        public Rigidbody body;

        [NonSerialized] public int nextCheckpoint;
        [NonSerialized] public int lap;
        [NonSerialized] public int checkpointsPassed;
        [NonSerialized] public int finishOrder;
        [NonSerialized] public int position;
    }

    [Header("Race Setup")]
    [Tooltip("In driving order. Element 0 is the start/finish line.")]
    [SerializeField] private Checkpoint[] checkpoints;
    [SerializeField] private int totalLaps = 3;
    [SerializeField] private Racer[] racers;

    public int TotalLaps => totalLaps;

    public event Action<Racer> RacerFinished;

    private readonly List<Racer> ranking = new List<Racer>();
    private int finishedCount;

    private void Awake()
    {
        if (checkpoints.Length < 2)
        {
            Debug.LogError("RaceManager needs at least 2 checkpoints (the finish line plus one more).");
            enabled = false;
            return;
        }

        for (int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].Init(this, i);
        }

        foreach (Racer racer in racers)
        {
            racer.nextCheckpoint = 1;
            racer.lap = 1;
            racer.checkpointsPassed = 0;
            racer.finishOrder = 0;
            ranking.Add(racer);
        }
    }

    private void Update()
    {
        ranking.Sort(CompareRacers);

        for (int i = 0; i < ranking.Count; i++)
        {
            ranking[i].position = i + 1;
        }
    }

    public void CheckpointReached(Rigidbody body, int index)
    {
        Racer racer = FindRacer(body);
        if (racer == null || racer.finishOrder != 0) return;

        if (index != racer.nextCheckpoint) return;

        racer.checkpointsPassed++;

        if (index == 0)
        {
            racer.lap++;

            if (racer.lap > totalLaps)
            {
                finishedCount++;
                racer.finishOrder = finishedCount;
                RacerFinished?.Invoke(racer);
            }
        }

        racer.nextCheckpoint = (index + 1) % checkpoints.Length;
    }

    private int CompareRacers(Racer a, Racer b)
    {
        if (a.finishOrder != 0 && b.finishOrder != 0) return a.finishOrder.CompareTo(b.finishOrder);
        if (a.finishOrder != 0) return -1;
        if (b.finishOrder != 0) return 1;

        if (a.checkpointsPassed != b.checkpointsPassed)
            return b.checkpointsPassed.CompareTo(a.checkpointsPassed);

        return DistanceToNextCheckpoint(a).CompareTo(DistanceToNextCheckpoint(b));
    }

    private float DistanceToNextCheckpoint(Racer racer)
    {
        Vector3 target = checkpoints[racer.nextCheckpoint].transform.position;
        return (racer.body.position - target).sqrMagnitude;
    }

    private Racer FindRacer(Rigidbody body)
    {
        foreach (Racer racer in racers)
        {
            if (racer.body == body) return racer;
        }
        return null;
    }

    public int GetPosition(int racerIndex) => racers[racerIndex].position;

    public int GetLap(int racerIndex) => Mathf.Min(racers[racerIndex].lap, totalLaps);

    public bool IsFinished(int racerIndex) => racers[racerIndex].finishOrder != 0;
}