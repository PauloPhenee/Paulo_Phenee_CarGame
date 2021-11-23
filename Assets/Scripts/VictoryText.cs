using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{
    Color[] colors;
    Color startingColor;
    Color targetColor;
    float timer;
    float duration;
    bool started;

    private void GetColor()
    {
        targetColor = colors[Random.Range(0, colors.Length)];
        while(startingColor == targetColor)
        {
            targetColor = colors[Random.Range(0, colors.Length)];
        }
    }

    public void Initalization()
    {
        started = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        colors = new Color[] { Color.white, Color.green, Color.blue, Color.red, Color.yellow };
        startingColor = colors[Random.Range(0, colors.Length)];
        GetColor();
        timer = 0.0f;
        duration = 2.0f;
        started = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(started)
        {
            timer += Time.deltaTime;
            gameObject.GetComponent<Text>().color = Color.Lerp(startingColor, targetColor, timer / duration);

            if (timer >= duration)
            {
                timer = 0.0f;
                startingColor = targetColor;
                GetColor();
            }
        }
    }
}
