using UnityEngine;
using UnityEngine.SceneManagement;

namespace Photorensic
{
    public class InteractionScript : MonoBehaviour
    {

        public GameObject InteractE;
        private bool triggerenter;


        public void Start()
        {
            InteractE.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            InteractE.SetActive(true);
            Debug.Log("EnterCollider");
            triggerenter = true;
        }

        private void OnTriggerExit(Collider other)
        {
            InteractE.SetActive(false);
            Debug.Log("ExitCollider");
            triggerenter = false;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && triggerenter)
            {
                triggerenter = true;
                SceneManager.LoadScene("Cutscene Intro");
                Debug.Log("ChangeScene");
            }
        }

    }
}
