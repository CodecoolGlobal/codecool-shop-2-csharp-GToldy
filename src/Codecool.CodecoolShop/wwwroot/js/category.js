InIt()

function InIt() {
    AddEventListenerToSelect()
}


function AddEventListenerToSelect() {
    const cardContent = document.querySelector("#product-cards");
    const categorySelect = document.querySelector("#select-category");
    categorySelect.addEventListener('change', (event) => {
        console.log(event.currentTarget.value);
        ClearCardContainer(cardContent);
        
    })
}

function ClearCardContainer(cardContent) {
    const bodyContainer = document.querySelector(".pb-3")
    let newCardContainer = document.createElement("div")

    newCardContainer.classList.add("container")
    newCardContainer.id = "product-cards"

    cardContent.remove();
    bodyContainer.appendChild(newCardContainer)
}