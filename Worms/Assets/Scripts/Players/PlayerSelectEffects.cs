using System.Collections;
using Oscillators;
using UnityEngine;

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
    
        private void DisplayEffects(global::Players.Player player)
        {
            AdjustScales(player);
            AdjustRideHeights(player);
            AdjustMaterials(player);

            _previousPlayer = player;
        }
    
        private void AdjustScales(global::Players.Player player)
        {
            AdjustScale(_previousPlayer, false);
            AdjustScale(player, true);
        }

        private void AdjustScale(global::Players.Player player, bool shouldEnlarge)
        {
            StartCoroutine(EasedLerpScale(player.transform, shouldEnlarge));
        }

        private void AdjustRideHeights(global::Players.Player player)
        {
            _previousPlayer.AdjustRideHeight(DEFAULT_RIDE_HEIGHT);
            player.AdjustRideHeight(UPPER_RIDE_HEIGHT);
        }

        private void AdjustMaterials(global::Players.Player player)
        {
            if (_previousPlayer.id > player.id) // If selection moves to fewer players...
            {
                _previousPlayer.EnableDitherMode();
            }
            player.RestoreDefaultMaterials();
        }

        private IEnumerator EasedLerpScale(Transform transform, bool shouldEnlarge)
        {
            var t = 0f;
            var easedT = 0f;
            while (Mathf.Abs(t) < 1f)
            {
                easedT = Easing.Back.Out(t);
                var size = (shouldEnlarge ? DEFAULT_PLAYER_SIZE : MAX_PLAYER_SIZE) + easedT * (MAX_PLAYER_SIZE - DEFAULT_PLAYER_SIZE) * (shouldEnlarge ? 1 : -1);
                transform.GetComponent<SquashAndStretch>().LocalEquilibriumScale = Vector3.one * size;
                t += Time.deltaTime;
                yield return null;
            }
        }
    
    }
}
