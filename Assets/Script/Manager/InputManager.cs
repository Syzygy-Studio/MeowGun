using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static KeyCode A = KeyCode.A;
    public static KeyCode D = KeyCode.D;
    public static KeyCode S = KeyCode.S;
    public static KeyCode Space = KeyCode.Space;
    public static KeyCode F = KeyCode.F;
    public static KeyCode Mouse0 = KeyCode.Mouse0;
    public static KeyCode Mouse1 = KeyCode.Mouse1;

    public static float FloatAD { private set; get; }

    public static void SetFloatAD(float value) => FloatAD = value;

    public static void GetKeyCodeCofign()
    {
    }

    private void Update()
    {
        if (Input.GetKey(A)) FloatAD -= Time.deltaTime * 4;
        else if (Input.GetKey(D)) FloatAD += Time.deltaTime * 4;
        else FloatAD = FloatAD >= 0 ? Mathf.Clamp01(FloatAD -= Time.deltaTime * 4) : Mathf.Clamp(FloatAD += Time.deltaTime * 4, -1, 0);

        FloatAD = Mathf.Clamp(FloatAD, -1, 1);
    }
}
