using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    bool tween;
    float timer;
    float duration;
    Color[] colors;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject playerCar = GameObject.FindGameObjectWithTag("Player");
        if (collision.gameObject == playerCar)
        {
            playerCar.GetComponent<PlayerController>().StopRun();
            tween = true;
        }
    }

    private void Awake()
    {
        colors = new Color[] { Color.white, Color.red };
        duration = 2.0f;
    }

    private void Update()
    {
        if (tween)
        {
            gameObject.GetComponent<Image>().color = Color.Lerp(colors[0], colors[1], timer / duration);
            timer += Time.deltaTime;

            if (timer >= duration)
            {
                Color temp = colors[0];
                colors[0] = colors[1];
                colors[1] = temp;
                timer = 0;
            }
        }
    }
}
