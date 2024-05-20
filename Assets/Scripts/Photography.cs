using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Photorensic
{
    public class Photography : MonoBehaviour
    {
        [Header("Photo Taker")]
        private Texture2D screenCapture;
        private bool viewingPhoto;

        [SerializeField] private Image photoDisplayArea;
        [SerializeField] private GameObject PhotoFrame;

        [Header("Flash")]
        [SerializeField] private GameObject cameraFlash;
        [SerializeField] private GameObject FlashUI;
        [SerializeField] private float flashTime;
        [SerializeField] private Animator flashAnimator;
        [SerializeField] private Animator FadingAnimation;

        [SerializeField] private GameObject CameraUI;
        [SerializeField] private AudioSource cameraAudio;
        [SerializeField] private GameObject PhotoFrame2;
        [SerializeField] private VolumeProfile gameVolume;

        [Header("Photography Switch")]
        [SerializeField] private GameObject gameView;
        [SerializeField] private GameObject cameraView;

        [Header("Show Photo Puzzle")]
        [SerializeField] private GameObject SlidingPhotoPuzzle;

        [Header("Character Controller")]
        public FPCharacterController fpCharacterController;

        [Header("Collider Trigger")]
        [SerializeField] public Collider specificCollider; // Ensure this is assigned in the Inspector
        private bool isPlayerInsideCollider = false;

        private bool AccessCamera = false;

        private void Start()
        {
            screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            cameraFlash.SetActive(false);
            CameraUI.SetActive(false);
            PhotoFrame.SetActive(false);
            PhotoFrame2.SetActive(false);
            cameraView.SetActive(false);
            SlidingPhotoPuzzle.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Debug.Log("isPlayerInsideCollider: " + isPlayerInsideCollider);
            Debug.Log("AccessCamera: " + AccessCamera);
            if (Input.GetKey(KeyCode.R))
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
            if (screenCapture == null)
            {
                Debug.LogError("screenCapture is not initialized!");
                yield break; // Exit the method early to avoid further errors
            }

            // Proceed with taking the photo
            CameraUI.SetActive(false);
            gameView.SetActive(false);
            cameraView.SetActive(true);
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

            if (isPlayerInsideCollider)
            {
                SlidingPhotoPuzzle.SetActive(true);
            }

            FadingAnimation.Play("Flash");
            Cursor.lockState = CursorLockMode.None;
            if (fpCharacterController != null)
            {
                fpCharacterController.SetMovementEnabled(false);
                fpCharacterController.SetRotationEnabled(false);
            }
            Cursor.visible = true;
            StartCoroutine(HidePhotoFrameAfterDelay(5f));
        }

        IEnumerator HidePhotoFrameAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            RemovePhoto();
        }

        IEnumerator CameraFlashEffect()
        {
            gameView.SetActive(true);
            cameraView.SetActive(false);
            flashAnimator.Play("FlashAnimator");
            cameraFlash.SetActive(true);
            FlashUI.SetActive(true);
            yield return new WaitForSeconds(flashTime);
            cameraFlash.SetActive(false);
            cameraAudio.Play();
        }

        public void RemovePhoto()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PhotoFrame.SetActive(false);
            viewingPhoto = false;
            CameraUI.SetActive(true);
            SlidingPhotoPuzzle.SetActive(false);

            if (fpCharacterController != null)
            {
                fpCharacterController.SetMovementEnabled(true);
                fpCharacterController.SetRotationEnabled(true);
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInsideCollider = true;
                Debug.Log("Player entered collider.");
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInsideCollider = false;
                Debug.Log("Player exited collider.");
            }
        }

        void OnDrawGizmosSelected()
        {
            if (specificCollider != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(specificCollider.bounds.center, specificCollider.bounds.size);
            }
        }
    }
}
