const cartBudge = document.querySelector('.cart-icon-with-budge .badge');
const cartIncreaseBtns = document.querySelectorAll('.cart-increase-btn');
const cartDecreaseBtns = document.querySelectorAll('.cart-decrease-btn');

cartBudge.textContent = sessionStorage.getItem('cartCount') || '';
document.querySelectorAll('.card.product .btn').forEach(el => {
    el.addEventListener('click', async (e) => {
        e.preventDefault();

        let reqType = el.classList.contains('cart-add-btn')? 'add' :'remove';
        let res = await fetch(`/cart/${reqType}`, {
            method: 'POST',
            body: JSON.stringify(+el.dataset.id),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(data => data.json());
        
        if (res) {
            let cartCount = sessionStorage.getItem('cartCount');
            if (reqType == 'add') {
                sessionStorage.setItem('cartCount', +cartCount + 1);
                el.textContent = 'Убрать из корзины';
                el.classList.replace('cart-add-btn', 'cart-remove-btn');
            } else {
                el.textContent = 'Добавить в корзину'
                sessionStorage.setItem('cartCount', cartCount - 1);
                el.classList.replace('cart-remove-btn', 'cart-add-btn');
            }
            cartBudge.textContent = sessionStorage.getItem('cartCount') || '';
        } 
    });
});

cartIncreaseBtns.forEach(el => {
    el.addEventListener('click', async (e) => {
        e.preventDefault();
        let res = await fetch('/cart/add', {
            method: 'POST',
            body: JSON.stringify(+el.dataset.id),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(data => data.json());
       
        if (res) {
            let value = document.querySelector('.' + el.dataset.for);
            value.textContent = +value.textContent + 1;         
        }
    });
});



cartDecreaseBtns.forEach(el => {
    el.addEventListener('click', async (e) => {
        e.preventDefault();
        let res = await fetch('/cart/decrease', {
            method: 'POST',
            body: JSON.stringify(+el.dataset.id),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(data => data.json());
   
        if (res) {
            let value = document.querySelector('.' + el.dataset.for);
            value.textContent = value.textContent - 1;          
        }
    });
});



document.querySelectorAll('.remove-cart-item-btn').forEach(el => {
    el.addEventListener('click', async (e) => {
        e.preventDefault();
        let res = await fetch('/cart/remove', {
            method: 'POST',
            body: JSON.stringify(+el.dataset.id),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(data => data.json());

        if (res) {
            let cartCount = sessionStorage.getItem('cartCount');
            document.querySelector('.' + el.dataset.for).remove();
            sessionStorage.setItem('cartCount', +cartCount == 1 ? '' : cartCount - 1);
            cartBudge.textContent = sessionStorage.getItem('cartCount') || '';
            if(cartCount == 1) document.querySelector('.orderItems').innerHTML = `<div class="empty-cart">Корзина пуста</div>`;
        }
    });
});


document.querySelectorAll('.cancel-order-btn').forEach(el => {
    el.addEventListener('click', async (e) => {
        e.preventDefault();
        let res = await fetch('/order/cancel', {
            method: 'Delete',
            body: JSON.stringify(+el.dataset.id),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(data => data.json());

        if (res) {
            document.querySelector(`[data-id="${el.dataset.id}"]`).remove();
        }
    });
});

document.querySelectorAll('.update-product-count-btn').forEach(el => {
    el.addEventListener('click', async (e) => {
        e.preventDefault();
        let productCountValue = document.querySelector(`.product-count-value[data-id="${el.dataset.id}"]`);
        let count = productCountValue.value;
        if (count < 0) return;

        await fetch(`/admin/updateProduct?id=${encodeURIComponent(el.dataset.id)}&count=${encodeURIComponent(count)}`, {
            method: 'Put',           
            headers: {
                'Content-Type': 'application/json'
            }
        });
        
    });
});


document.querySelector('.clear-cart-btn').addEventListener('click', async (e) => {
    e.preventDefault();
    await fetch('/cart/clear', {
        method: 'POST',
    });
    document.querySelector('.orderItems').innerHTML = `<div class="empty-cart">Корзина пуста</div>`;
    sessionStorage.setItem('cartCount', '');
    cartBudge.textContent = sessionStorage.getItem('cartCount') || '';

});