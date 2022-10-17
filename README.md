# README


# Project Antares Code Handbook

This section of this file outlines the SOP for file management, scene hierarchy and code layout for this project

Source article for scene hierarchy and file management: https://blog.theknightsofunity.com/7-ways-keep-unity-project-organized/

Source for code SOPs: https://gist.github.com/cgourlay/c419390066b22c4b2d4f

## Branches
* Master Branch to be considered Active Development Branch
* Release Branch to be kept seperate from Active Development Branch
* Each developer to have their own Developement Branch with the following name scheme: <FirstInitial>.DevBranch
* Branches must be reviewed before merging

## File Management
Folder must adhere to the following layout:

* 3rd-Part
* Animations
* Audio
    - Music
    - SFX
* Materials
* Models
* Plugins
* Prefabs
* Resources
* Textures
* Sandbox
* Scenes
    - Levels
    - Other
* Scripts
    - Editor
* Shaders
## Scene Hierarchy

* Management
* GUI
* Cameras
* Lights
* World
    - Terrain
    - Props
* _Dynamic

1. All empty objects should be located at 0,0,0 with default rotation and scale.
2. When you’re instantiating an object in runtime, make sure to put it in _Dynamic – do not pollute the root of your hierarchy or you will find it difficult to navigate through it.
3. For empty objects that are only containers for scripts, use “@” as prefix – e.g. @Cheats
## Code SOPs

* Member Variables, Parameters and Local Variables should use camalCase
* Functions, Classes, Properties and Events should use PascalCase
* Avoid using single line statements

