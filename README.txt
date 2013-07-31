Welcome to iReBuild (console)!
Thank you for using iReBuild/iLib.

Here's all the documentation I have to give you. So sorry.

iReBuild and iLib are all released under the GNU General Public License (GPL). Check LICENSE.txt for more.


* Quick Note: The iLib.dll used in this project was built with Mono.Data.SQLite. If you want to use it on Windows, you'll have to rebuild the lib. Or you could install Mono Runtime Enviroment. 
(http://www.go-mono.com/mono-downloads/download.html)
* Another Quick Note: I know this Lib works on iOS 6. Not sure about other versions.

ABOUT iReBuild (console):

iReBuild is (for now) a console tool to translate those messy WXYZ-named files inside iTunes_Control/Music/FXX to beautifully named Song.mp3 with all of its properties correctly set.
Usage is simple: give it the path to the iTunes_Control folder and a path where it will move the, now 'fixed', songs to. Press enter and wait.
If you don't know how to get the iTunes_Control folder: download iFunbox (no linux, sry), connect your iDevice, click "Raw File System" navigate to /private/var/mobile/Media, drag iTunes_Control to your desktop.
***I am not sure wether you must be jailbroken to access the iDevice's File System, mine's Jailbroken.

Its source code is poorly documented, I know. Sorry about that.
It shows basic usage of iLib (iLibraryReader,MediaFile) and shows how the MediaFile object is designed to work with tags.

