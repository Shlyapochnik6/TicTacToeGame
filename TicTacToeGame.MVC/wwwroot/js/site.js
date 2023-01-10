let createBtn = document.getElementById('create-btn');
let joinInput = document.getElementById('join-input');
let joinBtn = document.getElementById('joinBtn');
let connectionId = joinBtn.value;

createBtn.addEventListener('click', async function () {
    await fetch('AdminPanel/CreateGame/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify()
    })
})

joinBtn.addEventListener('click', async function () {
    await fetch('AdminPanel/JoinGame/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(connectionId)
    })
})



