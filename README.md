# clouds-and-bombs

A zig-zag style runner made in [Unity](https://unity3d.com/).

## Description

How far can you reach? Evade the bombs and collect as many stars as you can along the way.

- Different themes
- Endless, increasing-difficulty gameplay
- Beautiful, low-poly environment

## Controls

- Left click / tap: turn the ball onto the next path
- Right click / tap: jump over bombs

## Tech

The ball's speed ramps up gradually as the run continues (via a `difficulter` value fed into `GameManager`), so runs get harder the longer you survive. Jumps and falls are eased with `Vector3.Lerp`/`Quaternion.Lerp` rather than raw physics forces, which is what gives the movement its snappy, arcade feel instead of a floaty rigidbody.

## Requirements

- Unity 5.6.0f3 (or newer)

## Download

This game is presented on [Cafe Bazaar](https://cafebazaar.ir) for free!

[Download link](https://cafebazaar.ir/app/com.NavidAdelpour.CloudsAndBombs/)

## Screenshots

<p align="center">
  <img width="200" src="https://raw.githubusercontent.com/navidadelpour/clouds-and-bombs/master/Assets/ScreenShots/1.jpg" />
</p>
<p align="center">
  <img width="200" src="https://raw.githubusercontent.com/navidadelpour/clouds-and-bombs/master/Assets/ScreenShots/2.jpg" />
</p>
<p align="center">
  <img width="200" src="https://raw.githubusercontent.com/navidadelpour/clouds-and-bombs/master/Assets/ScreenShots/3.jpg" />
</p>
<p align="center">
  <img width="200" src="https://raw.githubusercontent.com/navidadelpour/clouds-and-bombs/master/Assets/ScreenShots/4.jpg" />
</p>
<p align="center">
  <img width="200" src="https://raw.githubusercontent.com/navidadelpour/clouds-and-bombs/master/Assets/ScreenShots/5.jpg" />
</p>


## Credits

- Graphics: [photoshop](https://www.adobe.com/products/photoshop.html) and [illustrator](https://www.adobe.com/products/illustrator.html)
- Game Engine: [Unity](https://unity3d.com/)
- Audio: [FL Studio](https://www.image-line.com/flstudio/) and [freesound.org](https://freesound.org/)
- Thanks to all of the friends for their awesome help.


Made with :heart: by [navidadelpour](https://navidadelpour.ir)
