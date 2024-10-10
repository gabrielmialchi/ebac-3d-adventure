using UnityEngine;

namespace Items
{
    public class ActionLifePack : MonoBehaviour
    {
        public SOInt soInt;
        public HealthBase health;

        public KeyCode recoverKey;

        private void Start()
        {
            soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
        }

        private void RecoverLife()
        {
            if (soInt.value > 0)
            {
                ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
                health.ResetLife();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(recoverKey)) RecoverLife();
        }
    }
}
