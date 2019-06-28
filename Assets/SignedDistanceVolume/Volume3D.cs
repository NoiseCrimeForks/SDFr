using UnityEngine;

namespace NoiseCrimeStudios.Features.Volumes
{
	// Note:
	// For compatibility with Visual Effect Graph, the distance must be negative inside surfaces.
	// Normalize the distance for better support of scaling bounds.
	// Max Distance is always the Magnitude of the baked bound size.

	public class Volume3D : VolumeBase // <Texture3D>
	{		
		// public Texture3D Texture { get; set; }

		void DestroyVolume()
		{
			if ( null != Texture ) GameObject.DestroyImmediate( Texture );
			Texture = null;
		}
		
		/// <summary>Create a 3D Volume.</summary>
		/// <param name="name">Name of Volume Texture.</param>
		/// <param name="dimensions">3D Volume dimensions, x,y,z.</param>
		/// <param name="distances"> Normalised distances to support scaling bounds</param>
		/// <param name="mipmaps">Automatically generate mipmaps?</param>
		public override void WriteValues( string name, Vector3Int dimensions, float[] distances, bool mipmaps )
		{
			// Destroy Previous - what happens if part of scriptableObject asset?
			DestroyVolume();

			// Create Texture3D and set name to filename of sdfData
            Texture			= new Texture3D( dimensions.x, dimensions.y, dimensions.z, TextureFormat.RHalf, mipmaps);
			Texture.name	= name;
            
            // TODO: Check for Unity updates to allow for native updating of volume textures from floats.
            Color[] colorBuffer = new Color[distances.Length];

            for (int i = 0; i < distances.Length; i++)            
                colorBuffer[i] = new Color(distances[i],0f,0f,0f); // distances[i] / maxDistance;
            
            (Texture as Texture3D).SetPixels(colorBuffer);
            (Texture as Texture3D).Apply();
		}

		// TODO: For future add overloads for WriteValues to write to RGB, RGBA etc.
	}
}