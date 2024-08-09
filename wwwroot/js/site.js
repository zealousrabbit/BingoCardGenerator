function generateCards() {
    const dimension = parseInt(document.getElementById('dimension').value);
    const phrases = document.getElementById('phrases').value.split(',').map(p => p.trim());
    const freeSpace = document.getElementById('freeSpace').checked;
    const numberOfCards = parseInt(document.getElementById('numberOfCards').value);

    if (dimension < 1 || numberOfCards < 1 || phrases.length < dimension * dimension) {
        alert('Please provide valid inputs.');
        return;
    }

    const cardsContainer = document.getElementById('cardsContainer');
    cardsContainer.innerHTML = '';

    fetch('/Index?handler=Generate', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            Dimension: dimension,
            Phrases: phrases,
            IncludeFreeSpace: freeSpace,
            NumberOfCards: numberOfCards
        })
    })
        .then(response => response.json())
        .then(cards => {
            cards.forEach(card => {
                const cardElement = document.createElement('div');
                cardElement.className = 'card';
                cardElement.style.gridTemplateColumns = `repeat(${dimension}, 1fr)`;
                cardElement.style.gridTemplateRows = `repeat(${dimension}, 1fr)`;

                card.forEach(row => {
                    row.forEach(cell => {
                        const cellElement = document.createElement('div');
                        cellElement.textContent = cell;
                        if (cell === 'FREE SPACE') {
                            cellElement.classList.add('freeSpace');
                        }
                        cardElement.appendChild(cellElement);
                    });
                });

                cardsContainer.appendChild(cardElement);
            });
        });
}