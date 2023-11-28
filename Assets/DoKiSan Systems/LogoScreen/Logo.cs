using System.Collections;
using UnityEngine;

public class Logo : MonoBehaviour
{
    static public bool viewed;
    
    public AnimationClip logoClip;

    [SerializeField] GameObject mainMenu;
    [SerializeField] RotationCameraMenu rotationCameraMenu;


    private void OnEnable()
    {
        gameObject.SetActive(!viewed);

        if (!viewed)
            StartCoroutine(CLogo());
        else
        {
            mainMenu.SetActive(true);
            rotationCameraMenu.enabled = true;
        }
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



    private IEnumerator CLogo()
    {
        yield return new WaitForSeconds(logoClip.length+0.5f);
        gameObject.SetActive(false);
        viewed = true;

        mainMenu.SetActive(true);
        rotationCameraMenu.enabled = true;
    }
}
