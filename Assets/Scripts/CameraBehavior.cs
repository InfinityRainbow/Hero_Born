using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    //1
    public Vector3 CamOffset3 = new Vector3(0f, 1.8f, -2.6f);
    public Vector3 CamOffset1 = new Vector3(0f, 1f, 0.3f);
    private bool _choose1;
    private bool _choose3;
    //2
    private Transform _target;
    private int _viewPoint;
    // Start is called before the first frame update
    void Start()
    {
        //3
        _target = GameObject.Find("Player").transform;
        _viewPoint = 3;
    }

    //4
    private void LateUpdate()
    {
        if(_choose1)
        {
            _viewPoint = 1;
            _choose1 = false;
        }
        else if(_choose3)
        {
            _viewPoint = 3;
            _choose3 = false;
        }
        //5
        if (_viewPoint == 1)
        {
            this.transform.position = _target.TransformPoint(CamOffset1);
            this.transform.rotation = _target.rotation;
            //this.transform.Rotate(Vector3.right, mouse Y);
            //this.transform.LookAt(_target.forward);
        }
        else if(_viewPoint==3)
        {
            this.transform.position = _target.TransformPoint(CamOffset3);
            this.transform.LookAt(_target);
        }
        //6aa
        
    }
    // Update is called once per frame
    void Update()
    {
        _choose1|= Input.GetKeyDown(KeyCode.Alpha1);
        _choose3 |= Input.GetKeyDown(KeyCode.Alpha3);
    }
}
