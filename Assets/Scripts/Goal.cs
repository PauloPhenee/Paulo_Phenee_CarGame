using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    GameObject StartingPosition;
    public GameObject FinishLine;

    public float rotation;

    [HideInInspector]
    public Vector3 start;
    [HideInInspector]
    public Vector3 finish;

    private void Awake()
    {
        start = StartingPosition.transform.position;
        finish = FinishLine.transform.position;
    }
}
