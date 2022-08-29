using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityGameEventListener : MonoBehaviour, IGameEventListener
{
    [Tooltip("Event ro register with.")]
    [SerializeField]
    private VoidEvent @event;

    [Tooltip("Response to invoke when event is raised.")]
    [SerializeField]
    private UnityEvent response;
    [SerializeField]
    private EventWeapon weaponResponse;

    public void OnEnable()
    {
        if (@event != null) @event.RegisterListener(this);
    }

    public void OnDisable()
    {
        @event.UnregisterListener(this);
    }
    public void OnEventRaised()
    {
        response?.Invoke();
    }

    public void OnEventRaised(Weapon_SO weapon)
    {
        weaponResponse?.Invoke(weapon);
    }
}
