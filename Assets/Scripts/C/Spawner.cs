using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Spawner : MonoBehaviour {

    [SerializeField]
    GameObject skeletonPrefab;

    Camera screen;

	// Use this for initialization
	void Start () {
        screen = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
