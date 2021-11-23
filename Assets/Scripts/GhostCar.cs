using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCar : MonoBehaviour
{
    Run run;
    Command currentCommand;
    float time;
    float commandTime;
    float speed;
    float turnSpeed;
    int step;
    bool finished;
    bool doingCommand;
    bool running;

    public void Initialization(Run _run, float _speed, float _turnSpeed)
    {        
        run = _run;
        gameObject.transform.position = run.startPosition;
        gameObject.transform.rotation = run.rotation;
        speed = _speed;
        turnSpeed = _turnSpeed;
        step = 0;
        time = 0.0f;
        currentCommand = run.commands[step];
        running = false;
    }

    public void Activation()
    {
        running = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        doingCommand = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            time += Time.deltaTime;
            if (run != null)
            {
                if (!finished)
                {
                    if (time >= run.commands[step].time)
                    {
                        currentCommand = run.commands[step];
                        doingCommand = true;
                    }
                }
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + Mathf.Cos(gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * speed * Time.deltaTime,
                                             gameObject.transform.position.y + Mathf.Sin(gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * speed * Time.deltaTime, 0);
                if (doingCommand)
                {
                    if (commandTime <= currentCommand.duration)
                    {
                        commandTime += Time.deltaTime;
                        if (currentCommand.Direction == Direction.Left)
                        {
                            gameObject.transform.Rotate(new Vector3(0, 0, turnSpeed / 10.0f));
                        }
                        else if (currentCommand.Direction == Direction.Right)
                        {
                            gameObject.transform.Rotate(new Vector3(0, 0, -turnSpeed / 10.0f));
                        }
                    }
                    else
                    {
                        transform.position = currentCommand.destination;
                        transform.rotation = currentCommand.rotation;
                        time = currentCommand.time + currentCommand.duration;
                        doingCommand = false;
                        commandTime = 0.0f;
                        step++;
                        if (step == run.commands.Count)
                        {
                            finished = true;
                        }
                    }
                }

                if (time >= run.totalTime)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
