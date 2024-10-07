using Ebac.Core.Singleton;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
    }

    public Vector3 GetLastCheckpointRespawnPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
}