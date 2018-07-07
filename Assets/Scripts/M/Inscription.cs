using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inscription : MonoBehaviour
{
    [SerializeField]
    string inscriptionName;
    [SerializeField]
    bool[] activationMatrix = new bool[16];

    void Start()
    {
    }

    public bool[] GetMatrix() { return activationMatrix; }
}