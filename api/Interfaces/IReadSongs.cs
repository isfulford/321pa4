using System.Collections.Generic;
using api.Models;

namespace api.Interfaces
{
    public interface IReadSongs
    {
        public List<Songs> GetAll();
        public Songs GetOne(int id);
         
    }
}