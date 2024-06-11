using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWaypointsInvisible : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

}
