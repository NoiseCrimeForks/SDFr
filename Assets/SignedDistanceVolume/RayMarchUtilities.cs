using UnityEngine;

namespace NoiseCrimeStudios.Features.Raymarching
{
	public static class RaymarchUtilities
	{
		/// <summary>Given a camera return the frustum corners in world space. Useful for </summary>
		/// <remarks>Useful for calculating ray direction in vertex shader.</remarks>
		public static Matrix4x4 GetCameraFrustumWorldCorners( Camera camera )
		{
			Transform camTransform		= camera.transform;
			Vector3[] frustumCorners	= new Vector3[4];

			camera.CalculateFrustumCorners( new Rect( 0, 0, 1, 1 ), camera.farClipPlane, camera.stereoActiveEye, frustumCorners );

			// Transform to world space
			Vector3 bottomLeft	= camTransform.TransformVector(frustumCorners[0]);
			Vector3 topLeft		= camTransform.TransformVector(frustumCorners[1]);
			Vector3 topRight	= camTransform.TransformVector(frustumCorners[2]);
			Vector3 bottomRight = camTransform.TransformVector(frustumCorners[3]);

			// Use matrix as simple storage mechansim to pass to shader - not an actual matrix!
			Matrix4x4 frustumCornersArray = Matrix4x4.identity;
			frustumCornersArray.SetRow( 0, bottomLeft );
			frustumCornersArray.SetRow( 1, bottomRight );
			frustumCornersArray.SetRow( 2, topLeft );
			frustumCornersArray.SetRow( 3, topRight );

			return frustumCornersArray;
		}
		

		public static Matrix4x4 GetRaymarchingMatrix( float fieldOfView, Matrix4x4 view, Vector2 screenSize)
        {    
            Vector4 screenSizeParams = new Vector4(screenSize.x, screenSize.y, 1.0f / screenSize.x, 1.0f / screenSize.y);            
            return ComputePixelCoordToWorldSpaceViewDirectionMatrix( fieldOfView * Mathf.Deg2Rad, Vector2.zero, screenSizeParams, view, false );            
        }


		//From HDRP
        public static Matrix4x4 ComputePixelCoordToWorldSpaceViewDirectionMatrix(
			float verticalFoV, 
			Vector2 lensShift, 
			Vector4 screenSize,
			Matrix4x4 worldToViewMatrix,
			bool renderToCubemap)
        {
            // Compose the view space version first.
            // V = -(X, Y, Z), s.t. Z = 1,
            // X = (2x / resX - 1) * tan(vFoV / 2) * ar = x * [(2 / resX) * tan(vFoV / 2) * ar] + [-tan(vFoV / 2) * ar] = x * [-m00] + [-m20]
            // Y = (2y / resY - 1) * tan(vFoV / 2)      = y * [(2 / resY) * tan(vFoV / 2)]      + [-tan(vFoV / 2)]      = y * [-m11] + [-m21]
            
            float tanHalfVertFoV = Mathf.Tan(0.5f * verticalFoV);
            float aspectRatio    = screenSize.x * screenSize.w;

            // Compose the matrix.
            float m21 = (1.0f - 2.0f * lensShift.y) * tanHalfVertFoV;
            float m11 = -2.0f * screenSize.w * tanHalfVertFoV;

            float m20 = (1.0f - 2.0f * lensShift.x) * tanHalfVertFoV * aspectRatio;
            float m00 = -2.0f * screenSize.z * tanHalfVertFoV * aspectRatio;

            if (renderToCubemap)
            {
                // Flip Y.
                m11 = -m11;
                m21 = -m21;
            }

            var viewSpaceRasterTransform = new Matrix4x4(new Vector4(m00, 0.0f, 0.0f, 0.0f),
                new Vector4(0.0f, m11, 0.0f, 0.0f),
                new Vector4(m20, m21, -1.0f, 0.0f),
                new Vector4(0.0f, 0.0f, 0.0f, 1.0f));

            // Remove the translation component.
            var homogeneousZero = new Vector4(0, 0, 0, 1);
            worldToViewMatrix.SetColumn(3, homogeneousZero);

            // Flip the Z to make the coordinate system left-handed.
            worldToViewMatrix.SetRow(2, -worldToViewMatrix.GetRow(2));

            // Transpose for HLSL.
            return Matrix4x4.Transpose(worldToViewMatrix.transpose * viewSpaceRasterTransform);
        }
	}
}