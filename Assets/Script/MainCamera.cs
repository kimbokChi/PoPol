﻿using System.Collections;
using UnityEngine;

public class MainCamera : Singleton<MainCamera>
{
    private Coroutine _ShakeRoutine;
    private Coroutine _ColorRoutine;
    private Coroutine  _MoveRoutine;

    public void Shake(float power, float time)
    {
        if (_ShakeRoutine == null)
        {
            _ShakeRoutine = new Coroutine(this);
            _ShakeRoutine.RoutineStopEvent += () =>
            {
                transform.localPosition = Vector2.zero;
                transform.Translate(0, 0, -10f);
            };
        }
        _ShakeRoutine.StartRoutine(EShake(power, time));
    }
    public void Move(float time, Vector2 start, Vector2 goal, System.Action overAction = null)
    {
        if (_MoveRoutine == null)
        {
            _MoveRoutine = new Coroutine(this);
        }
        transform.parent.position = start;
        _MoveRoutine.StartRoutine(EMove(time, goal));

        void OverAction()
        {
            overAction?.Invoke();
            _MoveRoutine.RoutineStopEvent -= OverAction;
        }
        _MoveRoutine.RoutineStopEvent += OverAction;
    }
    public void ColorChange(float time, Color color)
    {
        if (_ColorRoutine == null)
        {
            _ColorRoutine = new Coroutine(this);
        }
        _ColorRoutine.StartRoutine(EColorChange(time, color));
    }
    // =================== IEnumator =================== //
    private IEnumerator EShake(float power, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            i = Mathf.Min(i, time);
            power = Mathf.Lerp(power, 0, i / time);

            transform.localPosition = Random.insideUnitCircle * power;
            transform.Translate(0, 0, -10f, Space.Self);

            yield return null;
        }
        _ShakeRoutine.FinshRoutine();
    }
    private IEnumerator EMove(float time, Vector2 position)
    {
        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            transform.parent.position = Vector2.Lerp(transform.position, position, ratio);
            transform.parent.Translate(0, 0, -10f);

            yield return null;
        }
        _MoveRoutine.FinshRoutine();
    }
    private IEnumerator EColorChange(float time, Color color)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float ratio = Mathf.Min(i, time) / time;

            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, color, ratio);
            yield return null;
        }
        _ColorRoutine.FinshRoutine();
    }
}
