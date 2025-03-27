
using UnityEngine.XR.ARFoundation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARSession ARSession;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Enemy Settings")]
    [SerializeField] private int enemyCount = 1;
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private float despawnrate = 5.0f;
 
    private bool _gameStarted = false;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();  
    private int _score =0;
    void Start()
    {
       
        //Called from UI button
        UIManager.OnStartButtonPressed += StartGame;
        UIManager.OnRestartButtonPressed += RestartGame;
    }

    void StartGame()
    {
        if (_gameStarted) return;
        _gameStarted = true;
        print("Game started!!!");

        planeManager.enabled = false;
        foreach (var plane in planeManager.trackables)
        {
            var meshVisual = plane.GetComponent<ARPlaneMeshVisualizer>();
            if (meshVisual) meshVisual.enabled = false;

            var lineVisual = plane.GetComponent<LineRenderer>();
            if (lineVisual) lineVisual.enabled = false;
        }
    }

    void RestartGame()
    {
        _gameStarted = false;
        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        while (ARSession.state != ARSessionState.SessionTracking)
        {
            yield return null;
        }
        ARSession.Reset();
        planeManager.enabled = true;
    }
    void spawmEnemy()
    {
        if (planeManager.trackables.count == 0) return;
        List<ARPlane> planesList = new List<ARPlane>();
        foreach (var plane in planeManager.trackables)
        {
            planesList.Add(plane);
        }
        var randomPlane = planesList[Random]
    }
    IEnumerator SpawnEnmies()
    {
        while (_gameStarted)
        {
            if (_spawnedEnemies.Count < enemyCount) 
            {
                spawmEnemy();
            }
            yield return new WaitForSeconds(spawmEnemy);

        } 
    }
    IEnumerator DespawnEnemies(GameObject enemy) 
    { 
      yield return new WaitForSeconds (despawnrate);
        if (_spawnedEnemies.Contains(enemy)) 
        { 
         _spawnedEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }
}
