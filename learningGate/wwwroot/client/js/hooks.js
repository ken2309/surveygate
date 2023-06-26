async function addFavorite(productId) {
    var usernameEl = document.getElementById("username");
    if (usernameEl == null) {
        window.location.href = "/Account/Login";
        //var username=usernameEl.innerText;
        //  if(username.length<1){
        //      window.location.href="/Account/Login";
        //  }
    }
    try {
        var response = await fetch(`/Favorite/AddItem?productId=${productId}`);
        console.log(response);
        if (response.status == 200) {
            var result = await response.json();
            // var cartCountEl = document.getElementById("cartCount");
            // cartCountEl.innerHTML = result;
            // window.location.href = "#cartCount";
        }
    } catch (err) {
        console.log(err);
    }
}

async function add(productId) {
    var usernameEl = document.getElementById("username");
    if (usernameEl == null) {
        window.location.href = "/Account/Login";
        //var username=usernameEl.innerText;
        //  if(username.length<1){
        //      window.location.href="/Account/Login";
        //  }
    }
    try {
        var response = await fetch(`/Cart/AddItem?productId=${productId}`);
        if (response.status == 200) {
            var result = await response.json();
            // var cartCountEl = document.getElementById("cartCount");
            // cartCountEl.innerHTML = result;
            window.location.href = "#cartCount";
        }
    } catch (err) {
        console.log(err);
    }
}