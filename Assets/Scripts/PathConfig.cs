using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PathConfig", fileName = "New Config")]
public class PathConfig : ScriptableObject
{
    [SerializeField] public new string name;
    [Header("Path Prefab")]
    [SerializeField] Transform path;

    public List<Transform> getPoints()
    {
        List<Transform> points = new List<Transform>();
        foreach (Transform t in path)
        {
            points.Add(t);
        }

        return points;
    }

    public Transform getStartingPoint()
    {
        return getPoints()[0];
    }

    public Transform getPointAt(int index)
    {
        return getPoints()[index];
    }

    public int getPathLenght()
    {
        return getPoints().Count;
    }

}
