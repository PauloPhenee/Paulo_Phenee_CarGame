using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left, Right
}
public struct Command
{
    public Direction Direction { get; set; }
    public float duration;
    public float time;
    public Vector3 destination;
    public Quaternion rotation;
    public Command (Direction _direction, float _duration, float _time, Vector3 _destination, Quaternion _rotation)
    {
        Direction = _direction;
        duration = _duration;
        time = _time - _duration;
        destination = _destination;
        rotation = _rotation;
    }

    public string CommandLog()
    {
        return "at " + time + " turn " + Direction.ToString() + " duration " + duration;
    }
}
public class Run
{
    public List<Command> commands = new List<Command>();
    public Vector3 startPosition;
    public Quaternion rotation;
    public float totalTime;

    public Run(Vector3 _startPosition, Quaternion _rotation)
    {
        startPosition = _startPosition;
        rotation = _rotation;
    }

    public void AddCommand(Direction _direction, float _duration, float _time, Vector3 _destination, Quaternion _rotation)
    {
        Command command = new Command(_direction, _duration, _time, _destination, _rotation);
        commands.Add(command);
    }


}
