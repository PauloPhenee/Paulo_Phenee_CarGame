using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameObject LeftTurnControl;
    GameObject RightTurnControl;
    Vector3 position;
    Quaternion rotation;

    bool turning;
    bool turningLeft;
    bool turningRight;
    float secondsPressed;
    float totalTime;
    float speed;
    float turnSpeed;

    bool running;

    MainController mainController;

    private Run Run { get; set; }

    public void SetControllers(GameObject leftController, GameObject rightController)
    {
        LeftTurnControl = leftController;
        RightTurnControl = rightController;

        LeftTurnControl.GetComponent<LeftTurnInput>().LeftTurn.AddListener(LeftTurn);
        LeftTurnControl.GetComponent<LeftTurnInput>().StopLeftTurn.AddListener(StopTurn);

        RightTurnControl.GetComponent<RightTurnInput>().RightTurn.AddListener(RightTurn);
        RightTurnControl.GetComponent<RightTurnInput>().StopRightTurn.AddListener(StopTurn);
        position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

        Run = new Run(position, rotation);
    }

    public void Activation()
    {
        running = true;
    }

    private void LeftTurn()
    {
        if (!turning)
        {
            turningLeft = true;
            turning = true;
            secondsPressed = 0.0f;
        }
    }

    private void RightTurn()
    {
        if (!turning)
        {
            turningRight = true;
            turning = true;
            secondsPressed = 0.0f;
        }
    }

    private void StopTurn()
    {
        if (turningLeft)
        {
            Run.AddCommand(Direction.Left, secondsPressed, totalTime, new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }
        else if (turningRight)
        {
            Run.AddCommand(Direction.Right, secondsPressed, totalTime, new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }
        turningLeft = false;
        turningRight = false;
        turning = false;
    }

    public void StopRun()
    {
        mainController.RepeatStage();
        Destroy(gameObject);
    }

    private void Success()
    {
        mainController.AddRun(Run);
        mainController.NewStage();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject FinishLine = mainController.currentGoal.FinishLine;
        if (collision.gameObject == FinishLine)
        {
            Success();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GhostCar")
        {
            StopRun();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainController>();
        speed = mainController.speed;
        turnSpeed = mainController.turnSpeed;
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            totalTime += Time.deltaTime;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + Mathf.Cos(gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * speed * Time.deltaTime,
                                              gameObject.transform.position.y + Mathf.Sin(gameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * speed * Time.deltaTime, 0);

            if (turningLeft)
            {
                secondsPressed += Time.deltaTime;
                gameObject.transform.Rotate(new Vector3(0, 0, turnSpeed / 10.0f));
            }
            else if (turningRight)
            {
                secondsPressed += Time.deltaTime;
                gameObject.transform.Rotate(new Vector3(0, 0, -turnSpeed / 10.0f));
            }
        }
    }
}
