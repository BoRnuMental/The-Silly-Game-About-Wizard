using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

public class HallucinationSpell : BaseSpell
{
    private Volume _volume;
    private SoundSystem _soundSystem;

    [Inject]
    private void Construct(Volume volume, SoundSystem soundSystem)
    {
        _volume = volume;
    }
    public override void DoMagic()
    {
        if (_volume.profile.TryGet<ChromaticAberration>(out var component))
        {
            DOTween.To(() => component.intensity.value, x => component.intensity.value = x, 1f, 5f);
        }
    }
}
