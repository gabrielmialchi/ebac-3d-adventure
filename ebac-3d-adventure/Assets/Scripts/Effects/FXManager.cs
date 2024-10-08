using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class FXManager : MonoBehaviour
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette _vignette;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        c.value = Color.red;

        _vignette.color = c;
    }
}
