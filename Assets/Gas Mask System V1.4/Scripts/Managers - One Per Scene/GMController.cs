using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace GasMaskSystem
{
    public class GMController : MonoBehaviour

    {
        public enum GasMaskState { GasMaskOn, GasMaskOffInSmoke, GasMaskOffOutOfSmoke }
        private GasMaskState _gasMaskState;
        public GameObject PrincessVarient;

        [Header("Gas Mask Features")]
        [SerializeField] private bool hasGasMaskOnStart = false;
        [Range(0, 10)][SerializeField] private float maxEquipMaskTimer = 1f; //The maximum time you want to wait before putting on or taking off the mask. Same as "maskTimer"
        private float maskBeforeTimer = 0.99f; //Just a millisecond before the max timer so we can stop this looping, it will autofill from the start function if edited
        private float equipMaskTimer = 1f;
        private bool hasGasMask = false;
        private bool puttingOn = false, pullingOff = false;

        [Header("Movement Speeds")]
        [SerializeField] private float walkNorm = 5;
        [SerializeField] private float walkGas = 2;
        [SerializeField] private float runNorm = 10;
        [SerializeField] private float runGas = 4;

        [Header("Filter Options")]
        [Range(0, 20)][SerializeField] private float filterFallRate = 2f; //Increase this to make the filter deplete faster
        [Range(0, 100)][SerializeField] private int warningPercentage = 20; //The percentage the system will give a warning

        [Range(0, 100)][SerializeField] private float _filterTimer = 100f; //Set the same as your max value, do not change!
        private bool hasFilter = true; //Whether you have a filter or not
        private bool filterChanged = false; //Has the filter changed
        [SerializeField] private int _maskFilters = 0; //How many filters does your player currently have? Increase this value at the start to give them more!
        private float replaceFilterTimer = 1.0f;
        private float maxReplaceFilterTimer = 1.0f;

        [Header("Human Audio")]
        [SerializeField] private Sound deepBreathAudio = null;
        [SerializeField] private Sound breathInAudio = null;
        [SerializeField] private Sound breathOutAudio = null;
        [SerializeField] private Sound breathingFullAudio = null;
        [SerializeField] private Sound chokingAudio = null;

        [Header("Gas Mask Audio")]
        [SerializeField] private Sound pickupAudio = null;
        [SerializeField] private Sound replaceFilterAudio = null;
        [SerializeField] private Sound warningAudio = null;

        private bool canBreath = true;
        private bool playOnce = false;
        private bool shouldUpdateEquip = false;
        private bool shouldUpdateFilter = false;

        private bool openInventory;
        private bool showUI;

        private GameObject mainCamera;
        private FirstPersonController player;

        public static GMController instance;

        public bool isGasMaskEquipped { get; set; } = false;
        public float maxFilterTimer { get; set; } = 100f;

        public float filterTimer
        {
            get { return _filterTimer; }
            set { _filterTimer = value;  }
        }

        public int maskFilters
        {
            get { return _maskFilters; }
            set { _maskFilters = value; }
        }

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }

        void Start()
        {
            filterTimer = maxFilterTimer;
            equipMaskTimer = maxEquipMaskTimer;
            maskBeforeTimer = maxEquipMaskTimer - 0.01f;
            player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
            GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterNumber);

            if (hasGasMaskOnStart)
            {
                hasGasMask = true;
                GMUIManager.instance.UpdateMaskUI(GMUIManager.MaskUIState.MaskNormal);
            }
        }

        void PlayerMovement(bool slowPlayer)
        {
            if (slowPlayer)
            {
                player.m_WalkSpeed = walkGas;
                player.m_RunSpeed = runGas;
            }
            else
            {
                player.m_WalkSpeed = walkNorm;
                player.m_RunSpeed = runNorm;
            }
        }

        public void PickupGasMask()
        {
            hasGasMask = true;
            GMAudioManager.instance.Play(pickupAudio);
            GMUIManager.instance.UpdateMaskUI(GMUIManager.MaskUIState.MaskNormal);
        }

        public void PickupFilter()
        {
            _maskFilters++;
            GMAudioManager.instance.Play(pickupAudio);
            GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterNumber);
        }

        void Update()
        {
            MyInput();
        }

        void MyInput()
        {
            if (Input.GetKeyDown(GMInputManager.instance.toggleInventory))
            {
                showUI = !showUI;
                GMUIManager.instance.GasMaskInventoryUI(showUI);
            }

            ///EQUIPPING THE GAS MASK
            if (Input.GetKey(GMInputManager.instance.equipMaskKey) && hasFilter && hasGasMask && !isGasMaskEquipped && !puttingOn && !pullingOff)
            {
                shouldUpdateEquip = false;
                equipMaskTimer -= Time.deltaTime;
                GMUIManager.instance.UpdateIndicatorUI(equipMaskTimer);

                if (equipMaskTimer <= 0)
                {
                    equipMaskTimer = maxEquipMaskTimer;
                    GMUIManager.instance.IndicatorUIReset(maxEquipMaskTimer);
                    StartCoroutine(MaskOn());
                    StartCoroutine(Wait());
                }
            }

            ///UNEQUIPPING THE GAS MASK
            else if (Input.GetKey(GMInputManager.instance.equipMaskKey) && hasFilter && hasGasMask && isGasMaskEquipped && !puttingOn)
            {
                shouldUpdateEquip = false;
                equipMaskTimer -= Time.deltaTime;
                GMUIManager.instance.UpdateIndicatorUI(equipMaskTimer);

                if (equipMaskTimer <= 0)
                {
                    equipMaskTimer = maxEquipMaskTimer;
                    GMUIManager.instance.IndicatorUIReset(maxEquipMaskTimer);
                    pullingOff = true;
                    MaskOff();
                    StartCoroutine(Wait());
                }
            }
            else
            {
                if (shouldUpdateEquip)
                {
                    equipMaskTimer += Time.deltaTime;
                    GMUIManager.instance.UpdateIndicatorUI(equipMaskTimer);

                    if (equipMaskTimer >= maskBeforeTimer)
                    {
                        equipMaskTimer = maxEquipMaskTimer;
                        GMUIManager.instance.IndicatorUIReset(maxEquipMaskTimer);
                        shouldUpdateEquip = false;
                        StartCoroutine(Wait());
                    }
                }
            }

            if (Input.GetKeyUp(GMInputManager.instance.equipMaskKey))
            {
                shouldUpdateEquip = true;
            }

            if (hasGasMask && isGasMaskEquipped)
            {
                filterTimer -= Time.deltaTime * filterFallRate;
                GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterValue);
                if (filterTimer <= 1)
                {
                    if (_maskFilters >= 1)
                    {
                        ReplaceFilter();
                    }
                    else
                    {
                        filterTimer = 0;
                        GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterValue);
                        hasFilter = false;
                        MaskOff();
                    }
                }

                if (filterTimer <= ((maxFilterTimer / 100) * warningPercentage) && !filterChanged)
                {
                    GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterAlarm);
                    GMAudioManager.instance.Play(warningAudio);
                    filterChanged = true;
                }
            }

            ///EQUIPPING FILTER
            if (hasGasMask)
            {
                if (Input.GetKey(GMInputManager.instance.replaceFilterKey) && _maskFilters >= 1)
                {
                    shouldUpdateFilter = false;
                    print("Equipping Filter");
                    replaceFilterTimer -= Time.deltaTime;
                    GMUIManager.instance.UpdateIndicatorUI(replaceFilterTimer);

                    if (replaceFilterTimer <= 0)
                    {
                        replaceFilterTimer = maxReplaceFilterTimer;
                        GMUIManager.instance.IndicatorUIReset(maxReplaceFilterTimer);
                        ReplaceFilter();
                    }
                }
                else
                {
                    if (shouldUpdateFilter)
                    {
                        replaceFilterTimer += Time.deltaTime;
                        GMUIManager.instance.UpdateIndicatorUI(replaceFilterTimer);

                        if (replaceFilterTimer >= maxReplaceFilterTimer)
                        {
                            replaceFilterTimer = maxReplaceFilterTimer;
                            GMUIManager.instance.IndicatorUIReset(maxReplaceFilterTimer);
                            shouldUpdateFilter = false;
                        }
                    }
                }
                if (Input.GetKeyUp(GMInputManager.instance.replaceFilterKey))
                {
                    shouldUpdateFilter = true;
                }
            }

            if (!canBreath)
            {
                if (isGasMaskEquipped) _gasMaskState = GasMaskState.GasMaskOn;
                else _gasMaskState = GasMaskState.GasMaskOffInSmoke;
            }
            else
            {
                if (isGasMaskEquipped) _gasMaskState = GasMaskState.GasMaskOn;
                else _gasMaskState = GasMaskState.GasMaskOffOutOfSmoke;
            }

            switch (_gasMaskState)
            {
                case GasMaskState.GasMaskOffOutOfSmoke:
                    if (playOnce)
                    {
                        GMAudioManager.instance.StopPlaying(chokingAudio);
                        GMAudioManager.instance.Play(deepBreathAudio);
                        playOnce = false;
                    }
                    break;

                case GasMaskState.GasMaskOffInSmoke:
                    if (!playOnce)
                    {
                        GMAudioManager.instance.StopPlaying(deepBreathAudio);
                        GMAudioManager.instance.StopPlaying(chokingAudio);
                        GMAudioManager.instance.Play(chokingAudio);
                        playOnce = true;
                    }
                    GMHealthManager.instance.DamageHealth();
                    break;

                case GasMaskState.GasMaskOn:
                    GMAudioManager.instance.StopPlaying(chokingAudio);
                    GMAudioManager.instance.StopPlaying(deepBreathAudio);
                    break;
            }
            return;
        }

        void ReplaceFilter()
        {
            _maskFilters--;
            filterTimer = maxFilterTimer;
            hasFilter = true;
            GMAudioManager.instance.Play(replaceFilterAudio);
            GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterNormal);
            GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterNumber);
            GMUIManager.instance.UpdateFilterUI(GMUIManager.FilterState.FilterValue);
            filterChanged = false;
        }

        public void DamageGas()
        {
            canBreath = false;
            PlayerMovement(true);
            GMHealthManager.instance.UpdateHealthUI();
            GMHealthManager.instance.ToggleHealthRegeneration(false);
            GMUIManager.instance.GasChokingEffect(true);
        }

        public void CanBreath()
        {
            canBreath = true;
            PlayerMovement(false);
            GMHealthManager.instance.ToggleHealthRegeneration(true);
            if (!isGasMaskEquipped)
            {
                GMUIManager.instance.GasChokingEffect(false);
            }
        }

        IEnumerator MaskOn()
        {
            isGasMaskEquipped = true;
            GMAudioManager.instance.Play(breathInAudio);

            const float waitDuration = 1.5f;
            yield return new WaitForSeconds(waitDuration);

            GMAudioManager.instance.Play(breathingFullAudio);

            GMUIManager.instance.UpdateMaskUI(GMUIManager.MaskUIState.MaskEquipped);
            GMUIManager.instance.GasMaskVisorUI(true);
        }

        void MaskOff()
        {
            isGasMaskEquipped = false;
            GMAudioManager.instance.Play(breathOutAudio);
            GMAudioManager.instance.StopPlaying(breathingFullAudio);
            GMAudioManager.instance.StopPlaying(deepBreathAudio);

            GMUIManager.instance.UpdateMaskUI(GMUIManager.MaskUIState.MaskNormal);
            GMUIManager.instance.GasMaskVisorUI(false); 
        }

        IEnumerator Wait()
        {
            if (!isGasMaskEquipped) pullingOff = true;
            else puttingOn = true;

            const float waitDuration = 1.5f;
            yield return new WaitForSeconds(waitDuration);
            puttingOn = pullingOff = false;
        }
    }
}
