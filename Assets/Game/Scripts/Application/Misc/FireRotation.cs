using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRotation : MonoBehaviour
{
    private float _speed = 360f;

	void Update ()
    {
		transform.Rotate(Vector3.forward, _speed * Time.deltaTime, Space.Self);
	}
}
