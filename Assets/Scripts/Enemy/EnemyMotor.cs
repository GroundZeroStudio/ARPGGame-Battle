using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class EnemyMotor : MonoBehaviour
	{

        public WayLine wayLine;
        public Transform[] TempPoints;
        private int CurrentPointIndex;
        private float mMoveSpeed = 5f;

        //public EnemyMotor()
        //{
        //    CurrentPointIndex = 0;
        //}

		public void MoveForward()
        {
            transform.Translate(transform.forward * mMoveSpeed * Time.deltaTime);
        }

		public void LookRotation(Vector3 nDirection)
        {
            //transform.rotation = Quaternion.FromToRotation(transform.position, nDirection);
            //旋转速度
            transform.LookAt(nDirection);
        }

        /// <summary>
        /// 寻路
        /// </summary>
        /// <returns></returns>
		public bool Pathfinding()
        {
            if (CurrentPointIndex >= wayLine.Points.Length) return false;

            Vector3 targetPoint = wayLine.Points[CurrentPointIndex];
            if(targetPoint != transform.rotation.eulerAngles)
            {
                LookRotation(targetPoint);
            }
            MoveForward();
            if (Vector3.Distance(targetPoint, transform.position) < 0.01f)
            {
                CurrentPointIndex++;
            }
            return true;
        }

        public void Update()
        {
            //Debug.Log(CurrentPointIndex);
            if (CurrentPointIndex >= TempPoints.Length) 
                return;
            Vector3 targetPoint = TempPoints[CurrentPointIndex].position;
            if (targetPoint != transform.rotation.eulerAngles)
            {
                LookRotation(targetPoint);
            }
            MoveForward();
            Debug.Log(Vector3.Distance(targetPoint, transform.position));
            if (Vector3.Distance(targetPoint, transform.position) < 2f)
            {
                CurrentPointIndex++;
            }
        }
    }
}

