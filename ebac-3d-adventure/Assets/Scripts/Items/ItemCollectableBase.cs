using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;
        
        public GameObject graphicItem;
        public ParticleSystem particleSystems;

        public string compareTag = "Player";
        public float timeToHide = 3;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystems != null) particleSystems.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particleSystems != null) particleSystems.Play();
            if (audioSource != null) audioSource.Play();

            ItemManager.Instance.AddByType(itemType);
        }
    }
}