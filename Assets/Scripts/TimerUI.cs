using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [Header("Start parameters")]
    public bool isTick;
    public bool isRunDown;
    public bool onlySeconds;

    [Header("References")]
    [SerializeField] private Text timerText;

    private float _time;
    private int _minuts;
    private int _sec;
    private int _millisec;

    public static Action TimerIsZero;

    private void Update()
    {
        if (!isTick || Time.timeScale == 0) return;

        _time += isRunDown ? -Time.deltaTime : Time.deltaTime;

        FloatToTime();

        if (isRunDown && _time <= 0)
        {
            TimerIsZero?.Invoke();
            isTick = false;
        }
    }

    #region Time
    private void FloatToTime()
    {
        if(!onlySeconds)
            _minuts = (int)_time / 60;
        _sec = (int)_time % 60;
        _millisec = Math.Max(0, (int)(_time % 1 * 100));

        DrawTime();
    }

    private void DrawTime()
    {
        string temp = "";

        if(!onlySeconds)
            temp = IntToString(_minuts )+ " : ";
        temp += IntToString(_sec) + " : ";
        temp += IntToString(_millisec);

        timerText.text = temp;
    }

    private string IntToString(int value)
    {
        if (value < 10) return "0" + value;
        else return value.ToString();
    }

    public float GetTime() => _time;
    public void SetTime(float time)
    {
        _time = time;
        FloatToTime();
    }
    #endregion

    public void SetTextColor(Color color)
    {
        timerText.color = color;
    }
}
