using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using api.Interfaces;
using api.Models;

namespace api.Utilities
{
    public class SongUtilDatabase : ISongUtilities
    {
        public List<Songs> playlist { get; set; }
        public ConnectionString myConnection = new ConnectionString();

        public SongUtilDatabase()
        {
            playlist = new List<Songs>();
        }

        public void AddSong(string title)
        {
            using var con = new MySqlConnection(myConnection.cs);
            con.Open();
            string stm = @"INSERT INTO songs(title) VALUES(@title)";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteSong(int id)
        {
            using var con = new MySqlConnection(myConnection.cs);
            con.Open();
            string stm = @"UPDATE songs SET deleted = true where id = @id";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void FavoriteSong(int id)
        {
            using var con = new MySqlConnection(myConnection.cs);
            con.Open();
            string stm = @"UPDATE songs SET favorited = true where id = @id";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void EditSong()
        {
            using var con = new MySqlConnection(myConnection.cs);
            con.Open();

            string stm = @"UPDATE songs SET title = @title where id = @id";
            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", PromptSongToEdit());
            cmd.Parameters.AddWithValue("@title", PromptSongDetails());
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public int PromptSongToEdit()
        {
            string userInput;

            do
            {
                Console.Clear();
                PrintPlaylist();
                Console.WriteLine("What is the ID of the song you want to edit?");
                userInput = Console.ReadLine();

            } while (!CheckValidInput(userInput));

            return int.Parse(userInput);
        }

        public List<Songs> PrintPlaylist()
        {
            playlist.Clear();
            using var con = new MySqlConnection(myConnection.cs);
            con.Open();

            string stm = @"SELECT * from songs where deleted = false order by time desc ";
            using var cmd = new MySqlCommand(stm, con);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Songs item = new Songs();
                    item.SongID = reader.GetInt32(0);

                    item.SongTitle = reader.GetString(1);

                    item.SongTimestamp = reader.GetDateTime(2);

                    item.Deleted = reader.GetBoolean(3);

                    item.Favorited = reader.GetBoolean(4);

                    playlist.Add(item);
                }

                foreach (Songs song in playlist)
                {
                    System.Console.WriteLine(song.ToString());

                }

            }
            con.Close();
            return playlist;
        }

        public string PromptSongDetails()
        { 
            Console.Clear();
            Console.WriteLine("What is the title of your song?");
            return Console.ReadLine();
        }

        public int PromptSongToDelete()
        {

            string userInput;

            do
            {
                Console.Clear();
                PrintPlaylist();
                Console.WriteLine("What is the ID of the song you want to delete?");
                userInput = Console.ReadLine();

            } while (!CheckValidInput(userInput)); // ID entered must be an integer
            return int.Parse(userInput);

        }

        public bool CheckValidIndex(int index)
        {
            if (index == -1)
            { // if returns -1, the ID was not found
                Console.WriteLine("\nID does not exist in the current playlist. Press any key to continue");
                Console.ReadKey();
                return false;
            }
            return true;
        }


        public bool CheckValidInput(string userInput)
        {

            int parsedInput;

            if (!int.TryParse(userInput, out parsedInput))
            {
                Console.WriteLine("Invalid input. Try again.");
                Console.ReadKey();
                return false;
            }
            return true;
        }

    }
}