// about page
function parallaxImg (e){
    // Change background image of a div by clicking on the button using js
	e.target.style.backgroundImage = "url('../images/monday_attachments/4.png')";
}
$(".parallax-window").click(parallaxImg)

// partner page
$(document).ready(function() { 
    // Change background image of a div by clicking on the button using jquery
    $(".myContainer").click(function() { 
        var imageUrl =  "url('../images/4k.jpg')"; 
        $(".myContainer").css("background-image", imageUrl); 
    }); 
});
