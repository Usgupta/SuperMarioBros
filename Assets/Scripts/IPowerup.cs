using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerup
{
    void DestroyPowerup();
    void SpawnPowerup();
    void ApplyPowerup(MonoBehaviour i);

    PowerupType powerupType{
        get;
    }

    bool hasSpawned{
        get;
    }
}

public interface IPowerupApplicable
{
    public void RequestPowerupEffect(float i);
}


public enum PowerupType
{
    Coin = 0,
    MagicMushroom = 1,
    OneUpMushroom = 2,
    StarMan =3
}