fetch('http://localhost:53910/player')
    .then(x => x.json())
    .then(y => console.log(y));