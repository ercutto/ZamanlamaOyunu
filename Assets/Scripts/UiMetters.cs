
using UnityEngine;
using UnityEngine.UI;



public class UiMetters : MonoBehaviour
{
    public Slider CorrectCountSlider;
    public Slider CountDownSlider;
    public Score _score;
    
    public bool changeApplied=true;
    private bool Win;
    // Start is called before the first frame update
    void Start()
    {
        Win = false;
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
            if (!changeApplied)
            {
                Calculatescore();
            }

            float distance = CountDownSlider.value - CorrectCountSlider.value;


            if (distance >= 0.1f)
            {

                CountDownSlider.value = Mathf.Lerp(CountDownSlider.value, CorrectCountSlider.value, 0.005f);

            }
            else if (distance <= 0.1f) { AddScore(-5f); }
        }
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
    void AddScore(float addScore)
    {
        CorrectCountSlider.value += addScore;
        _score.UpdateScore(-1);

    }
 


}
