using UnityEngine;

namespace GasMaskSystem
{
    public class GMInputManager : MonoBehaviour
    {
        [Header("Raycast Pickup Input")]
        public KeyCode interactKey;

        [Header("Equipping Gas Mask Inputs")]
        public KeyCode equipMaskKey;
        public KeyCode replaceFilterKey;

        [Header("Inventory Inputs")]
        public KeyCode toggleInventory;

        public static GMInputManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
