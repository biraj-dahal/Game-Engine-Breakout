using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GameState
    {
        None,
        Menu,
        GetReady,
        Playing,
        Oops,
        GameOver
    }
public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager S;

    // game state
    public GameState curState = GameState.None;
    
    // ui elements
    public Text GameMessage;
    public Text ScoreVal;
    public Text label;

    private int LIVES_AT_START = 3;
    private int livesLeft;

    // game params
    public float ballSpeed = 1.0f;
    private int score;
    public Vector3 ballDirection;
    public GameObject ballPrefab;
    public GameObject curBall;
    public GameObject bricksPrefab;
    public GameObject curBricks;

    public int bricksLeft;
    public bool _checkBricks = false;


    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("GameManager.Awake() - Attempted to assign second GameManager.S!");
        }
    }
    void Start()
    {
        SetGameMenu();

    }

    private void SetGameMenu(){
        curState = GameState.Menu;

        GameMessage.text = "Press 'S' to start";
        GameMessage.enabled = true;
        label.text = "SCOREBOARD";



    }

    // Update is called once per frame
    void Update()
    {
        if ((curState == GameState.Menu) && (Input.GetKeyDown(KeyCode.S))){
                InitializeGame();
        }

        if ((curState == GameState.GameOver) && (Input.GetKeyDown(KeyCode.S))){
                InitializeGame();
        }

        if ((curState == GameState.Playing) && _checkBricks){
            CheckBricksRemaining();
            _checkBricks = false;
            

  
    }
    }

    private void CheckBricksRemaining()
{
    Debug.Log("Bricks Left: " + curBricks.transform.childCount);

    if (curBricks == null || curBricks.transform.childCount <= 0)
    {
        if (curBall != null)
        {
            curBall.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; 
        }
        GameOver("You won!\r\nPress 'S' to play again!");
    }
}

    private void InitializeGame(){
        score = 0;
        ScoreVal.text = score.ToString();
        livesLeft = LIVES_AT_START;

        if (curBricks != null){
            Destroy(curBricks);
        }
        curBricks = Instantiate(bricksPrefab);

        ReadyRound();
    }

    private void ReadyRound(){
        curState = GameState.GetReady;

        GameMessage.text = "Get Ready!!";
        GameMessage.enabled = true;

        // spawn ball
        if (curBall != null){
            Destroy(curBall);
        }
        curBall = Instantiate(ballPrefab);
        curBall.tag = "Ball";

        StartCoroutine(GetReadyDelay());
        // StartRound(); 
    }

    private void StartRound(){

        GameMessage.enabled = false;

            
        Rigidbody rb = curBall.GetComponent<Rigidbody>();
 
        // set the velocity
        rb.linearVelocity = ballDirection.normalized * ballSpeed; 

        curState = GameState.Playing;

    }

    public void BallLeftPlay(){

        curState = GameState.Oops;

        curBall.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        livesLeft--;

        StartCoroutine(OopsDelay());
    }

    public void IncreaseScore(int val){
        score += val;
        ScoreVal.text = score.ToString();
        
        _checkBricks= true;
    }

    private void GameOver(string message){
        curState = GameState.GameOver;
        GameMessage.text = message;
        GameMessage.enabled = true;
    }

    public IEnumerator GetReadyDelay(){

        Renderer ballRenderer = curBall.GetComponent<Renderer>();

        for(int i =0; i<2; i++){
            ballRenderer.enabled = false;
            GameMessage.enabled = true;
            yield return new WaitForSeconds(0.5f);

            ballRenderer.enabled = true;
            GameMessage.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
        StartRound();
    }

    public IEnumerator OopsDelay(){

        GameMessage.enabled = true;
        GameMessage.text = "You have " + livesLeft + " lives left";
        yield return new WaitForSeconds(2.0f);

        if (livesLeft > 0){
            ReadyRound();
        }
        else{
            GameOver("You lost!\r\nPress 'S' to restart");
        }
    }
}