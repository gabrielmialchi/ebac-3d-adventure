using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        //public List<SkinnedMeshRenderer> meshRenderers;
        public SkinnedMeshRenderer bodyMesh;
        public Texture2D texture;

        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D)bodyMesh.materials[0].GetTexture(shaderIdName);
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            bodyMesh.materials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(ClothSetup clothSetup)
        {
            bodyMesh.materials[0].SetTexture(shaderIdName, clothSetup.texture);
        }

        public void ResetTexture()
        {
            bodyMesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }
    }
}