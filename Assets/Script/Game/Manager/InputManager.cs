namespace SonderStudio
{
    namespace Input
    {
        using UnityEngine;

        public class InputManager : MonoBehaviour
        {
            public struct KeyCodeConfig
            {

            };

            public static Transform Player { private set; get; }

            public static KeyCode A = KeyCode.A;
            public static KeyCode D = KeyCode.D;
            public static KeyCode S = KeyCode.S;
            public static KeyCode W = KeyCode.W;
            public static KeyCode Space = KeyCode.Space;
            public static KeyCode F = KeyCode.F;
            public static KeyCode Mouse0 = KeyCode.Mouse0;
            public static KeyCode Mouse1 = KeyCode.Mouse1;

            public static float FloatAD { private set; get; }
            public static float FloatWS { private set; get; }
            public static float FloatSpace { private set; get; }
            public static float FloatDownSpace { private set; get; }

            public static Quaternion PlayerRotate { private set; get; }
            public static float PlayerRotateLerp { private set; get; }

            public static void SetFloatAD(float value) => FloatAD = value;
            public static void SetFloatWS(float value) => FloatWS = value;
            public static void SetFloatSpace(float value) => FloatSpace = value;
            public static void SetFloatDownSpace(float value) => FloatDownSpace = value;

            public static void SetPlayer(Transform transform) => Player = transform;

            public static void GetKeyCodeConfig()
            {
            }

            private DefaultQuaternion defaultQuaternion;

            private void FixedUpdate()
            {
                //FloatAD
                if (Input.GetKey(A)) FloatAD -= Time.deltaTime * 4;
                else if (Input.GetKey(D)) FloatAD += Time.deltaTime * 4;
                else FloatAD = FloatAD >= 0 ? Mathf.Clamp01(FloatAD -= Time.deltaTime * 4) : Mathf.Clamp(FloatAD += Time.deltaTime * 4, -1, 0);

                FloatAD = Mathf.Clamp(FloatAD, -1, 1);

                //FloatWS
                if (Input.GetKey(S)) FloatWS -= Time.deltaTime * 4;
                else if (Input.GetKey(W)) FloatWS += Time.deltaTime * 4;
                else FloatWS = FloatWS >= 0 ? Mathf.Clamp01(FloatWS -= Time.deltaTime * 4) : Mathf.Clamp(FloatWS += Time.deltaTime * 4, -1, 0);

                FloatWS = Mathf.Clamp(FloatWS, -1, 1);


                //FloatSpace
                if (!Player.transform.IsGround()) FloatSpace += Time.deltaTime * 4;
                else FloatSpace -= Time.deltaTime * 4;

                FloatSpace = Mathf.Clamp01(FloatSpace);

                //FloatDownSpace
                FloatDownSpace -= Time.deltaTime;
                FloatDownSpace = Mathf.Clamp01(FloatDownSpace);

                //PlayerRotate
                PlayerRotate = Quaternion.Lerp(defaultQuaternion.FrontQuaternion, defaultQuaternion.BackQuaternion, PlayerRotateLerp);
                if (Player.GetComponent<PlayerCtrl>().GetDirection() == Direction.Left) PlayerRotateLerp += Time.deltaTime * 4;
                else PlayerRotateLerp -= Time.deltaTime * 4;
                PlayerRotateLerp = Mathf.Clamp01(PlayerRotateLerp);
            }

            RaycastHit raycastHit;
            private void Awake()
            {
                raycastHit = new RaycastHit();
            }
        }
    }
}