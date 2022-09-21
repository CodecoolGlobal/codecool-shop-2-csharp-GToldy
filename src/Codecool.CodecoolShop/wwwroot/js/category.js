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
        let response = ApiGet("/CategoryApi?id=1");
        PopulateContainer(response);
    })
}

function ClearCardContainer(cardContent) {
    const bodyContainer = document.querySelector(".pb-3");
    let newCardContainer = document.createElement("div");

    newCardContainer.classList.add("container");
    newCardContainer.id = "product-cards";

    cardContent.remove();
    bodyContainer.appendChild(newCardContainer);
}

function PopulateContainer(response) {
    const cardContainer = document.querySelector("#product-cards");
    for (let i = 0; i < response.length; i++) {
        let card = document.createElement("div")
        let image = document.createElement("img")
        let cardBody = document.createElement("div")
        let cardName = document.createElement("h5")
        let name = document.createElement("p")
        let description = document.createElement("p")
        let category = document.createElement("p")
        let supplyer = document.createElement("p")
        let addToCartBtn = document.createElement("a")

        cardName.innerHTML = `Product ${i}`
        name.innerHTML = response[i].productCategory.name
        description.innerHTML = response[i].productCategory.description
        supplyer.innerHTML = response[i].supplyer
        name.innerHTML = "Add to cart"

        cardBody.appendChild(cardName)
        cardBody.appendChild(name)
        cardBody.appendChild(description)
        cardBody.appendChild(category)
        cardBody.appendChild(supplyer)
        cardBody.appendChild(addToCartBtn)

        card.appendChild(image)
        card.appendChild(cardBody)

        cardContainer.appendChild(card)
        

    }
}



async function ApiGet(url) {
    let response = await fetch(url);
    if (response.ok) {
        return response
    }
}