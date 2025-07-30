using UnityEngine;
using Ebac.StateMachine;

public class LandState : StateBase
{
    public override void OnStateEnter(params object[] args)
    {
        Debug.Log("🛬 Entrando no estado LAND");

        var player = Player.Instance;

        if (player != null && player.animator != null)
        {
            player.animator.SetTrigger("Land");
        }
    }

    public override void OnStateStay()
    {
        // Pode aplicar lógica de idle, ou preparar para próximo movimento
    }

    public override void OnStateExit()
    {
        Debug.Log("🚶‍♂️ Saindo do estado LAND");
    }
}