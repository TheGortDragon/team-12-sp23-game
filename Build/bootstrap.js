// bootstrap.js
var head = document.getElementsByTagName('head')[0];

// Bootstrap core CSS
var bootstrapCSS = document.createElement('link');
bootstrapCSS.rel = 'stylesheet';
bootstrapCSS.href = 'https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css';
bootstrapCSS.integrity = 'sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm';
bootstrapCSS.crossOrigin = 'anonymous';
head.appendChild(bootstrapCSS);

// Core theme JS
var coreThemeJS = document.createElement('script');
coreThemeJS.src = 'js/scripts.js';
head.appendChild(coreThemeJS);

// jQuery
var jQueryJS = document.createElement('script');
jQueryJS.src = 'https://code.jquery.com/jquery-3.2.1.slim.min.js';
jQueryJS.integrity = 'sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN';
jQueryJS.crossOrigin = 'anonymous';
head.appendChild(jQueryJS);

// Popper.js
var popperJS = document.createElement('script');
popperJS.src = 'https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js';
popperJS.integrity = 'sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q';
popperJS.crossOrigin = 'anonymous';
head.appendChild(popperJS);

// Bootstrap core JS
var bootstrapJS = document.createElement('script');
bootstrapJS.src = 'https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js';
bootstrapJS.integrity = 'sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl';
bootstrapJS.crossOrigin = 'anonymous';
bootstrapJS.onload = function () {
    // After the Bootstrap JS is loaded, trigger the DOMContentLoaded event
    window.dispatchEvent(new Event('DOMContentLoaded'));
};
head.appendChild(bootstrapJS);