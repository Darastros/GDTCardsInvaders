using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class FiniteStateMachine<T> : MonoBehaviour
    where T : Enum, IComparable
{
    [SerializeField]
    private T _requestedStep;
    private T _currentStep;
    private MethodInfo _currentUpdateMethod;

    protected void RequestStep(T step) => _requestedStep = step;

    protected virtual void Start()
    {
        if (_requestedStep == null)
        {
            _requestedStep = (T)Enum.GetValues(typeof(T)).GetValue(0);
        }

        EnterStep();
    }

    protected virtual void Update()
    {
        if (_currentStep.CompareTo(_requestedStep) != 0)
        {
            ExitStep();
            EnterStep();
        }
        else
        {
            UpdateStep();
        }
    }

    private void EnterStep()
    {
        var enterName = $"{_requestedStep}_Enter";
        var m = FindMethodByName(enterName);
        if (m == null)
            Debug.LogWarning($"Didn't find public method {enterName} in class {GetType().FullName}");
        m?.Invoke(this, null);

        _currentStep = _requestedStep;

        var updateName = $"{_requestedStep}_Update";
        _currentUpdateMethod = FindMethodByName(updateName);
        if (_currentUpdateMethod == null)
            Debug.LogWarning($"Didn't find public method {updateName} in class {GetType().FullName}");
    }

    private void ExitStep()
    {
        var exitName = $"{_requestedStep}_Exit";
        var m = FindMethodByName(exitName);
        if (m == null)
            Debug.LogWarning($"Didn't find public method {exitName} in class {GetType().FullName}");
        m?.Invoke(this, null);
    }

    private void UpdateStep()
    {
        _currentUpdateMethod?.Invoke(this, null);
    }

    private MethodInfo FindMethodByName(string name) =>  GetType().GetMethod(name);
}
