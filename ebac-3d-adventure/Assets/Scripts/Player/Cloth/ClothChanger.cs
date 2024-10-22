using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        //public List<SkinnedMeshRenderer> meshRenderers;
        public SkinnedMeshRenderer mesh;
        public Texture2D texture;

        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;
        private Texture2D _currentTexture;
        private ClothManager _clothManager;

        private void Awake()
        {
            _defaultTexture = (Texture2D)mesh.sharedMaterials[0].GetTexture(shaderIdName);
            _currentTexture = _defaultTexture;
            _clothManager = FindObjectOfType<ClothManager>();
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(ClothSetup clothSetup)
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, clothSetup.texture);
        }

        public void ResetTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, _defaultTexture);
        }

        private void GetTextureByType(ClothType type)
        {
            ClothSetup setup = _clothManager.GetSetupByType(type);
            if (setup != null)
            {
                _currentTexture = setup.texture;
                ChangeTexture();
            }
        }

        private void OnApplicationQuit()
        {
            ResetTexture();
        }
    }
}