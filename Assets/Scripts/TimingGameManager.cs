using System.Collections;

using UnityEngine;

public class TimingGameManager : MonoBehaviour
{
    public GameBoard board;
    public Score score;
    public UiMetters uImetters;
    private bool ispaused=false;
    public void Awake()
    {
        board.AwakeBoard();
        score.ResetVar();
    }
    public void StartGame()
    {

       
        board.StartBoard();
    }
    public void PauseGame()
    {
       board.Paused(false); 
    }
    public void UnPause()
    {
       board.Paused(true); 
    }
    public void RestartGame()
    {
      board.ResetVar();
    }
    public void Win()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
