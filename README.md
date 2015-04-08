# FRC-Video-Splitter
A program for fast splitting of FRC match videos

Instructions for installation:
To run using the source, open the Solution in Visual Studio. If you don't want to use the YouTube features, delete the client_secrets.json reference. To use the Youtube features you will need to go to https://console.developers.google.com and create a Client ID for native application, then download the JSON and copy it as client_secrets.json into the project (there should already be a missing reference to it)

To run as an executable, double click on setup.exe. Click Install at the Dialog. This will install the program and then launch it. For subsequent runs you can launch the program by double clicking on FRCVideoSplitter.application or using the Start Menu shortcut that gets installed.

Instructions for splitting video:
1. Enter the title of the event. ("2015 Granite State District Event")

2. Browse to the location of the large video file you want to split.

3. Browse to the folder you want to save the match videos to.

4. Enter qualification match information. If the video includes qualification matches, set the numbers of the first and last qualification matches in the large video.

5. Enter playoff / elimination match information. If the video includes playoff matches, select them from the box..

6. If desired, override the default video length. This is set to 3 minutes to give a little wiggle room when inputting the time stamps.

7. Click the "Generate Timestamp Table" button. Video Splitter will generate a table of all the matches you indicated on the right.

8. Enter time stamp information for the first video. This is the part I couldn't automate. You'll have to scrub through your video and enter the start time of the first match in the HH:MM:SS format.

9-1. MANUAL: Continue scrubbing through the video and entering the start time for all additional matches.

OR

9-2. AUTO: Make sure you have a working internet connection, check the "Use TBA Data?" checkbox and enter the Event Code (a list of event codes can be found here: http://frclinks.com/) and click "Get"

10. Click "SPLIT VIDEOS" and sit back and watch the magic.

Uploading Match Videos:
If you've just split videos as described above, skip down to step #3

1. Enter the title of the event. ("2015 Granite State District Event")

2. Browse to the folder where the videos to upload are located using the "Match Video Destination" box.

3. If you want the YouTube descriptions to contain the teams and final score, check "Use TBA Data?" and enter the Event Code (a list of event codes can be found here: http://frclinks.com/)

4. If you want to add to an existing YouTube playlist, check the "Use Existing Playlist" box and enter the Playlist ID (the part after "list=" in the URL). 
Otherwise the program will create a new playlist named the same as the Event Name

5. Click Upload Match Videos. If this is your first time uploading videos with the program you will be prompted to log in to your YouTube account and allow the program access.
The program will then upload all match videos in the folder, add them to the playlist, and add the appropriate info for adding to The Blue Alliance video spreadsheet to a CSV
file in the same folder as the videos. After the upload is complete you can open the CSV in Excel or Google Docs and copy the information into The Blue Alliance spreadsheet 
located here: https://docs.google.com/spreadsheet/ccc?key=0ApRO2Yzh2z01dExFZEdieV9WdTJsZ25HSWI3VUxsWGc#gid=0

Notes:

 - This currently only works in windows. I wrote it in C# because I'm not very good with python GUIs yet.

 - Make sure you have enough disk space on the drive you're saving videos to.

 - This is in beta.
 
