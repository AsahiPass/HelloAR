using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class ARPlaceCube : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ARRaycastManager raycastManager;
    bool isPlacing = false;

    // Update is called once per frame
    void Update()
    {
        // Touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame && !isPlacing)
        {
            isPlacing = true;
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            PlaceObject(touchPos);
        }
        // Mouse input (for Editor testing)
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && !isPlacing)
        {
            isPlacing = true;
            Vector2 mousePos = Mouse.current.position.ReadValue();
            PlaceObject(mousePos);
        }
    }
    void PlaceObject(Vector2 touchPosition)
    {
        var rayHits = new List<ARRaycastHit>();
        raycastManager.Raycast(touchPosition, rayHits, TrackableType.AllTypes);

        if (rayHits.Count > 0)
        {
            Vector3 hitPosePosition = rayHits[0].pose.position;
            Quaternion hitPoseRotation = rayHits[0].pose.rotation;
            Instantiate(raycastManager.raycastPrefab, hitPosePosition, hitPoseRotation);

        }
        StartCoroutine(SetIsPlaceingToFalseWithDelay());
    }

    IEnumerator SetIsPlaceingToFalseWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        isPlacing = false;
    }
}
