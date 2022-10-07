using System.Collections;
using Oscillators;
using UnityEngine;
using Utilities;

namespace Players
{
    // Fill this class with methods which will be subscribed to OnPlayerChanged
    public class PlayerSelectEffects : MonoBehaviour
    {
        #region Constants
        private const float DEFAULT_RIDE_HEIGHT = 2f;
        private const float UPPER_RIDE_HEIGHT = 2.75f;

        private const float DEFAULT_PLAYER_SIZE = 1f;
        private const float MAX_PLAYER_SIZE = 1.2f;
        #endregion

        #region Variables
        private Player _previousPlayer;
        #endregion
        
        void OnEnable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged += DisplayEffects;
        }
    
        void OnDisable()
        {
            PlayerManager.Instance.OnCurrentPlayerChanged -= DisplayEffects;
        }

        private void Start()
        {
            var currentPlayer = PlayerManager.Instance.CurrentPlayer;
            _previousPlayer = currentPlayer;
        }

        private void DisplayEffects(Player player)
        {
            AdjustScales(player);
            AdjustRideHeights(player);
            AdjustMaterials(player);

            _previousPlayer = player;
        }
    
        private void AdjustScales(Player player)
        {
            AdjustScale(_previousPlayer, false);
            AdjustScale(player, true);
        }

        private void AdjustScale(Player player, bool shouldEnlarge)
        {
            StartCoroutine(EasedLerpScale(player.transform, shouldEnlarge));
        }

        private void AdjustRideHeights(Player player)
        {
            _previousPlayer.AdjustRideHeight(DEFAULT_RIDE_HEIGHT);
            player.AdjustRideHeight(UPPER_RIDE_HEIGHT);
        }

        private void AdjustMaterials(Player player)
        {
            if (_previousPlayer.id > player.id) // If selection moves to fewer players...
            {
                _previousPlayer.EnableDitherMode();
            }
            player.RestoreDefaultMaterials();
        }

        // I tried to seperate this function into it's own utilities file (see Easing.EasedLerp), as similar variations
        // in a few places. However, I can't see how I could  get the IEnumerator to return a (i.e. float size) on every
        // loop, which the caller would pick up on. 
        private IEnumerator EasedLerpScale(Transform transform, bool shouldEnlarge)
        {
            var t = 0f;
            var easedT = 0f;
            while (Mathf.Abs(t) < 1f)
            {
                easedT = EasingUtils.Back.Out(t);
                var size = (shouldEnlarge ? DEFAULT_PLAYER_SIZE : MAX_PLAYER_SIZE) + easedT * (MAX_PLAYER_SIZE - DEFAULT_PLAYER_SIZE) * (shouldEnlarge ? 1 : -1);
                
                /*transform.GetComponent<SquashAndStretch>().LocalEquilibriumScale = Vector3.one * size;
                t += Time.deltaTime;
                yield return null;*/
                
                transform.GetComponent<SquashAndStretch>().LocalEquilibriumScale = Vector3.one * size;
                yield return new WaitForFixedUpdate(); // Surely this is poor practise.
                t += Time.deltaTime; // Is this the correct deltaTime for a IEnumerator?
            }
        }
    }
}
