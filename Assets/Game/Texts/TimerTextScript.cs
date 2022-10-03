using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TimerTextScript : MonoBehaviour
{
    [SerializeField]
    float _beginTime = 1.0f;
    float _time;
    bool _running = false;

    [SerializeField]
    bool _startAtAwake = false;

    [SerializeField]
    InputActionReference _debug;

    public UnityEvent OnTimeHit;

    TextMeshProUGUI _text;
    void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
        ResetTime();
        if (_startAtAwake)
            StartTime();
        ConvertTimeToText();
    }

    // Update is called once per frame
    void Update() {
        if (_running) {
            _time -= Time.deltaTime;

            if (_time < 0.0f) {
                _time += _beginTime;
                OnTimeHit.Invoke();
            }
            ConvertTimeToText();

            if (_debug.action.triggered) {
                ResetTime();
                OnTimeHit.Invoke();
            }
        }
    }

    public void StartTime() {
        _running = true;
    }

    public void PauseTime() {
        _running = false;
    }
    public void StopTime() {
        _running = false;
        ResetTime();
    }

    public void ResetTime() {
        _time = _beginTime;
        ConvertTimeToText();
    }

    void ConvertTimeToText() {
        int seconds = Mathf.FloorToInt(_time % 60);

        int centiseconds = Mathf.FloorToInt(_time % 1 * 100);
        _text.text = string.Format("{0:00}:{1:00}", seconds, centiseconds);
    }

}
