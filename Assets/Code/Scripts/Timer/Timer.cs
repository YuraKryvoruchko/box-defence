using System;
using Cysharp.Threading.Tasks;

namespace BoxDefence.TimerSystem
{
    public class Timer
    {
        #region Fields

        private float _delay = 0;

        #endregion

        #region Actions

        public event Action OnStartTimer;
        public event Action OnEndTimer;

        #endregion

        #region Constructor

        public Timer(float delayInSeconds)
        {
            SetDelay(delayInSeconds);
        }

        #endregion

        #region Public Methods

        public async UniTask StartTimer()
        {
            OnStartTimer?.Invoke();

            await StartCountdown(_delay);

            OnEndTimer?.Invoke();
        }
        public void SetDelay(float delayInSeconds)
        {
            _delay = delayInSeconds;
        }

        #endregion

        #region Private Methods

        private async UniTask StartCountdown(float delay)
        {
            TimeSpan shootRateInMilliseconds = TimeSpan.FromSeconds(delay);

            await UniTask.Delay((int)shootRateInMilliseconds.TotalMilliseconds);
        }

        #endregion
    }
}
