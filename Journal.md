# 18/10/2023

I ran into a problem with speed control, to fix this i used a speed limiter to cap the speed of the player to the given move speed, this stops the player from being able to reach very high speeds.

There was a problem with a camera jitter whenever it was moved,  to fix this i changed the original `Time.deltaTime` function to `Time.fixedDeltaTime`, this stopped the issue of the camera jittering.