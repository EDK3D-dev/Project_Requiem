using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    GameObject hero;

    [SerializeField]
    Camera cam;

    [SerializeField]
    float randomGeneration = 0.5f;
    float skeletonSpawnFrequency = 2f;
    Spawner spawner;

	// Use this for initialization
	void Start () {
        if (hero == null) { Debug.Log("GM : hero not found"); }
        if (cam == null) { Debug.Log("GM : camera not found"); }

        spawner = cam.GetComponent<Spawner>();
        spawner.setHero(hero);
        spawner.setRandomGeneration(randomGeneration);
        spawner.setSkeletonSpawnFrequency(skeletonSpawnFrequency);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setHero(GameObject _hero) { hero = _hero; }
    public GameObject getHero() { return hero; }
}
