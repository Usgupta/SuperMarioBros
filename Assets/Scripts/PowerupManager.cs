using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PowerupManager: MonoBehaviour
{
    public UnityEvent<IPowerup> powerupAffectsManager;
    public UnityEvent<IPowerup> powerupAffectsPlayer;
}
