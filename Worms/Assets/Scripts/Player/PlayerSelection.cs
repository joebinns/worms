using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSelection : MonoBehaviour
{
    // Constants
    private const float DEFAULT_RIDE_HEIGHT = 2f;
    private const float UPPER_RIDE_HEIGHT = 2.75f;

    private const float MIN_PLAYER_SIZE = 1f;
    private const float MAX_PLAYER_SIZE = 1.2f;

    // Variables
    private Player _previousPlayer;

    public TMP_InputField nameInput;
    public HatRack hatRack;

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
        player.DisableDitherMode();
    }

    public void NextPlayer()
    {
        var currentPlayer = PlayerManager.currentPlayer;
        if (currentPlayer.id < 3)
        {
            // Save Edits to Player
            currentPlayer.playerName = nameInput.text;
            currentPlayer.SetHat(hatRack.currentHat);
            currentPlayer.hasUserEdits = true;

            // Activate players hat    
            currentPlayer.hat.SetActive(true);
            currentPlayer.hat.transform.localPosition = Vector3.up * 0.825f;

            // Change Player
            PlayerManager.SetCurrentPlayer(currentPlayer.id + 1);
            currentPlayer = PlayerManager.currentPlayer;

            if (currentPlayer.hasUserEdits)
            {
                // Restore any saved edits
                nameInput.text = currentPlayer.playerName;
                hatRack.ChangeHat(currentPlayer.hat.GetComponent<Hat>().id);
            }
            else
            {
                // Otherwise restore to default
                nameInput.text = "";
                hatRack.ChangeHat(0);
            }
            
        }
        else
        {
            CinemachineShake.Instance.ShakeCamera();
        }
    }

    public void PreviousPlayer()
    {
        var currentPlayer = PlayerManager.currentPlayer;
        if (currentPlayer.id > 0)
        {
            // Save Edits to Player
            currentPlayer.playerName = nameInput.text;
            currentPlayer.SetHat(hatRack.currentHat);
            currentPlayer.hasUserEdits = true;

            // Deactive players hat
            currentPlayer.hat.SetActive(false);

            // Change Player
            PlayerManager.SetCurrentPlayer(currentPlayer.id - 1);
            currentPlayer = PlayerManager.currentPlayer;

            // Deactive players hat
            currentPlayer.hat.SetActive(false);

            if (currentPlayer.hasUserEdits) // This should always be the case...
            {
                // Restore saved edits
                nameInput.text = currentPlayer.playerName;
                hatRack.ChangeHat(currentPlayer.hat.GetComponent<Hat>().id);
            }

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
        var currentPlayer = PlayerManager.currentPlayer;
        AdjustScale(currentPlayer, false);

        StartCoroutine(LoadingScreen.TransitionToLoadingScreen());
    }

    private void BehindTheCurtain()
    {
        var currentPlayer = PlayerManager.currentPlayer;

        // Save Edits to final Player
        currentPlayer.playerName = nameInput.text;
        currentPlayer.SetHat(hatRack.currentHat);
        currentPlayer.hat.transform.localPosition = Vector3.up * 0.825f;
        currentPlayer.hasUserEdits = true;
        currentPlayer.hat.SetActive(true);

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
