let cart = {};
let list = document.getElementById('items-list');

function renderThePage() {
    sum = 0;
    list.innerHTML = '';
    for (let plantInCart of Object.values(JSON.parse(localStorage.getItem('cart')))) {
        let div = document.createElement('div');
        div.className = 'item space-evently';
        text = `<div class="picture">
                <img class="plant" src="` + plantInCart.path + `">
            </div>
            <div class="description">
                <h1 class="display">` + plantInCart.name + `</h1>
                <p class="description-text">` + plantInCart.description + `, ` + plantInCart.price + `</p>
            </div>
            <div class="cart-buttons">
                <button class="cart-btn plus" data-id="` + plantInCart.id + `">+</button>
                <div class="cart-count">` + plantInCart.count + `</div>
                <button class="cart-btn minus" data-id="` + plantInCart.id + `">-</button>
            </div>`

        div.innerHTML += text;
        list.appendChild(div);
        sum += plantInCart.count * parseFloat(plantInCart.price, 10);
    }

    let totalPrice = document.getElementById('total-price');
    totalPrice.innerHTML = '';
    totalPrice.insertAdjacentHTML('beforeend', "Total price: " + sum.toFixed(2));
}

function plusItem(id) {
    cart[id]['count']++;
    localStorage.setItem('cart', JSON.stringify(cart));
    renderThePage();
}

function minusItem(id) {
    if (cart[id]['count'] - 1 < 1) {
        delete cart[id];
        localStorage.setItem('cart', JSON.stringify(cart));
        renderThePage();
        return true;
    }

    cart[id]['count']--;
    localStorage.setItem('cart', JSON.stringify(cart));
    renderThePage();
}

if ('cart' in localStorage) {
    array = Object.values(JSON.parse(localStorage.getItem('cart')));
    for (let i = 0; i < array.length; i++)
        cart[array[i]['id']] = array[i];

    renderThePage();
}

list.addEventListener('click', function (e) {
    if (e.target.classList.contains('plus'))
        plusItem(e.target.dataset['id']);
    if (e.target.classList.contains('minus'))
        minusItem(e.target.dataset['id']);
});
