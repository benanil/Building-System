using AnilTools;
using Assets.BuildingSystem;
using System;
using UnityEngine;

namespace BuildingSystem
{    
    [RequireComponent(typeof(Rigidbody))]
    public class Prop : MonoBehaviour
    {
        public PropData propData;
        public bool Placed = false;

        [NonSerialized]
        public Material material;
        private MeshRenderer meshRenderer;

        public void SetProp(PropData propData)
        {
            this.propData = propData;
            
            if (propData.mesh)
            {
                GetComponent<MeshFilter>().mesh = propData.mesh;
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.localScale = new Vector3(propData.widthX, propData.height, propData.widthZ);
            }
            
            GetComponent<Rigidbody>().isKinematic = true;
            gameObject.AddComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>();

            material = PoolManager.instance.transparentMaterial;

            meshRenderer.material = material;
            gameObject.SetActive(true);
        }

        public void Place()
        {
            // todo: particle effect and sound
            material = PoolManager.instance.woodMaterial;
            material.SetTexture(ShaderPool._MainTex, propData.texture);
            meshRenderer.sharedMaterial = material;
            Placed = true;
        }

        public void Remove()
        {
            // todo: particle effect and sound
            material = PoolManager.instance.transparentMaterial;
            meshRenderer.material = material;

            Placed = false;
            gameObject.SetActive(false);
        }

    }
}
