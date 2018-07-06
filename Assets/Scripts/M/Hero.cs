using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    float rage = 0;
    int score = 0;
    [SerializeField]
    int maxHealth = 2;
    int health;

	// Use this for initialization
	void Start () {
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

    public float getRage() { return rage; }
    public int getScore() { return score; }
    public int getHealth() { return health; }

    public void addRage(float _value) {
        if(rage + _value >= 100)
        {
            rage = 100; //maximum rage
        } else
        {
            rage += _value;
        }
    }

    public void addScore(int _value)
    {
        score += _value;
    }
}
