using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy {

    [SerializeField]
    private GameObject target;
    [SerializeField]
    float timeToPosition = 2f;
    float timeToPositionDuration = 0f;
    bool inPosition = false;
    
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    float timeToReload = 2f;

    protected override void Initialisation()
    {
        timeToPositionDuration = timeToPosition;
    }

    protected override void Behaviour()
    {
        if (target != null)
        {
            if(!inPosition)
            {
                if (timeToPositionDuration <= 0)
                {
                    inPosition = true;
                }
                else
                {
                    timeToPositionDuration -= Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                }
            } else
            {
                //start shooting 
            }
        }
    }


    protected override void OnDestroyBehaviour()
    {
    }

    public void SetTarget(GameObject _target) { target = _target; }
}
