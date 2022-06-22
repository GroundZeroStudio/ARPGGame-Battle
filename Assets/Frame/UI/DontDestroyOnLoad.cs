using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
        }
    }
}

