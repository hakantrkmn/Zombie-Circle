using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;

    public float spawnTime;

    private float timer;

    Transform area;

    Vector3 spawnPos;
    
    private void OnEnable()
    {
        EventManager.PlayerKilledZombie += PlayerKilledZombie;
    }

    private void OnDisable()
    {
        EventManager.PlayerKilledZombie -= PlayerKilledZombie;
    }
    public void PlayerKilledZombie()
    {
        spawnTime -= .005f;
    }
    void Start()
    {
        area = GameObject.FindObjectOfType<Area>().transform;
    }

    void Update()
    {
        SpawnZombie();
    }

    public void SpawnZombie()
    {
        
        timer += Time.deltaTime;
        if (timer>spawnTime)
        {
            GeneratePos();
            var temp = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
            timer = 0;
        }
    }

    public void GeneratePos()
    {
        var randPos = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        if (Vector3.Distance(randPos,area.transform.position)>20)
        {
            spawnPos = randPos;
        }
        else
        {
            GeneratePos();
        }
    }
}
