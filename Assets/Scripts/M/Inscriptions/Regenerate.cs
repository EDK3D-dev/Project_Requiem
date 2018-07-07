using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : Inscription
{
    Hero hero;
    SpriteRenderer sr;

    [SerializeField]
    float heal = 1f;

    [SerializeField]
    float scoreModifier = 0.5f;
    float initialScoreModifier = 0f;

    protected override void Behaviour()
    {
        
    }

    protected override void Initialisation()
    {
        hero = player.GetComponent<Hero>();
        sr = player.GetComponent<SpriteRenderer>();

        hero.AddHealth(heal);
        initialScoreModifier = hero.GetScoreModifier();
        hero.SetScoreModifier(scoreModifier);

        Color p = Color.magenta;
        p.a = 0.8f;
        sr.color = p;
    }

    protected override void OnDestroyBehaviour()
    {
        hero.SetScoreModifier(initialScoreModifier);

        Color w = Color.white;
        w.a = 1f;
        sr.color = w;
    }
}
