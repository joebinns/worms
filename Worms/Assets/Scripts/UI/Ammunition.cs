using Items.Weapons;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Ammunition : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [SerializeField] private Color32 _outOfAmmo = new Color32(255, 255/4, 255/4, 255);
        [SerializeField] private Color32 _hasAmmo = new Color32(255, 255, 255, 255);

        private string _outOfAmmoHex;
        private string _hasAmmoHex;

        private void Start()
        {
            _outOfAmmoHex = ColorUtility.ToHtmlStringRGBA(_outOfAmmo);
            _hasAmmoHex = ColorUtility.ToHtmlStringRGBA(_hasAmmo);
        }

        public void RefreshDisplay()
        {
            // Get current weapon's ammunition
            var weapon = PlayerManager.Instance.currentPlayer.weaponRack.currentItem.GetComponent<Weapon>();
            var currentAmmunition = weapon.CurrentAmmunition;
            var maxAmmunition = weapon.WeaponSettings.maxAmmunition;

            // Determine color
            var hexColor = _hasAmmoHex;
            if (currentAmmunition == 0)
            {
                hexColor = _outOfAmmoHex;
            }

            // Display
            _text.text = "<color=#" + hexColor + "><size=200%>" + currentAmmunition.ToString() + " <color=#" + _hasAmmoHex + "><size=100%>/" + maxAmmunition.ToString();

        }

    }
}
