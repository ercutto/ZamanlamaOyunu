

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.MaterialProperty;

public class Score : MonoBehaviour
{
    public GameBoard gameBoard;
    public Text scoreText;
    public int _score = 0;

    int minute = 0;
    int i = 0;

    int _yDim { get { return gameBoard._yDim; } }
    int _xDim { get { return gameBoard._xDim; } }
   
    public int _scoreCoefficient;
    public UiMetters _uiMetters;
    public int Level = 0;
    public int Level1 = 4;
    public int Level2 = 8;
    public int Level3= 12;
    private int current = 0;
    

    //private void Start()
    //{
    //    ResetVar();
    //}
    public void ResetVar()
    {

        _score = 0;
        minute = 0;

        i = 0;
        StartCoroutine(Clock());
    }
    public void UpdateScore(int addScore)
    {

        
        
        _score += addScore;
        Debug.Log(_score);
        if (_score <= 0) { _score = 0; }

        if (addScore > 0)
        {
            _uiMetters.changeApplied = false;
        }
        else if (addScore < 0) { _uiMetters.DecreeseScore(addScore); }

       

        
        
        if (_score == 9 && _xDim * _yDim != 4) { gameBoard.StartGame(_xDim-1, _yDim); }

        if (_score == 10 && _xDim * _yDim != 6) { gameBoard.StartGame(_xDim + 1, _yDim); }//6

        if (_score == 19&&_xDim*_yDim!=6) { gameBoard.StartGame(_xDim, _yDim-1); }

        if (_score == 20 && _xDim * _yDim != 9) { gameBoard.StartGame(_xDim , _yDim + 1); }//8

        if (_score == 29 && _xDim * _yDim != 9) { gameBoard.StartGame(_xDim, _yDim-1); }

        if (_score == 30 && _xDim * _yDim != 12) { gameBoard.StartGame(_xDim , _yDim +1); }//9

        if (_score == 39 && _xDim * _yDim != 12) { gameBoard.StartGame(_xDim -1, _yDim); }

        if (_score == 40 && _xDim* _yDim != 16) { gameBoard.StartGame(_xDim + 1, _yDim); }


    }


    IEnumerator Clock()
    {

        for (i = 0; i < 60; i++)
        {


            if (i == 59)
            {
                i = 0;
                minute++;
            }

            scoreText.text = minute.ToString() + " : " + i.ToString();
            yield return new WaitForSeconds(1);
        }
    }


}
