using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public abstract class Enemy : MonoBehaviour {

    Rigidbody2D rb;
    BoxCollider2D col;

    [SerializeField]
    public int scoreValue = 50;
    [SerializeField]
    public float rageValue = 2.5f;
    [SerializeField]
    public float damage = 1f;

    [SerializeField]
    protected float speed = 1f;

    public bool active = true;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        Initialisation();
	}
	
	// Update is called once per frame
	void Update () {
        if (active) Behaviour();
	}

    private void OnDestroy()
    {
        OnDestroyBehaviour();
    }

    protected abstract void Behaviour();
    protected abstract void Initialisation();
    protected abstract void OnDestroyBehaviour();


    public void OnTriggerEnter2D(Collider2D _collider)
    {
        //Debug.Log("collision : " + _collider.gameObject.name);
    }
}
