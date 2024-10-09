using UnityEngine;

namespace Items
{
    public class ItemCollectableCoin : ItemCollectableBase
    {
        public Collider collider;
        protected override void OnCollect()
        {
            base.OnCollect();
            ItemManager.Instance.AddByType(ItemType.COIN);
            if(collider != null) collider.enabled = false;
        }
    }
}
