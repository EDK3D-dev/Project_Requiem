using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inscription : MonoBehaviour
{
    [SerializeField]
    string inscriptionName;
    [SerializeField]
    bool[] activationMatrix = new bool[16];

    bool isInitialised = false;

    [SerializeField]
    protected float duration = 0f;
    private float durationTimer = 0f;

    protected GameObject player;

    void Start()
    {
        durationTimer = duration;
    }

    private void Update()
    {
        if(player != null)
        {
            if (!isInitialised)
            {
                Initialisation();
                isInitialised = true;
            }
            else
            {
                if (durationTimer <= 0)
                {
                    durationTimer = 0;
                    Destroy(gameObject);
                }

                durationTimer -= Time.deltaTime;

                Behaviour();
            }
        }
    }

    private void OnDestroy()
    {
        OnDestroyBehaviour();
    }

    protected abstract void Initialisation();
    protected abstract void Behaviour();
    protected abstract void OnDestroyBehaviour();

    public void SetDuration(float _duration) { duration = _duration; }
    public float GetDuration() { return duration; }
    public void SetPlayer(GameObject _player) { player = _player; }
    public bool[] GetMatrix() { return activationMatrix; }
    public bool CompareMatrix(bool[] _matrix)
    {
        for(int i = 0; i < activationMatrix.Length; i++)
            if (activationMatrix[i] != _matrix[i])
                return false;
        return true;
    }
}