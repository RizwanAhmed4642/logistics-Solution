$(document).ready(function(){

var current_fs, next_fs, previous_fs; //fieldsets
var opacity;
var current = 1;
var steps = $("fieldset").length;

setProgressBar(current);

$(".next").click(function(){

current_fs = $(this).parent();
next_fs = $(this).parent().next();

//Add Class Active
$("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

//show the next fieldset
next_fs.show();
//hide the current fieldset with style
current_fs.animate({opacity: 0}, {
step: function(now) {
// for making fielset appear animation
opacity = 1 - now;

current_fs.css({
'display': 'none',
'position': 'relative'
});
next_fs.css({'opacity': opacity});
},
duration: 500
});
setProgressBar(++current);
});

$(".previous").click(function(){

current_fs = $(this).parent();
previous_fs = $(this).parent().prev();

//Remove class active
$("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

//show the previous fieldset
previous_fs.show();

//hide the current fieldset with style
current_fs.animate({opacity: 0}, {
step: function(now) {
// for making fielset appear animation
opacity = 1 - now;

current_fs.css({
'display': 'none',
'position': 'relative'
});
previous_fs.css({'opacity': opacity});
},
duration: 500
});
setProgressBar(--current);
});

function setProgressBar(curStep){
var percent = parseFloat(100 / steps) * curStep;
percent = percent.toFixed();
$(".progress-bar")
.css("width",percent+"%")
}

$(".submit").click(function(){
return false;
})






// starting mint index js

	new WOW().init();

	$('.main-name-silder').slick({
		  centerMode: true,
		  centerPadding: '60px',
		  slidesToShow: 3,
		   autoplay: false,
		   arrows: false,
		   autoplaySpeed:1000,
		  responsive: [
			{
			  breakpoint: 767,
			  settings: {
				arrows: true,
				autoplay: true,
				centerMode: true,
				centerPadding: '0px',
				slidesToShow: 1
			  }
			},
			{
			  breakpoint: 575,
			  settings: {
				arrows: true,
				autoplay: true,
				centerMode: true,
				centerPadding: '0px',
				slidesToShow: 1
			  }
			}
		  ]
	});

	// brand slider 
	$('.brand-slider-sec').slick({
		dots: false,
		infinite: true,
		centerPadding: '60px',
		speed: 300,
		autoplay: true,
		   arrows: true,
		slidesToShow: 6,
		slidesToScroll: 1,
		responsive: [
			{
				breakpoint: 1199,
				settings: {
				  slidesToShow: 5,
				  slidesToScroll: 1,
				  infinite: true
				 
				}
			  },
		  {
			breakpoint: 991,
			settings: {
			  slidesToShow: 4,
			  slidesToScroll: 1,
			  infinite: true
			 
			}
		  },
		  {
			breakpoint: 767,
			settings: {
			  slidesToShow: 3,
			  slidesToScroll: 1,
			  infinite: true
			}
		  },
		  {
			breakpoint: 575,
			settings: {
			  slidesToShow: 2,
			  slidesToScroll: 1,
			  infinite: true
			}
		  },
		  {
			breakpoint: 420,
			settings: {
			  slidesToShow: 1,
			  slidesToScroll: 1,
			  infinite: true
			}
		  }
		]
	  });


	  // product slider 
	$('.tab-pro-slider').slick({
		dots: false,
		infinite: true,
		centerPadding: '60px',
		speed: 300,
		autoplay: true,
		   arrows: true,
		slidesToShow: 4,
		slidesToScroll: 1,
		responsive: [
			{
				breakpoint: 1199,
				settings: {
				  slidesToShow: 3,
				  slidesToScroll: 1,
				  infinite: true
				 
				}
			  },
		  {
			breakpoint: 991,
			settings: {
			  slidesToShow: 2,
			  slidesToScroll: 1,
			  infinite: true
			 
			}
		  },
		  {
			breakpoint: 767,
			settings: {
			  slidesToShow: 3,
			  slidesToScroll: 1,
			  infinite: true
			}
		  },
		  {
			breakpoint: 575,
			settings: {
			  slidesToShow: 2,
			  slidesToScroll: 1,
			  infinite: true
			}
		  },
		  {
			breakpoint: 359,
			settings: {
			  slidesToShow: 1,
			  slidesToScroll: 1,
			  infinite: true
			}
		  }
		]
	  });


    $(".fixed-caret").click(function() {
        $("html, body").animate({ 
            scrollTop: 0 
        }, "slow");
        return false;
    });


	$('.flag-image').on('click', function() {
		$('.flag-drop').toggle();
	});
	$('.flag-f > img').on('click', function() {
		$('.flag-drop').toggle();
		var flagname = $(this).attr('src');
		$('.flag-image').html('<img src="'+flagname+'" class="flag-s">');
	});
	$(document).on('click', function() {
		$('.flag-drop').hide();
	});
	$('.main-flag').on('click', function() {
		event.stopPropagation();
	});
    if($(window).width() < 768){
        $('.foot_links_heading').on('click', function() {
        $('.foot_links_heading').parent().addClass('foot-col').removeClass('this-show');
        $(this).parent().removeClass('foot-col').addClass('this-show');
        $('.foot-col .footer-links-c').removeClass('showw');
        $('.foot-col .foot_links_heading').removeClass('fot-min');
	    $(this).parent('.this-show').children('.footer-links-c').toggleClass('showw');
        $(this).toggleClass('fot-min');
      
	});




    }

           
	// $('.curncy select').on('change', function() {
	// 	var flagname = $(this).val();
	// 	$('.flag-image').html('<img src="images/img/flag/'+flagname+'.png" class="flag-s">')
	// });

	$('.cusdrop').on('click', function(event){
		var events = $._data(document, 'events') || {};
		events = events.click || [];
		for(var i = 0; i < events.length; i++) {
			if(events[i].selector) {
			//Check if the clicked element matches the event selector
			if($(event.target).is(events[i].selector)) {
				events[i].handler.call(event.target, event);
			}
			// Check if any of the clicked element parents matches the 
			// delegated event selector (Emulating propagation)
			$(event.target).parents(events[i].selector).each(function(){
				events[i].handler.call(this, event);
			});
			}
		}
		event.stopPropagation(); //Always stop propagation
	});

	$(".links-drop").click(function(event){
		event.preventDefault();
	});


$(document).click(function (e) {
	e.stopPropagation();
	var container = $(".navbar-toggler");
	$('.navbar-collapse').show();

	//check if the clicked area is dropDown or not
	if (container.has(e.target).length === 0) {
		$('.navbar-collapse').hide();
		$('.navbar-collapse').removeClass('show');
	}
});

$('.responsive-slider-cus .carousel').carousel({
	interval: 2000,
})

$('.responsive-slider-cus .carousel .carousel-item').each(function() {
    var minPerSlide = 3;
    var next = $(this).next();
    if (!next.length) {
        next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));

    for (var i = 0; i < minPerSlide; i++) {
        next = next.next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }

        next.children(':first-child').clone().appendTo($(this));
    }
});


$('.responsive-slider-cus-2 .carousel').carousel({
    interval: 2000
})

$('.responsive-slider-cus-2 .carousel .carousel-item').each(function() {
    var minPerSlide = 2;
    var next = $(this).next();
    if (!next.length) {
        next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));

    for (var i = 0; i < minPerSlide; i++) {
        next = next.next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }

        next.children(':first-child').clone().appendTo($(this));
    }
});



$('.responsive-slider-cus-3 .carousel').carousel({
	interval: 2000,
})

$('.responsive-slider-cus-3 .carousel .carousel-item').each(function() {
    var minPerSlide = 2;
    var next = $(this).next();
    if (!next.length) {
        next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));

    for (var i = 0; i < minPerSlide; i++) {
        next = next.next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }

        next.children(':first-child').clone().appendTo($(this));
    }
});

$('.responsive-slider-cus-4 .carousel').carousel({
	interval: 2000,
})

$('.responsive-slider-cus-4 .carousel .carousel-item').each(function() {
    var minPerSlide = 2;
    var next = $(this).next();
    if (!next.length) {
        next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));

    for (var i = 0; i < minPerSlide; i++) {
        next = next.next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }

        next.children(':first-child').clone().appendTo($(this));
    }
});


// review slider
$('.review-slider').slick({
	centerMode: true,
	dots: false,
	infinite: true,
	autoplay: true,
	arrows: true,
	centerPadding: '0px',
	slidesToShow: 3,
	responsive: [
	  {
		breakpoint: 767,
		settings: {
		  arrows: false,
		  centerMode: true,
		  centerPadding: '0px',
		  slidesToShow: 1
		}
	  },
	  {
		breakpoint: 480,
		settings: {
		  arrows: false,
		  centerMode: true,
		  centerPadding: '40px',
		  slidesToShow: 1
		}
	  }
	]
  });

  $('.slider-image-gallery').slick({
	slidesToShow: 1,
	slidesToScroll: 1,
	arrows: true,
	fade: true,
	asNavFor: '.image-gallery-small'
  });
  $('.image-gallery-small').slick({
	slidesToShow: 2,
	slidesToScroll: 1,
	asNavFor: '.slider-image-gallery',
	dots: false,
	centerMode: true,
	focusOnSelect: true
  });
  $('.js-pscroll').each(function(){
	$(this).css('position','relative');
	$(this).css('overflow','hidden');
	var ps = new PerfectScrollbar(this, {
		wheelSpeed: 1,
		scrollingThreshold: 1000,
		wheelPropagation: false,
	});

	$(window).on('resize', function(){
		ps.update();
	})
});
$('.js-addwish-b2').on('click', function(e){
	e.preventDefault();
});

$('.js-addwish-b2').each(function(){
	var nameProduct = $(this).parent().parent().find('.js-name-b2').html();
	$(this).on('click', function(){
		swal(nameProduct, "is added to Cart !", "success");

		$(this).addClass('js-addedwish-b2');
		$(this).off('click');
	});
});

$('.js-addwish-detail').each(function(){
	var nameProduct = $(this).parent().parent().parent().find('.js-name-detail').html();

	$(this).on('click', function(){
		swal(nameProduct, "is added to Cart !", "success");

		$(this).addClass('js-addedwish-detail');
		$(this).off('click');
	});
});

/*---------------------------------------------*/

$('.js-addcart-detail').each(function(){
	var nameProduct = $(this).parent().parent().parent().parent().find('.js-name-detail').html();
	$(this).on('click', function(){
		swal(nameProduct, "is added to cart !", "success");
	});
});
$('.gallery-lb').each(function() { // the containers for all your galleries
	$(this).magnificPopup({
		delegate: 'a', // the selector for gallery item
		type: 'image',
		gallery: {
			enabled:true
		},
		mainClass: 'mfp-fade'
	});
});
$('.parallax100').parallax100();	
$(".js-select2").each(function(){
	$(this).select2({
		minimumResultsForSearch: 20,
		dropdownParent: $(this).next('.dropDownSelect2')
	});
});
var quantitiy=0;
   $('.quantity-right-plus').click(function(e){
        
        // Stop acting like a button
        e.preventDefault();
        // Get the field name
        var quantity = parseInt($(this).parent().parent().children('.quantity').val());
        
        // If is not undefined
            
		$(this).parent().parent().children('.quantity').val(quantity + 1);

          
			// Increment
		});

		$('.quantity-left-minus').click(function(e){
		   // Stop acting like a button
		   e.preventDefault();
		   // Get the field name
		   var quantity = parseInt($(this).parent().parent().children('.quantity').val());
		   
		   // If is not undefined
		 
			// Increment
			if(quantity>1){
			$(this).parent().parent().children('.quantity').val(quantity - 1);
			}
	   });

	   $('.click-social-icon').click(function(){
		$('.extra-social-icon').toggle();
	   });
	   $(document).click(function () {
		$('.extra-social-icon').hide();
	   });
	   $('.click-social-icon,.extra-social-icon').click(function (e) {
		e.stopPropagation();
	});
});



		$('.cust-radio').on('click', function(){  
			$('.collapse').hide();
			$(this).parent().parent().children('.collapse').toggle();
		
			$('.card-header, .custom-radio').addClass('opacity1');
			$('.card-header, .custom-radio').removeClass('opacity2');
			$('.card-header').removeClass('boder-card');
			$(this).parent().parent().children('.card-header, .custom-radio').addClass('opacity2');
			$(this).parent().parent().children('.card-header').addClass('boder-card');
		  });
		  $('#return-checkout').click(function() {
			location.reload();
		});







	$('.main-click-accordion').click(function() {
		 $('.double-acordion-question').addClass('otherhide');
		$(this).parent().find('.double-acordion-question').toggleClass('onlythis otherhide');
});



// mehrose

$( function() {
    $( "#accordion" ).accordion({
		collapsible: true, 
		active: false
	});
});

// education page
$('.edu-slider').slick({
    autoplay: true,
    dots: true,
    infinite: true,
    speed: 300,
    slidesToShow: 6,
    slidesToScroll: 1,
    responsive: [
		{
			breakpoint: 1024,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 3,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 600,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2
			}
		},
		{
			breakpoint: 480,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1
			}
		}
    // You can unslick at a given breakpoint now by adding:
    // settings: "unslick"
    // instead of a settings object
	]
	 
});
// faq
$("#accordionFaq").on("hide.bs.collapse show.bs.collapse", e => {
	$(e.target)
	  .prev()
	  .find("i:last-child")
	  .toggleClass("fa-minus fa-plus");
});
// footer
$("#accordionFooter").on("hide.bs.collapse show.bs.collapse", e => {
	$(e.target)
	  .prev()
	  .find("i:last-child")
	  .toggleClass("fa-angle-up fa-angle-down");
});
  
// why choose us counter
$("#circleOne").each(function() {
    var $this = $(this),
		$dataV = $this.data("percent"),
		$dataDeg = $dataV * 3.6,
		$round = $this.find(".round_per");
	$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");	
	$this.append(
		`<div class="circle_inbox">
			<span class="percent_text">${""}</span>
		</div>`
	);
	$this.prop('Counter', 0).animate({Counter: $dataV},{
		duration: 2000, 
		easing: 'swing', 
		step: function (now) {
            $this.find(".percent_text").text(Math.ceil(now)+"");
        }
    });
	if($dataV >= 51){
		$round.css("transform", "rotate(" + 360 + "deg)");
		setTimeout(function(){
			$this.addClass("percent_more");
		},1000);
		setTimeout(function(){
			$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");
		},1000);
	} 
});

$("#circleTwo").each(function() {
    var $this = $(this),
		$dataV = $this.data("percent"),
		$dataDeg = $dataV * 3.6,
		$round = $this.find(".round_per");
	$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");	
	$this.append(
		`<div class="circle_inbox">
			<span class="percent_text">${""}</span>
		</div>`
	);
	$this.prop('Counter', 0).animate({Counter: $dataV},{
		duration: 2000, 
		easing: 'swing', 
		step: function (now) {
            $this.find(".percent_text").text(Math.ceil(now)+"");
        }
    });
	if($dataV >= 51){
		$round.css("transform", "rotate(" + 360 + "deg)");
		setTimeout(function(){
			$this.addClass("percent_ship");
		},1000);
		setTimeout(function(){
			$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");
		},1000);
	} 
});
$("#circleThree").each(function() {
    var $this = $(this),
		$dataV = $this.data("percent"),
		$dataDeg = $dataV * 3.6,
		$round = $this.find(".round_per");
	$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");	
	$this.append(
		`<div class="circle_inbox">
			<span class="percent_text">${""}</span>
		</div>`
	);
	$this.prop('Counter', 0).animate({Counter: $dataV},{
		duration: 2000, 
		easing: 'swing', 
		step: function (now) {
            $this.find(".percent_text").text(Math.ceil(now)+"");
        }
    });
	if($dataV >= 51){
		$round.css("transform", "rotate(" + 360 + "deg)");
		setTimeout(function(){
			$this.addClass("percent_tsale");
		},1000);
		setTimeout(function(){
			$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");
		},1000);
	} 
});
$("#circleFour").each(function() {
    var $this = $(this),
		$dataV = $this.data("percent"),
		$dataDeg = $dataV * 3.6,
		$round = $this.find(".round_per");
	$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");	
	$this.append(
		`<div class="circle_inbox">
			<span class="percent_text">${""}</span>
		</div>`
	);
	$this.prop('Counter', 0).animate({Counter: $dataV},{
		duration: 2000, 
		easing: 'swing', 
		step: function (now) {
            $this.find(".percent_text").text(Math.ceil(now)+"");
        }
    });
	if($dataV >= 51){
		$round.css("transform", "rotate(" + 360 + "deg)");
		setTimeout(function(){
			$this.addClass("percent_shipped");
		},1000);
		setTimeout(function(){
			$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");
		},1000);
	} 
});
$("#circleFive").each(function() {
    var $this = $(this),
		$dataV = $this.data("percent"),
		$dataDeg = $dataV * 3.6,
		$round = $this.find(".round_per");
	$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");	
	$this.append(
		`<div class="circle_inbox">
			<span class="percent_text">${""}</span>
		</div>`
	);
	$this.prop('Counter', 0).animate({Counter: $dataV},{
		duration: 2000, 
		easing: 'swing', 
		step: function (now) {
            $this.find(".percent_text").text(Math.ceil(now)+"");
        }
    });
	if($dataV >= 51){
		$round.css("transform", "rotate(" + 360 + "deg)");
		setTimeout(function(){
			$this.addClass("percent_pending");
		},1000);
		setTimeout(function(){
			$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");
		},1000);
	} 
});
$("#circleSix").each(function() {
    var $this = $(this),
		$dataV = $this.data("percent"),
		$dataDeg = $dataV * 3.6,
		$round = $this.find(".round_per");
	$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");	
	$this.append(
		`<div class="circle_inbox">
			<span class="percent_text">${""}</span>
		</div>`
	);
	$this.prop('Counter', 0).animate({Counter: $dataV},{
		duration: 2000, 
		easing: 'swing', 
		step: function (now) {
            $this.find(".percent_text").text(Math.ceil(now)+"");
        }
    });
	if($dataV >= 51){
		$round.css("transform", "rotate(" + 360 + "deg)");
		setTimeout(function(){
			$this.addClass("percent_customer");
		},1000);
		setTimeout(function(){
			$round.css("transform", "rotate(" + parseInt($dataDeg + 180) + "deg)");
		},1000);
	} 
});
// counting value
// $('.count').each(function () {
//     $(this).prop('Counter',0).animate({
//         Counter: $(this).text()
//     }, {
//         duration: 1500,
//         easing: 'linear',
//         step: function (now) {
//             $(this).text(Math.ceil(now));
//         }
//     });
// });