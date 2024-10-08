using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Ebac.Core.Singleton;

public class FXManager : Singleton<FXManager>
{
    public PostProcessVolume processVolume;

    [SerializeField] private Vignette _vignette;

    public float duration = 2f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();
        FloatParameter f = new FloatParameter();

        float time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.grey, Color.red, time / duration);
            f.value = Mathf.Lerp(0.6f, 0.3f, time/duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            _vignette.intensity.Override(f);
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.grey, time / duration);
            f.value = Mathf.Lerp(0.3f, 0.6f, time / duration);
            time += Time.deltaTime;
            _vignette.color.Override(c);
            _vignette.intensity.Override(f);
            yield return new WaitForEndOfFrame();
        }
    }
}
