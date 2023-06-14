using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : MonoBehaviour
{
    public enum ShootingState
    {
        OneStraight = 10,
        TwoStraight = 20,
        ThreeStraight = 30,
        TwoSkewed = 21,
        ThreeSkewed = 31,
    }

    static UnitState instance;
    [SerializeField] ShootingState shootingState;

    public ShootingState GetCurrentShootingState()
    {
        return shootingState;
    }

    public void SetOneStraight()
    {
        shootingState = ShootingState.OneStraight;
    }

    public void SetTwoStraight()
    {
        shootingState = ShootingState.TwoStraight;
    }

    public void SetThreeStraight()
    {
        shootingState = ShootingState.ThreeStraight;
    }

    public void SetTwoSkewed()
    {
        shootingState = ShootingState.TwoSkewed;
    }

    public void SetThreeSkewed()
    {
        shootingState = ShootingState.ThreeSkewed;
    }
}
