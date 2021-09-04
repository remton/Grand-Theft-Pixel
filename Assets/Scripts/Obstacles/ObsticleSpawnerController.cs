using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleSpawnerController : MonoBehaviour {


    // Relies on the fact that there are 3 spawners
    public GameObject[] Spawners;
    //public List<GameObject> obsticles_DEPRACATED;
    public List<Choosable> obsticles;


    // spawnDeley decreases by the spawnDeleyChangeRate until it is equal to minSpawnDeley.
    [Header("Spawn Deley (s)")]
    public float startSpawnDeley;
    public float minSpawnDeley;
    public float minDistToNextCar;
    [Tooltip("In miliseconds/second")]
    public float spawnDeleyChangeRate;
    public float spawnDeley;

    // spawnChance increases by the spawnChanceChangeRateuntil it is equal to maxSpawnChance.
    [Header("SpawnChance (%)")]
    public float startSpawnChance;
    public float maxSpawnChance;
    [Tooltip("In percent per second")]
    public float spawnChanceChangeRate;
    public float spawnChance;

    public float startSpeedDif;
    public float speedDifChangeRate;
    public float maxSpeedDif;
    public float speedDif;

    [Tooltip("If the waves are spawning fast enough, the game will gurentee a nearby open lane in each wave")]
    public bool useLaneProtection;
    [Tooltip("The max spawnDeley before the game uses lane protection")]
    public float laneProtectionDeley;

    private void Awake()
    {
        GameEvents.instance.onCarStop += StopSpawns;
        spawnDeley = startSpawnDeley;
        spawnChance = startSpawnChance;
        speedDif = startSpeedDif;
    }
    
    private bool doSpawns = true;
    private void StopSpawns()
    {
        doSpawns = false;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onCarStop -= StopSpawns;
    }


    private void Start()
    {
        StartCoroutine("SpawnRoutine");
    }
    private void Update()
    {
        // Spawn Deley
        if (spawnDeley > minSpawnDeley)
            spawnDeley -= spawnDeleyChangeRate / 1000 * Time.deltaTime;
        else
            spawnDeley = minSpawnDeley;
        // Spawn Chance
        if (spawnChance < maxSpawnChance)
            spawnChance += spawnChanceChangeRate * Time.deltaTime;
        else
            spawnChance = maxSpawnChance;

        //speedDif
        if (speedDif < maxSpeedDif)
            speedDif += speedDifChangeRate / 1000 * Time.deltaTime;
        else
            speedDif = maxSpeedDif;
    }

    bool[] lastSpawns = new bool[3]; //True means an obstacle spawned
    List<int> potentialOpenings = new List<int>();
    int opening; //this is the index of the guarenteed open lane
    private void SpawnWave()
    {
        potentialOpenings.Clear();
        
        if(useLaneProtection && spawnDeley < laneProtectionDeley)
        {
            // A list containing the indexes for the lanes is shuffled
            List<int> lanes = new List<int>();
            lanes.Add(0); lanes.Add(1); lanes.Add(2);
            lanes.Shuffle();
            // The list of lanes is looped through until an open space from the previous obstacle wave is found
            foreach (int i in lanes)
            {
                if (lastSpawns[i] == false)
                {
                    potentialOpenings.Add(i);
                    if (i - 1 >= 0)
                        potentialOpenings.Add(i - 1);
                    if (i + 1 < 3)
                        potentialOpenings.Add(i + 1);
                    break;
                }
            }
            opening = potentialOpenings[Random.Range(0, potentialOpenings.Count)];
        }
        else
        {
            opening = Random.Range(0, 3);
        }
        // Spawns obstacles Randomly at all lanes except the chosen open lane
        lastSpawns = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            if (i == opening)
                continue;
            if(spawnChance > Random.Range(0, 100))
            {
                lastSpawns[i] = true;
                GameObject obs = Instantiate(Util.ChooseRandom(obsticles), Spawners[i].transform.position, Quaternion.identity);
                obs.GetComponent<Obstacle>().speedOnSpawn = -Score.instance.speed - speedDif;
            }
        }
    }

    IEnumerator SpawnRoutine()
    {
        do{
            yield return new WaitForSeconds(spawnDeley);
            if(doSpawns)
                SpawnWave();
        } while (doSpawns);
    }





}
