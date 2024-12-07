
using UnityEngine;

namespace Steerings
{
    public class InterfereMovement: SteeringBehaviour
    {

        public RotationalPolicy rotationalPolicy = RotationalPolicy.FTI;
        public GameObject target;
        public float requiredDistance=60;


        public override SteeringOutput GetSteering()
        {
            if (this.ownKS == null) this.ownKS = GetComponent<KinematicState>();

            SteeringOutput result = InterfereMovement.GetSteering(this.ownKS, this.target, this.requiredDistance);

            base.applyRotationalPolicy(rotationalPolicy, result, target);

            return result;
        }

        public static SteeringOutput GetSteering(KinematicState _ownKS, GameObject _target, float _requiredDistance)
        {
            
            KinematicState targetKS = _target.GetComponent<KinematicState>();
            if (targetKS == null)
            {
                Debug.LogError("Target does not have a KinematicState component.");
                return NULL_STEERING;
            }

            // Calculate the direction in which the target is moving
            Vector3 directionOfMovement = targetKS.linearVelocity.normalized;

            // Calculate the required position in front of the target
            Vector3 requiredPosition = _target.transform.position + directionOfMovement * _requiredDistance;

            // Set the surrogate target's position to the required position
            SURROGATE_TARGET.transform.position = requiredPosition;

            // Use the Arrive behavior to move to the surrogate target's position
            return Arrive.GetSteering(_ownKS, SURROGATE_TARGET);
        }
    }
}
