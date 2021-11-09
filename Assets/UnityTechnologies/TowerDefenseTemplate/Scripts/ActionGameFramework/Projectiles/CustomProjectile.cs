using System;
using System.Collections.Generic;
using ActionGameFramework.Helpers;
using UnityEngine;

namespace ActionGameFramework.Projectiles
{
	public class CustomProjectile : MonoBehaviour, IProjectile
	{
		void Update()
        {
			transform.localPosition = new Vector3(0, 0, 0);
        }
		
		void FixedUpdate()
        {
			transform.localPosition = new Vector3(0, 0, 0);
        }

		/// <summary>
		/// Event fired when this projectile is launched
		/// </summary>
		public event Action fired;

		/// <summary>
		/// Fires this projectile from a designated start point to a designated world coordinate.
		/// </summary>
		/// <param name="startPoint">Start point of the flight.</param>
		/// <param name="targetPoint">Target point to fly to.</param>
		public void FireAtPoint(Vector3 startPoint, Vector3 targetPoint)
		{
			fired();
		}

		/// <summary>
		/// Fires this projectile in a designated direction.
		/// </summary>
		/// <param name="startPoint">Start point of the flight.</param>
		/// <param name="fireVector">Vector representing direction of flight.</param>
		public void FireInDirection(Vector3 startPoint, Vector3 fireVector)
		{
			fired();
		}

		/// <summary>
		/// Fires this projectile at a designated starting velocity, overriding any starting speeds.
		/// </summary>
		/// <param name="startPoint">Start point of the flight.</param>
		/// <param name="fireVelocity">Vector3 representing launch velocity.</param>
		public void FireAtVelocity(Vector3 startPoint, Vector3 fireVelocity)
		{
			fired();
		}
	}
}
