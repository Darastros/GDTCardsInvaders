using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum CinematicActionType
{
    DialogueBox,
}

[Serializable]
public struct CinematicAction
{
    public CinematicActionType Type;
    public string[] Parameters;
    public float Duration;
    public bool WaitForInput;

    //TODO public bool IsShaking; // shake the camera
    //TODO public Sound WithSound; // play given sound
}

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _hideForCinematic;
    [SerializeField] private CinematicAction[] _actions;

    [SerializeField] private GameObject _speechBubbleRoot;
    [SerializeField] public TextMeshProUGUI _speechBubbleText;
    [SerializeField] private BlinkAnimation _waitInputBlinker;

    public void RunCinematic(Action onDone)
    {
        StartCoroutine(RunCinematicCoroutine(onDone));
    }

    private IEnumerator RunCinematicCoroutine(Action onDone)
    {
        SetStuffActive(false);

        foreach (var action in _actions)
        {
            switch (action.Type)
            {
                case CinematicActionType.DialogueBox:
                    yield return RunCinematicDialogue(action);
                    break;
            }
        }

        SetStuffActive(true);
        onDone?.Invoke();
    }

    private IEnumerator RunCinematicDialogue(CinematicAction action)
    {
        _speechBubbleRoot.SetActive(true);
        _speechBubbleText.text = action.Parameters[0];
        //TODO set speaker color

        if (action.WaitForInput)
        {
            _waitInputBlinker.enabled = true;
            yield return WaitForInput();
        }
        else
        {
            _waitInputBlinker.enabled = false;
            yield return new WaitForSeconds(action.Duration);
        }

        _speechBubbleRoot.SetActive(false);
    }

    private IEnumerator WaitForInput()
    {
        while (true)
        {
            if (Input.anyKeyDown)
                break;
            else yield return new WaitForFixedUpdate();
        }
    }

    private void OnCinematicFinished()
    {

    }

    private void SetStuffActive(bool val)
    {
        foreach(var go in  _hideForCinematic)
        {
            go.SetActive(val);
        }
    }
}
