using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer; //capa de el player
    [SerializeField] private float detectionRadius; //radio de deteccion del player
    [SerializeField] private float enemyCheckRadius;//radio para detectar cuantos enemigos vivos hay
    [SerializeField] private GameObject[] enemyPrefabs;//prefabs de los enemigos
    [SerializeField] private GameObject[] spawnPoints;//spawn de los enemigos 
    [SerializeField] private int totalWaves = 3;//numero total de las waves
    [SerializeField] private int enemiesPerWave = 3;//enemigos por wave
    [SerializeField] private float spawnDelay = 0.5f;//tiempo entre cada spawn de enemigos
    [SerializeField] private float waveDelay = 1f;//tiempo entre oleadas

    [SerializeField] private GameObject walls1;
    [SerializeField] private GameObject walls2;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private bool playerDetected = false;
    private int currentWave = 0;//wave actual
    private List<GameObject> aliveEnemies = new List<GameObject>();//lista de enemigos vivos
    private bool waveInProgress = false;
    private float waveTimer = 0f;

    void Update()
    {
        if (!playerDetected)//si el jugador fue detectado
        {
            playerDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
            if (playerDetected)
            {
                ActivateWalls();//activar paredes
                StartCoroutine(SpawnWave());//iniciar primer oleada
                cinemachineCamera.enabled = false;
            }
        }

        //si esta la wave en progreso, detectar si quedan enemigos vivos
        if (waveInProgress)
        {
            if (CheckEnemiesInRadius() == 0)
            {
                if (currentWave < totalWaves)
                {
                    waveTimer += Time.deltaTime;
                    if (waveTimer >= waveDelay)
                    {
                        waveTimer = 0f;
                        StartCoroutine(SpawnWave());//iniciar siguiente oleada
                    }
                }
                else
                {
                    //si ya se completaron todas las waves
                    Debug.Log("Ultima oleada completada");
                    DeactivateWalls();
                    waveInProgress = false;
                    gameObject.SetActive(false);
                    cinemachineCamera.enabled = true;
                }
            }
        }
    }

    //corutina para spawnear waves
    IEnumerator SpawnWave()
    {
        if (currentWave < totalWaves)
        {
            waveInProgress = true;
            aliveEnemies.Clear();
            Debug.Log($"Iniciando oleada {currentWave + 1}");

            int enemiesThisWave = enemiesPerWave + currentWave;//aumenta la cantidad de enemigos con cada wave

            for (int i = 0; i < enemiesThisWave; i++)
            {
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];//selecciona enemigo
                GameObject spawnPoint = spawnPoints[i % spawnPoints.Length];//selecciona spawn 


                //spawnea al enemigo
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
                aliveEnemies.Add(enemy);

                yield return new WaitForSeconds(spawnDelay);//espera para spawnear al siguiente enemigo
            }


            currentWave++;//avanzar al siguiente enemgio
        }
    }

    //verifica cuantos enemigos hay en el radio
    private int CheckEnemiesInRadius()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemyCheckRadius);
        int enemiesInRadius = 0;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                enemiesInRadius++;
            }
        }

        return enemiesInRadius;
    }

    //llama cuando el enemigo muere
    public void NotifyEnemyDeath(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
    }

    //activa las paredes 
    private void ActivateWalls()
    {
        if (walls1 != null) walls1.SetActive(true);
        if (walls2 != null) walls2.SetActive(true);
    }

    //desacctiva las paredes
    private void DeactivateWalls()
    {
        if (walls1 != null) walls1.SetActive(false);
        if (walls2 != null) walls2.SetActive(false);
    }

    //dibuja radio de deteccion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyCheckRadius);
    }
}
