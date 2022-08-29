using BoxDefence.TimerSystem;

namespace BoxDefence.Towers
{
    public class ShootingBlocator
    {
        #region Fields

        private Timer _timer;

        private bool _canShoot = true;

        private const float DEFAULT_SECONDS_FOR_TIMER = 0;

        #endregion

        #region Constructor

        public ShootingBlocator()
        {
            _timer = new Timer(DEFAULT_SECONDS_FOR_TIMER);
        }

        #endregion

        #region Public Methods

        public async void BlockShootingOn(float second)
        {
            _canShoot = false;

            _timer.SetDelay(second);
            await _timer.StartTimer();

            _canShoot = true;
        }
        public bool CanShoot()
        {
            return _canShoot;
        }

        #endregion
    }
}
