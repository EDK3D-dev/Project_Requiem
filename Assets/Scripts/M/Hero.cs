using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    int rageLevel;
    [SerializeField]
    int maxHealth = 2;
    int health;

	// Use this for initialization
	void Start () {
        rageLevel = 0;
        health = maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack(GameObject _target)
    {

    }

    public void Hit(GameObject _enemy, int damage)
    {
        if(health - damage > 0)
        {
            health -= damage;
        } else
        {
            Debug.Log("death");
            health = 0;
        }
        Debug.Log("health : " + health);
    }
}
