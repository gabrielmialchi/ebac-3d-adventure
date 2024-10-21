using Items;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;

        public GameObject graphicItem;

        public float timeToHide = 0.1f;
        public float clothTextureDuration = 5f;
        public string compareTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }
        public virtual void Collect()
        {
            var setup = ClothManager.Instance.GetSetupByType(clothType);

            PlayerBase.Instance.ChangeTexture(setup, clothTextureDuration);

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
            //if (particleSystems != null) particleSystems.Play();
            //if (audioSource != null) audioSource.Play();

            Destroy(gameObject);
        }

    }
}
