using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    float maximumRage = 100f;
    float rage = 0f;
    float score = 0;
    float scoreModifier = 1f;

    [SerializeField]
    float maxHealth = 2f;
    float health;

    //invulnerability 
    [SerializeField]
    float invulnerabilityFrameDuration = 1f;
    bool canBeHit = true;
    float invulnerabilityDuration = 0;
    float invulnerabilityTimer = 0;

    [SerializeField]
    public float clearRadius = 0.5f;

	// Use this for initialization
	void Start () {
        ResetHero();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(!canBeHit)
        {
            if (invulnerabilityDuration <= 0)
            {
                invulnerabilityDuration = 0;
                CanBeHit(true);
            } else 
                invulnerabilityDuration -= Time.deltaTime;
        }
	}

    public void Attack(GameObject _target)
    {
        if(_target != null)
        {
            Enemy enemy = _target.GetComponent<Enemy>();
            if(enemy != null)
            {
                AddRage(enemy.rageValue);
                AddScore(enemy.scoreValue);
                Destroy(_target);
            }
        }
    }

    public void Hit(GameObject _enemy, float damage)
    {
        if (canBeHit)
        {
            if (health - damage > 0)
            {
                health -= damage;
                SetInvulnerability(invulnerabilityFrameDuration);
            }
            else
            {
                Debug.Log("death");
                health = 0;
            }
        }
    }

    public float GetRage() { return rage; }
    public float GetScore() { return score; }
    public float GetHealth() { return health; }
    public void AddHealth(float _value) { health += _value; }

    public void CanBeHit(bool _state) { canBeHit = _state; }
    public void SetInvulnerability(float _timer)
    {
        Debug.Log("Hero : Invulnerability ("+_timer+")");
        CanBeHit(false);
        invulnerabilityTimer = _timer;
        invulnerabilityDuration = invulnerabilityTimer;
    }

    public void AddRage(float _value) {
        if(rage + _value >= maximumRage)
        {
            rage = maximumRage;
        } else if (rage + _value <= 0)
        {
            rage = 0;
        } else
        {
            rage += _value;
        }
    }

    public void AddScore(float _value)
    {
        score += (_value * scoreModifier);
    }

    public void ClearArea(float _radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _radius);
        foreach (Collider2D collider in hitColliders)
        {
            Attack(collider.gameObject);
        }
    }

    public void ResetRage() { rage = 0; }
    public float GetMaximumRage() { return maximumRage; }
    public float GetScoreModifier() { return scoreModifier; }
    public void SetScoreModifier(float _value) { scoreModifier = _value; }
    public void ResetHero()
    {
        health = maxHealth;
        rage = 0;
        score = 0;
        scoreModifier = 1f;
        canBeHit = true;
    }
}
