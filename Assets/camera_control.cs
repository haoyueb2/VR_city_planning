using UnityEngine;
using System.Collections.Generic;
using wvr;
using System.Collections;
using UnityEngine.SceneManagement;
using WaveVR_Log;

public class camera_control : MonoBehaviour
{

    public WVR_DeviceType device = WVR_DeviceType.WVR_DeviceType_Controller_Right;
    public WVR_DeviceType hmd = WVR_DeviceType.WVR_DeviceType_HMD;
    public GameObject ControlledObject;


    WVR_InputId[] axisIds = new WVR_InputId[] {
        WVR_InputId.WVR_InputId_Alias1_Touchpad,
        WVR_InputId.WVR_InputId_Alias1_Trigger
    };

    private Vector3 StartPos;
    void Start()
    {
        StartPos = ControlledObject.transform.localPosition;
    }
    public void Reset()
    {
        ControlledObject.transform.localPosition = StartPos;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToUpLayer()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        //和touchPad同时用方向不准确，弃用
        //float TranslateSpeed = 0.4f;
        //if (WaveVR_Controller.Input(device).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Up))
        //{

        //    ControlledObject.transform.Translate(Vector3.forward * TranslateSpeed);
        //}
        //if (WaveVR_Controller.Input(device).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Down))
        //{

        //    ControlledObject.transform.Translate(Vector3.back * TranslateSpeed);
        //}
        //if (WaveVR_Controller.Input(device).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Right))
        //{

        //    ControlledObject.transform.Translate(Vector3.right * TranslateSpeed);
        //}
        //if (WaveVR_Controller.Input(device).GetPress(WVR_InputId.WVR_InputId_Alias1_DPad_Left))
        //{
        //    //Vector3.forward 表示“向前”
        //    ControlledObject.transform.Translate(Vector3.left * TranslateSpeed);
        //}

        float TranslateSpeed = 0.4f;
        float rotateSpeed = 0.5f;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) 
        {
            ControlledObject.transform.Translate(Vector3.left * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ControlledObject.transform.Translate(Vector3.right * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {

            ControlledObject.transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            ControlledObject.transform.Translate(Vector3.back * 10 * Time.deltaTime);//小车控制时，前进后退movespeed前都有负号
        }


        foreach (WVR_InputId axisId in axisIds)
        {

            // button touched
            if (WaveVR_Controller.Input(device).GetTouch(axisId))
            {
                var axis = WaveVR_Controller.Input(device).GetAxis(axisId);


                ControlledObject.transform.Translate(axis.x * (15 * Time.deltaTime), 0, 0);
                ControlledObject.transform.Translate(0, 0, axis.y * (15 * Time.deltaTime));



            }
        }
        float yRotation = WaveVR_Controller.Input(hmd).transform.rot.eulerAngles.y - ControlledObject.transform.localRotation.eulerAngles.y;
        ControlledObject.transform.Rotate(0, yRotation, 0);



    } // Update
}
