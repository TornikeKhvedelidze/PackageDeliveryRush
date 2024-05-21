using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    public float Distance = 12f;
    public int Count = 40;
    public GameObject Object;

    void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate(Object, transform.position + Vector3.forward * Distance * i, Quaternion.identity);
        }
    }
}
