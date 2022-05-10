using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomController : MonoBehaviour
{

	public new Rigidbody rigidbody;
	public new CapsuleCollider collider;
	public LayerMask ignore;

	private readonly Collider[] overlaps = new Collider[5];
	private readonly List<RaycastHit> contacts = new List<RaycastHit>();
    private const float MinMoveDistance = 0f;
    private void Start()
    {
		rigidbody.freezeRotation = true;
    }
    public void Move(Vector3 motion)
	{
		GetState(motion);
		ClearVariables();
		HandleCollision();
		HandleContacts();
		Depenetrate();
		SetState();
	}

	private void GetState(Vector3 motion)
	{
		rigidbody.velocity = motion;
	}

	private void SetState()
	{
		rigidbody.MovePosition(rigidbody.position);
	}

	private void ClearVariables()
	{
		contacts.Clear();
	}

	private void HandleCollision()
	{
		if (rigidbody.velocity.sqrMagnitude > MinMoveDistance)
		{
			Vector3 localVelocity = transform.InverseTransformDirection(rigidbody.velocity) * Time.deltaTime;
			Vector3 lateralVelocity = new Vector3(localVelocity.x, 0, localVelocity.z);
			Vector3 verticalVelocity = new Vector3(0, localVelocity.y, 0);

			lateralVelocity = transform.TransformDirection(lateralVelocity);
			verticalVelocity = transform.TransformDirection(verticalVelocity);

			CapsuleSweep(lateralVelocity.normalized, lateralVelocity.magnitude, 0);
			CapsuleSweep(verticalVelocity.normalized, verticalVelocity.magnitude, 0);
		}
	}

	private void HandleContacts()
	{
		if (contacts.Count > 0)
		{

			foreach (RaycastHit contact in contacts)
			{

				rigidbody.velocity -= Vector3.Project(rigidbody.velocity, contact.normal);
			}
		}
	}

	private void CapsuleSweep(Vector3 direction, float distance, float stepOffset, float minSlideAngle = 180, float maxSlideAngle = 360)
	{
		Vector3 origin, top, bottom;
		RaycastHit hitInfo;
		float safeDistance;
		float slideAngle;

		float capsuleOffset = collider.height * 0.5f - collider.radius;

		for (int i = 0; i < 5; i++)
		{
			origin = rigidbody.position + collider.center - direction * collider.radius;
			bottom = origin - rigidbody.transform.up * (capsuleOffset - stepOffset);
			top = origin + rigidbody.transform.up * capsuleOffset;

			if (Physics.CapsuleCast(top, bottom, collider.radius, direction, out hitInfo, distance + collider.radius))
			{
				slideAngle = Vector3.Angle(rigidbody.transform.up, hitInfo.normal);
				safeDistance = hitInfo.distance - collider.radius;
				rigidbody.position += direction * safeDistance;
				contacts.Add(hitInfo);

				if ((slideAngle >= minSlideAngle) && (slideAngle <= maxSlideAngle))
				{
					break;
				}

				direction = Vector3.ProjectOnPlane(direction, hitInfo.normal);
				distance -= safeDistance;
			}
			else
			{
				rigidbody.position += direction * distance;
				break;
			}
		}
	}

	private void Depenetrate()
	{
		float capsuleOffset = collider.height * 0.5f - collider.radius;
		Vector3 top = rigidbody.position + rigidbody.transform.up * capsuleOffset;
		Vector3 bottom = rigidbody.position - rigidbody.transform.up * capsuleOffset;
		int overlapsNum = Physics.OverlapCapsuleNonAlloc(top, bottom, collider.radius, overlaps, ignore , QueryTriggerInteraction.Ignore);

		if (overlapsNum > 0)
		{
			for (int i = 0; i < overlapsNum; i++)
			{
				if ((overlaps[i].transform != transform) && Physics.ComputePenetration(collider, rigidbody.position, transform.rotation,
					overlaps[i], overlaps[i].transform.position, overlaps[i].transform.rotation, out Vector3 direction, out float distance))
				{
					rigidbody.position += direction * distance;
					rigidbody.velocity -= Vector3.Project(rigidbody.velocity, -direction);
				}
			}
		}
	}
}
