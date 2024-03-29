﻿const modal = document.querySelector("#modal");
const modalBody = document.querySelector("#modal__body");
const modalHead = document.querySelector("#modal__head");
const productContent = document.querySelector("#product-cards");
const categorySelect = document.querySelector("#select-category");
const supplierSelect = document.querySelector("#select-supplier");
const continueButton = document.querySelector("#continue-shopping");


InIt()

function InIt() {
    GetAllProducts()
    GetProductCategories()
    GetProductSuppliers()
    AddEventListenerToCategorySelect()
    AddEventListenerToSupplierSelect()
}


function AddEventListenerToSupplierSelect() {
    supplierSelect.addEventListener('change', (event) => {
        if (event.currentTarget.value == 0) {
            GetAllProducts()
        } else {
            GetSupplierProductsContent(event.currentTarget.value)
        }
    })
}

function GetSupplierDropdownMenuOptions(response) {
    for (let i = 0; i < response.length; i++) {
        console.log(response)
        let option = document.createElement("option")
        option.value = response[i].id
        option.innerHTML = response[i].name
        supplierSelect.appendChild(option)
    }
}

async function GetProductSuppliers() {
    let response = await ApiGet(`/SupplierApi/GetProductSuppliers`)
    GetSupplierDropdownMenuOptions(response)
}

async function GetSupplierProductsContent(id) {
    let response = await ApiGet(`/SupplierApi/GetProductBySupplier?id=${id}`);
    PopulateContainer(response);
}

function AddEventListenerToCategorySelect() {
    categorySelect.addEventListener('change', (event) => {
        if (event.currentTarget.value == 0) {
            GetAllProducts()
        } else {
            GetProductsCategoryContent(event.currentTarget.value)
        }
    });

    continueButton.addEventListener('click', () => {
        modal.classList.toggle("hidden");
        modal.classList.toggle("visible");
    });
}

async function GetProductsCategoryContent(id) {
    let response = await ApiGet(`/CategoryApi/GetProducts?id=${id}`);
    PopulateContainer(response);
}


function GetCategoryDropdownMenuOptions(response) {
    for (let i = 0; i < response.length; i++) {
        let option = document.createElement("option")
        option.value = response[i].id
        option.innerHTML = response[i].name
        categorySelect.appendChild(option)

    }
}

async function GetProductCategories() {
    let response = await ApiGet(`/CategoryApi/GetProductCategories`)
    GetCategoryDropdownMenuOptions(response)
}

async function GetAllProducts() {
    let response = await ApiGet(`/CategoryApi/GetAllProducts`)
    PopulateContainer(response)
}

function PopulateContainer(response) {
    productContent.innerHTML = "";
    for (let i = 0; i < response.length; i++) {
        let card = document.createElement("div")
        let imageContainer = document.createElement("div");
        let image = document.createElement("img");
        let cardBody = document.createElement("div");
        let name = document.createElement("h5");
        let description = document.createElement("p");
        let category = document.createElement("p");
        let supplier = document.createElement("p");
        let price = document.createElement("p");
        let addToCartBtn = document.createElement("a");
        let cardDescription = document.createElement("div");
        let productDetails = document.createElement("div");

        cardDescription.classList.add("card-description");
        productDetails.classList.add("product-details");

        //cardName.innerHTML = `Product ${i + 1}`;
        name.innerHTML = response[i].name;
        description.innerHTML = response[i].description;
        supplier.innerHTML = `Supplier: ${response[i].supplier.name}`;
        category.innerHTML = `Category: ${response[i].productCategory.name}`;
        price.innerHTML = `Price: ${response[i].price}`;
        addToCartBtn.innerHTML = "Add to cart";

        card.classList.add("product-card");

        imageContainer.classList.add("image-container");

        image.src = `img/${response[i].image}`
        imageContainer.appendChild(image);

        cardBody.classList.add("card-body")

        name.classList.add("card-name")

        description.classList.add("card-description")

        category.classList.add("card-category")

        price.classList.add("card-price")

        addToCartBtn.type = "button"
        addToCartBtn.style = "float:bottom"
        addToCartBtn.classList.add("btn")
        addToCartBtn.classList.add("btn-primary")
        addToCartBtn.classList.add("add-to-cart")
        addToCartBtn.dataset.productId = response[i].id
        addToCartBtn.addEventListener('click', async event => {
            let id = event.currentTarget.dataset.productId;
            AddToCart(id)
        })

        cardDescription.appendChild(name)
        cardDescription.appendChild(description)
        productDetails.appendChild(category)
        productDetails.appendChild(supplier)
        cardBody.appendChild(cardDescription);
        cardBody.appendChild(productDetails);
        cardBody.appendChild(addToCartBtn);

        card.appendChild(imageContainer)
        card.appendChild(cardBody)

        productContent.appendChild(card)
    }
}

async function AddToCart(id) {
    let result = await ApiGet(`/api/cart/Add/${id}`);
    PopulateModalBody(result);
}

function PopulateModalBody(response) {
    modalBody.innerHTML = "";
    modalHead.innerHTML = "Shopping cart";
    for (let item of response) {
        let bodyContent = document.createElement("div");
        bodyContent.classList.add("modal__body-content");
        let card = document.createElement("div");
        card.classList.add("modal__card");
        let imageBox = document.createElement("div");
        imageBox.classList.add("modal__card-image-box");
        let image = document.createElement("img");
        image.classList.add("modal__card-image");
        let details = document.createElement("div");
        details.classList.add("modal__card-details");
        let name = document.createElement("span");
        name.classList.add("modal__card-name");
        let counting = document.createElement("div");
        counting.classList.add("modal__card-counting");
        let countContainer = document.createElement("div");
        countContainer.classList.add("modal__card-count-container");
        let price = document.createElement("span");
        price.classList.add("modal__card-price");
        let quantity = document.createElement("span");
        quantity.classList.add("modal__card-quantity");
        let total = document.createElement("span");
        total.classList.add("modal__card-total");

        image.src = `img/${item.product.image}`;
        name.innerHTML = item.product.name;
        price.innerHTML = `Price: ${item.product.defaultPrice}`;
        quantity.innerHTML = `Qty: ${item.quantity}`;
        total.innerHTML = `Total: ${item.quantity * item.product.defaultPrice}`;

        countContainer.appendChild(price);
        countContainer.appendChild(quantity);
        counting.appendChild(countContainer);
        counting.appendChild(total);
        details.appendChild(name);
        details.appendChild(counting);
        imageBox.appendChild(image);
        card.appendChild(imageBox);
        card.appendChild(details);
        bodyContent.appendChild(card);
        modalBody.appendChild(bodyContent);
    }
    modal.classList.toggle('hidden');
    modal.classList.toggle('visible');
}

async function ApiGet(url) {
    let response = await fetch(url);
    if (response.ok) {
        return await response.json()
    }
}