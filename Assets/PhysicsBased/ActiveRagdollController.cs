using UnityEngine;

public class ActiveRagdollController : MonoBehaviour
{
    public ConfigurableJoint joint;
    public Transform ragdollReference;

    private void Start()
    {
    }

    void Update()
    {
        ActivateRagdoll();
    }

    void ActivateRagdoll()
    {
        Quaternion reversedRotation = Quaternion.Euler(-ragdollReference.localEulerAngles);

        joint.targetRotation = reversedRotation;
        //joint.targetPosition = ragdollReference.position;
    }
}
