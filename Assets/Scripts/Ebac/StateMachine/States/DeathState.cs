using UnityEngine;
using Ebac.StateMachine;

public class DeathState : StateBase
{
    public override void OnStateEnter(params object[] args)
    {
        Debug.Log("💀 Entrando no estado DEATH");

        var player = Player.Instance;

        if (player != null)
        {
            // Animação de morte
            if (player.animator != null)
                player.animator.SetTrigger("Death");

            // Desativa colisor
            player.colliders.ForEach(c => c.enabled = false);

            // Desativa movimento, se tiver flag
            // player.canMove = false; // se existir

            // Pode iniciar lógica de respawn ou efeitos visuais
        }
    }

    public override void OnStateStay()
    {
        // Efeitos como câmera lenta ou fade
    }

    public override void OnStateExit()
    {
        Debug.Log("☠️ Saindo do estado DEATH");
        // Preparar para respawn, reativar colisor, etc.
    }
}