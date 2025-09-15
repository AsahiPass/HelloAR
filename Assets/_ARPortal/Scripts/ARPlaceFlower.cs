using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class ARPlaceFlower : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Flower Prefabs")]
    [SerializeField] private GameObject yellowFlower;
    [SerializeField] private GameObject rose;
    [SerializeField] private GameObject colorFlower;

    private GameObject seletedFlower;

    [Header("AR Setting")]
    [SerializeField] private ARRaycastManager raycastManager;

    [Header("SoundEffect")]
    [SerializeField] private AudioClip plantSound;
    private AudioSource plantSource;



    private bool isPlacing = false;

    void Start()
    {
        seletedFlower = yellowFlower;   
        plantSource = GetComponent<AudioSource>();
        plantSource.playOnAwake = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (IsPointerOverUI())
        {
            return;
        }
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
            if (seletedFlower != null)
            {
                Instantiate(seletedFlower, hitPosePosition, hitPoseRotation);
                plantSource.PlayOneShot(plantSound);
            }
            

        }
        StartCoroutine(SetIsPlaceingToFalseWithDelay());
    }

    IEnumerator SetIsPlaceingToFalseWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        isPlacing = false;
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return true;
        if (Input.touchCount >0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return true;
        return false;
    
    }
    public void SelectYellowFlower()
    {
        seletedFlower = yellowFlower;
        Debug.Log("Selected Yellow Flower");
    }

    public void SelectRose()
    {
        seletedFlower = rose;
        Debug.Log("Selected rose");
    }

    public void SelectColorFlower()
    {
        seletedFlower =colorFlower;
        Debug.Log("Selected color Flower");
    }

}
