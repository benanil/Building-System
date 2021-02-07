using AnilTools;
using Assets.BuildingSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BuildingSystem
{
    public class PoolManager : Singleton<PoolManager>
    {
        [SerializeField]
        private int PoolSize = 300;

        public Material transparentMaterial;
        public Material woodMaterial;

        public static readonly List<Prop> Props = new List<Prop>();

        private void Start()
        {
            SpawnProps();
        }

        public Prop RequestProp(PropData propData)
        {
            if (Props.All(x => x.Placed)) // poolda obje kalmamış
            {
                SpawnProps();
            }

            var findedProp = Props.Find(x => !x.Placed);

            findedProp.SetProp(propData);

            return findedProp;
        }


        private void SpawnProps()
        { 
            for (int i = 0; i < PoolSize; i++)
            {
                var createdProp = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<Prop>();
                createdProp.gameObject.SetActive(false);
                createdProp.gameObject.transform.SetParent(transform);
                Props.Add(createdProp);
            }
        }
    }
}