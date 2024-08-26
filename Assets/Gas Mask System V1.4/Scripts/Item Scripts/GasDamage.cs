using UnityEngine;

namespace GasMaskSystem
{
    public class GasDamage : MonoBehaviour
    {
        private const string playerTag = "Player";

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(playerTag) && !GMController.instance.isGasMaskEquipped)
            {
                GMController.instance.DamageGas();
            }

            else if (other.CompareTag(playerTag) && GMController.instance.isGasMaskEquipped)
            {
                GMController.instance.CanBreath();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag) && !GMController.instance.isGasMaskEquipped)
            {
                GMController.instance.CanBreath();
            }
        }
    }
}
