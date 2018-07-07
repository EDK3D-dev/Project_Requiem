using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InscriptionController))]
[RequireComponent(typeof(GameManager))]
public class InscriptionView : MonoBehaviour {

    InscriptionController ic;
    GameManager gm;

    [SerializeField]
    GameObject VInscription;
    [SerializeField]
    GameObject VIcanvas;
    [SerializeField]
    List<GameObject> VIButtons = new List<GameObject>();
    [SerializeField]
    GameObject VIlabel;

    private bool[] input = new bool[16];

    void Start()
    {
        gm = GetComponent<GameManager>();
        ic = GetComponent<InscriptionController>();
        ic.SetButtons(VIButtons);
        ic.SetInscriptionView(this);
        VInscription.SetActive(false);
    }

    void Update()
    {
        if(VInscription.activeSelf)
        {
            VIlabel.GetComponent<Text>().text = ic.GetTimer().ToString();
        }
    }

    public void SwitchView()
    {
        Debug.Log("IV : SwitchView");
        VInscription.SetActive(!VInscription.activeSelf);
        if(VInscription.activeSelf)
        {
            ic.Activate();
            VIlabel.GetComponent<Text>().text = ic.GetTimer().ToString();
        } else
        {
            gm.SwitchUnits(true);
        }
    }

    public bool IsEnabled() { return VInscription.activeSelf; }
}
