using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InscriptionController : MonoBehaviour {

    InscriptionView iv;
    GameManager gm;

    [SerializeField]
    float inscriptionTimer = 10f;
    private float currentTimer = 0f;

    [SerializeField]
    List<GameObject> inscriptions = new List<GameObject>();
    bool[] currentMatrix = new bool[16];

    List<GameObject> buttons = new List<GameObject>();

    private bool active = false;

	// Use this for initialization
	void Start () {
        currentTimer = inscriptionTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if(active)
        {
            //TIMER
            if(currentTimer > 0)
            {
                currentTimer -= Time.deltaTime;
            } else
            {
                active = false;
                currentTimer = inscriptionTimer;
                iv.SwitchView();
            }

            //BUTTONS
            if(Input.GetMouseButton(0))
            {
                //Debug.Log("Down");
                foreach (GameObject go in buttons)
                {
                    InscriptionButton ibTemp = go.GetComponent<InscriptionButton>();
                    Image imgTemp = go.GetComponent<Image>();

                    ibTemp.isUsable = true;
                    if (ibTemp.isStartingPoint)
                        imgTemp.color = Color.red;
                    if (ibTemp.isDragged)
                        imgTemp.color = Color.yellow;
                    if (ibTemp.isEndingPoint)
                        imgTemp.color = Color.blue;
                }
            } else if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Release");
                for(int i = 0; i < buttons.Count; i++)
                {
                    GameObject go = buttons[i];
                    currentMatrix[i] = go.GetComponent<InscriptionButton>().hasBeenActivated();

                    go.GetComponent<InscriptionButton>().ResetState();
                    go.GetComponent<Image>().color = Color.white;

                }
            }
            
        }
	}

    private Inscription CheckInscription(bool[] _input)
    {
        foreach(GameObject go in inscriptions)
        {
            Inscription ins = go.GetComponent<Inscription>();
            if(ins.GetMatrix().Equals(_input))
            {
                return ins;
            }
        }
        return null;
    }

    public void Activate() { active = true; }
    public bool GetState() { return active; }

    public void SetButtons(List<GameObject> _buttons) {
        buttons = _buttons;
    }

    public void SetInscriptionView(InscriptionView _iv) { iv = _iv; }
    public float GetTimer() { return Mathf.Clamp(currentTimer, 0, inscriptionTimer); }
}
