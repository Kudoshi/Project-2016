using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MaskSpawner : MonoBehaviour
{
    [SerializeField] private Mask[] maskPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float moveDuration = 1f;

    public Mask CurrentMask { get; private set; }
    public event Action<Mask> OnMaskArrived;

    public void SpawnMask()
    {
        if (CurrentMask != null) return;

        var index = Random.Range(0,
            maskPrefabs.Length);
        var mask = Instantiate(maskPrefabs[index],
            spawnPoint.position, spawnPoint.rotation);
        CurrentMask = mask;

        var mover = mask.GetComponent<MaskMover>();
        mover.MoveTo(interactionPoint.position, moveDuration, ()
            =>
        {
            OnMaskArrived?.Invoke(mask);
        });
    }

    public void DespawnMask(Action onComplete = null)
    {
        if (CurrentMask == null) return;

        var mask = CurrentMask;
        CurrentMask = null;

        var mover = mask.GetComponent<MaskMover>();
        mover.MoveTo(exitPoint.position, moveDuration, () =>
        {
            Destroy(mask.gameObject);
            onComplete?.Invoke();
        });
    }
}