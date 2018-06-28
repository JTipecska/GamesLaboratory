using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject controlledLight;
    public Material movePlaneMaterial;
    public float limitX = 10;
    public float limitZ = 10;

    GameObject movePlane;
    Vector3 anchor;

	// Use this for initialization
	void Start () {
        Data.interactableObjects.Add(gameObject);
        anchor = controlledLight.transform.position;

        if (movePlaneMaterial)
        {
            // Create MovePlane
            movePlane = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Quad));
            movePlane.name = "MovePlane of " + transform.name;
            movePlane.transform.parent = GameObject.Find("_Dynamic").transform;
            movePlane.transform.position = anchor;
            movePlane.transform.localScale = new Vector3(2 * limitX, 1, 2 * limitZ);
            movePlane.GetComponent<Renderer>().material = movePlaneMaterial;
            Destroy(movePlane.GetComponent<MeshCollider>());
            movePlane.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        controlledLight.transform.position = new Vector3(Clamp(anchor.x - limitX, anchor.x + limitX, controlledLight.transform.position.x), controlledLight.transform.position.y, Clamp(anchor.z - limitZ, anchor.z + limitZ, controlledLight.transform.position.z));
    }

    void Action()
    {
        Data.realCharacter.GetComponent<RealCharacterController>().SetLightController(gameObject);
        ToggleLightMoveArea();
    }
    void ToggleLightMoveArea()
    {
        if (movePlane)
            movePlane.SetActive(!movePlane.activeSelf);
    }

    private float Clamp(float min, float max, float val)
    {
        return val < min ? min : (val < max ? val : max);
    }
}
