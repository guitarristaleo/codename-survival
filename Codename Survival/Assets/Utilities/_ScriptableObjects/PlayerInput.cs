using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SriptableObjectUtilities/PlayerInput", order = 53)]
public class PlayerInput : ScriptableObject
{
    public bool Dpad_up;
    public bool Dpad_down;
    public bool Dpad_left;
    public bool Dpad_right;

    public bool Button_North;
    public bool Button_South;
    public bool Button_Weast;
    public bool Button_East;

    public bool Bumper_Left;
    public bool Bumper_Right;

    public bool Trigger_Left;
    public bool Trigger_Right;

    public bool Stick_Left;
    public bool Stick_Right;
    public float Stick_Left_Axis_x;
    public float Stick_Left_Axis_y;
    public float Stick_Right_Axis_x;
    public float Stick_Right_Axis_y;

    public bool Button_Start;
    public bool Button_Select;

}
