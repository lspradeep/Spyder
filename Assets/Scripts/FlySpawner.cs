using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{

    [SerializeField] List<Transform> paths;
    [SerializeField] GameObject fly;
    [SerializeField] bool spawn = false;

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
            Instantiate(fly, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(10);
        }
    }

    public Transform GetAPath()
    {
        return paths[0];
    }
}
