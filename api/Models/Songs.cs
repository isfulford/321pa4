using System.Net.Sockets;
using System;
using System.IO;

namespace api.Models
{
    public class Songs: IComparable<Songs>
    {
        // auto implemented properties
        public int SongID { get; set; }
        public string SongTitle { get; set; }
        public DateTime SongTimestamp { get; set; }
        public bool Deleted { get; set; }
        public bool Favorited { get; set; }

        public override string ToString()
        {
            return SongTitle + " (ID: " + SongID + ", Added " + SongTimestamp + ")";
        }

        public string ToFile()
        {
            return SongID + "#" + SongTitle + "#" + SongTimestamp + "#" + Deleted;
        }

        public int CompareTo(Songs temp)
        {
            return -this.SongTimestamp.CompareTo(temp.SongTimestamp);
        }
    }
}