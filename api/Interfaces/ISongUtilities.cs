using System.Collections.Generic;
using api.Models;

namespace api.Interfaces
{
    public interface ISongUtilities
    {
         public List<Songs> playlist { get; set; }
         public void AddSong(string title);
         public void DeleteSong(int id);
         public void EditSong();
         public List<Songs> PrintPlaylist();

    }
}