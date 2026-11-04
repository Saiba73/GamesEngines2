using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    private RaceManager manager;
    private int index;

    public void Init(RaceManager raceManager, int checkpointIndex)
    {
        manager = raceManager;
        index = checkpointIndex;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody body = other.attachedRigidbody;
        if (body == null || manager == null) return;

        manager.CheckpointReached(body, index);
    }

    private void OnDrawGizmos()
    {
        BoxCollider box = GetComponent<BoxCollider>();
        if (box == null) return;

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new Color(0f, 1f, 0.3f, 0.15f);
        Gizmos.DrawCube(box.center, box.size);
        Gizmos.color = new Color(0f, 1f, 0.3f, 0.9f);
        Gizmos.DrawWireCube(box.center, box.size);

#if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.TransformPoint(box.center), gameObject.name);
#endif
    }
}