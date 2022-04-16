const baseUrl = "https://localhost:5001/api/Songs"; 
var songLists = [];

function handleOnLoad(){
  populateList();
}

function populateList(){
let html = ``;
    const allSongsApiUrl = baseUrl;
    console.log(allSongsApiUrl);
    fetch(allSongsApiUrl).then(function(response){
        return response.json();
    }).then(function(json){
        console.log(json);
        songLists = json; //displays cards
            songLists.forEach((song)=>{
            html += `<div class="card col-md-4 bg-dark text-white">`;
			html += `<img src="./resources/images/music.jpeg" class="card-img" alt="...">`;
			html += `<div class="card-img-overlay">`;
			html += `<h5 class="card-title">`+song.songTitle+ "-" +song.songID+`</h5>`;
            html += `<div id = "favoriteButton" class ="button">`;
            html += `</div>`;
            html += `</div>`;
            html += `</div>`;
        });
        document.getElementById("cardList").innerHTML = html; //take html and applies to the list
    }).catch(function(error){
        console.log(error);
    })
}

function deleteSong(){
    const songID = document.getElementById("deleted").value;
    console.log(songID)
    const deleteSongApiUrl = baseUrl + "/" + songID;
    fetch(deleteSongApiUrl, {
        method: "DELETE",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        }
    })
    .then((response)=>{
        console.log(response);
        populateList();
    });
}

function favoriteSong(){
    let songID = document.getElementById("favorited").value;
    //console.log(songID)
    const favSongApiUrl = baseUrl + "/" + songID;
    fetch(favSongApiUrl, {
        method: "PUT",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        }
    })
    .then((response)=>{
        console.log(response);
        populateList();
    });
}

function postSong(){ //add

    const postSongsApiUrl = baseUrl;
    var title = document.getElementById("title").value;
    console.log(title);
    fetch(postSongsApiUrl, {
        method: "POST",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',
        },
        body: JSON.stringify(title)
    })
    .then((response)=>{
        console.log(response);
        populateList();
    });
}

