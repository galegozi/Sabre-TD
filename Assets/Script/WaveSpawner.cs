using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//current problems: if the spawn goes on too long, there will still be monsters spawning ontop.
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveIndex = 0;
    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); //Countdown timer, set up so it wont show negative numbers

        waveCountdownText.text = "Wave Countdown: " + string.Format("{0:00.0}", countdown); 
    }

    //IEnumerator comes from System.Collections and allows us to pause spawnWave when we choose to.
    IEnumerator spawnWave()
    {
        waveIndex++;
        PlayerStats.Rounds++;


        for (int i = 0; i < waveIndex; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f); // will wait .5 seconds between spawns so enemies are not ontop of each other coming out of start.
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
