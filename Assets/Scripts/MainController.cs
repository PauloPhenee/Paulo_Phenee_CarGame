using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public GameObject LeftTurnControl;
    public GameObject RightTurnControl;
    public GameObject VictoryText;

    public Text Timer;

    public GameObject[] GoalObjects;
    private List<Run> runs;
    public GameObject GhostCarPrefab;
    public GameObject PlayerCarPrefab;
    public GameObject Canvas;
    public float speed;
    public float turnSpeed;

    [HideInInspector]
    public Goal currentGoal;

    private PlayerController playerController;
    private int stage;
    private float timer;
    bool gameIsRuning;
    bool freezeScreen;

    private void CreateGhostCars()
    {
        foreach (Run run in runs)
        {
            GameObject ghostCar = Instantiate(GhostCarPrefab);
            ghostCar.transform.parent = Canvas.transform;
            ghostCar.GetComponent<GhostCar>().Initialization(run, speed, turnSpeed);
        }
    }

    private void SetupTrack()
    {
        timer = 0.0f;
        freezeScreen = true;
        GameObject[] ghostCarsToDestroy = GameObject.FindGameObjectsWithTag("GhostCar");

        for (int i = 0; i < ghostCarsToDestroy.Length; i++)
        {
            Destroy(ghostCarsToDestroy[i]);
        }

        currentGoal = GoalObjects[stage].GetComponent<Goal>();
        CreateGhostCars();
        foreach (GameObject goalObject in GoalObjects)
        {
            if (goalObject.GetComponent<Goal>() == currentGoal)
            {
                goalObject.SetActive(true);
            }
            else
            {
                goalObject.SetActive(false);
            }
        }
        GameObject playerCar = Instantiate(PlayerCarPrefab);
        playerCar.transform.SetParent(Canvas.transform);
        playerCar.transform.SetPositionAndRotation(currentGoal.start, Quaternion.Euler(0,0, currentGoal.rotation));
        playerController = playerCar.GetComponent<PlayerController>();
        playerController.SetControllers(LeftTurnControl, RightTurnControl);
    }

    public void NewStage()
    {
        stage++;
        if (stage < GoalObjects.Length)
        {
            SetupTrack();
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            else
            {
                GameObject[] ghostCarsToDestroy = GameObject.FindGameObjectsWithTag("GhostCar");

                for (int i = 0; i < ghostCarsToDestroy.Length; i++)
                {
                    Destroy(ghostCarsToDestroy[i]);
                }
                CreateGhostCars();

                VictoryText.SetActive(true);
                VictoryText.GetComponent<VictoryText>().Initalization();
                gameIsRuning = false;
                Timer.text = FinalTime();
            }
        }
    }

    public void RepeatStage()
    {
        SetupTrack();
    }

    public void AddRun(Run run)
    {
        if (runs.Count < 8)
        {
            run.totalTime = timer;
            runs.Add(run);
        }
    }

    private string TimeAsString()
    {
        int centiseconds = (int)(timer * 100.0f);
        return "Time\n" + (centiseconds / 100).ToString() + "." + (centiseconds % 100).ToString();

    }
    private string FinalTime()
    {
        float totalTime = 0.0f;
        foreach (Run run in runs)
        {
            totalTime += run.totalTime;
        }
        int centiseconds = (int)(totalTime * 100.0f);
        return "Total Time\n" + (centiseconds / 100).ToString() + "." + (centiseconds % 100).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        stage = -1;
        runs = new List<Run>();
        NewStage();
        freezeScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeScreen)
        {
            if (Input.touchCount > 0)
            {
                freezeScreen = false;
                gameIsRuning = true;
                playerController.Activation();
                GameObject[] ghostCars = GameObject.FindGameObjectsWithTag("GhostCar");
                foreach (var item in ghostCars)
                {
                    item.GetComponent<GhostCar>().Activation();
                }
            }
        }
        if (gameIsRuning)
        {
            timer += Time.deltaTime;
            Timer.text = TimeAsString();
        }
    }
}
