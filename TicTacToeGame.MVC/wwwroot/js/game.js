const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/game-hub")
    .build();

hubConnection.on("GetConnectionInfo", function (gameInfoDto) {
    if (gameInfo.gameStatus === 'Finished') {
        if (window.confirm("The second player left the game!")) {
            document.location.href = '/';
        } else {
            document.location.href = '/';
        }
    }
    gameInfo = gameInfoDto;
    gameInfo.playingField = gameInfoDto.board;
});

hubConnection.on("GetWinnerPlayer", function (winnerPlayerName, isWin) {
    console.log('winner')
    let winnerInfoContainer = document.querySelector('.winner-info-container');
    winnerInfoContainer.innerHTML = winnerInfoContainer.innerHTML
        .replace("{winner}", `${winnerPlayerName}`)
        .replace("{win}", isWin ? "You won!" : "You lost!");
    winnerInfoContainer.style.display = 'flex';
});

hubConnection.on("GetPlayerName", function (player) {
    playerName = player;
});

let cross = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-x-lg" viewBox="0 0 16 16">
    <path
        d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8 2.146 2.854Z"/>
</svg>`;
let zero = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-circle" viewBox="0 0 16 16">
  <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z"/>
</svg>`;
let playerName;
let gameInfo = {
    playerMoveName: '', stepTypes: '', playingField: [], gameStatus: ''
};

async function stepMove(element) {
    if (gameInfo.stepTypes === 'x' && playerName === gameInfo.playerMoveName
        && element.innerHTML === ''){
        element.innerHTML = cross;
    } else if (gameInfo.stepTypes === 'o' && playerName === gameInfo.playerMoveName
        && element.innerHTML === ''){
        element.innerHTML = zero;
    } else{
        return;
    }
    await hubConnection.invoke("PlayerStep", connectionId, +element.id);
}

let connectionId = window.location.search.split('=').slice(-1)[0];
let url = location.href;

window.onload = async function () {
    await hubConnection.start();
    await hubConnection.invoke("GetPlayerName");
    await hubConnection.invoke("SendAllGame");
    await hubConnection.invoke("Connect", connectionId);
    history.pushState({page: 1}, "title 1", location.href);
}