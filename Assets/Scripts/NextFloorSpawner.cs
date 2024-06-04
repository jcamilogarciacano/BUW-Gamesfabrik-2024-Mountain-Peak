using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloorSpawner : MonoBehaviour
{
    public GameObject[] nextFloors; // Array of possible next floors

    public GameObject[] towers; // Array of possible next floors
    public Transform spawnPoint; // Spawn point for the next floor

    public Transform towerSpawnPoint; // Spawn point for the next floor
    float spawnInterval = 4f; // Time interval between spawning next floors

    float spawnTowerInterval = 24; // Time interval between spawning next towers
    float timer = 0f; // Timer to keep track of elapsed time

    //create a variable to store all previosly spawned floors
    List<GameObject> previousFloors = new List<GameObject>();
    float towerTimer = 0f; // Timer to keep track of elapsed time
    // Start is called before the first frame update
    void Start()
    {
        SpawnNextFloor();

        SpawnNextTower();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime; // Increase the timer by the time passed since the last frame
        towerTimer += Time.deltaTime; // Increase the timer by the time passed since the last frame

        if (timer >= spawnInterval)
        {
            SpawnNextFloor(); // Spawn the next floor

          
            timer = 0f; // Reset the timer
        }

          if (towerTimer >= spawnTowerInterval)
            {
                SpawnNextTower(); // Spawn the next floor
                towerTimer = 0f; // Reset the timer
            }

        //DespawnPreviousFloors(); // Despawn previous floors after 15 seconds
        //DespawnPreviousTowers(); // Despawn previous floors after 15 seconds
    }

    void DespawnPreviousFloors()
    {
        // Get all the spawned floors in the scene
        GameObject[] spawnedFloors = GameObject.FindGameObjectsWithTag("LevelFloor");

        // Iterate through each spawned floor
        foreach (GameObject floor in spawnedFloors)
        {
            // Destroy the floor after 15 seconds
            Destroy(floor, 120f);
        }
    }

    void DespawnPreviousTowers()
    {
        // Get all the spawned floors in the scene
        GameObject[] spawnedTowers = GameObject.FindGameObjectsWithTag("Tower");

        // Iterate through each spawned floor
        foreach (GameObject tower in spawnedTowers)
        {
            // Destroy the floor after 15 seconds
            Destroy(tower, 210f);
        }
    }
    void SpawnNextFloor()
    {
        // Randomly select a next floor from the array
        int randomIndex = Random.Range(0, nextFloors.Length);
        GameObject nextFloor = nextFloors[randomIndex];

        // Check if the next floor is the same as the previously spawned floor
        if (previousFloors.Count > 0)
        {
            while (previousFloors[previousFloors.Count - 1].name == nextFloor.name)
            {
                // Select another random index
                randomIndex = Random.Range(0, nextFloors.Length);
                nextFloor = nextFloors[randomIndex];
            }
        }

        // Add the next floor to the previously spawned floors list
        previousFloors.Add(nextFloor);

        // Spawn the next floor at the spawn point
        Instantiate(nextFloor, spawnPoint.position, Quaternion.identity);

        // Update the spawn point for the next floor
        spawnPoint.position += Vector3.up * 8f;
    }
    void SpawnNextTower()
    {
        // Randomly select a next floor from the array
        int randomIndex = Random.Range(0, towers.Length);
        GameObject nextTower = towers[randomIndex];

        // Spawn the next floor at the spawn point
        Instantiate(nextTower, towerSpawnPoint.position, Quaternion.identity);

        // Update the spawn point for the next floor
        towerSpawnPoint.position += Vector3.up * 140f;
    }
}
