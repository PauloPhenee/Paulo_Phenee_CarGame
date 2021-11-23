using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LeftTurnInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent LeftTurn;
    public UnityEvent StopLeftTurn;

    public void OnPointerDown(PointerEventData eventData)
    {
        LeftTurn.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopLeftTurn.Invoke();
    }

    private void Awake()
    {
        LeftTurn = new UnityEvent();
        StopLeftTurn = new UnityEvent();
    }
}

