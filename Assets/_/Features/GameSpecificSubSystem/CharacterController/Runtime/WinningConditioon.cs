using UnityEngine;

namespace CharacterController.Runtime
{
    public class WinningConditioon : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
                gameObject.SetActive(false);
                characterController.EndGame();
               
            }
        }
    }
}
