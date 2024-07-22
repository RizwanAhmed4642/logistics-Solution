// form activate method
function showForm(evt, formNumber) {
    var i, tabcontent, tablinks;
    // forms
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    // buttons
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(formNumber).style.display = "block";
    evt.currentTarget.className += " active";

}
// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultOpen").click();

$(".nav-tabs li").click(changeHeader)
function changeHeader(e) {
    if (e.target.id == "defaultOpen") {
        $(".sub-title h2")[0].innerText = $(`#${e.target.id}`).attr("contact");
        $(".sub-title h5")[0].innerText = $(`#${e.target.id}`).attr("heading");
        $(".title-sec h3")[0].innerText = $(`#${e.target.id}`).attr("sub-heading");
        $(".sub-title a").remove();
        $(".sub-title").append(`<a class="tel" href="http://"><i class="fa fa-phone"></i> ${$(`#${e.target.id}`).attr("tellephone")}</a>`);

    } else if (e.target.id == "tab-6") {
        $(".sub-title h2")[0].innerText = $(`#${e.target.id}`).attr("contact");
        $(".sub-title h5")[0].innerText = $(`#${e.target.id}`).attr("heading");
        $(".title-sec h3")[0].innerText = $(`#${e.target.id}`).attr("sub-heading");
        $(".sub-title a").remove();
        // $(".sub-title h5").remove();
        $(".sub-title").append(
            `<div>
<a href="https://www.google.com/maps/place/14135+128+Ave+NW,+Edmonton,+AB+T5L+3H3,+Canada/@53.5845425,-113.5671893,17z/data=!3m1!4b1!4m5!3m4!1s0x53a023f88a514fc5:0xd2df6924d3a61721!8m2!3d53.5845393!4d-113.5650006"
target="_blank"
>
<p class="m-2 active_golden">${$(`#${e.target.id}`).attr("address")}</p>

</a>
</div>`
        );

    } else {
        $(".sub-title h2")[0].innerText = $(`#${e.target.id}`).attr("contact");
        $(".sub-title h5")[0].innerText = $(`#${e.target.id}`).attr("heading");
        $(".title-sec h3")[0].innerText = $(`#${e.target.id}`).attr("sub-heading");
        $(".sub-title a").remove();
        $(".sub-title").append(`<a class="tel" href="http://"> ${$(`#${e.target.id}`).attr("mail")}</a>`);
    }

}

// mobile view form active

$('#optns').change(headChanger)

function headChanger(e) {
    if (e.target.selectedOptions[0].id == "tab-1") {
        $(".sub-title h2")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("contact");
        $(".sub-title h5")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("heading");
        $(".title-sec h3")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("sub-heading");
        $(".sub-title a").remove();
        $(".sub-title p").remove();
        $(".sub-title").append(`<a class="tel" href="http://"><i class="fa fa-phone"></i> ${$(`#${e.target.selectedOptions[0].id}`).attr("tellephone")}</a>`);

    } else if (e.target.selectedOptions[0].id == "tab-6") {
        $(".sub-title h2")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("contact");
        $(".sub-title h5")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("heading");
        $(".title-sec h3")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("sub-heading");
        $(".sub-title a").remove();
        // $(".sub-title p").remove();
        $(".sub-title").append(
            `<div>
<a href="https://www.google.com/maps/place/14135+128+Ave+NW,+Edmonton,+AB+T5L+3H3,+Canada/@53.5845425,-113.5671893,17z/data=!3m1!4b1!4m5!3m4!1s0x53a023f88a514fc5:0xd2df6924d3a61721!8m2!3d53.5845393!4d-113.5650006"
target="_blank"
>
<p class="m-2 white"> ${$(`#${e.target.selectedOptions[0].id}`).attr("address")}</p>

</a>
</div>`
        );

    } else {
        $(".sub-title h2")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("contact");
        $(".sub-title h5")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("heading");
        $(".title-sec h3")[0].innerText = $(`#${e.target.selectedOptions[0].id}`).attr("sub-heading");
        $(".sub-title a").remove();
        $(".sub-title p").remove();
        $(".sub-title").append(`<a class="tel" href="http://"><i "fa fa-email"></i> ${$(`#${e.target.selectedOptions[0].id}`).attr("mail")}</a>`);
    }

}

$('#optns').change(changeForm)

function changeForm(e) {
    var i, tabcontent, tablinks;
    // forms
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    // buttons
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    if (e.target.selectedOptions[0].id == "tab-1") {
        document.getElementById("form-1").style.display = "block"
        e.target.className += " active";

    } else if (e.target.selectedOptions[0].id == "tab-2") {
        document.getElementById("form-2").style.display = "block"
        e.target.className += " active";

    } else if (e.target.selectedOptions[0].id == "tab-3") {
        document.getElementById("form-3").style.display = "block"
        e.target.className += " active";

    } else if (e.target.selectedOptions[0].id == "tab-4") {
        document.getElementById("form-4").style.display = "block"
        e.target.className += " active";

    } else if (e.target.selectedOptions[0].id == "tab-5") {
        document.getElementById("form-5").style.display = "block"
        e.target.className += " active";

    } else if (e.target.selectedOptions[0].id == "tab-6") {
        document.getElementById("form-6").style.display = "block"
        e.target.className += " active";

    }
}

// function changer (e){
// // Change background image of a div by clicking on the button using js
// e.target.style.backgroundImage = "url('../images/monday_attachments/4.png')";
// }
// $(".hero").click( changer)