# 3D Worms-like

## Controls
### Menus
Navigate menus using <kbd>Mouse</kbd> and <kbd>Keyboard</kbd>.

### Game
Toggle between *movement mode* and *aiming mode*, using <kbd>Right Click</kbd>.

#### Movement mode
Whilst in *movement mode*, use <kbd>W</kbd><kbd>A</kbd><kbd>S</kbd><kbd>D</kbd> and <kbd>Space</kbd> to move and jump.  
Zoom in using <kbd>Scroll Wheel</kbd>.

#### Aiming mode
Whilst in *aiming mode*, press <kbd>1</kbd> or <kbd>2</kbd> to switch between equipping your baseball bat and basketball.  
Use <kbd>Mouse</kbd> to aim and, with a weapon equipped, <kbd>Left Click</kbd> to fire.  
Toggle zoom using <kbd>Left Shift</kbd>.

## Features
I have learnt a lot from this project! Although my code is far from perfect, it’s the first time I’ve ever built so many different systems into a single project. It’s been my first time using Events, Singletons, Getters and Setters, Scriptable Objects and a proper attempt at adhering to the C# naming convention. With so many new prinnciples on the table, I have now come to realise that I have overly relied on Singletons. Whilst I haven’t run into any practical problems with Singletons yet, I am aware of their dangers and will be a little more precautious in the future. Going ahead, I expect that spending more time to plan out my code structure will help me to minimise dependencies and alleviate my reliance on dependencies. I am aiming for a VG, but not too bothered!

### General
- [x] (G) Only play scene is required
- [x] (VG, small) Add main menu (start) scene and game over scene
- [ ] (VG, medium) Implement Pause menu and settings menu 

Find the scenes in [*Worms/Assets/Scenes/*](https://github.com/joebinns/worms/blob/main/Worms/Assets/Scenes) and begin running from Main Menu! I have implemented a pause menu, but not settings.

### Turn based game
- [x] (G) You can have two players using the same input device taking turns.
- [x] (VG, large) Support up to 4 players (using the same input device taking turns)
- [ ] (VG, large) Implement a simple AI opponent.

I support 2-4 players through [*Worms/Assets/Scripts/Players/PlayerSelection.cs*](https://github.com/joebinns/worms/blob/main/Worms/Assets/Scripts/Players/PlayerSelection.cs).

### Terrain
- [x] (G) Basic Unity terrain or primitives will suffice for a level
- [ ] (VG, large) Destructible terrain (You can use Unity's built in terrain or your own custom solution)

I used ProBuilder, and made certain terrain react dynamically to the player using [*Worms/Assets/Scripts/Oscillators/Oscillator.cs*](https://github.com/joebinns/worms/blob/main/Worms/Assets/Scripts/Oscillators/Oscillator.cs).

### Player
- [x] (G) A player only controls one worm
- [x] (G) Use the built in Character Controller. Add jumping.
- [x] (G) Has hit points
- [x] (VG, small) Implement a custom character controller to control the movement of the worm.
- [ ] (VG, small) A worm can only move a certain range 
- [ ] (VG, medium) A player controls a team of (multiple worms)

The physics based character controller, [stylised-character-controller](https://github.com/joebinns/stylised-character-controller), is my own previous work. I was unfortunately unable to bring it up to the new standards of this project. However, since it is currently a digital jungle of if-statemen, I will be working on cleaning it up in whenever I find some time. See my player-centric scripts at [*Worms/Assets/Scripts/Players/*](https://github.com/joebinns/worms/tree/main/Worms/Assets/Scripts/Players/), starting with the Player Manager.

### Camera
- [x] (G) Focus camera on active player
- [x] (VG, small) Camera movement

I use cinemachine, with a variety of scripts each serving unique purposes: [*Worms/Assets/Scripts/Cameras/*](https://github.com/joebinns/worms/tree/main/Worms/Assets/Scripts/Cameras).

### Weapon system
- [x] (G) Minimum of two different weapons/attacks, can be of similar functionality, can be bound to an individual button, like weapon 1 is left mouse button and weapon 2 is right mouse button
- [x] (VG, small) a weapon can have ammo and needs to reload
- [x] (VG, medium) The two types of weapons/attacks must function differently, I.E a pistol and a hand grenade. The player can switch between the different weapons and using the active weapon on for example left mouse button

I implemented a melee baseball bat and a projectile basketball for weapons. I am proud of my item system, which embraces scriptable objects and inheritance to implement both hats and weapons. I then implemented both hat selection and weapon selection through [*Worms/Assets/Scripts/Items/ItemRack.cs*](https://github.com/joebinns/worms/tree/main/Worms/Assets/Scripts/Items).

## Credits
### Sound Effects
The sound effects and audio were kindly created and arranged by [Clara Summerton](mailto:clarasummerton@gmail.com).

### Models
[Hats](https://sketchfab.com/microsoft/models) are from Microsoft, licensed under [Creative Commons Attribution](http://creativecommons.org/licenses/by/4.0/).  
[Sports Equipment](https://skfb.ly/osyyY) are from Alberto Luviano, licensed under [Creative Commons Attribution](http://creativecommons.org/licenses/by/4.0/).

### All else
Is [my own](https://joebinns.com/).

## Development Log
### Week 2
"So weapons are still in the pipeline... BUT HATS AREN'T!
Here's my second week's progress for a 'Worms 3D' inspired assignment at Futuregames."

[<img alt="3D Worms-like: Week 2 Progress" width="503" src="https://joebinns.com/documents/gifs/worms_2.gif" />](https://youtu.be/goXkOxxxBmk)
### Week 1
"Here's my first week's progress for a 'Worms 3D' inspired assignment at Futuregames. Now to make some weapons ;)"

[<img alt="3D Worms-like: Week 1 Progress" width="503" src="https://joebinns.com/documents/gifs/worms_1.gif" />](https://youtu.be/cWKQxPpcWVM)


