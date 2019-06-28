using UnityEngine;

namespace NoiseCrimeStudios.Features.Volumes
{
	// WIP - Not yet tested or used.
	/// <summary>Class for generating 3D volume textures using just a 2D Texture.</summary>
	public class Volume2DAtlas : VolumeBase	// <Texture2D>
	{
		//public Texture2D Texture { get; set; }

		void DestroyVolume()
		{
			if ( null != Texture ) GameObject.DestroyImmediate( Texture );
			Texture = null;
		}

		/// <summary>Create a 2D Atlas Volume.</summary>
		/// <param name="name">Name of Volume Texture.</param>
		/// <param name="dimensions">3D Volume dimensions, x,y,z.</param>
		/// <param name="distances"> Normalised distances to support scaling bounds</param>
		/// <param name="mipmaps">Automatically generate mipmaps?</param>
		public override void WriteValues( string name, Vector3Int dimensions, float[] distances, bool mipmaps )
		{
			// Destroy Previous - what happens if part of scriptableObject asset?
			DestroyVolume();

			// Create Texture3D and set name to filename of sdfData
			Texture			= new Texture2D( dimensions.x * dimensions.z,  dimensions.y, TextureFormat.RHalf, mipmaps );
			Texture.name	= name;

			// TODO: Check for Unity updates to allow for native updating of volume textures from floats.
			Color[] colorBuffer = new Color[distances.Length];

			for ( int i = 0; i < distances.Length; i++ )
				colorBuffer[ i ] = new Color( distances[ i ], 0f, 0f, 0f ); // distances[i] / maxDistance;

			(Texture as Texture2D).SetPixels( colorBuffer );
			(Texture as Texture2D).Apply();
		}

		// TODO: For future add overloads for WriteValues to write to RGB, RGBA etc.
	}
}