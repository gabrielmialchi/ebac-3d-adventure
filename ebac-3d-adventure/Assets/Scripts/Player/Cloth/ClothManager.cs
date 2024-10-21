using UnityEngine;
using Ebac.Core.Singleton;
using System.Collections.Generic;

namespace Cloth
{
    public enum ClothType
    {
        DEFAULT,
        SPEED,
        SHIELD
    }

    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.clothType == clothType);
        }
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;

        public Texture2D texture;
    }
}