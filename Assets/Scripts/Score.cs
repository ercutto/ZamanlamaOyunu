

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameBoard gameBoard;
    public Text scoreText;
    public  int _score=0;
    public int _Level;
    public int _maxLevel;
    private  int _maxExprience = 5;
    int _yDim { get { return gameBoard._yDim; } }
    int _xDim { get { return gameBoard._xDim; } }
    private int _exprience;

    public UiMetters _uiMetters;
    



    private void Start()
    {
        _score = 0;
        StartCoroutine(Clock());
    }
    public void UpdateScore(int addScore)
    {
        _score += addScore;
        if (_score < 0) { _score = 0; }

        //scoreText.text = _score.ToString();
        
        _exprience+=addScore;
        if (addScore > 0)
        {
            _uiMetters.changeApplied = false;
        }
        
        
      
        if (_Level <= _maxLevel)
        {
               

            if (_exprience >= _maxExprience)
            {
                    
                _Level++; 
                    
                _exprience = 0;

                //Check();
            }

        }

            if (_score == 5) { gameBoard.StartGame(_xDim, _yDim + addScore); }
            else if (_score == 10) { gameBoard.StartGame(_xDim + addScore, _yDim); }
            else if (_score == 15) { gameBoard.StartGame(_xDim, _yDim + addScore); }
            else if (_score == 20) { gameBoard.StartGame(_xDim, _yDim + addScore); }

           
    }
  
    void Check()
    {
        if (_Level == 1) { gameBoard.StartGame(_xDim, _yDim + 1); }
        else if (_Level == 2) { gameBoard.StartGame(_xDim + 1, _yDim); }
        else if (_Level == 3) { gameBoard.StartGame(_xDim, _yDim + 1); }
        else if (_Level == 4) { gameBoard.StartGame(_xDim, _yDim + 1); }
    }
    IEnumerator Clock()
    {
        int minute = 0;
        for (int i = 0; i < 60; i++)
        {
            
            
            if (i == 59)
            {
                i = 0;
                minute++;
            }

            scoreText.text =minute.ToString()+" : " +i.ToString();
            yield return new WaitForSeconds(1);
        }
    }
    
   
}
