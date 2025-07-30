using UnityEngine;
using Ebac.StateMachine;

public class PlayerFSM : MonoBehaviour
{
    private StateMachine<PlayerStates> fsm;
    public StateMachine<PlayerStates> stateMachine => fsm;

    public enum PlayerStates { JUMP, LAND, DEATH }

    public void Initialize()
    {
        fsm = new StateMachine<PlayerStates>(PlayerStates.LAND);

        fsm.RegisterStates(PlayerStates.JUMP, new JumpState());
        fsm.RegisterStates(PlayerStates.LAND, new LandState());
        fsm.RegisterStates(PlayerStates.DEATH, new DeathState());
    }

    public void SwitchState(PlayerStates state)
    {
        fsm.SwitchState(state);
    }

    public void Update()
    {
        fsm.Update();
    }
}