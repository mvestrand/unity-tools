# Tools for Unity Core Package

## Overview

The contents of this package are general purpose tools to make working with Unity more convenient.

## Package Contents

This package currently contains the following:
- Globals - ScriptableObjects used as data-based global variables for communication and to prevent singletons. 
Based on a [Unite talk by Ryan Hipple](https://www.youtube.com/watch?v=raQ3iHhE_Kk) and an [associated article](https://unity.com/how-to/architect-game-code-scriptable-objects)
- Pool - A MonoBehaviour class for pooling game objects and its associated classes.
- SerializableType - A type class that can be serialized and edited within Unity's editor window.
- Extensions - Class extensions for unity objects to provide debugging info such as scene hierarchy path names.
- Utility - Catch-all for code that doesn't belong anywhere else. Currently contains type declarations for single argument UnityEvents with common types.
- Compatibility - Declarations of dummy attributes to prevent not having odin inspector from causing errors.

## Installation Instructions

Add this package to an existing unity project from the built in package manager by selecting "Add package from git URL..." and giving the url: "https://github.com/mvestrand/unity-tools.git?path=/Assets/Core" 

## Requirements

This package currently does not require any external libraries, but some of the editors are more nicely formatted if Odin Inspector is also included.
