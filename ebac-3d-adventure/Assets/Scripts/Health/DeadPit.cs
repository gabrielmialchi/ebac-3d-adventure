using UnityEngine;

public class DeadPit : MonoBehaviour
{
    [SerializeField] private Collider deadPitCollider;
    [SerializeField] private GameObject player;
    [SerializeField] private HealthBase playerHealthBase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHealthBase.Kill();
        }
    }
}
