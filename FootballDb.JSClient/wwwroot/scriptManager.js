let managers = [];

let connection = null;

let managerIdToUpdate = -1;

getdata();

setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ManagerCreated", (user, message) => {
        getdata();
    });

    connection.on("ManagerDeleted", (user, message) => {
        getdata();
    });

    connection.on("ManagerUpdated", (user, message) => {
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
    await fetch('http://localhost:53910/manager')
        .then(x => x.json())
        .then(y => {
            managers = y;
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    managers.forEach(m => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + m.managerId + "</td><td>"
            + m.name + "</td><td>"
            + m.salary + "</td><td>" +
        `<button type="button" onclick="remove(${m.managerId})">Delete</button>` +
        `<button type="button" onclick="showupdate(${m.managerId})">Update</button>`
            + "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('managernameupdate').value = managers.find(m => m['managerId'] == id)['name'];
    document.getElementById('managersalaryupdate').value = managers.find(m => m['managerId'] == id)['salary'];

    document.getElementById('updateformdiv').style.display = 'flex';
    managerIdToUpdate = id;
}

function create() {
    let mname = document.getElementById('managername').value;
    let msalary = document.getElementById('managersalary').value;

    const managerData = { name: mname, salary: Number(msalary) };

    fetch('http://localhost:53910/manager', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(managerData)
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

    let mname = document.getElementById('managernameupdate').value;
    let msalary = document.getElementById('managersalaryupdate').value;

    const managerData = { managerId: managerIdToUpdate, name: mname, salary: Number(msalary) };

    fetch('http://localhost:53910/manager', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(managerData)
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:53910/manager/' + id, {
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