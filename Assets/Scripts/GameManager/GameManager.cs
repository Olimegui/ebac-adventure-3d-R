using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates { JUMP, LAND, DEATH }

    private GameStates currentState;

    public GameStates CurrentState => currentState;

    public void SwitchState(GameStates state)
    {
        currentState = state;
        Debug.Log("Mudando para estado: " + currentState);

        var player = Player.Instance;
        if (player == null) return;

        // Resetando triggers antigos antes de definir novo estado
        player.animator.ResetTrigger("Jump");
        player.animator.ResetTrigger("Land");
        player.animator.ResetTrigger("Death");


        switch (state)
        {
            case GameStates.JUMP:
                if (player != null) player.animator.SetTrigger("Jump");
                break;

            case GameStates.LAND:
                if (player != null) player.animator.SetTrigger("Land");
                break;

            case GameStates.DEATH:
                if (player != null) player.animator.SetTrigger("Death");
                break;
        }
    }
}