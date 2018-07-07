using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InscriptionView))]
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

    InscriptionView Iview;

    //UTILITIES
    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject enemies;

    // Use this for initialization
    void Start () {
        if (GOhero == null) { Debug.Log("GM : hero not found"); }
        if (cam == null) { Debug.Log("GM : camera not found"); }

        //init
        hero = GOhero.gameObject.GetComponent<Hero>();
        Iview = GetComponent<InscriptionView>();

        //spawner
        spawner = cam.GetComponent<Spawner>();
        spawner.SetHero(GOhero);
        spawner.SetRandomGeneration(randomGeneration);
        spawner.SetSkeletonSpawnFrequency(skeletonSpawnFrequency);

        //view
        UIcanvas = UI.GetComponentInChildren<Canvas>().gameObject;
        if(UIcanvas != null)
        {
            UpdateScore();
            UpdateRage();
            UpdateHealth();
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (hero.GetRage() != oldRage)
        {
            Debug.Log("rage : " + hero.GetRage());
            UpdateRage();
        }
        if(hero.GetScore() != oldScore)
        {
            Debug.Log("score : " + hero.GetScore());
            UpdateScore();
        }

        oldRage = hero.GetRage();
        oldScore = hero.GetScore();

        if(hero.GetRage() == 20 && !Iview.IsEnabled()) //!!!!!!!!CHANGE
        {
            Iview.SwitchView();
            SwitchUnits(!Iview.isActiveAndEnabled);
            hero.AddRage(-1f);
        }
        
	}

    public void UpdateScore()
    {
        if (UIscore != null) { UIscore.GetComponent<Text>().text = " Score : " + hero.GetScore(); }
    }

    public void UpdateRage()
    {
        if (UIrage != null) { UIrage.GetComponent<Text>().text = "  Rage : " + hero.GetRage(); }
    }

    public void UpdateHealth()
    {
        if (UIhealth != null) { UIhealth.GetComponent<Text>().text = "Health : " + hero.GetHealth(); }
    }

    public void SwitchUnits(bool _state)
    {
        GOhero.GetComponent<PlayerMotor>().active = _state;
        foreach(Transform t in enemies.transform)
        {
            t.gameObject.GetComponent<Enemy>().active = _state;
        }
        spawner.SwitchSpawning(_state);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public void SetHero(GameObject _hero) { GOhero = _hero; }
    public GameObject GetHero() { return GOhero; }
}
