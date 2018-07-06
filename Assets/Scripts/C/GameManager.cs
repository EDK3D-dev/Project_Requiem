using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //PLAYER
    [SerializeField]
    GameObject GOhero;
    Hero hero;
    private float oldRage = 0;
    private int oldScore = 0;


    //SPAWNER
    [SerializeField]
    float randomGeneration = 0.5f;
    float skeletonSpawnFrequency = 2f;
    Spawner spawner;

    //VIEW
    [SerializeField]
    GameObject UI;
    [SerializeField]
    GameObject UIcanvas;
    [SerializeField]
    GameObject UIscore;
    [SerializeField]
    GameObject UIrage;
    [SerializeField]
    GameObject UIhealth;

    //UTILITIES
    [SerializeField]
    Camera cam;

    // Use this for initialization
    void Start () {
        if (GOhero == null) { Debug.Log("GM : hero not found"); }
        if (cam == null) { Debug.Log("GM : camera not found"); }

        //init
        hero = GOhero.gameObject.GetComponent<Hero>();

        //spawner
        spawner = cam.GetComponent<Spawner>();
        spawner.setHero(GOhero);
        spawner.setRandomGeneration(randomGeneration);
        spawner.setSkeletonSpawnFrequency(skeletonSpawnFrequency);

        //view
        UIcanvas = UI.GetComponentInChildren<Canvas>().gameObject;
        if(UIcanvas != null)
        {
            updateScore();
            updateRage();
            updateHealth();
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (hero.getRage() != oldRage)
        {
            Debug.Log("rage : " + hero.getRage());
            updateRage();
        }
        if(hero.getScore() != oldScore)
        {
            Debug.Log("score : " + hero.getScore());
            updateScore();
        }

        oldRage = hero.getRage();
        oldScore = hero.getScore();
        
	}

    public void updateScore()
    {
        if (UIscore != null) { UIscore.GetComponent<Text>().text = " Score : " + hero.getScore(); }
    }

    public void updateRage()
    {
        if (UIrage != null) { UIrage.GetComponent<Text>().text = "  Rage : " + hero.getRage(); }
    }

    public void updateHealth()
    {
        if (UIhealth != null) { UIhealth.GetComponent<Text>().text = "Health : " + hero.getHealth(); }
    }

    public void setHero(GameObject _hero) { GOhero = _hero; }
    public GameObject getHero() { return GOhero; }
}
