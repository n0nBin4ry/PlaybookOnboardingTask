# PlaybookOnboardingTask
 Implemented a drag-and-drop object manipulator; similar to a primitive version of the Unity editor.

## Features
- Can manipulate objects with Unity Editor-like clickable gimbal controls.
    - Only one set of gimbal controls active at a time (on the currently selected obj) to keep the scene from getting crowded.
- Can spawn objects by clicking-and-dragging the object from a haptic 3D button.
    - Object can be dragged back to button before releasing spawning mouse-drag to cancel the spawn.
- Implemented with extendability in mind, for example: Cycle between 3 primitive objects to spawn using arrow buttons (cube, cylinder, sphere).
