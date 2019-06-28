using UnityEngine;

namespace NoiseCrimeStudios.Features.Volumes
{
	public abstract class VolumeBase
	{	
		public Texture Texture { get; set; }

		public abstract void WriteValues( string name, Vector3Int dimensions, float[] distances, bool mipmaps );
	}
}