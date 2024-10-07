using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActivated = false;
    private string checkpointKey = "CheckpointKey";

    private void OnValidate()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointActivated && other.CompareTag("Player"))
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnOn();
        SaveCheckpoint();
    }

    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    [NaughtyAttributes.Button]
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    private void SaveCheckpoint()
    {
        //if (PlayerPrefs.GetInt(checkpointKey, 0) > key)
        //  PlayerPrefs.SetInt(checkpointKey, key);

        CheckpointManager.Instance.SaveCheckpoint(key);

        checkpointActivated = true;
    }
}
