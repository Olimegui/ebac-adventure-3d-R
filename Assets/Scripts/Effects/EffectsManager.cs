using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Ebac.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    [Header("Referências")]
    public PostProcessVolume processVolume;

    [Header("Parâmetros de Vinheta")]
    [SerializeField] private float duration = 1f;
    [SerializeField] private Color startColor = Color.black;
    [SerializeField] private Color endColor = Color.red;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    private IEnumerator FlashColorVignette()
    {
        // Verifica se o volume e perfil estão atribuídos
        if (processVolume == null || processVolume.profile == null)
        {
            Debug.LogError("PostProcessVolume ou seu perfil está nulo!");
            yield break;
        }

        // Tenta pegar o efeito Vignette do perfil
        Vignette vignette;
        if (!processVolume.profile.TryGetSettings(out vignette))
        {
            Debug.LogError("Efeito Vignette não encontrado no perfil!");
            yield break;
        }

        // Configura o parâmetro de cor
        ColorParameter colorParam = new ColorParameter();

        float time = 0f;

        // Transição: Preto → Vermelho
        while (time < duration)
        {
            colorParam.value = Color.Lerp(startColor, endColor, time / duration);
            vignette.color.Override(colorParam);
            time += Time.deltaTime;
            yield return null;
        }

        // Transição: Vermelho → Preto
        time = 0f;
        while (time < duration)
        {
            colorParam.value = Color.Lerp(endColor, startColor, time / duration);
            vignette.color.Override(colorParam);
            time += Time.deltaTime;
            yield return null;
        }

        // Restaura cor final (opcional)
        vignette.color.Override(new ColorParameter { value = startColor });
    }
}