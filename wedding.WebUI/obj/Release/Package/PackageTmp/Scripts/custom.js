$(document).ready(function () {
    $(".accordionContent").hide();

    $(".showHide").each(function () {
        var hideEl = $(this).attr('data-hide');
        var showEl = $(this).attr('data-show');
        $('#' + showEl).hide();
        $('#' + hideEl).hide();

        if ($(this).checked == 'checked') {
            $('#' + showEl).show();
        }
    });

    $(".dashboardTile").hide();
    $(".dashboardTile.first").show(); 
    $(".dashboardItem.first").addClass('dashboardItemBorder');

    // set values for sticky navigation across devices
    var stickyNavTop = 0;
    var navTopSet = false;

    var tabStickyNavTop = 0;
    var tabNavTopSet = false;

    var stickyMobileNavTop = 0;
    var mobileNavTopSet = false;

    // Sticky Navigation 

    if ($("#navBar").css("display") == "block") {
        stickyNavTop = $('#navBar').offset().top;
        navTopSet = true;
    }

    if ($("#mobile-navBar").css("display") == "block") {
        stickyMobileNavTop = $('#mobile-navBar').offset().top;
        mobileNavTopSet = true;
    }
    var stickyNav = function (orientation) {
        if ($("#navBar").length) {
            if (!navTopSet && orientation == "desk") {
                stickyNavTop = $('#navBar').offset().top;
                navTopSet = true;
                tabNavTopSet = false;
            } else if (!tabNavTopSet && orientation == "tab") {
                stickyNavTop = $('#navBar').offset().top;
                tabNavTopSet = true;
                navTopSet = false;
            }
            var scrollTop = $(window).scrollTop();

            if (scrollTop > stickyNavTop) {
                $('#navBar').addClass('sticky');
            } else {
                $('#navBar').removeClass('sticky');
            }
        }

    };

    var mobileStickyNav = function () {
        if ($("#mobile-navBar").length) {
            if (!mobileNavTopSet) {
                stickyMobileNavTop = $('#mobile-navBar').offset().top;
                mobileNavTopSet = true;
            }
            var scrollTop = $(window).scrollTop();

            if (scrollTop > stickyMobileNavTop) {
                $('#mobile-navBar').addClass('sticky');
            } else {
                $('#mobile-navBar').removeClass('sticky');
            }
        }
    };

    if (window.innerWidth > 600 && window.innerWidth <= 1280) {
        stickyNav('tab');
    } else if (window.innerWidth > 1280) {
        stickyNav('desk');
    } else {
        mobileStickyNav();
    }

    $(window).scroll(function () {
        if (window.innerWidth > 600 && window.innerWidth <= 1280) {
            stickyNav('tab');
        } else if (window.innerWidth > 1280) {
            stickyNav('desk');
        } else {
            mobileStickyNav();
        }
    });

    // Countdown
    $("#countdown")
       .countdown("2017/06/17", function(event) {
            $(this).text(
              event.strftime('%D days %H:%M:%S')
            );
       });

    // Write function to inspect cookie for relevant values (also add loads of values to the cookie!)
   // alert('onload cookie functions!!!');
    var cookie = getCookie("Chris and Jess Wedding");
    var nicknameStr = "nickname=";
    var nickname = cookie.substring(cookie.lastIndexOf(nicknameStr) + nicknameStr.length, cookie.length);
    if (nickname.indexOf("&") > 0) {
        nickname = nickname.substring(0, nickname.lastIndexOf("&"));
    }
    
    if (nickname != undefined) {
        $("#welcomeMessage").html("<p>Good to see you "+nickname+"!</p>");
    }

    $('#printDirections').click(function () {
        PrintElem($('#directions'));
    })
    
    $('.RSVPChange').click(function (e) {
        var _this = this;
        var thisButton = $(_this).prev().prev('.RSVPButton')[0];
        var thisRadio = $(_this).prev().prev().prev('.RSVPCheckbox')[0]; 
        var thisConf = $(_this).prev('.RSVPConfirmation')[0];

        $(thisButton).slideToggle('slow');
        $(thisRadio).slideToggle('slow');
        $(thisConf).slideToggle('slow');
        $(_this).slideToggle('slow');


    })

    $(".RSVPGuest").each(function (index) {
        var thisButton = $(this).find('.RSVPButton')[0];
        var thisRadio = $(this).find('.RSVPCheckbox')[0];
        var thisChange = $(this).find('.RSVPChange')[0];
        var thisConf = $(this).find('.RSVPConfirmation')[0];
        var radioTrue = $(thisRadio).find(".true")[0];
        var radioFalse = $(thisRadio).find(".false")[0];

        if ($(this).hasClass("Attending")) {
            setupRSVP();
            $(thisConf).addClass('yes');
            $(radioTrue).prop("checked", true);
            $(radioFalse).prop("checked", false);

        } else if ($(this).hasClass("NotAttending")) {
            setupRSVP();
            $(thisConf).addClass('no');
            $(radioTrue).prop("checked", false);
            $(radioFalse).prop("checked", true);
        }

        function setupRSVP() {
            $(thisButton).css('display', 'none');
            $(thisRadio).css('display', 'none');
            $(thisChange).css('display', 'block');
            $(thisConf).css({'width':'100px', 'height':'100px', 'display':'block', 'opacity': '1', 'overflow':'hidden'});

        }
    })

    $('#signOut').click(function (e) {
        $.ajax({
            type: "POST",
            url: 'Wedsite/deleteCookie',
            dataType: 'json',
            success: function (response) {
                var jsResultObj = $.parseJSON(response);
                if (jsResultObj == true) {
                    location.reload();
                }
            },
            failure: function (e) {
                console.log(e);
            },
            error: function (e) {
                console.log(e);
            }
        })
        e.preventDefault();
        return false;
    })

    $(".RSVPSubmit").on("click", (function (e) {
        var test = '';
        var id = this.id.substr(this.id.indexOf("_") + 1);
        this.disabled = true;
        var RSVP = true;
        if ($("#RSVPFalse"+id).prop('checked') == true){
            RSVP = false;
        } else if ($("#RSVPTrue" + id).prop('checked') == true) {
            RSVP = true;
        }
        $.ajax({
            type: "POST",
            url: "Wedsite/RSVP",
            data: { RSVP: RSVP, WeddingId: $("#weddingId").val(), GuestId: $("#guestId" + id).val() },
            dataType: "json",
            success: function (response) {
                var message = "";
                if (response) {
                    var jsonResultString = JSON.stringify(response);
                    var jsResultObj = $.parseJSON(response);
                    if (jsResultObj.RSVP == true) {
                        easeInImage(jsResultObj, 'yes');
                        $('#confirmationText_' + jsResultObj.GuestId).html('Fantastic!');
                        
                    } else if (jsResultObj.RSVP == false) {
                        easeInImage(jsResultObj, 'no');
                        $('#confirmationText_' + jsResultObj.GuestId).html('Boooo!');
                    }
                }
            },
            failure: function (e) {
                console.log(e);
            },
            error: function (e) {
                console.log(e);
            }
        })
        e.preventDefault();
        return false;
    }))
});

$(".header").click(function () {
    $(this).next().slideToggle( "slow", function() {
        console.log('slide');
    });
});

$(".dashboardItem").click(function () {
    var attr = $(this).attr('data-tile');
    $(".dashboardTile").hide();
    $(".dashboardTile." + attr).show();
    $(".dashboardItem").removeClass('dashboardItemBorder');
    $(this).addClass('dashboardItemBorder');
})

$(".showHide").change(function () {
    var hideEl = $(this).attr('data-hide');
    var showEl = $(this).attr('data-show');

    $('#' + showEl).show();
    $('#' + hideEl).hide();
    
});

// Dependants
$("input[name=dependants]").on("change paste", function () {
    var numberOfDependants = $(this).val();
    
    if ($('#GuestContainer').length) {
        $('#GuestContainer').remove();
    }
    var newhtml = '';
    for (var i = 1; i <= Number(numberOfDependants); i++) {
        newhtml = newhtml + '<div class="guestContainer"><p>Guest ' + i + '</p><label class="control-label col-md-2" for="GuestfirstName' + i + '">First name</label><input id="GuestfirstName' + i + '" type="text" name="dependant[' + (i - 1) + '].FirstName"><label class="control-label col-md-2" for="GuestLastName' + i + '">Surname</label><input id="GuestLastName' + i + '" type="text" name="dependant[' + (i - 1) + '].LastName"></div>';
    }
    newhtml = '<div id="GuestContainer">'+newhtml+'</div>';
    $(newhtml).appendTo('#GuestDependantsContainer');
    
});

// Dashboard open tiles
$(".dashboardSeeAll").on("click", function () {
    var state = $(this).attr('data-state') == 'hidden' ? 'hidden' : 'shown';
    console.log(state);
    if (state == 'hidden') {
        $(this).parent().animate({
            height: "100%"
        }, 300, function () {
            $(this).attr('data-state', 'shown');
        });
    } else {
        $(this).parent().animate({
            height: "300px"
        }, 300, function () {
            $(this).attr('data-state', 'hidden');
        });
    }
});


// Main nav slides
$('.menu_item').click(function (e) {
    slideToEl(this);

});

function slideToEl(el) {
    var slideToId = $(el).attr('data-slideTo');
    var navHeight = $('#navBar').height();
    var top = 0;
    if (slideToId == 'mainSection') {
        var top = 0;
    } else if (!$('#navBar').hasClass('sticky')) {
        top = $('#' + slideToId).offset().top - (navHeight * 2);
    } else {
        top = $('#' + slideToId).offset().top - navHeight;
    }
    $('html, body').animate({ scrollTop: top }, 1000);
}

function toggleDirections(e) {
    var childNode = e.target.nextSibling;
    $(childNode).toggle('medium');
}

function isEmpty(el) {
    return !$.trim(el.html())
}

$('#loginSubmitButton').click(function (e) {
    $.ajax({
        type: "POST",
        url: 'Wedsite/Login',
        data: { username: $("#username").val(), email: $("#email").val(), surname: $("#surname").val() },
        dataType: 'json',
        success: function (result) {
            var message = "";
            if (result) {
                var jsonResultString = JSON.stringify(result);
                var jsResultObj = $.parseJSON(result);
            }

            if (jsResultObj.status == 0) {
                // no username matched - show error
                var errorMessage = "<p>Sorry, the invitation code does not exist... try again or get in touch with Chris and Jess (or maybe you're not invited)</p>";
                $('#errorMsg').html(errorMessage);
            } else if(jsResultObj[0].username != null && jsResultObj[0].emailAddress == null ){
                // Only do AJAX for the username check (see if there is an email address, if not, ask for it). 
                // Then do a page reload when this is successful.
                message = getMessageString(jsResultObj[0], true);
                var emailHtml = '<div class="form-group"><label for="email">Oh hi there ' + jsResultObj[0].firstName + ', please enter your email for wedding updates!</label><div><input class="form-control text-box single-line" id="email" name="email" type="email" value="" onkeyup="emailValidation(event)"><span id="emailValidationError"></span></div></div>';
                $('#emailContainer').html(emailHtml);
                $('#username').prop('disabled', true);
                $("#loginSubmitButton").prop("disabled", true);
                $('#errorMsg').html('');
            } else if (jsResultObj[0].username != null && jsResultObj[0].emailAddress != null) {
                message = getMessageString(jsResultObj[0], false);
                // Store
                localStorage.setItem("username", jsResultObj[0].username);
                location.reload();
            } else {
                message = "Shits gone down... contact Chris and he'll try sort it!";
            }
        },
        failure: function (e) {
            console.log(e);
        },
        error: function (e) {
            console.log(e);
        }
    })
    e.preventDefault();
    return false;
})

function getMessageString(jsResultObj, emailRequired) {
    var message = "";
    var successMessage = "Wohay! Welcome to the party ";
    var emailMessage = "I just need you to provide your email address so I can send you annoying WEDDING updates!"
    if (jsResultObj.length > 1) {
        var emailHtml = "";
        var namesString = "";
        var delimiter = ",";
        for (var i = 0; i < jsResultObj.length; i++) {
            if (i == jsResultObj[jsResultObj.length - 1]) {
                delimiter = " and ";
            }
            message = namesString + delimiter + jsResultObj.lastName + "!";
        }

        message = successMessage + namesString;
    } else {
        message = successMessage + jsResultObj.surname + "!";
    }

    if (emailRequired) {
        message = message + emailMessage;
    }

    return message;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for(var i = 0; i <ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length,c.length);
        }
    }
    return "";
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function emailValidation(event) {
    var validEmail = validateEmail(event.target.value);
    if (validEmail) {
        $("#loginSubmitButton").prop("disabled", false);
        $("#emailValidationError").html("");
    } else {
        $("#emailValidationError").html("Sorry that is not a valid email");
    }
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toUTCString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
    
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}

function eraseAllCookies() {
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++)
        eraseCookie(cookies[i].split("=")[0]);
}

var easeInImage = function (jsResultObj, RSVPclass) {
    var thisButton = $('#confirmationButton_' + jsResultObj.GuestId).prev('.RSVPButton')[0];
    var inputButton = thisButton.children[0];
    var thisRadio = $('#confirmationButton_' + jsResultObj.GuestId).prev().prev('.RSVPCheckbox')[0];
    var thisChange = $('#confirmationButton_' + jsResultObj.GuestId).next()[0];
    $('#confirmationButton_' + jsResultObj.GuestId).width('0');
    $('#confirmationButton_' + jsResultObj.GuestId).height('0');
    $('#confirmationButton_' + jsResultObj.GuestId).css('display', 'block');
    $('#confirmationButton_' + jsResultObj.GuestId).removeClass('yes');
    $('#confirmationButton_' + jsResultObj.GuestId).removeClass('no');
    $('#confirmationButton_' + jsResultObj.GuestId).addClass(RSVPclass);
    $('#confirmationButton_' + jsResultObj.GuestId).css('opacity', '0');
    $('#confirmationButton_' + jsResultObj.GuestId).animate({ height: '200px', width: '200px', opacity: 1 },
              {
                  duration: 2000
              })
    setTimeout(
      function () {
          $('#confirmationButton_' + jsResultObj.GuestId).animate({ height: '100px', width: '100px' },
              {
                  duration: 2000,
                  specialEasing: {
                      width: "linear",
                      height: "easeOutBounce"
                  },
                  queue: false
              })
      },
      $(thisButton).slideToggle('slow'),
      $(thisRadio).slideToggle('slow'),
         100);
    setTimeout(
      function () {
          $(thisChange).toggle('slide')
      }, 1500);
    setTimeout(function () { $(inputButton).prop('disabled', false) }, 2000);
    
    
}



function PrintElem(elem) {
    Popup($(elem).html());
}

function Popup(data) {
    var mywindow = window.open('', 'my div', 'height=400,width=600');
    mywindow.document.write('<html><head><title>Directions to Horsleygate Hall</title>');
    /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
    mywindow.document.write('</head><body >');
    mywindow.document.write(data);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10

    mywindow.print();
    mywindow.close();

    return true;
}

