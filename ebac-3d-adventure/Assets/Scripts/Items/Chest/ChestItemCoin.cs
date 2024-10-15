using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Items
{
    public class ChestItemCoin : ChestItemBase
    {
        public int coinNumber;
        public float tweenEndTime = .5f;

        public GameObject coinPrefab;

        public Vector2 randomRange = new Vector2(-2f, 2f);

        private List<GameObject> _items = new List<GameObject>();

        public override void ShowItem()
        {
            base.ShowItem();
            CreateItems();
        }

        [NaughtyAttributes.Button]
        private void CreateItems()
        {
            for (int i = 0; i < coinNumber; i++)
            {
                var item = Instantiate(coinPrefab);
                item.transform.position = transform.position
                    + Vector3.forward * Random.Range(randomRange.x, randomRange.y)
                    + Vector3.right * Random.Range(randomRange.x, randomRange.y);
                item.transform.DOScale(0, tweenEndTime).SetEase(Ease.OutBack).From();
                _items.Add(item);
            }
        }

        [NaughtyAttributes.Button]
        public override void Collect()
        {
            base.Collect();
            foreach (var item in _items)
            {
                item.transform.DOMoveY(2f, tweenEndTime).SetRelative();
                item.transform.DOScaleY(0f, tweenEndTime / 2).SetDelay(tweenEndTime / 2);
                ItemManager.Instance.AddByType(ItemType.COIN);
            }
        }
    }
}
