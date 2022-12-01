const hub = new signalR.HubConnectionBuilder()
    .withUrl("/game")
    .build();
