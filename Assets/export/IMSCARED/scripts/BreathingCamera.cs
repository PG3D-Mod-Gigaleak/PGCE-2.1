using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IMSCARED {
	public class BreathingCamera : MonoBehaviour {
		void Update() {
			transform.localPosition = new Vector3(0, Mathf.Sin(Time.time*0.45f)/8.5f, 0);
		}
	}
}