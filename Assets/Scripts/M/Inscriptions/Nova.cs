using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nova : Inscription {

    Hero hero;
    PlayerMotor motor;
    SpriteRenderer sr;

    [SerializeField]
    float radius = 3f;
    [SerializeField]
    float speedModifier = 0.5f;
    float initialSpeed = 1f;

    protected override void Behaviour()
    {
    }

    protected override void Initialisation()
    {
        hero = player.GetComponent<Hero>();
        motor = player.GetComponent<PlayerMotor>();
        sr = player.GetComponent<SpriteRenderer>();

        initialSpeed = motor.GetSpeed();
        motor.SetSpeed(initialSpeed * speedModifier);

        hero.ClearArea(radius);

        sr.color = Color.blue;
    }

    protected override void OnDestroyBehaviour()
    {
        motor.SetSpeed(initialSpeed);
        sr.color = Color.white;
    }
}
