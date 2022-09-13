using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCrimeStudios.Features.Volumes
{
	public class SignedDistanceRenderer : MonoBehaviour
	{
		static protected string		defaultSignedDistanceShader = "SignedDistanceShader";
		static protected Material	defaultMaterial;
		static Camera				activeCamera;
		
		[Header("Rendering")]
		[Tooltip("Custom material override - Optional")]
		public Material				material;
		
		[Header("SignedDistanceVolume")]
		public SignedDistanceVolume	volume;
		public float				volumeEpsilon;
		public float				volumeNormalDelta;

		/// <summary>Cache of the default Signed Distance Volume Renderer Material.</summary>
        /// <remarks>Always specify the default sdf Shader in the Always Included Shader list.</remarks>
        public static Material DefaultSignedDistanceMaterial
        {
            get
            {
                if ( null == defaultMaterial ) defaultMaterial = new Material( Shader.Find( defaultSignedDistanceShader ) ); 
                return defaultMaterial;
            }
        }

		/// <summary>Returns the material if overriden otherwise uses the default signedDistanceMaterial.</summary>
		public Material Material
        {
            get
            {
                if (null != material ) return material;
                return DefaultSignedDistanceMaterial;
            }

            set { material = value; }
        }
				


		void Start()
		{

		}

		void Update()
		{

		}

		void OnRenderObject()
        {
            // try to get active camera...
            if ( null == activeCamera ) activeCamera = Camera.main;

#if UNITY_EDITOR
			// lastActiveSceneView
			if ( UnityEditor.SceneView.currentDrawingSceneView != null)
				activeCamera = UnityEditor.SceneView.currentDrawingSceneView.camera;
#endif

			DrawVolume();			
        }

		void DrawVolume()
		{


		}


	}
}