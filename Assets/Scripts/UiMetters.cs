
using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class UiMetters : MonoBehaviour
{
    public Slider CorrectCountSlider;
    public Slider CountDownSlider;
    public Score _score;
    public GameBoard _board;
    public TimingGameManager _timingGameManager;
    
    public bool changeApplied=true;
    public bool Win;
    public bool paused=false;
    bool scoreDecreesed=false;
    private float sliderMin =0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetVar();

    }
    public void ResetVar()
    {
        Win = false;
        paused = false;
        changeApplied = true;
        SetFrontMax(100f);
        SetFront(0f);
        SetMaxBg(100);
        SetBackgroundSlider(5f);
    }
    void Update()
    {
        Checkhealth();

    }
    void Checkhealth()
    {
        if (Win)
        {
            return;
        }
        else
        {
            if (paused) { return; }
            else
            {
                if (!changeApplied)
                {
                    Calculatescore();
                }

                float distance = CountDownSlider.value - CorrectCountSlider.value;

                if (_board.nextLevel == true && _board.reFilling == false)
                {
                    if (distance >= 0.1f)
                    {

                        CountDownSlider.value = Mathf.Lerp(CountDownSlider.value, CorrectCountSlider.value, 0.005f);

                    }
                    else if (distance <= 0.1f)
                    {
                        if (!scoreDecreesed)
                        {
                            StartCoroutine(CallAddScore());
                        }
                    }
                }
                

               

            }
            
        }
    }

    IEnumerator CallAddScore()
    {
        AddScore(-5f);
        scoreDecreesed = true;

        yield return new WaitForSeconds(2);
        scoreDecreesed=false;

    }
    void Calculatescore()
    {
        changeApplied = false;
        float difference = CountDownSlider.value - CorrectCountSlider.value;

        float total =CorrectCountSlider.value += difference;
        if (total >= 98f)
        {
            CorrectCountSlider.value = 100f;
            Win = true;
            _timingGameManager.RestartGame();
        }
        CountDownSlider.value = CorrectCountSlider.value + 10f;
        changeApplied = true;
    }


    void SetFrontMax(float max)
    {
        CorrectCountSlider.maxValue = max;
    }
    void SetFront(float madeSomeChange)
    {
        CorrectCountSlider.value = madeSomeChange;
    }
    void SetMaxBg(float max)
    {
        CountDownSlider.maxValue = max;
    }
    void SetBackgroundSlider(float madeSomeChange)
    {
        CountDownSlider.value = madeSomeChange;
    }
    public void AddScore(float addScore)
    {
        
        CorrectCountSlider.value += addScore;
        if (CorrectCountSlider.value < sliderMin) { CorrectCountSlider.value = sliderMin; }
        _score.UpdateScore(-1);

    }
    public void DecreeseScore(float addScore)
    {
        
        CorrectCountSlider.value += addScore;
        if (CorrectCountSlider.value < sliderMin) { CorrectCountSlider.value = sliderMin; }
       

    }





}
