using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hero))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMotor : MonoBehaviour {

    Hero hero;
    Rigidbody2D rb;
    BoxCollider2D col;

    [SerializeField]
    float speed = 2f;
    [SerializeField]
    float chargeSpeed = 5f;
    [SerializeField]
    float rangeRadius = 2f;

    private GameObject target;

	// Use this for initialization
	void Start () {
        hero = GetComponent<Hero>();
        col = GetComponent<BoxCollider2D>();
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
        target = _target;
        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, chargeSpeed * Time.deltaTime);
        
        //hero.Attack(_target);
    }

    public Vector2 getPosition() { return transform.position; }

    public void OnTriggerEnter2D(Collider2D _collider)
    {
        Debug.Log("hero collision : " + _collider.gameObject.name);
        if (_collider.gameObject.Equals(target))
        {
            hero.addRage(target.gameObject.GetComponent<Enemy>().rageValue);
            hero.addScore(target.gameObject.GetComponent<Enemy>().scoreValue);
            Destroy(target);
        }
    }
}
