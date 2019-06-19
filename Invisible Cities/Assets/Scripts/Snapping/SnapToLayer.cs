using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToLayer : MonoBehaviour {
    [Header ("Object References")]
    [SerializeField] Transform targetObject = null;

    [Header ("Settings")]
    [SerializeField] LayerMask layer = new LayerMask ();
    [SerializeField] int rayDistance = 10;

    void LateUpdate () {
        Ray downwardsRay = new Ray (this.targetObject.transform.position + Vector3.up * this.rayDistance, Vector3.down);
        if (Physics.Raycast (downwardsRay, out RaycastHit rayHit, this.rayDistance * 2, this.layer)) {
            this.targetObject.position = rayHit.point;
        }
    }
}
