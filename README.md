# Beats Boxing


Beats Boxing is a rythmic endless runner where you will beat down 80s action tropes in tune to a fast paced beat. How long can you keep up? 

## Instructions
Swipe Up and Down on the left side of the screen on a mobile device, or press W and S, to move up and down. Attack by tapping on the right side of the screen, or by pressing the Spacebar.

## Why?
The project was done for IGM-590, Casual Game Development at RIT in 5 weeks, by the following fabulous individuals.

## Chris Mercado
* Engine Architect
* Taskmaster
* Game Designer
* AI Engineer
* Animation


## Sarah Bishop
* Artist
* Player Input and Programming
* Game Designer
* Sound Wizard
* Monetization


## Matthew Fobare
* Enemy AI / Generation
* Game Designer

## [Sean Maraia](http://seanmaraia.me "Sean Maraia")
* UI Designer & Engineer
* Mobile Build Lead
* Game Designer
* Artist

Write-Up
Team SMCS: Sean Maraia, Matt Fobare, Chris Mercado, and Sarah Bishop

	Our game for the third sprint was a continuation of our game, BeatsBoxing. The game is an infinite-running beat-em-up where the player scrolls right and defeats enemies along the way. As the player defeats enemies, the game speeds up and becomes tougher for the player. When the player gets hit, they are moved back and their “combo” bonus is reduced. 
For this iteration of BeatsBoxing, we decided we wanted to improve the mechanics of the game and polish it for our final version. We wanted to improve our enemy AI and spawning system, add in more explicit rhythm mechanics, create our own assets, and implement Unity ads.
Sean worked on sprucing up the UI, creating sprites, and polishing the visuals for our game. This includes animating lane swapping and creating a background animation for the game. He drew the spritesheet for the totally-not-Kung-Fury player character.
Matt improved our enemy spawning algorithm and worked on behind-the-scenes enemy management.He implemented a system where every individual enemy determines the percent chance of which enemy spawns next (not unlike a Markov Chain). He also created some new enemy types.
Chris worked on creating the beat system that allowed for global synching of events on a given beat. Spawning and attacking now follows the beats of the song and follow the game as it speeds up. He also improved the core enemy AI system making enemy creation easier, and implemented a 2D inverse-kinematic animation system for the player character.
Sarah worked on the music itself as well as better controls for the player. She spliced, mixed, remixed, added, layered, and implemented all audio in the game. She also implemented a high score system and integrated Unity Ads for mobile platforms. 

![Image of Game](screenshot.png "BeatsBoxing")