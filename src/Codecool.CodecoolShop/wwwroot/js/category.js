const productContent = document.querySelector("#product-cards");

InIt()

function InIt() {
    GetProductsContent(1)
    AddEventListenerToSelect()
}




function AddEventListenerToSelect() {
    const categorySelect = document.querySelector("#select-category");
    categorySelect.addEventListener('change', (event) => {
        console.log(event.currentTarget.value);
        GetProductsContent(1)
        
    })
}

async function GetProductsContent(id) {
    let response = await ApiGet(`/CategoryApi?id=${id}`);
    console.log(response)
    PopulateContainer(response);
}

function PopulateContainer(response) {
    productContent.innerHTML = "";
    for (let i = 0; i < response.length; i++) {
        let card = document.createElement("div")
        let image = document.createElement("img")
        let cardBody = document.createElement("div")
        let cardName = document.createElement("h5")
        let name = document.createElement("h5")
        let description = document.createElement("p")
        let category = document.createElement("p")
        let supplier = document.createElement("p")
        let price = document.createElement("p")
        let addToCartBtn = document.createElement("a")

        cardName.innerHTML = `Product ${i+1}`
        name.innerHTML = response[i].name
        description.innerHTML = response[i].description
        supplier.innerHTML = `Supplier: ${response[i].supplier.name}`
        price.innerHTML = `Price: ${response[i].price}`
        addToCartBtn.innerHTML = "Add to cart"

        card.classList.add("col-lg-3")
        card.classList.add("col-lg-3")
        card.style = "display: inline-block; max-width: 350px; height: 350px;"

        image.src = `img/${response[i].name}.jpg`
        image.style = "height: 50%; width: 50%; align-self: center; padding-top: 10px;"

        cardBody.classList.add("card-body")

        cardName.classList.add("card-title")
        cardName.classList.add("text-center")

        name.classList.add("card-title")

        description.classList.add("card-text")

        category.classList.add("card-text")

        price.classList.add("card-text")
        price.classList.add("text-center")

        addToCartBtn.type = "button"
        addToCartBtn.style = "float:bottom"
        addToCartBtn.classList.add("btn")
        addToCartBtn.classList.add("btn-primary")

        cardBody.appendChild(cardName)
        cardBody.appendChild(name)
        cardBody.appendChild(description)
        cardBody.appendChild(category)
        cardBody.appendChild(supplier)
        cardBody.appendChild(addToCartBtn)

        card.appendChild(image)
        card.appendChild(cardBody)

        productContent.appendChild(card)
        

    }
}



async function ApiGet(url) {
    let response = await fetch(url);
    if (response.ok) {
        return await response.json()
    }
}