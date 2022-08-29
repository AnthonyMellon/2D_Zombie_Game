using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponEvent", menuName = "Events/WeaponEvent")]
public class WeaponEvent : VoidEvent
{
    public void Raise(Weapon_SO weapon)
    {
        for (int i = m_eventListeners.Count - 1; i >= 0; i--)
        {
            m_eventListeners[i].OnEventRaised(weapon);
        }
    }
}
