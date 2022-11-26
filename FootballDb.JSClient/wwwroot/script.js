
let players = [];

let connection = null;

let playerIdToUpdate = -1;

getdata();

setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PlayerCreated", (user, message) => {
        getdata();
    });

    connection.on("PlayerDeleted", (user, message) => {
        getdata();
    });

    connection.on("PlayerUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:53910/player')
        .then(x => x.json())
        .then(y => {
            players = y;
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    players.forEach(p => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + p.playerId + "</td><td>"
            + p.name + "</td><td>" 
            + p.positon + "</td><td>" +
            + p.salary + "</td><td>" +
            + p.age + "</td><td>" +
            + p.goalsInSeason + "</td><td>" +
            + p.clubId + "</td><td>" +
             `<button type="button" onclick="remove(${p.playerId})">Delete</button>` +
             `<button type="button" onclick="showupdate(${p.playerId})">Update</button>`
            + "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('playernameupdate').value = players.find(p => p['playerId'] == id)['name'];
    document.getElementById('playerpositionupdate').value = players.find(p => p['playerId'] == id)['positon'];
    document.getElementById('playersalaryupdate').value = players.find(p => p['playerId'] == id)['salary'];
    document.getElementById('playerageupdate').value = players.find(p => p['playerId'] == id)['age'];
    document.getElementById('goalsinseasonupdate').value = players.find(p => p['playerId'] == id)['goalsInSeason'];
    document.getElementById('playerclubidupdate').value = players.find(p => p['playerId'] == id)['clubId'];
    document.getElementById('updateformdiv').style.display = 'flex';
    playerIdToUpdate = id;
}

function create() {
    let pname = document.getElementById('playername').value;
    let pposition = document.getElementById('playerposition').value;
    let psalary = document.getElementById('playersalary').value;
    let page = document.getElementById('playerage').value;
    let pgoals = document.getElementById('goalsinseason').value;
    let pclub = document.getElementById('playerclubid').value;

    const playerdata = { name: pname, positon: Number(pposition), salary: Number(psalary), age: Number(page), goalsInSeason: Number(pgoals), clubId: Number(pclub) };

    fetch('http://localhost:53910/player', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(playerdata)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let pname = document.getElementById('playernameupdate').value;
    let pposition = document.getElementById('playerpositionupdate').value;
    let psalary = document.getElementById('playersalaryupdate').value;
    let page = document.getElementById('playerageupdate').value;
    let pgoals = document.getElementById('goalsinseasonupdate').value;
    let pclub = document.getElementById('playerclubidupdate').value;

    const playerdata = { playerId: playerIdToUpdate, name: pname, positon: Number(pposition), salary: Number(psalary), age: Number(page), goalsInSeason: Number(pgoals), clubId: Number(pclub) };

    fetch('http://localhost:53910/player', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(playerdata)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:53910/player/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

//function convertEnumNumberToString(number) {
//    if (number == 0) {
//        return "Attacker";
//    }
//    else if (number == 1) {
//        return "Midfielder";
//    }
//    else if (number == 2) {
//        return "Defender";
//    }
//}

