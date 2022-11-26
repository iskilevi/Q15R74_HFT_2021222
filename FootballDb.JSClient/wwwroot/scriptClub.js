let clubs = [];

let connection = null;

let clubIdToUpdate = -1;

getdata();

setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ClubCreated", (user, message) => {
        getdata();
    });

    connection.on("ClubDeleted", (user, message) => {
        getdata();
    });

    connection.on("ClubUpdated", (user, message) => {
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
    await fetch('http://localhost:53910/club')
        .then(x => x.json())
        .then(y => {
            clubs = y;
            console.log(y);
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    clubs.forEach(c => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + c.clubId + "</td><td>"
            + c.name + "</td><td>"
            + c.nation + "</td><td>" +
            + c.managerId + "</td><td>" +
            + c.value + "</td><td>" +
            `<button type="button" onclick="remove(${c.clubId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${c.clubId})">Update</button>`
            + "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('clubnameupdate').value = clubs.find(p => p['clubId'] == id)['name'];
    document.getElementById('clubmanageridupdate').value = clubs.find(p => p['clubId'] == id)['managerId'];
    document.getElementById('clubnationupdate').value = clubs.find(p => p['clubId'] == id)['nation'];
    document.getElementById('clubvalueupdate').value = clubs.find(p => p['clubId'] == id)['value'];

    document.getElementById('updateformdiv').style.display = 'flex';
    clubIdToUpdate = id;
}

function create() {
    let cname = document.getElementById('clubname').value;
    let cmanager = document.getElementById('clubmanagerid').value;
    let cnation = document.getElementById('clubnation').value;
    let cvalue = document.getElementById('clubvalue').value;

    const clubdata = { name: cname, managerId: Number(cmanager), nation: cnation, value: Number(cvalue) };

    fetch('http://localhost:53910/club', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(clubdata)
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

    let cname = document.getElementById('clubnameupdate').value;
    let cmanager = document.getElementById('clubmanageridupdate').value;
    let cnation = document.getElementById('clubnationupdate').value;
    let cvalue = document.getElementById('clubvalueupdate').value;

    const clubdata = { clubId: clubIdToUpdate, name: cname, managerId: Number(cmanager), nation: cnation, value: Number(cvalue) };

    fetch('http://localhost:53910/club', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(clubdata)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:53910/club/' + id, {
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