using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {
    
    [SerializeField]
    private GameObject target;

	// Update is called once per frame
    protected override void Behaviour()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    protected override void Initialisation()
    {
    }

    protected override void OnDestroyBehaviour()
    {
    }

    public void SetTarget(GameObject _target) { target = _target; }
}
