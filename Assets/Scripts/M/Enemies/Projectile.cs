using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Enemy {

    Vector3 direction;

    protected override void Behaviour()
    {
        transform.position += (direction * (Time.deltaTime * speed));
    }

    protected override void Initialisation()
    {

    }

    protected override void OnDestroyBehaviour()
    {
    }

    public void SetDirection(Vector3 _direction) { direction = _direction; }

    private new void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.tag == "Border" || _collider.tag == "Player")
            Destroy(gameObject);
    }
}
