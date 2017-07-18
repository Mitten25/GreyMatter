using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Transform spawnPoint;

    private GameObject[] enemySpawnPoints;

    public GameObject Player;

    [HideInInspector]
    public GameObject PlayerPrefab;
    [HideInInspector]
    public GameObject EnemyPrefab;



    private void Awake()
    {
        //initialize singleton behavior
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");

        Player = Instantiate(PlayerPrefab, spawnPoint.position, Quaternion.identity) as GameObject;

        SetCameraTargets();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = Instantiate(PlayerPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
            SetCameraTargets();
            DeSpawnEnemies();
            SpawnEnemies();
        }
    }

    private void SetCameraTargets()
    {
        CameraControlScript.instance.m_Targets = new Transform[] { Player.transform };
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            Instantiate(EnemyPrefab, enemySpawnPoints[0].transform.position, Quaternion.identity);
        }
    }

    private void DeSpawnEnemies()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enem in Enemies)
        {
            Destroy(enem);
        }
    }
}
