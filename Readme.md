# Starter project: Visual Basic .net source code for a desktop application to interface with the [Arduino Uno microcontroller](https://github.com/tclupper/StarterUno).

### Rev 3/16/2021
### License: [Attribution-ShareAlike 4.0 International](https://creativecommons.org/licenses/by-sa/4.0)

---
## Background
The _Starter Project_ the final part of a Basic Electronics course that was taught at the [Route 9 Library and Innovation center](https://nccde.org/1389/Route-9-Library-Innovation-Center) back in October of 2019.  The videos were recorded as a follow-up for the students taking the class (*not meant to be a substitute for the in-person class*).  They can be viewed here:

### Course title: _Basic Electrical Circuits_

[Introduction video](https://youtu.be/7xKFPJ8yrWM)

[Part1: Current, Resistance and Voltage](https://youtu.be/wcw07wuuB8o)

[Part2: Ohm's Law and Power](https://youtu.be/5naIT84_2M0)

[Part3: Sensors and the Arduino](https://youtu.be/qC13UVfvqh0)

[Part4: Programming the Arduino](https://youtu.be/MEm4goe0QIw)

---
## Visual Basic code
This repository contains the Visual Basic source code necessary to interface to the _Starter Project_ [Arduino Uno microcontroller board](https://github.com/tclupper/StarterUno). You will need to download and install [Microsoft Visual Studio Community](https://visualstudio.microsoft.com/vs/community/).  At the current time it is available for free from microsoft, but there are no guarantees that it will stay that way.  Microsoft has made the decision to basically stop developing Visual Basic as one of their core languages and will only support it as a legacy product.

After installing Visual Studio Community, you will want to install the extension [Microsoft Visual Studio Installer Projects](https://marketplace.visualstudio.com/items?itemName=VisualStudioClient.MicrosoftVisualStudio2017InstallerProjects) so that you can compile programs into a single "Install" type file.

The Visual Basic application has a Windows Form that contains a basic set of user controls:
* text box with label
* checkbox
* radio buttons
* drop-down list box
* multi-line text box (used for input and output)
* menus with File and about boxes

It includes the necessary code to communicate with the Arduino Uno via the virtual com-port using the "commands" within the firmware.  You can even attached multiple Arduino Unos to the computer's USB ports and the program will allow you to select the one to communicated with.  When you start the program, it automatically loads the available Unos into a drop-down box for you to select.

The basic operation allows you to send commands to the Uno to control things like LEDs and get status of pushbuttons.  The main feature allows you to automatically read the analog port(s) of the Uno at regular intervals.  You specify the interval (in seconds) and the email notification information in the "starter.ini" file.  You check the box to start the streaming of the Arduino data.  If you specify a log file, it will log the data to the file as well as showing it on the screen.  Simply uncheck the box to stop.  Using an Arduino as a data-logger is an important and typical use case.  

In general, the idea is that you would use this code as a starting point to develop your own application. This project will be continuously evolving and improving.  However, I will try and prevent unnecessary "feature creep".

Enjoy!!

![](/images/screencapture.png)
### Figure#1: Screen capture


---
## A few notes about starting a new project
Visual Studio allows you to develop many types of projects using several languages.  The Starter projects are basic WIndows type desktop programs based on the .NET framework less than version 5.0.  Follow these basic steps to create a new project from scratch.

![](/images/VisualStudio_A.png)
### Figure#2: create a new project
![](/images/VisualStudio_B.png)
### Figure#3: Select "Windows Form App (.NET Framework)
![](/images/VisualStudio_C.png)
### Figure#4: Give it a name, select a directory and choose your .NET framework

---
When you want to create an "Installer project" to go along with your application, here are some screen shots that will help the process:

![](/images/InstallerProject_A.png)
### Figure#5: Right-click solution and "Add" a "New Project"
![](/images/InstallerProject_B.png)
### Figure#6: Select a "Setup Project"
![](/images/InstallerProject_C.png)
### Figure#7: Give it a different name than you project and put it in the same directory
![](/images/InstallerProject_D.png)
### Figure#8: Right-click "Install project" and Add the "Output" of your project to the Installer project
![](/images/InstallerProject_E.png)
### Figure#9: Add primary output
![](/images/InstallerProject_F.png)
### Figure#10: Right-click and add a shortcut to the Desktop
![](/images/InstallerProject_G.png)
### Figure#11: Select primary output as the shortcut

