using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleFrame;
using System;

public class Monitor : MonoSingleton<Monitor>
{
    private Camera currCam;
    private Camera[] monitors;
    private RenderTexture[] renderTextures;

    private string renderTexturePath = "Texture";

    // Start is called before the first frame update
    void Awake()
    {
        this.currCam = Camera.main;
        monitors = GetComponentsInChildren<Camera>();
        MsgCenter.AddListener<int>(MsgDefine.SwitchCamera, SwitchCamera);
        renderTextures = Resources.LoadAll<RenderTexture>(this.renderTexturePath);
    }

    private void SwitchCamera(int val)
    {
        int monitorSize = this.monitors.Length;
        int renderTextureSize = this.renderTextures.Length;

        for (int i = 0; i < monitorSize && i < renderTextureSize; i++)
        {
            if (val == i)
            {
                this.monitors[i].enabled = true;
                this.monitors[i].targetTexture = null;
                this.currCam = this.monitors[i];
            }
            else
            {
                this.monitors[i].enabled = false;
                this.monitors[i].targetTexture = this.renderTextures[i];
            }
        }

        TagView.Instance.SetMainCam(currCam);
    }


    private float minPosZ = 0f;
    private float maxPosZ = 16f;
    // Update is called once per frame
    void Update()
    {
        if (this.currCam != null && this.currCam.tag == "MainCamera")
        {
            //float mooz = InputMgr.ScrollWheel;
            //this.currCam.fieldOfView = Mathf.Clamp(this.currCam.fieldOfView - mooz * this.moozMulti, this.minMooz, this.maxMooz);
            //if (InputMgr.MouseLeftDown)
            //{
            //    Ray ray = Camera.main.ScreenPointToRay(InputMgr.MousePosition);
            //    RaycastHit hit;
            //    if (Physics.Raycast(ray, out hit))
            //    {
            //        LayerMask layer = hit.collider.gameObject.layer;
            //        if (layer == LayerMask.NameToLayer("Robot") || layer == LayerMask.NameToLayer("Board"))
            //        {
            //            this.currCam.transform.LookAt(hit.transform.position);
            //            MonoController.StartCoroutine(Focusing(minMooz));
            //        }
            //    }
            //}

            if (InputMgr.MouseRight)
            {
                //currCam.transform.localEulerAngles = new Vector3(45f, -90f, 0f);
                float mouseX = InputMgr.MouseX;
                Vector3 dis = transform.forward * mouseX * mouseXMulti * Time.deltaTime;
                Vector3 oldPos = currCam.transform.localPosition;
                float newPosZ = Mathf.Clamp(oldPos.z + dis.z, minPosZ, maxPosZ);
                this.currCam.transform.localPosition = new Vector3(oldPos.x, oldPos.y, newPosZ);
            }
        }

    }

    private float mouseXMulti = 30f;

    private float minMooz = 10f;
    private float maxMooz = 60f;
    private float moozMulti = 30f;

    public Camera CurrCam { get => currCam; set => currCam = value; }

    IEnumerator Focusing(float target)
    {
        while (true)
        {
            if (Mathf.Abs(this.currCam.fieldOfView - target) <= 0.1f || Mathf.Abs(InputMgr.ScrollWheel) >= 0.1f)
            {
                break;
            }
            this.currCam.fieldOfView = Mathf.Lerp(this.currCam.fieldOfView, this.minMooz, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
