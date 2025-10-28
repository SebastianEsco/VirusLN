using UnityEngine;
using UnityEngine.Video;

public class WebGLVideoFix : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        // En WebGL, forzar la carga completa antes de reproducir
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (v) =>
        {
            v.time = 0;
        };
#else
        videoPlayer.Prepare();
        videoPlayer.time = 0;
#endif
    }

    public void Replay()
    {
        videoPlayer.Stop();
        videoPlayer.time = 0;
        videoPlayer.Play();
    }
}
