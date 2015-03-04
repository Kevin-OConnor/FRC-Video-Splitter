# FRC-Video-Splitter
A program for fast splitting of FRC match videos

Instructions for use:

1. Enter the title of the event. ("2015 Granite State District Event")

2. Browse to the location of the large video file you want to split.

3. Browse to the folder you want to save the match videos to.

4. Enter qualification match information. If the video includes qualification matches, set the numbers of the first and last qualification matches in the large video.

5. Enter playoff / elimination match information. If the video includes qualification matches, enter their names (QF#, SF#, F#, SF1-2, etc).

6. If desired, override the default video length. This is set to 3 minutes to give a little wiggle room when inputting the time stamps.

7. Click the "Generate Timestamp Table" button. Video Splitter will generate a table of all the matches you indicated on the right.

8. Enter time stamp information. This is the part I couldn't automate. You'll have to scrub through your video and enter the start time of each match in the HH:MM:SS format. It's not too painful. I'm able to scrub through an entire day of matches in about 20 min.

9. Click "SPLIT VIDEOS" and sit back and watch the magic.


Notes:

 - This currently only works in windows. I wrote it in C# because I'm not very good with python GUIs yet.

 - You'll need to have FFmpeg installed and located at C:\ffmpeg.exe. (Download here)

 - Make sure you have enough disk space on the drive you're saving videos to.

 - This is in beta.