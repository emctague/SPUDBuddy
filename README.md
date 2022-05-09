# SPUDBuddy

This is a mod, and accompanying injection tool, that works with The Stanley Parable: Ultra Deluxe.

It consists of two projects:

 - **UnityPatcher** is a command-line tool that patches the Unity CoreModule DLL to 
   create a version which will load and run RuntimeInjectedCode.
 - **RuntimeInjectedCode** is a class library that, when built as a DLL and placed in the
   `Data/Managed` folder of a game that has been patched by UnityPatcher, will
   provide some simple cheat tools for TSP:UD.

This project might also be useful as a template for creating similar tools for *other* Unity-based games: the patcher is quite game-independent, and the RuntimeInjectedCode mod's injection and cheat-screen system are not specific to TSP either.

## Building / Installing

This project can be built in two phases: first, the unity DLLs must be patched and the project must be given proper references to the DLLs it depends on. This must only be done once, after which you are free to build and iterate on the actual *mod* as you please.

### Initial Setup / Patching

1. Copy the contents of the target unity game's `Data/Managed` folder to a directory called "Managed" under the root
of this solution.
2. Run UnityPatcher, which will properly patch the UnityEngine.CoreModule DLL in that directory.
3. Copy the patched UnityEngine.CoreModule.dll back to the *real* `Data/Managed` folder, overwriting the old unpatched copy.

### Building the mod

1. Build RuntimeInjectedCode in release mode.
2. Copy the resulting RuntimeInjectedCode.dll to the game's `Data/Managed` folder. The mod is now ready to run! 
