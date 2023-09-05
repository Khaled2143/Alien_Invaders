using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContianer;
    [SerializeField]
    private GameObject _TripleShotPreFab;
    [SerializeField]
    private GameObject _speedUpPreFab;
    [SerializeField]
    private GameObject _shieldPreFab;
   
    

    private bool _isDead = false;


    public void startSpawning(){
        StartCoroutine(spawnSpeedUpRoutine());
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerUpRoutine());
        StartCoroutine(spawnShieldPowerUpRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    //Spawn game objects every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield events
    //While loop = infinite game loop

    IEnumerator spawnEnemyRoutine(){
       yield return new WaitForSeconds(3.0f);
       while(_isDead == false){
        Vector3 posToSpawn = new Vector3(Random.Range(-6,6),7,0);
        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn,Quaternion.identity);
        newEnemy.transform.parent = _enemyContianer.transform;
        yield return new WaitForSeconds(1.0f);
       }
       //Will never exit the while loop, game will only end if player dies or something else happens
    }
    IEnumerator spawnPowerUpRoutine(){
        yield return new WaitForSeconds(3.0f);
        while(_isDead == false){
            Vector3 posToSpawn = new Vector3(Random.Range(-8,8),7,0);
            Instantiate(_TripleShotPreFab, posToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
            
        }

    }
    IEnumerator spawnSpeedUpRoutine(){
        while(_isDead == false){
            Vector3 posToSpawn = new Vector3(Random.Range(-8,8), 7, 0);
            Instantiate(_speedUpPreFab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }

    IEnumerator spawnShieldPowerUpRoutine(){
        while(_isDead == false){
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
            Instantiate(_shieldPreFab, posToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,6));
        }
    }

    public void onPlayerDeath(){
        _isDead = true;
    }
}
