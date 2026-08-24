function getCookie (name) {
var dcookie = document.cookie; 
var cname = name + "=";
var clen = dcookie.length;
var cbegin = 0;
        while (cbegin < clen) {
        var vbegin = cbegin + cname.length;
                if (dcookie.substring(cbegin, vbegin) == cname) { 
                var vend = dcookie.indexOf (";", vbegin);
                        if (vend == -1) vend = clen;
                return unescape(dcookie.substring(vbegin, vend));
                }
        cbegin = dcookie.indexOf(" ", cbegin) + 1;
                if (cbegin == 0) break;
        }
return null;
}

function setCookie (name, value, expires) {
        if (!expires) expires = new Date();
document.cookie = name + "=" + escape (value) + 
"; expires=" + expires.toGMTString() +  "; path=/";
}

function delCookie (name) {
var expireNow = new Date();
document.cookie = name + "=" +
"; expires=Thu, 01-Jan-70 00:00:01 GMT" +  "; path=/";
}

function setCookieArray(name){
this.length = setCookieArray.arguments.length - 1;
        for (var i = 0; i < this.length; i++) {
        this[i + 1] = setCookieArray.arguments[i + 1]
        setCookie (name + i, this[i + 1], expdate);
        }        
}

function getCookieArray(name){
var i = 0;
        while (getCookie(name + i) != null) {
        this[i + 1] = getCookie(name + i);
        i++; this.length = i; 
        }
}