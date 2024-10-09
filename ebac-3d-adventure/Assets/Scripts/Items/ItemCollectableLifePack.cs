using UnityEngine;

namespace Items
{
    public class ItemCollectableLifePack : ItemCollectableBase
    {
        public Collider collider;
        protected override void OnCollect()
        {
            base.OnCollect();
            ItemManager.Instance.AddByType(ItemType.LIFE_PACK);
            if (collider != null) collider.enabled = false;
        }
    }
}
