using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private Target currentTarget;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private GameObject unitSelectionIndicator;

    [SerializeField] private Animator unitAnimator;

    private bool willMove = false;
    public bool isSelected = false;
    private bool isAnimatorSpeedSet = false;

    private float navMeshAgentDefaultSpeed = 10f;
    private float defaultTargetThreshold = 20f;
    private float targetThreshold = 20f;
    private float animatorFloatCache = 0f;

    private void Start()
    {
        unitAnimator.SetFloat("Velocity", animatorFloatCache);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargetDistance();
        
        if (willMove)
        {
            Debug.Log("Calling follow target!");
            FollowTarget();
        }
    }

    public void SetCurrentTarget(Target t)
    {
        currentTarget = t;
        CheckTargetDistance();
    }

    private void FollowTarget()
    {
        agent.destination = currentTarget.GetTargetPositionOnMap();
    }

    private void CheckTargetDistance()
    {
        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.GetTargetPositionOnMap()) > targetThreshold)
            {
                if (willMove == false)
                {
                    willMove = true;
                }

                if (!isAnimatorSpeedSet)
                {
                    OnVelocityChange(animatorFloatCache);
                    isAnimatorSpeedSet = true;
                }
            }
            else
            {
                if (willMove)
                {
                    willMove = false;
                    isAnimatorSpeedSet = false;
                    OnMovementStop();
                }

                agent.ResetPath();
            }
        }
    }

    public void SetAgentSpeed(float value)
    {
        agent.speed = navMeshAgentDefaultSpeed * value;
        OnVelocityChange(value);
    }

    public void SetTargetThreshold(float value)
    {
        targetThreshold = defaultTargetThreshold * value;
    }

    public void ResetTarget()
    {
        currentTarget = null;
        agent.ResetPath();
        willMove = false;
    }

    public void OnUnitSelected()
    {
        isSelected = true;
        unitSelectionIndicator.SetActive(true);
    }

    public void OnUnitDeselected()
    {
        isSelected = false;
        unitSelectionIndicator.SetActive(false);
    }

    public void OnVelocityChange(float value)
    {
        if (willMove)
        {
            unitAnimator.SetFloat("Velocity", value);
            animatorFloatCache = value;
        }
    }

    public void OnMovementStop()
    {
        unitAnimator.SetFloat("Velocity", 0);
    }
}
