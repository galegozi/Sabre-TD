using System;
using System.Collections.Generic;
using Core.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Level
{
	/// <summary>
	/// WaveManager - handles wave initialisation and completion
	/// </summary>
	public class WaveManager : MonoBehaviour
	{
		public static bool isPaused;
		public static bool shouldContinue;
		public Button startWaveButton;
		public GameObject waveInfo;
		private bool active = false;

		/// <summary>
		/// Current wave being used
		/// </summary>
		protected int m_CurrentIndex;

		/// <summary>
		/// Whether the WaveManager starts waves on Awake - defaulted to null since the LevelManager should call this function
		/// </summary>
		public bool startWavesOnAwake;

		/// <summary>
		/// The waves to run in order
		/// </summary>
		[Tooltip("Specify this list in order")]
		public List<Wave> waves = new List<Wave>();

		/// <summary>
		/// The current wave number
		/// </summary>
		public int waveNumber
		{
			get { return m_CurrentIndex + 1; }
		}

		/// <summary>
		/// The total number of waves
		/// </summary>
		public int totalWaves
		{
			get { return waves.Count; }
		}

		public float waveProgress
		{
			get
			{
				if (waves == null || waves.Count <= m_CurrentIndex)
				{
					return 0;
				}
				return waves[m_CurrentIndex].progress;
			}
		}

		/// <summary>
		/// Called when a wave begins
		/// </summary>
		public event Action waveChanged;

		/// <summary>
		/// Called when all waves are finished
		/// </summary>
		public event Action spawningCompleted;

		/// <summary>
		/// Starts the waves
		/// </summary>
		public virtual void StartWaves()
		{
			if (waves.Count > 0)
			{
				InitCurrentWave();
			}
			else
			{
				Debug.LogWarning("[LEVEL] No Waves on wave manager. Calling spawningCompleted");
				SafelyCallSpawningCompleted();
			}
		}

		/// <summary>
		/// Inits the first wave
		/// </summary>
		protected virtual void Awake()
		{
			if (startWavesOnAwake)
			{
				StartWaves();
			}
		}

		private void Update()
        {
			if(waveProgress == 0 && active)
            {
				startWaveButton.gameObject.SetActive(true);
				waveInfo.SetActive(false);
				active = false;
				//startWaveButton.gameObject.setActive(true);
            }
			else if (waveProgress > 0)
            {
				active = true;
            }
			else if(!isPaused && shouldContinue)
            {
				InitCurrentWave();
				shouldContinue = false;
            }
        }

		/// <summary>
		/// Sets up the next wave
		/// </summary>
		protected virtual void NextWave()
		{
			if (waves.Next(ref m_CurrentIndex))
			{
				if (shouldContinue)
				{
					waves[m_CurrentIndex].waveCompleted -= NextWave;
					InitCurrentWave();
				}
			}
			else
			{
				SafelyCallSpawningCompleted();
			}
		}

		/// <summary>
		/// Initialize the current wave
		/// </summary>
		protected virtual void InitCurrentWave()
		{
			Wave wave = waves[m_CurrentIndex];
			wave.waveCompleted += NextWave;
			wave.Init();
			if (waveChanged != null)
			{
				waveChanged();
			}
		}

		/// <summary>
		/// Calls spawningCompleted event
		/// </summary>
		protected virtual void SafelyCallSpawningCompleted()
		{
			if (spawningCompleted != null)
			{
				spawningCompleted();
			}
		}

		public void ContinueWave()
        {
			isPaused = false;
			shouldContinue = true;
        }
	}
}