using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class RightTurnInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent RightTurn;
    public UnityEvent StopRightTurn;

    public void OnPointerDown(PointerEventData eventData)
    {
        RightTurn.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopRightTurn.Invoke();
    }

    private void Awake()
    {
        RightTurn = new UnityEvent();
        StopRightTurn = new UnityEvent();
    }
}
