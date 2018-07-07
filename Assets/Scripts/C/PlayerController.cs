using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    
    private PlayerMotor motor;

    [SerializeField]
    private LayerMask enemyLayer;

    Vector2 pos;

	// Use this for initialization
	void Start () {
        motor = GetComponent<PlayerMotor>();
    }
	
	// Update is called once per frame
	void Update () {
        if(enemyLayer == -1) { Debug.Log("error"); }
        if(Application.isMobilePlatform)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        } else
        {
            if(Input.GetMouseButton(0))
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }


        RaycastHit2D hit = Physics2D.Raycast(pos, pos, Mathf.Infinity, enemyLayer);
        if (hit && motor.IsInRange(hit.transform.gameObject))
        {
            motor.Attack(hit.transform.gameObject);
        }

        if (pos != motor.GetPosition())
            motor.MoveTo(pos);
	}

    public PlayerMotor GetPlayerMotor() { return motor; }
}
