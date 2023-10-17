using UnityEngine;
using System;
[CreateAssetMenu(menuName = "PluggableSM/Decisions/BuffTransform")]
public class BuffTransformDecision : Decision
{
    public BuffStateTransformMap[] map;

    public override bool Decide(StateController controller)
    {
        BuffStateController buffStateController = (BuffStateController)controller;
        // we assume that the state is named (string matched) after one of possible values in MarioState
        // convert between current state name into MarioState enum value using custom class EnumExtension
        // you are free to modify this to your own use
        BuffState toCompareState = EnumExtension.ParseEnum<BuffState>(buffStateController.currentState.name);
        Debug.Log("wihtout enuming its "+buffStateController.currentState.name);

        // loop through state transform and see if it matches the current transformation we are looking for
        for (int i = 0; i < map.Length; i++)
        {
            Debug.Log("Actual is "+ toCompareState);
            if (toCompareState == map[i].fromState && buffStateController.currentPowerupType == map[i].powerupCollected)
            {
                Debug.Log("mapping SUCESS "+map[i].fromState.ToString() + map[i].powerupCollected.ToString());
                return true;
            }

            if (buffStateController.currentPowerupType == PowerupType.StarMan)
            {
                Debug.Log("mapping is "+map[i].fromState.ToString() + map[i].powerupCollected.ToString());
                Debug.Log("Actual is "+ toCompareState);
            }
            
        }

        return false;

    }
}

[System.Serializable]
public struct BuffStateTransformMap
{
    public BuffState fromState;
    public PowerupType powerupCollected;
}