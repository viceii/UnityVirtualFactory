using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SimpleFrame;

namespace SampleFrame
{
    /// <summary>
    /// IO管理
    /// </summary>
    public static class InputMgr
    {
        public static void Init()
        {
            /// <summary>
            /// 读取键位配置文件
            /// </summary>
            /// <value></value>

        }

        public static Vector2 MousePosition
        {
            get
            {
                return Input.mousePosition;
            }
        }

        public static float ScrollWheel
        {
            get
            {
                return Input.GetAxis("Mouse ScrollWheel");
            }
        }

        public static bool MouseLeft
        {
            get
            {
                return Input.GetMouseButton(0);
            }
        }

        public static bool MouseRight
        {
            get
            {
                return Input.GetMouseButton(1);
            }
        }

        public static bool MouseLeftDown
        {
            get
            {
                return Input.GetMouseButtonDown(0);
            }
        }

        public static bool MouseRightDown
        {
            get
            {
                return Input.GetMouseButtonDown(1);
            }
        }

        public static float Forward
        {
            get
            {
                return Input.GetAxis(Constant.Input_Vertical);
            }
        }

        public static float Right
        {
            get
            {
                return Input.GetAxis(Constant.Input_Horizontal);
            }
        }

        public static bool Jump
        {
            get
            {
                return Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), Constant.Input_Jump));
            }
        }

        public static float MouseX
        {
            get
            {
                return Input.GetAxis(Constant.Input_Mouse_X);
            }
        }

        public static float MouseY
        {
            get
            {
                return Input.GetAxis(Constant.Input_Mouse_Y);
            }
        }

        public static bool Q
        {
            get
            {
                return Input.GetKeyDown(KeyCode.Q);
            }
        }

        public static bool E
        {
            get
            {
                return Input.GetKeyDown(KeyCode.E);
            }
        }
    }

}