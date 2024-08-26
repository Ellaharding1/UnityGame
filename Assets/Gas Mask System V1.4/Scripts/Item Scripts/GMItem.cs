using UnityEngine;

namespace GasMaskSystem
{
    public class GMItem : MonoBehaviour
    {
        private enum ItemType { None, GasMask, Filter }
        [SerializeField] private ItemType _itemType = ItemType.None;

        public void ObjectInteract()
        {
            switch (_itemType)
            {
                case ItemType.GasMask: GMController.instance.PickupGasMask(); break;
                case ItemType.Filter: GMController.instance.PickupFilter(); break;
            }
            gameObject.SetActive(false);
        }
    }
}
