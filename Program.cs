using System;
using System.IO;
using System.Data;
using TagLib;
using iLib;

namespace iReBuild
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//I AM REALLY SORRY THE COMMENTS ARE CONFUSING, BUT I'M WRITING ALL OF THIS 8:30 AM
			//AND I HAVEN'T SLEPT YET.

			//Vars
			string iControlPath;
			string MediaPath;
			string DBFilePath;
			string OutputFolder;
			string[] AudioFiles;
			string[] DBAudioFiles;

			//Hellos, Descriptions
			Console.WriteLine ("Hello and Welcome to iReBuild (console)!");
			Console.WriteLine ("If you don't know what you're doing here, don't be afraid.\nJust read the README.");
			Console.WriteLine ("\nIf you haven't done it yet, please dump the iTunes_Control folder located at " +
			                   "/private/var/mobile/Media inside your iDevice (use iFunBox or something like that).");
			Console.WriteLine ("Now that you've done all of that, please tell me the location of that folder\n" +
			                   "(including iTunes_Control, like: /path/to/iTunes_Control (on unix-based) or C:\\path\\to\\iTunes_Control (on windows))");

			//iTunes_Control folder Path... PLEASE early handle DirNotFound exceptions...
			//Mono is not in the mood to compile anything with Directory.Exists() ATM.
			iControlPath = Console.ReadLine ();

			Console.WriteLine ("Now tell me the output folder for the songs.");
			OutputFolder = Console.ReadLine ();

			Console.Clear ();
			Console.WriteLine ("Thank You!\nNow Wait until I finish...");

			Console.Write ("Setting MediaPath...");
			//Should work. Is not. IDK why. Not in the mood to think about it, actually.
			if (!iControlPath.EndsWith ("/") || !iControlPath.EndsWith ("\\")) {
				if (iControlPath.Split ('\\').Length > 1)
					iControlPath += "\\";
				else 
					iControlPath += "/";
			}
			MediaPath = iControlPath + "Music";
			Console.Write (" OK\n");

			Console.Write ("Setting Database Path...");
			DBFilePath = iControlPath + "/iTunes/MediaLibrary.sqlitedb"; //DB, of course.

			Console.Write ("Listing files inside iControl/Music...");
			//Populates the Array with paths to the file, however, the DB uses only the XXXX.mp3, so...
			AudioFiles = Directory.GetFiles (MediaPath, "*.*", SearchOption.AllDirectories);

			Console.Write (" OK\n");

			Console.Write ("Formatting list of files...");
			//here we have to remove everything else, but here, we store only the actual file names
			//inside another array, because later we'll use AudioFiles to modify the File's Tag...
			//This code is a little confusing, but it shouldn't be hard to understand.
			DBAudioFiles = (string[])AudioFiles.Clone(); //Just so everything goes well...
			for (int i = 0; i < DBAudioFiles.Length; i++) {
				string[] splitted = DBAudioFiles [i].Split ('/');
				DBAudioFiles [i] = splitted [splitted.Length - 1];
				if (DBAudioFiles [i] == ".DS_Store") { //.DS_Stores can cause some trouble.
					DBAudioFiles[i] = string.Empty;
				}
				Console.WriteLine (DBAudioFiles [i] + " " + AudioFiles [i]);
			}
			Console.Write (" OK\n");

			Console.WriteLine ("Found " + AudioFiles.Length + " files!\nPress ENTER/RETURN when ready to start rebuilding...");
			Console.Read (); //give the user the last word

			Console.WriteLine ("Starting Process...");
			iLibraryReader lib = new iLibraryReader (DBFilePath);
			for (int i = 0; i < DBAudioFiles.Length; i++) {
				if (DBAudioFiles [i] != string.Empty) {
					Console.WriteLine ("Gathering data for " + DBAudioFiles [i] + "...");

					MediaFile mf = lib.GetMediaFile (DBAudioFiles [i]);

					Console.WriteLine ("Writing to " + DBAudioFiles [i] + "'s TAG...");

					//Now we use TagLib to write all that data to the files tag...
					//to get to the file we'll use 

					TagLib.File song = TagLib.File.Create (AudioFiles[i]);

					song.Tag.Album = mf.Album;
					song.Tag.AlbumArtists = new string[]{mf.AlbumArtist};
					song.Tag.AlbumArtistsSort = new string[]{mf.AlbumArtistSort};
					song.Tag.AlbumSort = mf.AlbumSort;
					song.Tag.BeatsPerMinute = mf.BeatsPerMinute;
					song.Tag.Comment = mf.Comment;
					song.Tag.Composers = new string[]{ mf.Composers };
					song.Tag.ComposersSort = new string[]{ mf.ComposersSort };
					song.Tag.Copyright = mf.Copyright;
					song.Tag.Disc = mf.Disc;
					song.Tag.DiscCount = mf.DiscCount;
					song.Tag.Genres = new string[] {mf.Genres};
					song.Tag.Lyrics = mf.Lyrics;
					song.Tag.Performers = new string[] {mf.Performers};
					song.Tag.PerformersSort = new string[] {mf.PerformersSort};
					song.Tag.Title = mf.Title;
					song.Tag.TitleSort = mf.TitleSort;
					song.Tag.Track = mf.Track;
					song.Tag.TrackCount = mf.TrackCount;
					song.Tag.Year = mf.Year;

					song.Save ();

					Console.WriteLine ("Moving song to desired location....");

					if (!System.IO.File.Exists (OutputFolder + "/" + mf.Title + "." + DBAudioFiles [i].Split ('.') [1])) {
						System.IO.File.Move (AudioFiles [i], OutputFolder + "/" + mf.Title.Replace('/','-') + "." + DBAudioFiles [i].Split ('.') [1]);
					} else {
						int n = 2;
						while (System.IO.File.Exists (OutputFolder + "/" + mf.Title.Replace('/','-') + " ("+ n +")." + DBAudioFiles [i].Split ('.') [1])) {
							n += 1;
						}
						System.IO.File.Move (AudioFiles [i], OutputFolder + "/" + mf.Title.Replace('/','-') + " ("+ n +")." + DBAudioFiles [i].Split ('.') [1]);
					}
				}
			}

			Console.WriteLine ("It looks like I am finished here.");
			Console.Read ();
		}
	}
}
