
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

    public Sprite _spriteBackground;
    public Sprite _spriteFront;
    



    private void Start()
    {
        _score = 0;

    }
    public void UpdateScore(int addScore)
    {
        _score += addScore;
        scoreText.text = _score.ToString();
        
        _exprience+=addScore; 
        
        
      
        if (_Level <= _maxLevel)
        {
               

            if (_exprience >= _maxExprience)
            {
                    
                _Level++; 
                    
                _exprience = 0;
                if (_Level == 1) { gameBoard.StartGame(_xDim, _yDim + 1); }
                else if (_Level == 2) { gameBoard.StartGame(_xDim + 1, _yDim); }
                else if (_Level == 3) { gameBoard.StartGame(_xDim, _yDim + 1); }
                else if (_Level == 4) { gameBoard.StartGame(_xDim, _yDim + 1); }

            }

        }
        
       
        

        
    }
    
   
}
