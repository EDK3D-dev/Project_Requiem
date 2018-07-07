using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InscriptionController))]
public class InscriptionView : MonoBehaviour {

    InscriptionController ic;

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
        VInscription.SetActive(!VInscription.activeSelf);
        if(VInscription.activeSelf)
        {
            ic.Activate();
            VIlabel.GetComponent<Text>().text = ic.GetTimer().ToString();
        }
    }

    public bool IsEnabled() { return VInscription.activeSelf; }
}
