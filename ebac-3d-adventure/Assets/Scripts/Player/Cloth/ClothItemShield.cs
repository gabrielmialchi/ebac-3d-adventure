using UnityEngine;

namespace Cloth
{
    public class ClothItemShield : ClothItemBase
    {
        public int targetShield = 1;
        public float clothShieldDuration = 5f;
        public override void Collect()
        {
            base.Collect();
            PlayerBase.Instance.health.ChangeDamageMultiplier(targetShield, clothShieldDuration);
        }
    }
}