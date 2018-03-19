using System;
using System.Security.AccessControl;
using Boo.Lang.Environments;
using UnityEngine;

namespace QTInput
{
    public class KeyboardInput
    {
        public Key kLeft = new Key("a"),
            kForward = new Key("w"),
            kRight = new Key("d"),
            kBack = new Key("s"),
            kRun = new Key(KeyCode.LeftShift),
            kJump = new Key(KeyCode.Space);
        
        public Vector3 MouseDelta => new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        public Vector3 MoveInput {
            get
            {
                Vector3 m = new Vector3
                {
                    x = kLeft ? -1 : kRight ? 1 : 0,
                    y = kForward ? 1 : kBack ? -1 : 0
                };
                return m;
            }
        }
    }

    public class Key
    {
        private KeyCode key;

        public bool WasPressed => Input.GetKeyDown(key);
        public bool WasUp => Input.GetKeyDown(key);
        public bool IsPressed => Input.GetKey(key);
        
        public Key(KeyCode key)
        {
            Bind(key);
        }

        public Key(string key)
        {
            Bind(key);
        }
        
        public void Bind(KeyCode key)
        {
            this.key = key;
        }

        public void Bind(string key)
        {
            KeyCode.TryParse(key, true, out this.key);
        }

        public static implicit operator bool(Key k)
        {
            return k.IsPressed;
        }
    }
}