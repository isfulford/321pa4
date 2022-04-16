using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Utilities;
using api.Interfaces;
using api.Models;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        // GET: api/stores
        [EnableCors("AnotherPolicy")]
        [HttpGet]
        public List<Songs> Get()
        {
            ISongUtilities utilObj = new SongUtilDatabase();
            return utilObj.PrintPlaylist();
        }

        // POST: api/stores
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void Post([FromBody] string title) //add-create
        {
            ISongUtilities utilObject = new SongUtilDatabase();
            utilObject.AddSong(title);

        }

        // PUT: api/stores/5
        [EnableCors("AnotherPolicy")]
        [HttpPut("{id}")]
        public void Put(int id) 
        {
            //update
            System.Console.WriteLine(id);
             SongUtilDatabase mySongs = new();
             mySongs.FavoriteSong(id);
        }

        // DELETE: api/stores/5
        [EnableCors("AnotherPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
             Console.WriteLine(id);
             SongUtilDatabase yourSongs = new();
             yourSongs.DeleteSong(id);
        }
    }
}
