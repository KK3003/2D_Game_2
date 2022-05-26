using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this script spawns or shows the jumping fish ,  every n no. of seconds
/// </summary>
public class FishSpawner : MonoBehaviour
{
   
    public GameObject fish; // the jumpingfish gameobject
    public float spawnDelay; // the time difference between two fishesh

    bool canSpawn; // ensures that fishesh can spawn after some delay

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            StartCoroutine("SpawnFish");
        }
    }

    IEnumerator SpawnFish()
    {
        
        Instantiate(fish, transform.position, Quaternion.identity);// spawns the jumping fish
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
