using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hero))]
public class PlayerMotor : MonoBehaviour {

    Hero hero;

    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float chargeSpeed = 5f;
    [SerializeField]
    float rangeRadius = 2f;

	// Use this for initialization
	void Start () {
        hero = GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveTo(Vector2 _position)
    {
        //Debug.Log(_position + " | " + transform.position);
        transform.position = Vector2.MoveTowards(transform.position, _position, speed * Time.deltaTime);
    }

    public bool isInRange(GameObject _target)
    {
        return Vector2.Distance(transform.position, _target.transform.position) <= rangeRadius;
    }

    public void Attack(GameObject _target)
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, chargeSpeed * Time.deltaTime);

        //hero.Attack(_target);
    }

    public Vector2 getPosition() { return transform.position; }
}
