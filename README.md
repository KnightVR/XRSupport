# XR Manager Template

This github template repository includes a full unity project that is setup and ready to go for XR Management for use with Oculus.


When making a VR unity project for use with XR Management, to save time you can generate a new github repository from this template and be ready to go!


**Latest Unity version tested: 2019.3.12f1**

# Generating a repository from this template
Go to this repositories github [here](www.google.com)


Click 'Use this template'


Select the owner of the new repository


Enter a name for your new repository


Click create repository.


You will need to rename the unity project within unity to the name of your own VR project but this is simple.


Go to Project Settings -> Player -> Change the text in the fields product name / company name to your own project and company.


This is now ready to use, but you can tryout the example scene by opening an example scene:
```
Assets/Scenes/SampleScene
```

# XR Manager in a new project
Install XR Management Toolkit & XR Plugin Management from the package manager (Window -> Package Manager)


Make sure to show preview packages (Advanced -> Show preview packages)


Remove Vulcan from Gaphics APIs (Edit -> Project Settings -> Player -> Graphics APIs)


Add XR Player Prefab to scene


Plugin Oculus Quest


Build and run

# Useful guides
Unity provides guide on using XR Management Toolkit [here](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/manual/index.html)

# Unity SteamVR support
If you would like to use SteamVR in your unity project rather than XR Management then use this [github template repository](www.google.com) instead.


This is neccessary as Unity XR Manager does not support SteamVR. When and if this is added, these two will be joined.