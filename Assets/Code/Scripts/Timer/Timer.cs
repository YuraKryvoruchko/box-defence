using System;
using System.Threading.Tasks;

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

        public async Task StartTimer()
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

        private async Task StartCountdown(float delay)
        {
            TimeSpan shootRateInMilliseconds = TimeSpan.FromSeconds(delay);

            await Task.Delay((int)shootRateInMilliseconds.TotalMilliseconds);
        }

        #endregion
    }
}
