using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private List<Target> globalTargetList;

    [SerializeField] private List<Unit> globalUnitList;

    private void Start()
    {
        _playerController.CacheAvailableUnits(globalUnitList);
    }

    public void OnSpeedSliderChange(float value)
    {
        foreach (var unit in globalUnitList)
        {
            unit.SetAgentSpeed(value);
        }
    }

    public void OnThresholdSliderChange(float value)
    {
        foreach (var unit in globalUnitList)
        {
            unit.SetTargetThreshold(value);
        }
    }

    public void SelectAllUnits()
    {
        foreach (var unit in globalUnitList)
        {
            _playerController.AddUnitToSelected(unit);
        }
    }

    public void ClearUnitTargets()
    {
        foreach (var unit in globalUnitList)
        {
            unit.ResetTarget();
        }
    }
}
