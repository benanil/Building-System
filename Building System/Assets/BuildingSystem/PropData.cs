using UnityEngine;

namespace Assets.BuildingSystem
{
    [CreateAssetMenu(fileName = "PropData", menuName = "BuildingSystem/PropData", order = 0)]
    public class PropData : ScriptableObject
    {
        public float height = 1f;
        public float widthX = 1f;
        public float widthZ = 1f;
        public Texture2D texture;
        [Tooltip("not required")]
        public Mesh mesh;
    }
}