using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class EndGame : MonoBehaviour
{
    [SerializeField] private PlayableDirector sleepTimeline;
    [SerializeField] private GameObject room;
    [SerializeField] private float rotateDelta;
    [SerializeField] private GameObject spriteMask;

    [Space(10)] [Header("Parameters")] [SerializeField]
    private float scalingSpeed;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        sleepTimeline.stopped += OnSleepTimelineEnds;
        _playerControls.Movement.Move.Enable();
        _playerControls.Movement.Move.performed += MoveInput;
        _playerControls.Movement.Move.canceled += MoveInput;
    }

    private void OnDisable()
    {
        sleepTimeline.stopped -= OnSleepTimelineEnds;
        _playerControls.Movement.Move.Disable();
        _playerControls.Movement.Move.performed -= MoveInput;
        _playerControls.Movement.Move.canceled -= MoveInput;
    }

    private float _rotationInZ;
    private float _roomOriginalRotationInZ;
    private float _rotationSpeed = 0;
    private Vector3 _originalScale = Vector3.one;


    private void Start()
    {
        _roomOriginalRotationInZ = room.transform.eulerAngles.z;
        _rotationInZ = room.transform.eulerAngles.z - rotateDelta;
        _originalScale = room.transform.localScale;
    }

    private bool _sleepTimerEnded = false;
    private bool _onceRotationSwitch = false;
    private bool _onceWinStateCheck = true;
    private bool _win = false;
    private Coroutine _winStatEnumerator  = null;
    private Coroutine _spriteMaskGrowCoroutine = null;

    private void Update()
    {
        //if(!__sleepTimerEnded) return;
        //if (_win) return;

        if (!_input)
        {
            //Handles Rotation of the room
            if(_rotationInZ < 120f)
                _rotationSpeed += 0.5f * Time.deltaTime;
            if (!MyApproximation(room.transform.eulerAngles.z, _rotationInZ, 0.1f))
            {
                room.transform.rotation = Quaternion.RotateTowards(room.transform.rotation,
                    Quaternion.Euler(0, 0, _rotationInZ), Time.deltaTime * _rotationSpeed);
                _onceRotationSwitch = true;
            }
            else if (MyApproximation(room.transform.eulerAngles.z, _rotationInZ, 0.1f) && _onceRotationSwitch)
            {
                StartCoroutine(RotationSwitch());
            }

            //Handles Scale
            room.transform.localScale = Vector3.Lerp(room.transform.localScale, new Vector3(0.05f, 0.05f, 0f), Time.deltaTime * scalingSpeed);

            //Audio Increasing
            var audioSrc = AudioManager.Instance.GetComponent<AudioSource>();
            if (audioSrc && audioSrc.volume < 0.7f)
            {
                audioSrc.volume += 0.05f * Time.deltaTime;
            }
            
            //Stop Win State
            if(_winStatEnumerator != null) StopCoroutine(_winStatEnumerator);
            if(_spriteMaskGrowCoroutine != null) StopCoroutine(_spriteMaskGrowCoroutine);
            _onceWinStateCheck = true; //Add code for resetting stuff like audio and sprite mask
        }
        else
        {
            //Handles Rotation of the room
            if (_rotationSpeed > 0)
            {
                _rotationSpeed -= 2f * Time.deltaTime;
            }
            else
            {
                _rotationSpeed = 0f;
            }
            
            if (!MyApproximation(room.transform.eulerAngles.z, _rotationInZ, 0.1f) && _rotationSpeed > 0)
            {
                room.transform.rotation = Quaternion.RotateTowards(room.transform.rotation,
                    Quaternion.Euler(0, 0, _rotationInZ), Time.deltaTime * _rotationSpeed);
                _onceRotationSwitch = true;
            }
            else if (MyApproximation(room.transform.eulerAngles.z, _rotationInZ, 0.1f) && _onceRotationSwitch && _rotationSpeed > 0)
            {
                StartCoroutine(RotationSwitch());
            }
            else if (_rotationSpeed <= 0)
            {
                room.transform.rotation = Quaternion.Slerp(room.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 4f);
                _onceRotationSwitch = true;
            }

            //Handles Scale
            room.transform.localScale = Vector3.Lerp(room.transform.localScale, _originalScale, Time.deltaTime * 0.1f);
            
            //Audio Decreasing
            var audioSrc = AudioManager.Instance.GetComponent<AudioSource>();
            if (audioSrc && audioSrc.volume > 0)
                audioSrc.volume -= 0.1f * Time.deltaTime;
            if (audioSrc && audioSrc.volume <= 0 && _onceWinStateCheck)
            {
                _winStatEnumerator = StartCoroutine(WinState());
                _onceWinStateCheck = false;
            }
        }
    }
    
    //Win State
    private IEnumerator WinState()
    {
        Debug.Log("Win State Start");
        yield return new WaitForSeconds(2f);
        //Audio Change
        yield return new WaitForSeconds(2f);
        while (!MyApproximation(room.transform.eulerAngles.z, 90, 0.1f) && room.transform.localScale != _originalScale)
        {
            //scale
            room.transform.localScale = Vector3.Lerp(room.transform.localScale, _originalScale, Time.deltaTime * 4f);
            
            //rotation
            room.transform.rotation = Quaternion.Slerp(room.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 10f);
        }

        _spriteMaskGrowCoroutine = StartCoroutine(SpriteMaskGrowCoroutine());
        //Color Back 30 scale
        //Animation Change
        _win = true; //Make Input always false
    }

    private IEnumerator SpriteMaskGrowCoroutine()
    {
        while (spriteMask.transform.localScale.x < 30f)
        {
            var nextScale = spriteMask.transform.localScale.x + 5f;
            while (spriteMask.transform.localScale.x < nextScale)
            {
                var newScale = new Vector3(nextScale, nextScale, 0f);
                spriteMask.transform.localScale = Vector3.Lerp(spriteMask.transform.localScale, newScale, Time.deltaTime * 4f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    //For switching rotation after a delay
    private IEnumerator RotationSwitch()
    {
        _onceRotationSwitch = false;
        //yield return new WaitForSeconds(0.5f);
        yield return null;
        if (_rotationInZ < _roomOriginalRotationInZ)
        {
            _rotationInZ = _roomOriginalRotationInZ + rotateDelta;
        }
        else
        {
            _rotationInZ = _roomOriginalRotationInZ - rotateDelta;
        }
    }

    public bool _input = false;

    private void MoveInput(InputAction.CallbackContext ctx)
    {
        _input = ctx.performed;
    }


    private void OnSleepTimelineEnds(PlayableDirector playable)
    {
        _sleepTimerEnded = true;
    }

    private bool MyApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
}