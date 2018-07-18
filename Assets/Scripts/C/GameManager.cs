using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InscriptionView))]
[RequireComponent(typeof(InscriptionController))]
public class GameManager : MonoBehaviour {

    //PLAYER
    [SerializeField]
    GameObject GOhero;
    Hero hero;
    private float oldRage = 0;
    private float oldScore = 0;
    private float oldHealth = 0;


    //SPAWNER
    [SerializeField]
    float randomGeneration = 1f;
    float skeletonSpawnFrequency = 2f;
    float archerSpawnFrequency = 5f;
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

    [SerializeField]
    GameObject pauseCanvas;
    [SerializeField]
    GameObject restartButton;

    //INSCRIPTIONS
    InscriptionView Iview;
    InscriptionController Icontroller;
    bool awakenFromInscription = false;
    float inscriptionTimer = 0f;
    float inscriptionDuration = 0f;
    float decrementalUnit = 0f;

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
        Icontroller = GetComponent<InscriptionController>();
        restartButton.GetComponent<Button>().onClick.AddListener(OnClickRestart);


        //spawner
        spawner = cam.GetComponent<Spawner>();
        spawner.SetHero(GOhero);
        spawner.SetRandomGeneration(randomGeneration);
        spawner.SetSkeletonSpawnFrequency(skeletonSpawnFrequency);
        spawner.SetArcherSpawnFrequency(archerSpawnFrequency);

        //view
        if(UIcanvas != null)
        {
            UpdateScore();
            UpdateRage();
            UpdateHealth();
        }

        pauseCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if(hero.GetHealth() <= 0)
        {
            PauseGame();
        }

        if (hero.GetHealth() != oldHealth)
            UpdateHealth();

        if (hero.GetRage() != oldRage)
            UpdateRage();

        if(hero.GetScore() != oldScore)
            UpdateScore();
        
        oldHealth = hero.GetHealth();
        oldRage = hero.GetRage();
        oldScore = hero.GetScore();

        if(awakenFromInscription)
        {
            if (inscriptionDuration <= 0)
            {
                inscriptionDuration = 0;
                awakenFromInscription = false;
            }
            else
            {
                inscriptionDuration -= Time.deltaTime;
                hero.AddRage(-decrementalUnit);
            }
        }


        if(hero.GetRage() == 100 && !Iview.IsEnabled() && !awakenFromInscription)
        {
            Debug.Log("GM : Switch View");
            Iview.SwitchView();
            SwitchUnits(!Iview.isActiveAndEnabled);
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
        pauseCanvas.SetActive(true);
    }

    public void ContinueGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void AwakeInscription()
    {
        inscriptionTimer = Icontroller.GetCastedInscription().GetComponent<Inscription>().GetDuration();
        inscriptionDuration = inscriptionTimer;
        decrementalUnit = (hero.GetMaximumRage() * Time.deltaTime * 10) / inscriptionTimer;
        awakenFromInscription = true;
    }

    private void OnClickRestart()
    {
        //clear board
        foreach(Transform t in enemies.transform)
        {
            Destroy(t.gameObject);
        }

        //reset player
        GOhero.transform.position = new Vector3(0, 0, 0);
        hero.ResetHero();
        Icontroller.DestroyCastedInscription();

        //
        ContinueGame();
    }

    public void SetHero(GameObject _hero) { GOhero = _hero; }
    public GameObject GetHero() { return GOhero; }

}
