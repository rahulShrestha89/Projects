window.Modernizr=function(n,t,i){function a(n){c.cssText=n}function vt(n,t){return a(p.join(n+";")+(t||""))}function h(n,t){return typeof n===t}function v(n,t){return!!~(""+n).indexOf(t)}function ft(n,t){var u,r;for(u in n)if(r=n[u],!v(r,"-")&&c[r]!==i)return t=="pfx"?r:!0;return!1}function pt(n,t,r){var f,u;for(f in n)if(u=t[n[f]],u!==i)return r===!1?n[f]:h(u,"function")?u.bind(r||t):u;return!1}function f(n,t,i){var r=n.charAt(0).toUpperCase()+n.slice(1),u=(n+" "+st.join(r+" ")+r).split(" ");return h(t,"string")||h(t,"undefined")?ft(u,t):(u=(n+" "+ot.join(r+" ")+r).split(" "),pt(u,t,i))}function at(){u.input=function(i){for(var r=0,u=i.length;r<u;r++)b[i[r]]=!!(i[r]in o);return b.list&&(b.list=!!(t.createElement("datalist")&&n.HTMLDataListElement)),b}("autocomplete autofocus list placeholder max min multiple pattern required step".split(" ")),u.inputtypes=function(n){for(var u=0,r,f,e,h=n.length;u<h;u++)o.setAttribute("type",f=n[u]),r=o.type!=="text",r&&(o.value=d,o.style.cssText="position:absolute;visibility:hidden;",/^range$/.test(f)&&o.style.WebkitAppearance!==i?(s.appendChild(o),e=t.defaultView,r=e.getComputedStyle&&e.getComputedStyle(o,null).WebkitAppearance!=="textfield"&&o.offsetHeight!==0,s.removeChild(o)):/^(search|tel)$/.test(f)||(r=/^(url|email)$/.test(f)?o.checkValidity&&o.checkValidity()===!1:o.value!=d)),lt[n[u]]=!!r;return lt}("search tel url email datetime date month week time datetime-local number range color".split(" "))}var u={},tt=!0,s=t.documentElement,e="modernizr",ct=t.createElement(e),c=ct.style,o=t.createElement("input"),d=":)",et={}.toString,p=" -webkit- -moz- -o- -ms- ".split(" "),ht="Webkit Moz O ms",st=ht.split(" "),ot=ht.toLowerCase().split(" "),y={svg:"http://www.w3.org/2000/svg"},r={},lt={},b={},it=[],nt=it.slice,w,l=function(n,i,r,u){var l,a,c,v,f=t.createElement("div"),h=t.body,o=h||t.createElement("body");if(parseInt(r,10))while(r--)c=t.createElement("div"),c.id=u?u[r]:e+(r+1),f.appendChild(c);return l=["&#173;",'<style id="s',e,'">',n,"<\/style>"].join(""),f.id=e,(h?f:o).innerHTML+=l,o.appendChild(f),h||(o.style.background="",o.style.overflow="hidden",v=s.style.overflow,s.style.overflow="hidden",s.appendChild(o)),a=i(f,n),h?f.parentNode.removeChild(f):(o.parentNode.removeChild(o),s.style.overflow=v),!!a},yt=function(t){var i=n.matchMedia||n.msMatchMedia,r;return i?i(t).matches:(l("@media "+t+" { #"+e+" { position: absolute; } }",function(t){r=(n.getComputedStyle?getComputedStyle(t,null):t.currentStyle).position=="absolute"}),r)},ut=function(){function n(n,u){u=u||t.createElement(r[n]||"div"),n="on"+n;var f=n in u;return f||(u.setAttribute||(u=t.createElement("div")),u.setAttribute&&u.removeAttribute&&(u.setAttribute(n,""),f=h(u[n],"function"),h(u[n],"undefined")||(u[n]=i),u.removeAttribute(n))),u=null,f}var r={select:"input",change:"input",submit:"form",reset:"form",error:"img",load:"img",abort:"img"};return n}(),g={}.hasOwnProperty,rt,k;rt=h(g,"undefined")||h(g.call,"undefined")?function(n,t){return t in n&&h(n.constructor.prototype[t],"undefined")}:function(n,t){return g.call(n,t)},Function.prototype.bind||(Function.prototype.bind=function(n){var t=this,i,r;if(typeof t!="function")throw new TypeError;return i=nt.call(arguments,1),r=function(){var e,f,u;return this instanceof r?(e=function(){},e.prototype=t.prototype,f=new e,u=t.apply(f,i.concat(nt.call(arguments))),Object(u)===u)?u:f:t.apply(n,i.concat(nt.call(arguments)))},r}),r.flexbox=function(){return f("flexWrap")},r.flexboxlegacy=function(){return f("boxDirection")},r.canvas=function(){var n=t.createElement("canvas");return!!(n.getContext&&n.getContext("2d"))},r.canvastext=function(){return!!(u.canvas&&h(t.createElement("canvas").getContext("2d").fillText,"function"))},r.webgl=function(){return!!n.WebGLRenderingContext},r.touch=function(){var i;return"ontouchstart"in n||n.DocumentTouch&&t instanceof DocumentTouch?i=!0:l(["@media (",p.join("touch-enabled),("),e,")","{#modernizr{top:9px;position:absolute}}"].join(""),function(n){i=n.offsetTop===9}),i},r.geolocation=function(){return"geolocation"in navigator},r.postmessage=function(){return!!n.postMessage},r.websqldatabase=function(){return!!n.openDatabase},r.indexedDB=function(){return!!f("indexedDB",n)},r.hashchange=function(){return ut("hashchange",n)&&(t.documentMode===i||t.documentMode>7)},r.history=function(){return!!(n.history&&history.pushState)},r.draganddrop=function(){var n=t.createElement("div");return"draggable"in n||"ondragstart"in n&&"ondrop"in n},r.websockets=function(){return"WebSocket"in n||"MozWebSocket"in n},r.rgba=function(){return a("background-color:rgba(150,255,150,.5)"),v(c.backgroundColor,"rgba")},r.hsla=function(){return a("background-color:hsla(120,40%,100%,.5)"),v(c.backgroundColor,"rgba")||v(c.backgroundColor,"hsla")},r.multiplebgs=function(){return a("background:url(https://),url(https://),red url(https://)"),/(url\s*\(.*?){3}/.test(c.background)},r.backgroundsize=function(){return f("backgroundSize")},r.borderimage=function(){return f("borderImage")},r.borderradius=function(){return f("borderRadius")},r.boxshadow=function(){return f("boxShadow")},r.textshadow=function(){return t.createElement("div").style.textShadow===""},r.opacity=function(){return vt("opacity:.55"),/^0.55$/.test(c.opacity)},r.cssanimations=function(){return f("animationName")},r.csscolumns=function(){return f("columnCount")},r.cssgradients=function(){var n="background-image:";return a((n+"-webkit- ".split(" ").join("gradient(linear,left top,right bottom,from(#9f9),to(white));"+n)+p.join("linear-gradient(left top,#9f9, white);"+n)).slice(0,-n.length)),v(c.backgroundImage,"gradient")},r.cssreflections=function(){return f("boxReflect")},r.csstransforms=function(){return!!f("transform")},r.csstransforms3d=function(){var n=!!f("perspective");return n&&"webkitPerspective"in s.style&&l("@media (transform-3d),(-webkit-transform-3d){#modernizr{left:9px;position:absolute;height:3px;}}",function(t){n=t.offsetLeft===9&&t.offsetHeight===3}),n},r.csstransitions=function(){return f("transition")},r.fontface=function(){var n;return l('@font-face {font-family:"font";src:url("https://")}',function(i,r){var f=t.getElementById("smodernizr"),u=f.sheet||f.styleSheet,e=u?u.cssRules&&u.cssRules[0]?u.cssRules[0].cssText:u.cssText||"":"";n=/src/i.test(e)&&e.indexOf(r.split(" ")[0])===0}),n},r.generatedcontent=function(){var n;return l(["#",e,"{font:0/0 a}#",e,':after{content:"',d,'";visibility:hidden;font:3px/1 a}'].join(""),function(t){n=t.offsetHeight>=3}),n},r.video=function(){var i=t.createElement("video"),n=!1;try{(n=!!i.canPlayType)&&(n=new Boolean(n),n.ogg=i.canPlayType('video/ogg; codecs="theora"').replace(/^no$/,""),n.h264=i.canPlayType('video/mp4; codecs="avc1.42E01E"').replace(/^no$/,""),n.webm=i.canPlayType('video/webm; codecs="vp8, vorbis"').replace(/^no$/,""))}catch(r){}return n},r.audio=function(){var i=t.createElement("audio"),n=!1;try{(n=!!i.canPlayType)&&(n=new Boolean(n),n.ogg=i.canPlayType('audio/ogg; codecs="vorbis"').replace(/^no$/,""),n.mp3=i.canPlayType("audio/mpeg;").replace(/^no$/,""),n.wav=i.canPlayType('audio/wav; codecs="1"').replace(/^no$/,""),n.m4a=(i.canPlayType("audio/x-m4a;")||i.canPlayType("audio/aac;")).replace(/^no$/,""))}catch(r){}return n},r.localstorage=function(){try{return localStorage.setItem(e,e),localStorage.removeItem(e),!0}catch(n){return!1}},r.sessionstorage=function(){try{return sessionStorage.setItem(e,e),sessionStorage.removeItem(e),!0}catch(n){return!1}},r.webworkers=function(){return!!n.Worker},r.applicationcache=function(){return!!n.applicationCache},r.svg=function(){return!!t.createElementNS&&!!t.createElementNS(y.svg,"svg").createSVGRect},r.inlinesvg=function(){var n=t.createElement("div");return n.innerHTML="<svg/>",(n.firstChild&&n.firstChild.namespaceURI)==y.svg},r.smil=function(){return!!t.createElementNS&&/SVGAnimate/.test(et.call(t.createElementNS(y.svg,"animate")))},r.svgclippaths=function(){return!!t.createElementNS&&/SVGClipPath/.test(et.call(t.createElementNS(y.svg,"clipPath")))};for(k in r)rt(r,k)&&(w=k.toLowerCase(),u[w]=r[k](),it.push((u[w]?"":"no-")+w));return u.input||at(),u.addTest=function(n,t){if(typeof n=="object")for(var r in n)rt(n,r)&&u.addTest(r,n[r]);else{if(n=n.toLowerCase(),u[n]!==i)return u;t=typeof t=="function"?t():t,typeof tt!="undefined"&&tt&&(s.className+=" "+(t?"":"no-")+n),u[n]=t}return u},a(""),ct=o=null,function(n,t){function p(n,t){var i=n.createElement("p"),r=n.getElementsByTagName("head")[0]||n.documentElement;return i.innerHTML="x<style>"+t+"<\/style>",r.insertBefore(i.lastChild,r.firstChild)}function l(){var n=r.elements;return typeof n=="string"?n.split(" "):n}function u(n){var t=c[n[h]];return t||(t={},o++,n[h]=o,c[o]=t),t}function s(n,r,f){if(r||(r=t),i)return r.createElement(n);f||(f=u(r));var e;return e=f.cache[n]?f.cache[n].cloneNode():b.test(n)?(f.cache[n]=f.createElem(n)).cloneNode():f.createElem(n),e.canHaveChildren&&!w.test(n)?f.frag.appendChild(e):e}function v(n,r){if(n||(n=t),i)return n.createDocumentFragment();r=r||u(n);for(var e=r.frag.cloneNode(),f=0,o=l(),s=o.length;f<s;f++)e.createElement(o[f]);return e}function y(n,t){t.cache||(t.cache={},t.createElem=n.createElement,t.createFrag=n.createDocumentFragment,t.frag=t.createFrag()),n.createElement=function(i){return r.shivMethods?s(i,n,t):t.createElem(i)},n.createDocumentFragment=Function("h,f","return function(){var n=f.cloneNode(),c=n.createElement;h.shivMethods&&("+l().join().replace(/\w+/g,function(n){return t.createElem(n),t.frag.createElement(n),'c("'+n+'")'})+");return n}")(r,t.frag)}function a(n){n||(n=t);var f=u(n);return!r.shivCSS||e||f.hasCSS||(f.hasCSS=!!p(n,"article,aside,figcaption,figure,footer,header,hgroup,nav,section{display:block}mark{background:#FF0;color:#000}")),i||y(n,f),n}var f=n.html5||{},w=/^<|^(?:button|map|select|textarea|object|iframe|option|optgroup)$/i,b=/^(?:a|b|code|div|fieldset|h1|h2|h3|h4|h5|h6|i|label|li|ol|p|q|span|strong|style|table|tbody|td|th|tr|ul)$/i,e,h="_html5shiv",o=0,c={},i,r;(function(){try{var n=t.createElement("a");n.innerHTML="<xyz><\/xyz>",e="hidden"in n,i=n.childNodes.length==1||function(){t.createElement("a");var n=t.createDocumentFragment();return typeof n.cloneNode=="undefined"||typeof n.createDocumentFragment=="undefined"||typeof n.createElement=="undefined"}()}catch(r){e=!0,i=!0}})(),r={elements:f.elements||"abbr article aside audio bdi canvas data datalist details figcaption figure footer header hgroup mark meter nav output progress section summary time video",shivCSS:f.shivCSS!==!1,supportsUnknownElements:i,shivMethods:f.shivMethods!==!1,type:"default",shivDocument:a,createElement:s,createDocumentFragment:v},n.html5=r,a(t)}(this,t),u._version="2.6.2",u._prefixes=p,u._domPrefixes=ot,u._cssomPrefixes=st,u.mq=yt,u.hasEvent=ut,u.testProp=function(n){return ft([n])},u.testAllProps=f,u.testStyles=l,u.prefixed=function(n,t,i){return t?f(n,t,i):f(n,"pfx")},s.className=s.className.replace(/(^|\s)no-js(\s|$)/,"$1$2")+(tt?" js "+it.join(" "):""),u}(this,this.document)