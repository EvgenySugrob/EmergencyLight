using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCameraMenu : MonoBehaviour
{
    [SerializeField] GameObject rotationObject;
    [SerializeField] private Vector3 angleRotation = new Vector3(0f, 15f, 0f);
    [SerializeField] private Vector3 triggerPosition = new Vector3(-10.93f,8.50f,-22.49f);

    private Transform cameraRotation;
    private Quaternion startRotation;

    [SerializeField] MainMenuAnimationBackground mainAnimationBackground;
    [SerializeField] GameObject cameraAnimation;
    [SerializeField] Animator animator;

    private bool isAnim;

    private void Start()
    {
        cameraRotation = rotationObject.transform.GetChild(0).transform;
        startRotation = rotationObject.transform.rotation;

    }

    private void FixedUpdate()
    {
        
        rotationObject.transform.Rotate(angleRotation * Time.fixedDeltaTime);
        if (Vector3.Distance(cameraRotation.position, triggerPosition) <= 4 && isAnim)
        {
            isAnim = false;
            animator.SetBool("isPlay", true);
            Debug.Log("затухание");
        }
            

        if (Vector3.Distance(cameraRotation.position,triggerPosition)<=0.025)
        {
            Debug.Log("Есть контакт");
            SwapOnAnimationCamera();
        }
    }

    public void ReturnStartPosition()
    {
        rotationObject.transform.rotation = startRotation;
        isAnim= true;
    }

    public void SwapOnAnimationCamera()
    {
        cameraAnimation.SetActive(true);
        mainAnimationBackground.NightOnOff(true);
        rotationObject.SetActive(false);
    }
}
