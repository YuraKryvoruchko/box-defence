using System;
using System.Collections.Generic;
using UnityEngine;
using BoxDefence.Tilemaps;

namespace BoxDefence
{
    public class CompoundSpawner : MonoBehaviour, ISpawner
    {
        #region Fields

        [SerializeField] private List<SpawnTile> _spawnerTiles;

        private List<ISpawner> _spawners;

        private WavesCounter _wavesCounter;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _spawners = new List<ISpawner>();
        }
        private void OnEnable()
        {
            foreach (SpawnTile spawnTile in _spawnerTiles)
                spawnTile.OnCreateSpanwer += AddSpawner;
        }
        private void OnDisable()
        {
            foreach (SpawnTile spawnTile in _spawnerTiles)
                spawnTile.OnCreateSpanwer -= AddSpawner;
        }

        #endregion

        #region Public Methods

        public void AddSpawner(ISpawner spawner)
        {
            if (_spawners.Contains(spawner) == true)
                throw new Exception("Spawner is contains!");

            _spawners.Add(spawner);
        }
        public void Remove(ISpawner spawner)
        {
            if (_spawners.Contains(spawner) == true)
                throw new Exception("Spawner is contains!");

            _spawners.Remove(spawner);
        }
        public WavesCounter GetWavesCounter()
        {
            if (_wavesCounter == null)
                _wavesCounter = CreateNewCounter();

            return _wavesCounter;
        }

        #endregion

        #region Private Methods

        private WavesCounter CreateNewCounter()
        {
            WavesCounter[] wavesCounters = GetWavesCountersFromSpawners();

            WavesCounter currentWavesCounter = CreateCompoundCounter(wavesCounters);
            foreach (WavesCounter wavesCounter in wavesCounters)
            {
                wavesCounter.OnAddWaves += currentWavesCounter.AddWavesToCount;
                wavesCounter.OnDeleteWaves += currentWavesCounter.RemoveWavesFromCount;
            }

            return currentWavesCounter;
        }
        private WavesCounter[] GetWavesCountersFromSpawners()
        {
            WavesCounter[] wavesCounters = new WavesCounter[_spawners.Count];

            for (int i = 0; i < _spawners.Count; i++)
                wavesCounters[i] = _spawners[i].GetWavesCounter();

            return wavesCounters;
        }
        private WavesCounter CreateCompoundCounter(IEnumerable<WavesCounter> wavesCounters)
        {
            int currentWavesCount = 0;
            int maxWavesCount = 0;

            foreach (WavesCounter wavesCounter in wavesCounters)
            {
                currentWavesCount += wavesCounter.GetCurrentWavesCount();
                maxWavesCount += wavesCounter.GetMaxWavesCount();
            }

            WavesCounter currentWavesCounter = new WavesCounter(maxWavesCount, currentWavesCount);
            return currentWavesCounter;
        }

        #endregion
    }
}
