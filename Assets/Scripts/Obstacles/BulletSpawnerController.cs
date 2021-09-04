using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerController : MonoBehaviour
{
    public GameObject[] Spawners;
    public float spawnDeley;
    public float warningTime;
    private bool doSpawns = true;
    public GameObject bulletPrefab;
    public float speedDif;
    public GameObject BulletSpawnEffect;
    private void Awake()
    {
        GameEvents.instance.onCarStop += StopSpawns;
    }
    private void OnDestroy()
    {
        GameEvents.instance.onCarStop -= StopSpawns;
    }
    private void StopSpawns()
    {
        doSpawns = false;
    }

    private void Start()
    {
        StartCoroutine("SpawnRoutine");
    }

    IEnumerator SpawnBullet()
    {
        int spawnerToUse = Random.Range(0, 3);
        Debug.Log("BULLET IN: " + spawnerToUse.ToString());
        Instantiate(BulletSpawnEffect, Spawners[spawnerToUse].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(warningTime);
        GameObject bullet = Instantiate(bulletPrefab, Spawners[spawnerToUse].transform.position, Quaternion.identity);
        bullet.GetComponent<Obstacle>().speedOnSpawn = Score.instance.speed + speedDif;
    }

    IEnumerator SpawnRoutine()
    {
        do
        {
            yield return new WaitForSeconds(spawnDeley + warningTime);
            if (doSpawns)
                StartCoroutine("SpawnBullet");
        } while (doSpawns);
    }
}
