using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionScript : MonoBehaviour
{
    public GameObject actionObject;

    private bool inRange;

    private bool active;

    private Transform[] cameraTargets;

    private CameraControlScript cameraInstance;

    private bool interactable;

    // Use this for initialization
    void Start()
    {
        cameraInstance = Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>();
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable)
        {
            if (inRange && !active)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    active = true;
                    ChangeButtonColor("green");
                    StartCoroutine(ShowAction());
                    CameraControlScript.instance.m_DampTime = .2f;
                }
            }

            else if (inRange && active)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    active = false;
                    ChangeButtonColor("red");
                    actionObject.transform.Find("Light").gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator ShowAction()
    {
        interactable = false;

        DeactivateCharacter();

        Transform[] temp = CameraControlScript.instance.m_Targets;

        CameraControlScript.instance.m_Targets = new Transform[] { actionObject.transform };

        CameraControlScript.instance.m_DampTime = .3f;

        while ( Vector3.Distance(cameraInstance.m_DesiredPosition, cameraInstance.transform.position) > .01f)
        {
            yield return null;
        }

        actionObject.transform.Find("Light").gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        CameraControlScript.instance.m_Targets = temp;

        while (cameraInstance.m_DesiredPosition != cameraInstance.transform.position)
        {
            yield return null;
        }

        ActivateCharacter();
        interactable = true;
    }

    private void ActivateCharacter()
    {
        CharacterMovementClass.instance.enabled = true;
        CharacterCommandClass.instance.enabled = true;
        if (CharacterCommandClass.instance.shadow_active)
            ShadowInformationClass.instance.enabled = true;
    }

    private void DeactivateCharacter()
    {
        CharacterMovementClass.instance.enabled = false;
        CharacterMovementClass.instance.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        CharacterCommandClass.instance.enabled = false;
        if (CharacterCommandClass.instance.shadow_active)
            ShadowInformationClass.instance.enabled = false;
    }



    private void ChangeButtonColor(string color)
    {
        if (color == "green")
        {
            Renderer rend = GetComponent<Renderer>();
            rend.materials[1].SetColor("_EmissionColor", Color.green);
        }
        else if (color == "red")
        {
            Renderer rend = GetComponent<Renderer>();
            rend.materials[1].SetColor("_EmissionColor", Color.red);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.transform.tag == "Shadow" || collider.transform.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.tag == "Shadow" || collider.transform.tag == "Player")
        {
            inRange = false;
        }
    }
}
