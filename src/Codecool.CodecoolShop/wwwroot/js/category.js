InIt()

function InIt() {
    AddEventListenerToSelect()
}


function AddEventListenerToSelect() {
    const categorySelect = document.querySelector("#select-category")
    categorySelect.addEventListener('change', (event) => {
        console.log(event.currentTarget.value)
    })
}