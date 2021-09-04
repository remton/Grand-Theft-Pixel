//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Spawner : MonoBehaviour {

//    public GameObject obstacle;

//    private float timeBtwSpawn;
//    public float startTimeBtwSpawn = 3.0f;
//    public float decreaseTime = 0.1f;
//    public float minTime = 1.2f;
//    public float chance = 6;
//    public float spawnNum = 0;
//    public bool willSpawn = false;

//    public GameObject otherSpawnA;
//    public GameObject otherSpawnB;


//    private void Update()
//    {

//        spawnNum = Random.Range(1, 10);

//        if(spawnNum < chance)
//        {
//            willSpawn = true;
//        }
//        else
//        {
//            willSpawn = false;
//        }
//        if(otherSpawnA.GetComponent<Spawner>().willSpawn == true && otherSpawnB.GetComponent<Spawner>().willSpawn == true)
//        {
//            willSpawn = false;
//        }


//        if (timeBtwSpawn <= 0)
//        {
//            if(willSpawn == true)
//            {
//                Instantiate(obstacle, transform.position, Quaternion.identity);
//            }

//            timeBtwSpawn = startTimeBtwSpawn;

//            if (startTimeBtwSpawn >= minTime) {
//                startTimeBtwSpawn -= decreaseTime;
//            }
//        }
//        else
//        {
//            timeBtwSpawn -= Time.deltaTime;
//        }
//    }
//}
