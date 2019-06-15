using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToLayer : MonoBehaviour {
    [SerializeField] Transform targetObject = null;

    [SerializeField] LayerMask layer = new LayerMask();

    void LateUpdate() {
        Ray downwardsRay = new Ray(targetObject.transform.position + Vector3.up * 10, Vector3.down);
        if (Physics.Raycast(downwardsRay, out RaycastHit rayHit, 20, layer)) {
            targetObject.position = rayHit.point; 
        }
    }
}
