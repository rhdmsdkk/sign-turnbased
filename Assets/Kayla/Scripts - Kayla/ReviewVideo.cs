using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ReviewVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public BattleSystem battleSystem;

    void Awake()
    {
        if (videoPlayer == null)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }
        
        if (rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
        }
        
        SetupVideoPlayer();
    }

    void SetupVideoPlayer()
    {
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        
        RenderTexture renderTexture = new RenderTexture(1920, 1080, 24);
        videoPlayer.targetTexture = renderTexture;
        rawImage.texture = renderTexture;
    }

    public void PlayReviewVideo(string signWord)
    {
        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Reference Videos", signWord + ".mp4");

        videoPlayer.url = fullPath;
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += (source) => videoPlayer.Play();
    }

    public void StopReviewVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
    }

    void OnEnable()
    {
        rawImage.enabled = true;
        videoPlayer.enabled = true;
    }

    void OnDisable()
    {
        rawImage.enabled = false;
        videoPlayer.enabled = false;
        StopReviewVideo();
    }
}
