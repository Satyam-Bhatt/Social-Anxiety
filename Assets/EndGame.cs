using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Rendering;

public class EndGame : MonoBehaviour
{
    [SerializeField] private PlayableDirector sleepTimeline;
    [SerializeField] private GameObject room;
    [SerializeField] private float rotateDelta;
    [SerializeField] private GameObject spriteMask;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Volume postProcessVolume;

    [Space(10)] [Header("Parameters")] [SerializeField]
    private float scalingSpeed;

    private PlayerControls _playerControls;
    private Animator _animator;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
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
    private bool _inputCheckStart = false;
    private bool _winStateStart = false;
    private Coroutine _winStatEnumerator = null;
    private Coroutine _spriteMaskGrowCoroutine = null;
    private Coroutine _postProcessVolumeCoroutine = null;

    private void Update()
    {
        if(!_sleepTimerEnded) return;
        if (_win) return;

        var value = AudioManager.Instance.audioSource1.volume;
        if (value > 0.1f) value -= Time.deltaTime * 0.01f;
        AudioManager.Instance.audioSource1.volume = value;

        if (!_inputCheckStart)
        {
            var value2 = AudioManager.Instance.audioSource2.volume;
            if (value2 < 0.6f) value2 += Time.deltaTime * 0.03f;
            AudioManager.Instance.audioSource2.volume = value2;
        }

        if (!_inputCheckStart) return;

        //Checking if the player is moving or not
        if (_input)
        {
            //Handles Rotation of the room
            if (_rotationInZ < 120f)
                _rotationSpeed += Time.deltaTime;
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
            var audioSrc = AudioManager.Instance.audioSource2;
            if (audioSrc && audioSrc.volume < 0.7f)
                audioSrc.volume += 0.05f * Time.deltaTime;
            
            //Decrease confidence
            if (InventoryManager.Instance.healthValue > 0)
            {
                InventoryManager.Instance.ConfidenceDecreaseEndGame();
            }
            
            //Increase post process weight
            if (postProcessVolume.weight < 1)
            {
                postProcessVolume.weight += Time.deltaTime;
            }

            //Stop Win State
            if (_winStatEnumerator != null) StopCoroutine(_winStatEnumerator);
            if (_spriteMaskGrowCoroutine != null) StopCoroutine(_spriteMaskGrowCoroutine);
            if(_postProcessVolumeCoroutine != null) StopCoroutine(_postProcessVolumeCoroutine);
            if (AudioManager.Instance.audioSource2.clip != AudioManager.Instance.breathing_Heavy)
            {
                AudioManager.Instance.AudioPlay2(AudioManager.Instance.breathing_Heavy); //Audio Reset
                AudioManager.Instance.audioSource2.volume = 0f;
            }

            if (AudioManager.Instance.audioSource1.volume < 0.1f) AudioManager.Instance.audioSource1.volume = 0.1f;
            spriteMask.transform.localScale = Vector3.Lerp(spriteMask.transform.localScale, Vector3.zero, Time.deltaTime);
            _animator.SetBool("BW", true);
            _winStateStart = false;
            _onceWinStateCheck = true; //Add code for resetting stuff like audio and sprite mask
        }
        else
        {
            if (!_winStateStart)
            {
                //Handles Rotation of the room
                if (_rotationSpeed > 0)
                {
                    _rotationSpeed -= 6f * Time.deltaTime; //Changes how fast should we revert to rotation
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
                else if (_rotationSpeed <= 0 && !MyApproximation(room.transform.eulerAngles.z, 90, 0.1f))
                {
                    room.transform.rotation = Quaternion.Slerp(room.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime);
                    _onceRotationSwitch = true;
                }

                //Handles Scale
                room.transform.localScale = Vector3.Lerp(room.transform.localScale, _originalScale, Time.deltaTime * 0.1f);

                //Audio Decreasing
                var audioSrc = AudioManager.Instance.audioSource2;
                if (audioSrc && audioSrc.volume > 0)
                    audioSrc.volume -= 0.1f * Time.deltaTime;
                if (audioSrc.volume < 0.3f && AudioManager.Instance.audioSource1.volume > 0)
                    AudioManager.Instance.audioSource1.volume -= 0.1f * Time.deltaTime;
            }

            //Win State Met
            var audioSrc2 = AudioManager.Instance.audioSource2;
            if (audioSrc2 && audioSrc2.volume <= 0 && _onceWinStateCheck)
            {
                _winStatEnumerator = StartCoroutine(WinState());
                _onceWinStateCheck = false;
            }
        }
    }

    private IEnumerator InputCheckStart()
    {
        yield return new WaitForSeconds(10f);
        _inputCheckStart = true;
    }

    //Win State
    private IEnumerator WinState()
    {
        Debug.Log("Win State Start");
        yield return new WaitForSeconds(2f);
        _winStateStart = true;
        while (!MyApproximation(room.transform.eulerAngles.z, 90, 0.1f) || !MyApproximation(room.transform.localScale.x, _originalScale.x, 0.05f))
        {
            //scale
            room.transform.localScale = Vector3.Lerp(room.transform.localScale, _originalScale, Time.deltaTime);

            //rotation
            room.transform.rotation = Quaternion.RotateTowards(room.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        AudioManager.Instance.AudioPlay2(AudioManager.Instance.breathing_Easy);
        AudioManager.Instance.audioSource2.volume = 0.6f;
        _spriteMaskGrowCoroutine = StartCoroutine(SpriteMaskGrowCoroutine());
        _postProcessVolumeCoroutine = StartCoroutine(PostProcessDecrease());
        _animator.SetBool("BW", false);
    }

    private Coroutine _scaleUpCoroutine = null;

    private IEnumerator SpriteMaskGrowCoroutine()
    {
        var incrementValue = 2f;
        while (spriteMask.transform.localScale.x < 30f)
        {
            Debug.Log("Call");
            var nextScale = spriteMask.transform.localScale.x + incrementValue;
            var newScale = new Vector3(nextScale, nextScale, 0f);
            if (_scaleUpCoroutine != null) StopCoroutine(_scaleUpCoroutine);
            _scaleUpCoroutine = StartCoroutine(ScaleUp(spriteMask.transform.localScale, newScale));
            incrementValue += 1f;
            InventoryManager.Instance.ConfidenceIncreaseEndGame();
            yield return new WaitForSeconds(AudioManager.Instance.breathing_Easy.length / 2);
        }

        _win = true; //Make Input always false

        //Final Win Panel and Dialogue
        GameManager.Instance.ActivateWinPane();
        /*GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(11);
        AudioManager.Instance.AudioPlay(AudioManager.Instance.beforeBW_Clip);*/
    }

    private IEnumerator ScaleUp(Vector3 currentScale, Vector3 scaleUp)
    {
        while (true)
        {
            spriteMask.transform.localScale = Vector3.Lerp(spriteMask.transform.localScale, scaleUp, Time.deltaTime * 0.5f);
            yield return null;
        }
    }

    private IEnumerator PostProcessDecrease()
    {
        while (postProcessVolume.weight > 0)
        {
            postProcessVolume.weight -= Time.deltaTime * 0.05f;
            yield return null;
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
        
        StartCoroutine(InputCheckStart());

        //Audio Start
        AudioManager.Instance.AudioPlay2(AudioManager.Instance.breathing_Heavy);
        //AudioManager.Instance.audioSource2.volume = 0.5f;

        //Camera Set
        virtualCamera.Follow = room.transform;
        virtualCamera.m_Lens.OrthographicSize = 8.6f;
    }

    private bool MyApproximation(float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
}