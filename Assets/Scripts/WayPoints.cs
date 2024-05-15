using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public Transform otherWayPoint;

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.GetComponent<EnemyController>() != null)
        {
             hit.GetComponent<EnemyController>().currentWayPoint = otherWayPoint;
        }
    }
}
