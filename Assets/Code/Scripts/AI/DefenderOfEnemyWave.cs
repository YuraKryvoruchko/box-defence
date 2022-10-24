using UnityEngine;
using BoxDefence.TimerSystem;
using BoxDefence.Damage;

namespace BoxDefence.AI
{ 
    public class DefenderOfEnemyWave : Enemy
    {
        #region Fields

        [Space]
        [SerializeField] private float _damageAbsorptionPercentage = 15f;
        [SerializeField] private CircleCollider2D antiDamageShield;
        [Space]
        [SerializeField] private float _periodProtectEnemies = 5f;

        private Timer _timer;

        private const bool ENABLE_SHIELD = true;
        private const bool DISABLE_SHIELD = false;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            base.Awake();

            _timer = new Timer(_periodProtectEnemies);
            _timer.OnEndTimer += StartTimerToEnableShield;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamager damager) == true)
                damager.DepleteBy(_damageAbsorptionPercentage);
        }

        #endregion

        #region Private Methods

        private async void StartTimerToEnableShield()
        {
            antiDamageShield.enabled = ENABLE_SHIELD;

            await _timer.StartTimer();

            antiDamageShield.enabled = DISABLE_SHIELD;
        }

        #endregion
    }
}
