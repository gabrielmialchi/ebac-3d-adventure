using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        public float targetSpeed = 50f;
        public float clothSpeedDuration = 5f;
        public override void Collect()
        {
            base.Collect();
            PlayerBase.Instance.ChangeSpeed(targetSpeed, clothSpeedDuration);
        }
    }
}