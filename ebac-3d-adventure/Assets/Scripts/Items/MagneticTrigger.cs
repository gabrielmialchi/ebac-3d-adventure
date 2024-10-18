using Items;
using UnityEngine;

public class MagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemCollectableBase i = other.GetComponent<ItemCollectableBase>();
        if(i != null)
        {
            i.gameObject.AddComponent<Magnetics>();
        }
    }
}