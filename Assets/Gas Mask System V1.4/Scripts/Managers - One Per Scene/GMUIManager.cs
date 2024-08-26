using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace GasMaskSystem
{
    public class GMUIManager : MonoBehaviour
    {
        public enum MaskUIState { MaskNormal, MaskEquipped }

        public enum FilterState { FilterNumber, FilterAlarm, FilterNormal, FilterValue }

        public enum PostProcessState { OriginalPostProcess, GasPostProcess }

        [Header("Gas Mask UI Colours")]
        [SerializeField] private Color maskEquippedColor = Color.green;
        [SerializeField] private Color maskNormalColor = Color.white;

        [Header("Filter UI Colours")]
        [SerializeField] private Color filterAlarmColor = Color.red;
        [SerializeField] private Color filterNormalColor = Color.white;

        [Header("Health UI")]
        [SerializeField] private Text healthTextUI = null;
        [SerializeField] private Image healthSliderUI = null;

        [Header("Gas Mask UI")]
        [SerializeField] private Image _maskIconUI = null;
        [SerializeField] private Image _filterIconUI = null;
        [SerializeField] private Text _filterCountUI = null;
        [SerializeField] private Image _filterSliderUI = null;

        [Header("UI Canvas & Elements")]
        [SerializeField] private CanvasGroup gasMaskCanvas = null;
        [SerializeField] private CanvasGroup visorCanvas = null;
        [SerializeField] private Image radialIndicatorUI = null;
        [SerializeField] private Image _crosshair = null;

        [Header("Post Processing Effects")]
        [SerializeField] private PostProcessVolume _postProcessingVolume = null;
        [SerializeField] private PostProcessProfile _originalProfile = null;
        [SerializeField] private PostProcessProfile _gasMaskProfile = null;

        private MaskUIState _maskUIState;
        private FilterState _filterState;
        private PostProcessState _postProcessState;

        private Vignette _vignette;
        private DepthOfField _dof;

        public static GMUIManager instance;

        private void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            print("Please make sure Post Processing is installed, navigate to Package Manager > Unity Registery > Type 'Post Processing' - Then install");

            _gasMaskProfile.TryGetSettings(out _vignette);
            _gasMaskProfile.TryGetSettings(out _dof);

            GasMaskInventoryUI(false);
        }

        public Image Crosshair
        {
            get { return _crosshair; }
            set { _crosshair = value; }
        }

        public void GasChokingEffect(bool on)
        {
            EnableDOF(on);
            if (on)
            {
                SwapPostProcessingProfile(PostProcessState.GasPostProcess);
            }
            else
            {
                SwapPostProcessingProfile(PostProcessState.OriginalPostProcess);
            }
        }

        public void GasMaskVisorUI(bool on)
        {
            visorCanvas.alpha = on ? 1 : 0;
            EnableVignette(on);
            if (on)
            {
                SwapPostProcessingProfile(PostProcessState.GasPostProcess);
            }
            else
            {
                SwapPostProcessingProfile(PostProcessState.OriginalPostProcess);
            }
        }

        public void SwapPostProcessingProfile(PostProcessState _postProcessState)
        {
            switch(_postProcessState)
            {
                case PostProcessState.GasPostProcess: _postProcessingVolume.profile = _gasMaskProfile; break;
                case PostProcessState.OriginalPostProcess: _postProcessingVolume.profile = _originalProfile; break;
            }
        }

        public void EnableVignette(bool on)
        {
            _vignette.active = on;
        }

        public void EnableDOF(bool on)
        {
            _dof.active = on;
        }

        public void UpdateIndicatorUI(float equipTimer)
        {
            radialIndicatorUI.enabled = true;
            radialIndicatorUI.fillAmount = equipTimer;
        }

        public void IndicatorUIReset(float maxEquipTimer)
        {
            radialIndicatorUI.enabled = false;
            radialIndicatorUI.fillAmount = maxEquipTimer;
        }

        public void UpdateFilterUI(FilterState _filterState)
        {
            switch (_filterState)
            {
                case FilterState.FilterNumber: _filterCountUI.text = GMController.instance.maskFilters.ToString("0");
                    break;
                case FilterState.FilterAlarm: _filterIconUI.color = filterAlarmColor;
                    break;
                case FilterState.FilterNormal: _filterIconUI.color = filterNormalColor;
                    break;
                case FilterState.FilterValue: _filterSliderUI.fillAmount = (GMController.instance.filterTimer / GMController.instance.maxFilterTimer);
                    break;
            }
        }

        public void UpdateMaskUI(MaskUIState _maskUIState)
        {
            switch (_maskUIState)
            {
                case MaskUIState.MaskNormal: _maskIconUI.color = maskNormalColor; break;
                case MaskUIState.MaskEquipped: _maskIconUI.color = maskEquippedColor; break;
            }
        }

        public void UpdateHealthUI(float currentHealth, float maxHealth)
        {
            healthTextUI.text = currentHealth.ToString("0");
            healthSliderUI.fillAmount = (currentHealth / maxHealth);
        }

        public void GasMaskInventoryUI(bool on)
        {
            gasMaskCanvas.alpha = on ? 1 : 0;
        }

        public void HighlightCrosshair(bool on)
        {
            Crosshair.color = on ? Color.red : Color.white;
        }

        public void OnDestroy()
        {
            EnableVignette(false);
            EnableDOF(false);
        }
    }
}
