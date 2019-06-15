using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask buildingMask;

    [SerializeField] ObjectTest currentBuilding = null;

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            if(currentBuilding != null) {
                currentBuilding = null;
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, buildingMask)) {
                if(currentBuilding == null) {
                    currentBuilding = hit.rigidbody.gameObject.GetComponent<ObjectTest>();
                }
            }
        }     

        if(currentBuilding != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          
            if (Physics.Raycast(ray, out RaycastHit hit, groundMask)) {
                currentBuilding.transform.position = hit.point;
            }
        }
    }
}
