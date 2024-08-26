using UnityEngine;

namespace GasMaskSystem
{
    public class GMInteractor : MonoBehaviour
    {
        [Space(5)][SerializeField] private int interactDistance = 5;

        private GMItem raycastedObj;
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, interactDistance))
            {
                var selectedItem = hit.collider.GetComponent<GMItem>();
                if (selectedItem != null)
                {
                    raycastedObj = selectedItem;
                    HighlightCrosshair(true);
                }
                else
                {
                        ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }

            if (raycastedObj != null)
            {
                if (Input.GetKey(GMInputManager.instance.interactKey))
                {
                    raycastedObj.ObjectInteract();
                }
            }
        }

        private void ClearExaminable()
        {
            if (raycastedObj != null)
            {
                HighlightCrosshair(false);
                raycastedObj = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            GMUIManager.instance.HighlightCrosshair(on);
        }
    }
}
