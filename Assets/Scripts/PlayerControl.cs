using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction movement;
    void Start()
    {
        
    }
    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }
    // Update is called once per frame
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 27.8f;
    [SerializeField] float yRange = 14f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    void Update()
    {
        processTranslation();
        processRotation();
    }

    void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToControlThrow * pitchDueToPosition;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void processTranslation()
    {
         xThrow = movement.ReadValue<Vector2>().x;
         yThrow = movement.ReadValue<Vector2>().y;


        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;

        float rawPosX = transform.localPosition.x + xOffset;
        float rawPosY = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawPosX, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawPosY, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
