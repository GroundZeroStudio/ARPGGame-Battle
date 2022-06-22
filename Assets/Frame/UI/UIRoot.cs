using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public class UIRoot : MonoBehaviour
	{
		private static UIRoot m_Instance;

        public static UIRoot Instance => m_Instance;

        public GameObject UI_Root;
        public RectTransform PanelRoot;
        public Camera UICamera;
        public EventSystem EventSystem;

        private void Awake()
        {
            m_Instance = this;
        }
    }
}

