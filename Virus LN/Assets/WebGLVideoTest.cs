using UnityEngine;
using UnityEngine.Video;

public class WebGLVideoFix : MonoBehaviour
{
    // Asigna este componente en el Inspector
    public VideoPlayer videoPlayer;

    // 💡 IMPORTANTE: Escribe aquí el nombre EXACTO de tu archivo (ej: "intro.mp4")
    public string videoFileName = "video_por_defecto.mp4";

    void Start()
    {
        if (videoPlayer == null)
        {
            Debug.LogError("El VideoPlayer no está asignado en el Inspector.");
            return;
        }

        // 1. Configurar la fuente como URL (siempre necesario para rutas de StreamingAssets)
        videoPlayer.source = VideoSource.Url;

        // 2. Construir la ruta usando Application.streamingAssetsPath
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

        // 3. Asignar la URL al VideoPlayer
        videoPlayer.url = videoPath;

        // --- Lógica de preparación y reproducción ---

#if UNITY_WEBGL && !UNITY_EDITOR
        // En WebGL, el video debe ser cargado (Prepared) antes de reproducirse.
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (v) =>
        {
            // Opcional: Esto asegura que la reproducción comience desde el inicio.
            v.time = 0;
            // Aquí puedes añadir v.Play() si quieres que inicie automáticamente.
        };
        
        // **Recomendación:** Comprueba si la ruta es correcta en la consola del navegador.
        Debug.Log("WebGL: Asignada URL: " + videoPath);

#else
        // Para Editor (u otras plataformas), la carga es más inmediata.
        videoPlayer.Prepare();
        videoPlayer.time = 0;

#endif
    }

    public void Replay()
    {
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Stop();
            videoPlayer.time = 0;
            videoPlayer.Play();
        }
        else
        {
            // Si por alguna razón no estaba preparado, lo preparamos de nuevo.
            videoPlayer.Prepare();
        }
    }
}