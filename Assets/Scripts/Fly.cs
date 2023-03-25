using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Rigidbody2D rb;
    private int index = 0;
    private int count = 0;
    private int selfDestructTimeInSec = 60;
    [HideInInspector] public PathConfig pathConfig;
    private bool hitByEnemy = false;
    private FlySpawner flySpawner;
    private int speed = 1;


    private void Awake()
    {
        flySpawner = FindObjectOfType<FlySpawner>();
    }


    void Start()
    {
        count = pathConfig.getPathLenght();
        rb = GetComponent<Rigidbody2D>();
        transform.position = pathConfig.getStartingPoint().position;
        StartCoroutine(SelfDestruct());
        print(pathConfig.name + " " + pathConfig.getPoints().Count);
    }

    private void StartMoving()
    {
        if (index < count)
        {
            speed = flySpawner.getRandomSpeed();
            transform.position = Vector2.MoveTowards(transform.position, pathConfig.getPointAt(index).position, speed * Time.deltaTime);
            FlipCharcter();
        }

        if (index < count && transform.position == pathConfig.getPointAt(index).position)
        {
            index++;
            print("@@@ if " + index);
        }
        else if (index == count)
        {
            index = 0;
            print("@@@ else " + index);
        }
    }


    private void FlipCharcter()
    {
        if (transform.position.x < pathConfig.getPointAt(index).position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
       
    }

    void Update()
    {
        if (!hitByEnemy)
        {
            StartMoving();
        }
        else
        {
            if (gameObject != null)
            {
                GetComponent<Animator>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Web"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            hitByEnemy = true;
        }
        else if (collision.tag.Equals("Spyder") || collision.tag.Equals("Ground"))
        {
            hitByEnemy = true;
            Destroy(gameObject);
        }
    }


    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTimeInSec);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
