# XR Manager Support

This github repository includes a full unity project that is setup and ready to go for XR Management for use with Oculus.
It is adivised to instead use the unity package provided in releases and follow the 'XR Manager in a new project' instructions below. 


**Latest Unity version tested: 2019.3.13f1**

# XR Manager in a new project
Install XR Management Toolkit & XR Plugin Management from the package manager (Window -> Package Manager)


Make sure to show preview packages (Advanced -> Show preview packages)


Remove Vulcan from Gaphics APIs (Edit -> Project Settings -> Player -> Graphics APIs)


Go to XR Management (Edit -> Project Settings -> XR Management)


Make sure to add Oculus SDK by pressing '+'


If you change your build platform (e.g. from Android to PC) then make sure to update the project settings for that platform (Repeat steps above). 


Import XR Support Package from the releases section of this repository


Add 'XR Player Oculus' prefab to scene (XRSupport/Prefabs/XR Player Oculus)


Plugin Oculus Quest


Build and run

# Run in Virtual Desktop
Virtual Desktop allows you to run almost any game from Oculus Store or Steam VR via WiFi on the Oculus Quest.


Download the Virtual Desktop Streamer application on your computer from [here](https://www.vrdesktop.net/)


Install Virtual Desktop app using SideQuest [download sidequest](https://sidequestvr.com/setup-howto) [Virtual Desktop App](https://sidequestvr.com/app/16/virtual-desktop)


To run a Unity application that has been built for Oculus:


Startup the Virtual Desktop steamer (on computer)


Start the Virtual Desktop app (on Quest)


Select the PC in the app


Go to the taskbar icon for Virtual Desktop Streamer


Right click the icon and select 'inject game...'


Choose the exe in the build directory of this project


This should run the application inside the Quest!

# Generating a repository for your project
Clone this repository
```
git clone https://github.com/KnightVR/XRSupport.git
```
Delete the git folder
```
rmdir /s PATH_TO_REPO/.git
```
Create your own git repositroy on github

Initalise new repository
```
git add .
git commit -m "init commit"
```

Set the remote url to your new repository
```
git remote set-url origin YOUR_NEW_REPO_URL
```

Push repository to remote
```
git push -u origin master
```

You will need to rename the unity project within unity to the name of your own VR project but this is simple.


Go to Project Settings -> Player -> Change the text in the fields product name / company name to your own project and company.


This is now ready to use, but you can tryout the example scene by opening an example scene:
```
Assets/Scenes/XRSupport/InteractibleCubeScene
```

You can switch between Android and PC development by changing the platform target. The unity project has been full setup for use in both. 

# Useful guides
Unity provides guide on using XR Management Toolkit [here](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/manual/index.html)

# Unity SteamVR support
If you would like to use SteamVR in your unity project rather than XR Management then use this [github repository](https://github.com/KnightVR/SteamVRSupport) instead.


This is neccessary as Unity XR Manager does not support SteamVR. When and if this is added, these two will be joined.
