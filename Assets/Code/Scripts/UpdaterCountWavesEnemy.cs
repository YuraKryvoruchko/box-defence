using UnityEngine;
using UnityEngine.UI;

namespace BoxDefence.UI
{
    public class UpdaterCountWavesEnemy : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [Space]
        [SerializeField] private int _maxWaves = 5;

        private int _countWaves = 0;

        private void OnEnable()
        {
            Spawner.OnCreateWaves += UpdateWaves;
        }
        private void OnDisable()
        {
            Spawner.OnCreateWaves -= UpdateWaves;
        }

        private void UpdateWaves()
        {
            _countWaves++;

            _text.text = _countWaves.ToString() + ':' + _maxWaves.ToString() + " Waves";
        }
    }
}
