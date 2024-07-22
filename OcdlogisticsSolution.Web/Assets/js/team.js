const buttons = document.querySelector("#buttons").children
const product = document.querySelector("#product-items").children

for(let i=0; i<buttons.length; i++){
    buttons[i].addEventListener("click", function(){
        for(let j=0; j<buttons.length; j++){
            buttons[j].classList.remove("active")
        }

        this.classList.add("active")
        const target = this.getAttribute("data-target")

        for(let k=0; k<product.length; k++){
            product[k].style.display = "none"

            if(product[k].getAttribute("data-id") == target){
                product[k].style.display = "block" 
            }

            if(target == "all"){
                product[k].style.display = "block" 
            }
        }
    })
}
// ourteam backgroud color change
function changeColor (color){
    $(".myContainer")[0].style.backgroundColor = color
}

changeColor("")