using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {
    public const int PRIMARY_MOUSE_BUTTON = 0;

    [Header ("Scene References")]
    [SerializeField] Transform ground = null;

    [Header ("Settings")]
    [SerializeField] LayerMask groundLayer = new LayerMask ();
    [SerializeField] LayerMask buildingLayer = new LayerMask ();

    [Header ("Read Only")]
    [SerializeField, ReadOnly] private PlaceableEntity currentBuilding = null;
    [SerializeField, ReadOnly] private Plane groundPlane;

    void Awake () {
        this.groundPlane = new Plane (Vector3.up, this.ground.position);
    }

    void Update () {
        if (Input.GetMouseButtonDown (PRIMARY_MOUSE_BUTTON)) {
            if (this.currentBuilding != null) {
                ClearCurrentBuilding ();
            }
            else {
                GetBuildingUnderMousePosition ();
            }
        }

        if (this.currentBuilding != null) {
            MoveBuildingToMousePosition ();
        }
    }

    private void GetBuildingUnderMousePosition () {
        Ray ray = CreateRayFromCursor ();
        if (ShootRayOnLayer (ray, this.buildingLayer, out RaycastHit hit)) {
            if (this.currentBuilding == null) {
                SetCurrentBuildingTo (hit.rigidbody.gameObject);
            }
        }
    }

    private void MoveBuildingToMousePosition () {
        Ray ray = CreateRayFromCursor ();
        if (ShootRayOnLayer (ray, this.groundLayer, out RaycastHit hit)) {
            this.currentBuilding.transform.position = hit.point;
        }
        else if (Math3d.IntersectRayAndPlane (ray, this.groundPlane, out Vector3 intersection)) {
            this.currentBuilding.transform.position = intersection;
        }
    }

    private Ray CreateRayFromCursor () {
        return Camera.main.ScreenPointToRay (Input.mousePosition);
    }

    private bool ShootRayOnLayer (Ray ray, LayerMask layerMask, out RaycastHit hit) {
        return Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask);
    }

    private void SetCurrentBuildingTo (GameObject gameObject) {
        PlaceableEntity objectTest = gameObject?.GetComponent<PlaceableEntity> ();

        if (objectTest != null && !objectTest.Fixated) {
            this.currentBuilding = objectTest;
            this.currentBuilding.Activate ();
        }
    }

    private void ClearCurrentBuilding () {
        this.currentBuilding.Deactivate ();
        this.currentBuilding.Fixate ();
        this.currentBuilding = null;
    }
}
