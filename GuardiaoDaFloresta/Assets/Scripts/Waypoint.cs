using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject waypoint;
    private float speed = 10;

    private void Update()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);
        transform.position = newPos;
        if (transform.position == waypoint.transform.position)
        {

            waypoint.SetActive(false);
        }
    }
}
