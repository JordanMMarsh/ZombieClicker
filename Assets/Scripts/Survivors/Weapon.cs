using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponTypes
    {
        pistol,
        rifle,
        smg,
        sniper
    };
    private WeaponTypes myWeapon = WeaponTypes.pistol;

    public float CalculateWeaponDamage(bool isPlayer)
    {
        if (isPlayer)
        {
            return 100f;
        }
        else
        {
            return 10f;
        }
    }
}
