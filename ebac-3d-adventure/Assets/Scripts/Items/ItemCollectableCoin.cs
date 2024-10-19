using UnityEngine;

namespace Items
{
    public class ItemCollectableCoin : ItemCollectableBase
    {
        [Header("Coin Collider")]
        public Collider coinCollider;
        protected override void OnCollect()
        {
            base.OnCollect();
            if(coinCollider != null) coinCollider.enabled = false;
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
