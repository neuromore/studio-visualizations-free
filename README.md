# neuromore Unity prefab

This project contains the open source Unity prefab to add neuro-feedback with neuromore to your Unity games.

## How to add neurofeedback to your Unity games

1. Start by downloading neuromore Studio from our [release page](github.com/neuromore/studio/releases) and logging in to your user account. 
2. Within neuromore Studio open a classifier and state machine of your choice, for example the getting started example in the `examples/getting_started` folder.
3. Simply download or clone the repo and add the neuromore Unity prefab to your project. 
4. Start your game in Unity.
5. Start a session in neuromore Studio. neuromore Studio will from now on stream messages over OSC to Unity.

Check out [this video](https://www.youtube.com/watch?v=-kPzBAyA-og) for a step-by-step demonstration.

## Example project: The forest walk

To get a better understanding how to use the parameters from the neuromore prefab have a look at the forest walk example. All assets are free and we are explicitly permitted to redistribute them by the authors.

Parameters that are currently controllable within the forest scene (input ranges are 0 to 1):

| Control      | OSC-address     |
|--------------|-----------------|
| Sunlight     | /weather-sun    |
| Fog          | /weather-fog    |
| Clouds       | /weather-clouds |
| Camera speed | /movement-speed |

To control these parameters from your neuromore Studio experience, create custom output nodes for each parameter, enable OSC streaming on them and set the OSC-address to the respective address in the table. 
