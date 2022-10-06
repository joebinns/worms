using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialStorage : MonoBehaviour
    {
        #region Impermutable
        [SerializeField] private Material _defaultMaterial;
        public Material DefaultMaterial => _defaultMaterial;
        #endregion
    }
}
