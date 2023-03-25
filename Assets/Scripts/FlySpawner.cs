using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{

    [Header("Prefabs Configs")]
    [SerializeField] bool spawn = false;
    [SerializeField] float timeBetweenSpawn = 5f;
    [SerializeField] List<PathConfig> pathConfigs;
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] int minSpeed = 1;
    [SerializeField] int maxSpeed = 8;

    // Start is called before the first frame update
    void Start()
    {
        if (spawn)
        {
            StartCoroutine(SpawnFlies());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnFlies()
    {
        while (spawn)
        {
            GameObject flyObj = Instantiate(prefabs[Random.Range(0, prefabs.Count)], transform.position, Quaternion.identity);
            Rigidbody2D rb = flyObj.GetComponent<Rigidbody2D>();
            Fly fly = flyObj.GetComponent<Fly>();
            if (fly != null)
            {
                fly.pathConfig = GetPathConfig();

            }
            //if (rb != null)
            //{
            //    rb.velocity = Vector2.one * getRandomSpeed();

            //}
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    public PathConfig GetPathConfig()
    {
        return pathConfigs[Random.Range(0, pathConfigs.Count)];
    }




    public int getRandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }
}
