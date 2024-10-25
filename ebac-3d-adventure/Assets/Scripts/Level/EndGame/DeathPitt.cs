using System.Transactions;
using UnityEngine;

public class DeathPitt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerBase player = other.transform.GetComponent<PlayerBase>();

        if (player != null)
        {
            player.health._currentLife = 0;
        }
    }
}