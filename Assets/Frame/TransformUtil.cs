using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class TransformUtil : MonoBehaviour
	{
        /// <summary>
        /// 世界坐标转屏幕坐标
        /// </summary>
        public static Vector2 World2Screen(Vector3 v, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 worldPos = v;
            Vector3 pos = camera.WorldToScreenPoint(worldPos);
            return pos;
        }

        /// <summary>
        /// 屏幕坐标转世界坐标
        /// </summary>
        public static Vector3 Screen2World(Vector2 v, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 screenPos = new Vector3(v.x, v.y, -camera.transform.position.z);
            Vector3 pos = camera.ScreenToWorldPoint(screenPos);
            return pos;
        }

        /// <summary>
        /// 世界坐标转视口坐标
        /// </summary>
        public static Vector2 World2Viewport(Vector3 v, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 worldPos = v;
            Vector3 pos = camera.WorldToViewportPoint(worldPos);
            return pos;
        }

        /// <summary>
        /// 视口坐标转世界坐标
        /// </summary>
        public static Vector3 Viewport2World(Vector2 v, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 viewportPos = new Vector3(v.x, v.y, -camera.transform.position.z);
            Vector3 pos = camera.ViewportToWorldPoint(viewportPos);
            return pos;
        }

        /// <summary>
        /// 屏幕坐标转视口坐标
        /// </summary>
        public static Vector2 Screen2Viewport(Vector2 v, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 screenPos = v;
            Vector3 pos = camera.ScreenToViewportPoint(screenPos);
            return pos;
        }

        /// <summary>
        /// 视口坐标转屏幕坐标
        /// </summary>
        public static Vector2 Viewport2Screen(Vector2 v, Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            Vector3 viewportPos = v;
            Vector3 pos = camera.ViewportToScreenPoint(viewportPos);
            return pos;
        }

        /// <summary>
        /// 屏幕坐标转UI坐标
        /// </summary>
        public static Vector2 Screen2UI(Vector2 v, RectTransform rect, Camera camera = null)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, v, camera, out pos);
            return pos;
        }

        /// <summary>
        /// 世界坐标转UI坐标
        /// </summary>
        public static Vector2 World2UI(Vector3 v, RectTransform rect, Camera uiCamera, Camera worldCamera = null)
        {
            if (worldCamera == null)
            {
                worldCamera = Camera.main;
            }
            Vector2 screenPos = World2Screen(v, worldCamera);
            Vector2 pos = Screen2UI(screenPos, rect, uiCamera);
            return pos;
        }

    }
}

