
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameBoard gameBoard;
    public Text scoreText;
    public  int _score=0;
    public int _Level;
    private  int _maxExprience = 5;
    int _yDim = 2;
    int _xDim = 2;
    private int _exprience;
    
    public void UpdateScore(int addScore)
    {
        _score += addScore;
        _exprience++;

        if(_exprience >= _maxExprience)
        {
           
            _Level++;

            _exprience = 0;
            gameBoard.StartGame(_xDim+_Level,_yDim+_Level);
            
        }

        scoreText.text = _score.ToString();
    }
    private void Start()
    {
        _score = 0;
        
    }
   
}
