using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Utils
{
    public static class KeyPressListener
    {
        private static readonly KeyCode[] _keyCodes = Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k < (int)KeyCode.Mouse0))
            .ToArray();

        public static async Task<KeyCode?> ListenToPressedKey()
        {
            await UniTask.WaitUntil(() => Input.anyKey == true);
            return GetCurrentKeyDown();
        }
        
        private static KeyCode? GetCurrentKeyDown()
        {
            if (!Input.anyKey)
            {
                return null;
            }
 
            foreach (var key in _keyCodes)
            {
                if (Input.GetKey(key))
                {
                    return key;
                }
            }
            return null;
        }
    }
}