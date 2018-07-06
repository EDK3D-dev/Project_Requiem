using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

    [SerializeField]
    float speed = 1f;

    [SerializeField]
    private GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
	}

    private void OnDestroy()
    {
        
    }

    public void setTarget(GameObject _target) { target = _target; }
}
