using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammunition : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    [SerializeField] private Color32 outOfAmmo = new Color32(255, 255/4, 255/4, 255);
    [SerializeField] private Color32 hasAmmo = new Color32(255, 255, 255, 255);

    private string outOfAmmoHex;
    private string hasAmmoHex;

    private void Start()
    {
        outOfAmmoHex = ColorUtility.ToHtmlStringRGBA(outOfAmmo);
        hasAmmoHex = ColorUtility.ToHtmlStringRGBA(hasAmmo);
    }

    public void RefreshDisplay()
    {
        // Get current weapon's ammunition
        var weapon = PlayerManager.currentPlayer.weaponRack.currentItem.GetComponent<Weapon>();
        var currentAmmunition = weapon.currentAmmunition;
        var maxAmmunition = weapon.weaponSettings.maxAmmunition;

        // Determine color
        var hexColor = hasAmmoHex;
        if (currentAmmunition == 0)
        {
            hexColor = outOfAmmoHex;
        }

        // Display
        text.text = "<color=#" + hexColor + "><size=200%>" + currentAmmunition.ToString() + " <color=#" + hasAmmoHex + "><size=100%>/" + maxAmmunition.ToString();

    }

}
