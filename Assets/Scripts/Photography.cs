using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Photography : MonoBehaviour
{
    [Header("Photo Taker")]
    private Texture2D screenCapture;
    private bool viewingPhoto;

    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject PhotoFrame;
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private GameObject FlashUI;
    [SerializeField] private float flashTime;
    [SerializeField] private Animator flashAnimator;
    [SerializeField] private Animator FadingAnimation;
    [SerializeField] private GameObject CameraUI;
    [SerializeField] private AudioSource cameraAudio;
    [SerializeField] private GameObject PhotoFrame2;
    [SerializeField] private VolumeProfile gameVolume;

    private bool AccessCamera = false;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        cameraFlash.SetActive(false);
        CameraUI.SetActive(false);
        PhotoFrame.SetActive(false);
        PhotoFrame2.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (AccessCamera)
            {
                CameraUI.SetActive(false);
                AccessCamera = false;
            }
            else
            {
                CameraUI.SetActive(true);
                AccessCamera = true;
            }
        }

        if (Input.GetMouseButtonDown(1) && AccessCamera)
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }
        }
    }

    IEnumerator CapturePhoto()
    {
        CameraUI.SetActive(false);
        yield return new WaitForEndOfFrame();
        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        PhotoFrame.SetActive(true);
        FlashUI.SetActive(true);
        StartCoroutine(CameraFlashEffect());
        FadingAnimation.Play("Flash");
        StartCoroutine(HidePhotoFrameAfterDelay(5f));
    }

    IEnumerator HidePhotoFrameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RemovePhoto();
    }

    IEnumerator CameraFlashEffect()
    {
        flashAnimator.Play("FlashAnimator");
        cameraFlash.SetActive(true);
        FlashUI.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
        cameraAudio.Play();
    }

    public void RemovePhoto()
    {
        PhotoFrame.SetActive(false);
        viewingPhoto = false;
        CameraUI.SetActive(true);
    }
}
