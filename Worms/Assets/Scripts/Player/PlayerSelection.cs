using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    // Constants
    private const float _defaultRideHeight = 2f;
    private const float _increasedRideHeight = 2.5f;

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
        AdjustRideHeights(player);
        AdjustMaterials(player);

        _previousPlayer = player;
    }

    private void AdjustRideHeights(Player player)
    {
        _previousPlayer.AdjustRideHeight(_defaultRideHeight);
        player.AdjustRideHeight(_increasedRideHeight);
    }

    private void AdjustMaterials(Player player)
    {
        if (_previousPlayer.id > player.id) // If selection moves to fewer players...
        {
            _previousPlayer.EnableDitherMode();
        }
        player.DisableDitherMode();
    }

    public static void NextPlayer()
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

    public static void PreviousPlayer()
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
            player.AdjustRideHeight(_defaultRideHeight);
            player.SetLookDirectionOption(PhysicsBasedCharacterController.lookDirectionOptions.velocity);
            player.DisableDitherMode();
            player.EnableParticleSystem();
        }

        // Load scene
        LoadingScreen.LoadScene(SceneIndices.GAME);
        LoadingScreen.TransitionFromLoadingScreen();
    }
}
