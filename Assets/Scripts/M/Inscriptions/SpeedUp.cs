using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Inscription {

    PlayerMotor motor;
    Hero hero;
    SpriteRenderer sr;

    [SerializeField]
    float speedRatio = 1.5f;
    float initialSpeed = 0f;

    [SerializeField]
    float scoreModifier = 2f;
    float initialScoreModifier = 0f;

    protected override void Behaviour()
    {
    }

    protected override void Initialisation()
    {
        motor = player.GetComponent<PlayerMotor>();
        hero = player.GetComponent<Hero>();
        sr = player.GetComponent<SpriteRenderer>();

        initialSpeed = motor.GetSpeed();
        motor.SetSpeed(initialSpeed * speedRatio);

        initialScoreModifier = hero.GetScoreModifier();
        hero.SetScoreModifier(scoreModifier);

        sr.color = Color.yellow;
    }

    protected override void OnDestroyBehaviour()
    {
        motor.SetSpeed(initialSpeed);
        hero.SetScoreModifier(initialScoreModifier);
        sr.color = Color.white;
    }
}
