
let players = [];

getdata();

async function getdata() {
    await fetch('http://localhost:53910/player')
        .then(x => x.json())
        .then(y => {
            players = y;
            display();
        });
}


function display() {
    players.forEach(p => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + p.playerId + "</td><td>"
            + p.name + "</td><td>" +
            + p.positon + "</td><td>" +
            + p.salary + "</td><td>" +
            + p.age + "</td><td>" +
            + p.goalsInSeason + "</td><td>" +
            + p.clubId + "</td><td>" +
            `<button type="button" onclick="remove(${p.playerId})">Delete</button>`
            + "</td></tr>";
    });
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

