using Items.Weapons;
using Players;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Ammunition : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        #region Colors
        [SerializeField] private Color32 _outOfAmmo = new Color32(255, 255/4, 255/4, 255);
        private string _outOfAmmoHex;
        [SerializeField] private Color32 _hasAmmo = new Color32(255, 255, 255, 255);
        private string _hasAmmoHex;
        #endregion

        private void Start()
        {
            _outOfAmmoHex = ColorUtility.ToHtmlStringRGBA(_outOfAmmo);
            _hasAmmoHex = ColorUtility.ToHtmlStringRGBA(_hasAmmo);
        }

        public void RefreshDisplay()
        {
            // Get current weapon's ammunition
            var weapon = PlayerManager.Instance.CurrentPlayer.WeaponRack.CurrentItem.GetComponent<Weapon>();
            var currentAmmunition = weapon.CurrentAmmunition;
            var maxAmmunition = weapon.WeaponSettings.MaxAmmunition;
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
