using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[SerializeField]
	private Transform turretTop;
	[SerializeField]
	private Transform target;

	void Update () {
		turretTop.LookAt(target);
	}
}
