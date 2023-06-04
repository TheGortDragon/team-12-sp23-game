// scripts.js
window.addEventListener('DOMContentLoaded', function() {
    var headerContainer = document.getElementById('header');
    var footerContainer = document.getElementById('footer');

    // Create an XMLHttpRequest object for the header
    var headerXHR = new XMLHttpRequest();

    // Define the callback function for the header AJAX request
    headerXHR.onreadystatechange = function() {
        if (headerXHR.readyState === XMLHttpRequest.DONE) {
            if (headerXHR.status === 200) {
                // On successful AJAX request, set the response as the content of the header container
                headerContainer.innerHTML = headerXHR.responseText;
            } else {
                console.log('Error: ' + headerXHR.status);
            }
        }
    };

    // Open and send the AJAX request to fetch the header content
    headerXHR.open('GET', 'header.html', true);
    headerXHR.send();

    // Create an XMLHttpRequest object for the footer
    var footerXHR = new XMLHttpRequest();

    // Define the callback function for the footer AJAX request
    footerXHR.onreadystatechange = function() {
        if (footerXHR.readyState === XMLHttpRequest.DONE) {
            if (footerXHR.status === 200) {
                // On successful AJAX request, set the response as the content of the footer container
                footerContainer.innerHTML = footerXHR.responseText;
            } else {
                console.log('Error: ' + footerXHR.status);
            }
        }
    };

    // Open and send the AJAX request to fetch the footer content
    footerXHR.open('GET', 'footer.html', true);
    footerXHR.send();
});