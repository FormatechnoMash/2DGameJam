using UnityEngine;
using CharacterController.Runtime;

namespace CharacterController.Runtime
{
public class EnnemyBulletDamage : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CharacterController controller = collider.gameObject.GetComponent<CharacterController>();
            controller.DecreaseHealthpoint();
        }

    }
}
}
