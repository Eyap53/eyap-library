# RandomPoints

Utils class to get a random point inside a shape.
Measures of time taken by currents methods for random point inside a sphere:

Called GetInSphere, 100000 times in 31.3481 ms.
Called GetInSphereByDiscarding, 100000 times in 6.6905 ms.
Called GetInBiasedSphere, 100000 times in 24.9911 ms.
Called GetInHoledSphere, 100000 times in 36.7697 ms.
Called GetInHoledSphereByDiscarding, 100000 times in 6.8061 ms.

It appears that getting a random point inside a sphere by discarding wrong ones is the quickest. However, it may also be the one with the most variance.
