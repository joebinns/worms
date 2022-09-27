using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSelection : MonoBehaviour
{
    // Constants
    private const float DEFAULT_RIDE_HEIGHT = 2f;
    private const float UPPER_RIDE_HEIGHT = 2.75f;

    private const float DEFAULT_PLAYER_SIZE = 1f;
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
        var currentPlayer = PlayerManager.currentPlayer;
        _previousPlayer = currentPlayer;
        // Update name placeholder text
        ((TextMeshProUGUI)nameInput.placeholder).text = currentPlayer.playerSettings.suggestedName;
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
            // Activate players hat
            currentPlayer.hatSlot.gameObject.SetActive(true);

            // Change to next Player
            currentPlayer = ChangePlayer(currentPlayer.id + 1);
            
            // Deactivate new players hat
            //currentPlayer.hatSlot.gameObject.SetActive(false);

        }
        else
        {
            CinemachineShake.Instance.ShakeCamera();
        }
    }

    private Player ChangePlayer(int id)
    {
        var currentPlayer = PlayerManager.currentPlayer;
        
        // Save edits to Player
        currentPlayer.EditPlayerSettings(nameInput.text, hatRack.currentHat);
        
        // Change to next Player
        currentPlayer = PlayerManager.SetCurrentPlayer(id);

        // Update name placeholder text
        ((TextMeshProUGUI)nameInput.placeholder).text = currentPlayer.playerSettings.suggestedName;

        // Restore saved edits
        nameInput.text = currentPlayer.playerName;
        hatRack.ChangeHat(currentPlayer.playerSettings.hat.id);

        return currentPlayer;
    }

    public void PreviousPlayer()
    {
        var currentPlayer = PlayerManager.currentPlayer;
        if (currentPlayer.id > 0)
        {
            // Deactivate latest players hat
            currentPlayer.hatSlot.gameObject.SetActive(false);

            // Change to previous player
            currentPlayer = ChangePlayer(currentPlayer.id - 1);

            // Deactivate previous players hat
            currentPlayer.hatSlot.gameObject.SetActive(false);

        }
        else
        {
            CinemachineShake.Instance.ShakeCamera();
        }
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
        currentPlayer.EditPlayerSettings(nameInput.text, hatRack.currentHat);
        
        PlayerManager.FinaliseNumberOfPlayers(currentPlayer.id + 1);

        // For each remaining player...
        foreach (Player player in PlayerManager.players)
        {
            player.AdjustRideHeight(DEFAULT_RIDE_HEIGHT);
            player.SetLookDirectionOption(PhysicsBasedCharacterController.lookDirectionOptions.velocity);
            player.DisableDitherMode();
            player.EnableParticleSystem();

            // Save their settings
            player.PackPlayerSettings();
        }

        // Load scene
        LoadingScreen.LoadScene(SceneIndices.GAME);

        // Reveal new scene
        LoadingScreen.TransitionFromLoadingScreen();
    }
    
    private IEnumerator EasedLerpScale(Transform transform, bool shouldEnlarge)
    {
        var t = 0f;
        var easedT = 0f;
        while (Mathf.Abs(t) < 1f)
        {
            easedT = Easing.Back.Out(t);
            var size = (shouldEnlarge ? DEFAULT_PLAYER_SIZE : MAX_PLAYER_SIZE) + easedT * (MAX_PLAYER_SIZE - DEFAULT_PLAYER_SIZE) * (shouldEnlarge ? 1 : -1);
            //transform.localScale = Vector3.one * size;
            transform.GetComponent<SquashAndStretch>()._localEquilibriumScale = Vector3.one * size;
            yield return new WaitForFixedUpdate(); // Surely this is poor practise.
            t += Time.deltaTime; // Is this the correct deltaTime for a IEnumerator?
        }
        yield break;
    }
}
