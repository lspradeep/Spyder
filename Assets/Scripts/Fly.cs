using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform path;
    private List<Vector3> points = new List<Vector3>();
    private int index = 0;
    private int count = 0;
    private FlySpawner flySpawner;

    private void Awake()
    {
        flySpawner = FindObjectOfType<FlySpawner>();
    }

    void Start()
    {
        path = flySpawner.GetAPath();
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform t in path)
        {
            points.Add(t.position);
        }
        count = points.Count;
        gameObject.transform.position = points[index];
    }

    private void StartMoving()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[index], 1f * Time.deltaTime);
        if (transform.position == points[index] && index < count - 1)
        {
            index++;
        }
        else if (index == count - 1)
        {
            index = 0;
        }
    }

    void Update()
    {
        StartMoving();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Web"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (collision.tag.Equals("Spyder") || collision.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
