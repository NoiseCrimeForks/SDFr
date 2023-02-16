# SDFr
a signed distance field baker for Unity

NOTE: Unity 2021.2 now has an SDF Baking tool packaged with Visual Effect Graph, which produces far better results, I recommend giving Unity's a try (you might also be able to manually backport it to prior editor versions).

about
-----

- Generates signed distance fields as Texture3D in RHalf format for use with ray marching or the Visual Effect Graph.
- Written distances are between -1 and +1, normalized to the bounding box magnitude.
- Takes advantage of Jobs and Burst + Unity Mathematics, comment out the #define in SDFVolume.cs if Burst & Mathematics should not be used.
- Tested in Unity 2018.3

Updates By NoiseCrime
-----
![Visualisations](https://raw.githubusercontent.com/noisecrime/SDFr/master/Media/Visualisations.png)

2019.06.28
- Fixed SDFDebug shader depth value issue. Now SDF is correctly rendered into scene with existing models.
- Added alternative procedural quad method that generates ray direction as varying in vertex shader - maybe more efficient?
- Started new framework in SignedDistanceVolume folder. Work in progress.


2019.06.25
- Updated To include varity of visualisations of the raymarching ( distance, steps, heatmap ) to aid in learning.
- Some code change to improve performance and address some flaws in RayMarchExample Shader.
- Mainly being used as a testbed for learning more about rayMarching through SDF.
- Next step is a code refactor of rayMarch shaders to unify, simplify and improve overal performance.


Scenes
-----

SampleScene
- Demonstrates rendering SDF Volumes
- Added visualisation option ( distance, steps, heatmap )
- Uses correct normal generation.

RaymarchExample
- Demonstrates mixing raymarch of implicit surfaces with SDF volumes.
- Added visualisation option ( distance, steps, heatmap )
- Uses correct normal generation.

vfxGraph
- Demonstrates mixing SDF volume with Unity VFX.

BakerTesting
- Scene for testing out baking methods: different volume sizes, bound sizes, rescaling etc

DepthTestCube
- Scene for trying to determine why incorrect depth values are being written.

License
-------
[MIT](LICENSE.md)