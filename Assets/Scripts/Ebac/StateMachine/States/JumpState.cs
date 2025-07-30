using UnityEngine;
using Ebac.StateMachine;

public class JumpState : StateBase
{
    public override void OnStateEnter(params object[] args)
    {
        Debug.Log("🔼 Entrando no estado de pulo");

        var player = Player.Instance;
        if (player != null)
        {
            // Animação
            if (player.animator != null)
                player.animator.SetTrigger("Jump");

            // Aplicação de força
            if (player.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.velocity = Vector3.zero; // opcional: limpa velocidade anterior
                rb.AddForce(Vector3.up * player.jumpSpeed, ForceMode.Impulse);
            }
        }
    }

    public override void OnStateStay()
    {
        // Pode ser usado para efeitos visuais ou controle aéreo
    }

    public override void OnStateExit()
    {
        Debug.Log("⬇️ Saindo do estado de pulo");
        // Limpar triggers ou variáveis se necessário
    }
}
