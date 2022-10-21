using System.Collections.Generic;
using UnityEngine;
using BoxDefence.Damage;
using BoxDefence.TimerSystem;

namespace BoxDefence.AI
{
    public class Wizard : Enemy
    {
        #region Fields

        [Space]
        [SerializeField] private float _therapeuticPercentage = 3f;
        [SerializeField] private float _damageAbsorptionPercentage = 20f;
        [Space]
        [SerializeField] private float _periodTreatEnemys = 10f;

        private List<Enemy> _enemiesInTherapeuticRadius;

        private Timer _timer;

        private const DamageType BLOCKED_DAMAGE_TYPE = DamageType.Electrical;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            base.Awake();

            _enemiesInTherapeuticRadius = new List<Enemy>();
            _timer = new Timer(_periodTreatEnemys);
            _timer.OnEndTimer += StartTimerToTreatMoment;
        }
        private void Start()
        {
            StartTimerToTreatMoment();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
                _enemiesInTherapeuticRadius.Add(enemy);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
                _enemiesInTherapeuticRadius.Remove(enemy);
        }

        #endregion

        #region Public Methods

        public override void TakeDamage(IDamager damager)
        {
            if (damager.GetDamageType() == BLOCKED_DAMAGE_TYPE)
                damager.DepleteBy(_damageAbsorptionPercentage);

            base.TakeDamage(damager);
        }

        #endregion

        #region Private Methods

        private async void StartTimerToTreatMoment()
        {
            await _timer.StartTimer();

            TreatEnemysInTherapeuticRadius();
        }
        private void TreatEnemysInTherapeuticRadius()
        {
            foreach (Enemy enemy in _enemiesInTherapeuticRadius)
                enemy.TreatOn(_therapeuticPercentage);
        }

        #endregion
    }
}
