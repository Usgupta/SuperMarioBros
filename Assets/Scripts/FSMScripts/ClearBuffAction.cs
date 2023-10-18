using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ClearBuff")]
public class ClearBuffAction : Action
{
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        // if (m.currentPowerupType != PowerupType.StarMan)
            
        m.currentPowerupType = PowerupType.Default;
        // m.
    }
}