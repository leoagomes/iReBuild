Welcome to iReBuild (console)!
Thank you for using iReBuild/iLib.

Here's all the documentation I have to give you. So sorry.

iReBuild and iLib are all released under the GNU General Public License (GPL). Check LICENSE.txt for more.

*Another Quick Note: I know this Lib works on iOS 6. Not sure about other versions.

ABOUT iReBuild (console) {

iReBuild is (for now) a console tool to translate those messy WXYZ-named files inside iTunes_Control/Music/FXX to beautifully named Song.mp3 with all of its properties correctly set.
Usage is simple: give it the path to the iTunes_Control folder and a path where it will move the, now 'fixed', songs to. Press enter and wait.
If you don't know how to get the iTunes_Control folder: download iFunbox (no linux, sry), connect your iDevice, click "Raw File System" navigate to /private/var/mobile/Media, drag iTunes_Control to your desktop.
***I am not sure wether you must be jailbroken to access the iDevice's File System, mine's Jailbroken.

Its source code is poorly documented, I know. Sorry about that.
It shows basic usage of iLib (iLibraryReader,MediaFile) and shows how the MediaFile object is designed to work with tags.

}

ABOUT THE CODE (in general) {

Messy, not the best documentation, maybe not the best practices in the .NET world, but my excuse is:
I developed most of it around the morning of a day that I haven't slept the night before.

}

ABOUT iLib (in general){

This name is not great, I know. I am not good at picking names.
Remember it is still under development.

* DO NOT forget collect your SQLite-related garbage (.Dispose(), =null) or else you'll get some SIGSEGVs for christmas.

As I developed it on a Mac, through Mono/MonoDevelop/Xamarin Studios, and I didn't want to modify libsqlite3.dylib (I tried, which ended up rendering my Mac useless until I fixed it),
I use Mono.Data.Sqlite.
Comment that line, uncomment System.Data.SQLite and Capitalize 'SQL' in every type and you should be ready to go on Windows. *be sure to have the System.Data.SQLite.dll.
}

ABOUT THE MediaFile OBJECT {

It was mainly based on the properties of a TagLib# Tag.
Right after that there were some properties I found while trying to understand the MediaLibrary DB. All of that properties can be found inside (new MediaFile).iTunes.
I am pretty sure the names are self explanatory, except for:

"Location", which is actually the name of the file (e.g. ADZQ.mp3).
"LocationKind", which is more of a kind then a location. Well, it is the kind of the file in a human-readable way.
"BaseLocation", which is the folder (inside an iDevice) where the file ("Location") is stored.

}

ABOUT THE iLibraryReader {

Open/CloseSQLite() -> Opens/Closes (and disposes and sets to null) the SQLConnection;
GetAllMediaFiles() -> foreach row in the 'item_extra' table, calls GetMediaFile (row["location"]). Returns a List<MediaFile> of the elements;
GetMediaFile(location) -> returns a MediaFile object based on it "location" (actually the name of the file e.g. AHDS.mp3);
GetDataTable(query,oc) -> returns a DataTable object based on a query (that should ONLY READ). OC means Open/Close... if you want the method to Open/Close connection or if you'll DIY.
And Again:
* DO NOT forget collect your SQLite-related garbage (.Dispose(), =null) or else you'll get some SIGSEGVs for christmas.

}

*IF YOU LIKE THIS PROJECT AND ARE A DEVELOPER, PLEASE CONTRIBUTE.