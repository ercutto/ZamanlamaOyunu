
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GridBoard gridBoard;
    public Text scoreText;
    public  int _score=0;
    public int _Level;
    private  int _maxExprience = 10;
    int _yDim = 1;
    int _xDim = 2;
    private int _exprience;
    
    public void UpdateScore(int addScore)
    {
        _score += addScore;
        _exprience++;

        if(_exprience >= _maxExprience)
        {
            gridBoard.isCorrect = true;
            _Level++;

            _exprience = 0;
            //gridBoard.StartGame(_xDim+_Level,_yDim);
            //gridBoard.StartCoroutine("Fill");
        }

        scoreText.text = _score.ToString();
    }
    private void Start()
    {
        _score = 0;
        
    }
   
}
