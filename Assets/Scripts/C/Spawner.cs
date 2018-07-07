using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Spawner : MonoBehaviour {

    GameObject hero;

    //skeletons properties
    [SerializeField]
    GameObject skeletonPrefab;
    float skeletonSpawnFrequency = 2f; //redefined in GameManager
    float randomGeneration = 0.5f; //redefined in GameManager
    private bool skeletonSpawning = false;

    [SerializeField]
    GameObject enemies;
    Camera screen;
    public bool active = true;

    private Vector2 screenSize;
    private Transform topCollider;
    private Transform bottomCollider;
    private Transform rightCollider;
    private Transform leftCollider;
    private List<Transform> colliders;

    // Use this for initialization
    void Start() {
        screen = GetComponent<Camera>();

        //generate empty objects for border colliders
        topCollider = new GameObject().transform;
        bottomCollider = new GameObject().transform;
        rightCollider = new GameObject().transform;
        leftCollider = new GameObject().transform;

        //name border colliders
        topCollider.name = "TopCollider";
        bottomCollider.name = "BottomCollider";
        rightCollider.name = "RightCollider";
        leftCollider.name = "LeftCollider";

        //add edge colliders
        topCollider.gameObject.AddComponent<EdgeCollider2D>();
        bottomCollider.gameObject.AddComponent<EdgeCollider2D>();
        rightCollider.gameObject.AddComponent<EdgeCollider2D>();
        leftCollider.gameObject.AddComponent<EdgeCollider2D>();

        //add colliders to the list
        colliders = new List<Transform>();
        colliders.Add(topCollider);
        colliders.Add(bottomCollider);
        colliders.Add(rightCollider);
        colliders.Add(leftCollider);
        
        //set to trigger
        foreach(Transform t in colliders)
        {
            t.gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;
        }

        //parent to camera (for resizing purpose)
        topCollider.parent = transform;
        bottomCollider.parent = transform;
        rightCollider.parent = transform;
        leftCollider.parent = transform;

        //compute corner points
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) *0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        Vector2 topLeft = new Vector2(-screenSize.x, screenSize.y);
        Vector2 topRight = new Vector2(screenSize.x, screenSize.y);
        Vector2 bottomLeft = new Vector2(-screenSize.x, -screenSize.y);
        Vector2 bottomRight = new Vector2(screenSize.x, -screenSize.y);

        //add points to edge colliders
        topCollider.GetComponent<EdgeCollider2D>().points = new Vector2[]
        { topLeft, topRight };
        bottomCollider.GetComponent<EdgeCollider2D>().points = new Vector2[]
        { bottomLeft, bottomRight };
        rightCollider.GetComponent<EdgeCollider2D>().points = new Vector2[]
        { bottomRight, topRight };
        leftCollider.GetComponent<EdgeCollider2D>().points = new Vector2[]
        { bottomLeft, topLeft };
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (!skeletonSpawning) StartCoroutine(SpawnSkeleton());
        }
	}

    IEnumerator SpawnSkeleton()
    {
        skeletonSpawning = true;
        yield return new WaitForSeconds(skeletonSpawnFrequency);
        foreach(Transform c in colliders)
        {
            EdgeCollider2D ec = c.gameObject.GetComponent<EdgeCollider2D>();
            StartCoroutine(SpawnSkeletonBorders(ec));
        }
        skeletonSpawning = false;
    }

    IEnumerator SpawnSkeletonBorders(EdgeCollider2D _ec)
    {
        yield return new WaitForSeconds(Random.Range(0, randomGeneration));
        Vector3 pos = new Vector3(Random.Range(_ec.points[0].x, _ec.points[1].x), Random.Range(_ec.points[0].y, _ec.points[1].y), 0);
        GameObject skel = Instantiate(skeletonPrefab, pos, transform.localRotation);
        skel.transform.parent = enemies.transform;
        skel.GetComponent<Skeleton>().SetTarget(hero);
    }

    public void SwitchSpawning(bool _state)
    {
        active = _state;
        if(!active) StopAllCoroutines();
    }

    public void SetHero(GameObject _hero) { hero = _hero; }
    public void SetRandomGeneration(float _frequency) { randomGeneration = _frequency; }
    public void SetSkeletonSpawnFrequency(float _frequency) { skeletonSpawnFrequency = _frequency; }
}
