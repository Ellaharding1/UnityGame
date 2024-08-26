using UnityEngine;
using UnityEngine.Events;

namespace GasMaskSystem
{
    public class GMHealthManager : MonoBehaviour
    {
        [Header("Health Variables")]
        [Range(0, 100)] [SerializeField] private float currentHealth = 100.0f;
        [Range(0, 100)] [SerializeField] private float maxHealth = 100.0f;
        [SerializeField] private float healthFall = 2;

        [Header("Health Regeneration")]
        [SerializeField] private float regenerationDelay = 1.0f; //Make sure this is the default start time of the regeneration
        [SerializeField] private float regenerationSpeed = 50.0f; //Speed of the health renegeration

        [Header("Death Event")]
        [SerializeField] private UnityEvent onDeath = null; //The event that happens when you lose all health

        private float currentHealthTimer = 1.0f; //How long it takes before regeneration health
        private bool regenHealth = false;

        public static GMHealthManager instance;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            currentHealthTimer = regenerationDelay;
        }

        private void Update()
        {
            HandleHealthRegeneration();
        }

        private void HandleHealthRegeneration()
        {
            if (!regenHealth || currentHealth >= maxHealth)
            {
                regenHealth = false;
                currentHealthTimer = regenerationDelay;
                return;
            }

            currentHealthTimer -= Time.deltaTime;

            if (currentHealthTimer <= 0)
            {
                currentHealth = Mathf.Min(currentHealth + Time.deltaTime * regenerationSpeed, maxHealth);
                UpdateHealthUI();
                currentHealthTimer = regenerationDelay;
            }
        }

        public void ToggleHealthRegeneration(bool on)
        {
            regenHealth = on;
        }

        public void DamageHealth()
        {
            currentHealth = Mathf.Max(currentHealth - healthFall * Time.deltaTime, 0);
            UpdateHealthUI();
        }

        public void UpdateHealthUI()
        {
            GMUIManager.instance.UpdateHealthUI(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Death();
            }
        }

        public void Death()
        {
            onDeath.Invoke();
        }
    }
}
