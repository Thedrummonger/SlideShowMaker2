# Slide Show Maker
A program to create slideshows for information kiosks.

## Functionality

### General use
	-This program supports both video and images of the following File types
		-Images (".JPG", ".JPEG", ".BMP", ".GIF", ".PNG")
		-Video (".MP4", ".WMV", ".AVI")
	-Files can be added or removed from the source folder while the program is running, allowing updates to the presentaion to happen live. When the Transition occurs, the slide will update and play the next available file in the folder.
		-If no files are available, the last played file will remain on screen until new files are added. If the last played file was a video, this will result in a black screen.
	-While the slide is active, certain keys will affect the slide.
		-Esc: Exits the slide show
		-Tab: Procedes the next file
		-Backspace: Goes back to the previous File

### Ordering Files
	-If the “Shuffle” option is not selected, the files will be displayed in the same order they are shown in the folder (assuming the folder is sorted by name). To manually order files, change the name to begin with a number (01, 02, 03, etc...)
		-Example:
			01 First Image.jpg
			02 Second Image.png
			03 First Video.avi
			04 Third Image.png
			05 Second Video.mp4
			06 Third Video.avi

## Interface
### -File Folder Path
	-Type the path to the folder that contains video/picture files in this text box.

### -Folder Path Select
	-This button opens a folder select dialog box that will fill the Folder Path text box automatically based on the folder you select.

### -Transition Interval/Time Frame
	-Use these to set the amount of time an image will be show before moving on to the next one. This does not apply to videos as videos will always instantly move to the next file when the video is finished playing.

### -Search Sub Folders
	-By default, the program will only look for files in the root of the Folder path. Enable this option to allow the program to search for files in subfolders than exist in your root folder.

### -Shuffle
	-By default, the program will display files in the same order they appear in the folder. Enable this to shuffle the order the files are displayed.

### -Mute video sound
	-Enable this option to prevent videos in the slide show from playing sound.

### -Start Slide
	-Begin the slideshow

## Advanced Used

### Startup Options
	-By Default, the program will launch with the same options it was last closed with.
	-Pressing f4 will lock the current settings as default. Even if the settings are changed, the next time the program is opened these default settings will be loaded. This can be undone by pressing f4 	again.
	-These options are stored as files in the "%appdata%\SlideShowMaker" folder on your PC
		-The options saved when the application closes are saved in the "Options.ini" File.
		-The options saved when setting default options using f4 are saved in the "DefaultOptions.ini" File.
			-If you would like certain options to not have a default setting while keeping defaults for others, you can delete those entries from the "DefaultOptions.ini" file individually.

### Command Line
	-The program can be launched from the command line and will take arguments for each setting. Command line arguments will have priority over Default options
		-Command line arguments are structured as follows
			Key=value
		-"Key" is the name of the option and "value" is what is assigned to it
		-Valid key/value arguments are
			-path
				-Values: A standard folder path. Encase in quotes if the path contains spaces
				-Sets the programs file path
			-shuffle
				-Values: true or false
				-Sets the state of the “shuffle” check box
			-mute
				-Values: true or false
				-Sets the state of the “Mute Video Sound” check box
			-subfolders
				-Values: true or false
				-Sets the state of the “Search Sub Folders” check box
			-interval
				-Values: a number
				-Sets the value of the “transition Interval”
			-timeframe
				-Values: [s, m, h]
				-Sets the value of the “Time Frame”
		-The following arguments do not take a value
			-autostart
				-will automatically start the slide show when the program is launched.
			-kill
				-Will close any open instance of the program. Useful when managing the application remotely.
			-Any True/False argument
				-Any argument which only takes true or false can be used without a value, in which case it will default too “true”.
		-Examples
			-Starting the program automatically with shuffle enabled
				SlideShowMaker2.exe autostart shuffle=true
			-Opening the Program with a custom path, shuffle disabled, and Sub folders enable (subfolders is not given a value meaning it default to true)
				SlideShowMaker2.exe shuffle=false subfolders path="C:\Users\ExampleUser\Pictures\Saved Pictures"
			-Starting the program automatically with a transition interval of 2 minutes
				SlideShowMaker2.exe interval=2 timeframe=m autostart
