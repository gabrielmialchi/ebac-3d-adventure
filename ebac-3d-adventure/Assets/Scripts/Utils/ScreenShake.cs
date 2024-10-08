using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class ScreenShake : Singleton<ScreenShake>
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin c;

    private float shakeTime;

    [Header("Shake Settings")]
    public float amplitude = 3f;
    public float frequency = 10f;
    public float time = .3f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        ShakeCamera(amplitude, frequency, time);
    }

    public void ShakeCamera(float amplitude, float frequency, float time)
    {
        c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        c.m_AmplitudeGain = amplitude;
        c.m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            c.m_AmplitudeGain = 0f;
            c.m_FrequencyGain = 0f;
        }
    }
}
