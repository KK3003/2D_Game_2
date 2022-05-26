using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Spawns enemies during the boss battle
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy; // the enemy gameobject
    public float spawnDelay; // the time difference between two fishesh

    bool canSpawn; // ensures that enemies can spawn after some delay


    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);// spawns the jumping fish
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
