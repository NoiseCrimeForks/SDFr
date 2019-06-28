using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseCrimeStudios.Features.Volumes
{
	// ScriptableObject?
	public class SignedDistanceVolume
	{
		public VolumeBase	volume;
		public Bounds		bounds;

		static readonly int _SDFVolumeTex		= Shader.PropertyToID("_SDFVolumeTex");
		static readonly int _SDFVolumeExtents	= Shader.PropertyToID("_SDFVolumeExtents");

		public void SetMaterialProperties( MaterialPropertyBlock props )
		{
			props.SetTexture( _SDFVolumeTex, volume.Texture );
			props.SetVector( _SDFVolumeExtents, bounds.extents );
		}



	}
}