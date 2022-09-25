using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerSelection : MonoBehaviour
{
    // Constants
    private const float DEFAULT_RIDE_HEIGHT = 2f;
    private const float UPPER_RIDE_HEIGHT = 2.5f;

    private const float MIN_PLAYER_SIZE = 0.8f;
    private const float MAX_PLAYER_SIZE = 1f;

    // Variables
    private Player _previousPlayer;

    private void OnEnable()
    {
        PlayerManager.OnPlayerChanged += ChangePlayerSelection;
        LoadingScreen.OnTransitionedToLoadingScreen += BehindTheCurtain;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerChanged -= ChangePlayerSelection;
        LoadingScreen.OnTransitionedToLoadingScreen -= BehindTheCurtain;
    }

    private void Start()
    {
        _previousPlayer = PlayerManager.currentPlayer;
    }

    private void ChangePlayerSelection(Player player)
    {
        AdjustScales(player);
        AdjustRideHeights(player);
        AdjustMaterials(player);

        _previousPlayer = player;
    }

    private void AdjustScales(Player player)
    {
        StartCoroutine(EasedLerpScale(_previousPlayer.transform, false));
        StartCoroutine(EasedLerpScale(player.transform, true));
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
        player.DisableDitherMode();
    }

    public void NextPlayer()
    {
        if (PlayerManager.currentPlayer.id < 3)
        {
            PlayerManager.SetCurrentPlayer((PlayerManager.currentPlayer.id + 1));
        }
        else
        {
            CinemachineShake.Instance.ShakeCamera();
        }
    }

    public void PreviousPlayer()
    {
        if (PlayerManager.currentPlayer.id > 0)
        {
            PlayerManager.SetCurrentPlayer((PlayerManager.currentPlayer.id - 1));
        }
        else
        {
            CinemachineShake.Instance.ShakeCamera();
        }
    }

    private IEnumerator EasedLerpScale(Transform transform, bool shouldEnlarge)
    {
        var t = 0f;
        var easedT = 0f;
        while (Mathf.Abs(t) < 1f)
        {
            easedT = Easing.Back.Out(t);
            var size = (shouldEnlarge ? MIN_PLAYER_SIZE : MAX_PLAYER_SIZE) + easedT * (MAX_PLAYER_SIZE - MIN_PLAYER_SIZE) * (shouldEnlarge ? 1 : -1);
            //transform.localScale = Vector3.one * size;
            transform.GetComponent<SquashAndStretch>()._localEquilibriumScale = Vector3.one * size;
            yield return new WaitForFixedUpdate(); // Surely this is poor practise.
            t += Time.deltaTime; // Is this the correct deltaTime for a IEnumerator?
        }
        yield break;
    }

    public void FinaliseSelection()
    {
        StartCoroutine(LoadingScreen.TransitionToLoadingScreen());
    }

    private void BehindTheCurtain()
    {
        PlayerManager.FinaliseNumberOfPlayers(PlayerManager.currentPlayer.id + 1);
        // Remove excess Players from list
        // Delete excess Player game objects
        // Reset current player

        // For each remaining player...
        foreach (Player player in PlayerManager.players)
        {
            player.AdjustRideHeight(DEFAULT_RIDE_HEIGHT);
            player.SetLookDirectionOption(PhysicsBasedCharacterController.lookDirectionOptions.velocity);
            player.DisableDitherMode();
            player.EnableParticleSystem();
        }

        // Load scene
        LoadingScreen.LoadScene(SceneIndices.GAME);
        LoadingScreen.TransitionFromLoadingScreen();
    }
}
