// Yes, Sasha, this code is a fucking mess.  I promise I'll clean it up.


/*

	**********************************
	*** jQuery Highlight extension ***
	**********************************
	
	from: http://johannburkard.de/blog/programming/javascript/highlight-javascript-text-higlighting-jquery-plugin.html
	
	usage: 
		CSS: 
		.highlight { background-color: yellow }
		JS:
		$('li').highlight('bla'); // highlight
		$('#highlight-plugin').removeHighlight(); // remove

*/

jQuery.fn.highlight=function(b){function a(e,j){var l=0;if(e.nodeType==3){var k=e.data.toUpperCase().indexOf(j);if(k>=0){var h=document.createElement("span");h.className="highlight";var f=e.splitText(k);var c=f.splitText(j.length);var d=f.cloneNode(true);h.appendChild(d);f.parentNode.replaceChild(h,f);l=1}}else{if(e.nodeType==1&&e.childNodes&&!/(script|style)/i.test(e.tagName)){for(var g=0;g<e.childNodes.length;++g){g+=a(e.childNodes[g],j)}}}return l}return this.each(function(){a(this,b.toUpperCase())})};jQuery.fn.removeHighlight=function(){return this.find("span.highlight").each(function(){this.parentNode.firstChild.nodeName;with(this.parentNode){replaceChild(this.firstChild,this);normalize()}}).end()};


/*

	*********************************************
	*** jQuery UI Autocomplete HTML Extension ***
	*********************************************
	
	Allows usage of HTML in autocomplete dropdown results 

 	Copyright 2010, Scott González (http://scottgonzalez.com)
 	Dual licensed under the MIT or GPL Version 2 licenses.
 
 	http://github.com/scottgonzalez/jquery-ui-extensions

*/


(function( $ ) {

	var proto = $.ui.autocomplete.prototype,
		initSource = proto._initSource;
	
	function filter( array, term ) {
		var matcher = new RegExp( $.ui.autocomplete.escapeRegex(term), "i" );
		return $.grep( array, function(value) {
			return matcher.test( $( "<div>" ).html( value.label || value.value || value ).text() );
		});
	}
	
	$.extend( proto, {
		_initSource: function() {
			if ( this.options.html && $.isArray(this.options.source) ) {
				this.source = function( request, response ) {
					response( filter( this.options.source, request.term ) );
				};
			} else {
				initSource.call( this );
			}
		},
	
		_renderItem: function( ul, item) {
			if (item.noresults) {
				return $( "<li><a><p class=\"protip\">Sorry. No results found. Try typing less - usually you just need to type a few characters.</p><p class=\"protip\">Example Searches:  John, Otaku, Jennifer</p></a></li>" ).data( "item.autocomplete", item ).appendTo(ul);
			}
			else {
				return $( "<li></li>" )
				.data( "item.autocomplete", item )
				.append( $( "<a></a>" )[ this.options.html ? "html" : "text" ]( obToolbar.superSearchFormat(item) ) )
				.appendTo( ul );
			}
		}
	});

})( jQuery );


/*

	************************************************************
	** mustache.js - from https://github.com/janl/mustache.js **
	************************************************************

*/

;(function($){var Mustache=(typeof module!=="undefined"&&module.exports)||{};(function(exports){exports.name="mustache.js";exports.version="0.5.0-dev";exports.tags=["{{","}}"];exports.parse=parse;exports.compile=compile;exports.render=render;exports.clearCache=clearCache;exports.to_html=function(template,view,partials,send){var result=render(template,view,partials);if(typeof send==="function"){send(result);}else{return result;}};var _toString=Object.prototype.toString;var _isArray=Array.isArray;var _forEach=Array.prototype.forEach;var _trim=String.prototype.trim;var isArray;if(_isArray){isArray=_isArray;}else{isArray=function(obj){return _toString.call(obj)==="[object Array]";};}
var forEach;if(_forEach){forEach=function(obj,callback,scope){return _forEach.call(obj,callback,scope);};}else{forEach=function(obj,callback,scope){for(var i=0,len=obj.length;i<len;++i){callback.call(scope,obj[i],i,obj);}};}
var spaceRe=/^\s*$/;function isWhitespace(string){return spaceRe.test(string);}
var trim;if(_trim){trim=function(string){return string==null?"":_trim.call(string);};}else{var trimLeft,trimRight;if(isWhitespace("\xA0")){trimLeft=/^\s+/;trimRight=/\s+$/;}else{trimLeft=/^[\s\xA0]+/;trimRight=/[\s\xA0]+$/;}
trim=function(string){return string==null?"":String(string).replace(trimLeft,"").replace(trimRight,"");};}
var escapeMap={"&":"&amp;","<":"&lt;",">":"&gt;",'"':'&quot;',"'":'&#39;'};function escapeHTML(string){return String(string).replace(/&(?!\w+;)|[<>"']/g,function(s){return escapeMap[s]||s;});}
function debug(e,template,line,file){file=file||"<template>";var lines=template.split("\n"),start=Math.max(line-3,0),end=Math.min(lines.length,line+3),context=lines.slice(start,end);var c;for(var i=0,len=context.length;i<len;++i){c=i+start+1;context[i]=(c===line?" >> ":"    ")+context[i];}
e.template=template;e.line=line;e.file=file;e.message=[file+":"+line,context.join("\n"),"",e.message].join("\n");return e;}
function lookup(name,stack,defaultValue){if(name==="."){return stack[stack.length-1];}
var names=name.split(".");var lastIndex=names.length-1;var target=names[lastIndex];var value,context,i=stack.length,j,localStack;while(i){localStack=stack.slice(0);context=stack[--i];j=0;while(j<lastIndex){context=context[names[j++]];if(context==null){break;}
localStack.push(context);}
if(context&&target in context){value=context[target];break;}}
if(typeof value==="function"){value=value.call(localStack[localStack.length-1]);}
if(value==null){return defaultValue;}
return value;}
function renderSection(name,stack,callback,inverted){var buffer="";var value=lookup(name,stack);if(inverted){if(value==null||value===false||(isArray(value)&&value.length===0)){buffer+=callback();}}else if(isArray(value)){forEach(value,function(value){stack.push(value);buffer+=callback();stack.pop();});}else if(typeof value==="object"){stack.push(value);buffer+=callback();stack.pop();}else if(typeof value==="function"){var scope=stack[stack.length-1];var scopedRender=function(template){return render(template,scope);};buffer+=value.call(scope,callback(),scopedRender)||"";}else if(value){buffer+=callback();}
return buffer;}
function parse(template,options){options=options||{};var tags=options.tags||exports.tags,openTag=tags[0],closeTag=tags[tags.length-1];var code=['var buffer = "";',"\nvar line = 1;","\ntry {",'\nbuffer += "'];var spaces=[],hasTag=false,nonSpace=false;var stripSpace=function(){if(hasTag&&!nonSpace&&!options.space){while(spaces.length){code.splice(spaces.pop(),1);}}else{spaces=[];}
hasTag=false;nonSpace=false;};var sectionStack=[],updateLine,nextOpenTag,nextCloseTag;var setTags=function(source){tags=trim(source).split(/\s+/);nextOpenTag=tags[0];nextCloseTag=tags[tags.length-1];};var includePartial=function(source){code.push('";',updateLine,'\nvar partial = partials["'+trim(source)+'"];','\nif (partial) {','\n  buffer += render(partial,stack[stack.length - 1],partials);','\n}','\nbuffer += "');};var openSection=function(source,inverted){var name=trim(source);if(name===""){throw debug(new Error("Section name may not be empty"),template,line,options.file);}
sectionStack.push({name:name,inverted:inverted});code.push('";',updateLine,'\nvar name = "'+name+'";','\nvar callback = (function () {','\n  return function () {','\n    var buffer = "";','\nbuffer += "');};var openInvertedSection=function(source){openSection(source,true);};var closeSection=function(source){var name=trim(source);var openName=sectionStack.length!=0&&sectionStack[sectionStack.length-1].name;if(!openName||name!=openName){throw debug(new Error('Section named "'+name+'" was never opened'),template,line,options.file);}
var section=sectionStack.pop();code.push('";','\n    return buffer;','\n  };','\n})();');if(section.inverted){code.push("\nbuffer += renderSection(name,stack,callback,true);");}else{code.push("\nbuffer += renderSection(name,stack,callback);");}
code.push('\nbuffer += "');};var sendPlain=function(source){code.push('";',updateLine,'\nbuffer += lookup("'+trim(source)+'",stack,"");','\nbuffer += "');};var sendEscaped=function(source){code.push('";',updateLine,'\nbuffer += escapeHTML(lookup("'+trim(source)+'",stack,""));','\nbuffer += "');};var line=1,c,callback;for(var i=0,len=template.length;i<len;++i){if(template.slice(i,i+openTag.length)===openTag){i+=openTag.length;c=template.substr(i,1);updateLine='\nline = '+line+';';nextOpenTag=openTag;nextCloseTag=closeTag;hasTag=true;switch(c){case"!":i++;callback=null;break;case"=":i++;closeTag="="+closeTag;callback=setTags;break;case">":i++;callback=includePartial;break;case"#":i++;callback=openSection;break;case"^":i++;callback=openInvertedSection;break;case"/":i++;callback=closeSection;break;case"{":closeTag="}"+closeTag;case"&":i++;nonSpace=true;callback=sendPlain;break;default:nonSpace=true;callback=sendEscaped;}
var end=template.indexOf(closeTag,i);if(end===-1){throw debug(new Error('Tag "'+openTag+'" was not closed properly'),template,line,options.file);}
var source=template.substring(i,end);if(callback){callback(source);}
var n=0;while(~(n=source.indexOf("\n",n))){line++;n++;}
i=end+closeTag.length-1;openTag=nextOpenTag;closeTag=nextCloseTag;}else{c=template.substr(i,1);switch(c){case'"':case"\\":nonSpace=true;code.push("\\"+c);break;case"\r":break;case"\n":spaces.push(code.length);code.push("\\n");stripSpace();line++;break;default:if(isWhitespace(c)){spaces.push(code.length);}else{nonSpace=true;}
code.push(c);}}}
if(sectionStack.length!=0){throw debug(new Error('Section "'+sectionStack[sectionStack.length-1].name+'" was not closed properly'),template,line,options.file);}
stripSpace();code.push('";',"\nreturn buffer;","\n} catch (e) { throw {error: e, line: line}; }");var body=code.join("").replace(/buffer \+= "";\n/g,"");if(options.debug){if(typeof console!="undefined"&&console.log){console.log(body);}else if(typeof print==="function"){print(body);}}
return body;}
function _compile(template,options){var args="view,partials,stack,lookup,escapeHTML,renderSection,render";var body=parse(template,options);var fn=new Function(args,body);return function(view,partials){partials=partials||{};var stack=[view];try{return fn(view,partials,stack,lookup,escapeHTML,renderSection,render);}catch(e){throw debug(e.error,template,e.line,options.file);}};}
var _cache={};function clearCache(){_cache={};}
function compile(template,options){options=options||{};if(options.cache!==false){if(!_cache[template]){_cache[template]=_compile(template,options);}
return _cache[template];}
return _compile(template,options);}
function render(template,view,partials){return compile(template)(view,partials);}})(Mustache);$.mustache=function(template,view,partials){return Mustache.render(template,view,partials);};$.fn.mustache=function(view,partials){return $(this).map(function(i,elm){var template=$(elm).html().trim();var output=$.mustache(template,view,partials);return $(output).get();});};})(jQuery);


/*
* qTip2 - Pretty powerful tooltips
* http://craigsworks.com/projects/qtip2/
*
* Version: nightly
* Copyright 2009-2010 Craig Michael Thompson - http://craigsworks.com
*
* Dual licensed under MIT or GPLv2 licenses
*   http://en.wikipedia.org/wiki/MIT_License
*   http://en.wikipedia.org/wiki/GNU_General_Public_License
*
* Date: Sat Mar  3 09:04:15.0000000000 2012
*//*jslint browser: true, onevar: true, undef: true, nomen: true, bitwise: true, regexp: true, newcap: true, immed: true, strict: true *//*global window: false, jQuery: false, console: false, define: false */
(function(a){typeof define==="function"&&define.amd?define(["jquery"],a):a(jQuery)})(function(a){function B(f,h){function w(a){var b=a.precedance==="y",c=n[b?"width":"height"],d=n[b?"height":"width"],e=a.string().indexOf("center")>-1,f=c*(e?.5:1),g=Math.pow,h=Math.round,i,j,k,l=Math.sqrt(g(f,2)+g(d,2)),m=[p/f*l,p/d*l];m[2]=Math.sqrt(g(m[0],2)-g(p,2)),m[3]=Math.sqrt(g(m[1],2)-g(p,2)),i=l+m[2]+m[3]+(e?0:m[0]),j=i/l,k=[h(j*d),h(j*c)];return{height:k[b?0:1],width:k[b?1:0]}}function v(b){var c=k.titlebar&&b.y==="top",d=c?k.titlebar:k.content,e=a.browser.mozilla,f=e?"-moz-":a.browser.webkit?"-webkit-":"",g=b.y+(e?"":"-")+b.x,h=f+(e?"border-radius-"+g:"border-"+g+"-radius");return parseInt(d.css(h),10)||parseInt(l.css(h),10)||0}function u(a,b,c){b=b?b:a[a.precedance];var d=l.hasClass(q),e=k.titlebar&&a.y==="top",f=e?k.titlebar:k.content,g="border-"+b+"-width",h;l.addClass(q),h=parseInt(f.css(g),10),h=(c?h||parseInt(l.css(g),10):h)||0,l.toggleClass(q,d);return h}function t(a,d,g,h){if(k.tip){var l=i.corner.clone(),n=g.adjusted,o=f.options.position.adjust.method.split(" "),p=o[0],q=o[1]||o[0],r={left:c,top:c,x:0,y:0},s,t={},u;i.corner.fixed!==b&&(p==="shift"&&l.precedance==="x"&&n.left&&l.y!=="center"?l.precedance=l.precedance==="x"?"y":"x":p==="flip"&&n.left&&(l.x=l.x==="center"?n.left>0?"left":"right":l.x==="left"?"right":"left"),q==="shift"&&l.precedance==="y"&&n.top&&l.x!=="center"?l.precedance=l.precedance==="y"?"x":"y":q==="flip"&&n.top&&(l.y=l.y==="center"?n.top>0?"top":"bottom":l.y==="top"?"bottom":"top"),l.string()!==m.corner.string()&&(m.top!==n.top||m.left!==n.left)&&i.update(l,c)),s=i.position(l,n),s.right!==e&&(s.left=-s.right),s.bottom!==e&&(s.top=-s.bottom),s.user=Math.max(0,j.offset);if(r.left=p==="shift"&&!!n.left)l.x==="center"?t["margin-left"]=r.x=s["margin-left"]-n.left:(u=s.right!==e?[n.left,-s.left]:[-n.left,s.left],(r.x=Math.max(u[0],u[1]))>u[0]&&(g.left-=n.left,r.left=c),t[s.right!==e?"right":"left"]=r.x);if(r.top=q==="shift"&&!!n.top)l.y==="center"?t["margin-top"]=r.y=s["margin-top"]-n.top:(u=s.bottom!==e?[n.top,-s.top]:[-n.top,s.top],(r.y=Math.max(u[0],u[1]))>u[0]&&(g.top-=n.top,r.top=c),t[s.bottom!==e?"bottom":"top"]=r.y);k.tip.css(t).toggle(!(r.x&&r.y||l.x==="center"&&r.y||l.y==="center"&&r.x)),g.left-=s.left.charAt?s.user:p!=="shift"||r.top||!r.left&&!r.top?s.left:0,g.top-=s.top.charAt?s.user:q!=="shift"||r.left||!r.left&&!r.top?s.top:0,m.left=n.left,m.top=n.top,m.corner=l.clone()}}var i=this,j=f.options.style.tip,k=f.elements,l=k.tooltip,m={top:0,left:0},n={width:j.width,height:j.height},o={},p=j.border||0,r=".qtip-tip",s=!!(a("<canvas />")[0]||{}).getContext;i.mimic=i.corner=d,i.border=p,i.offset=j.offset,i.size=n,f.checks.tip={"^position.my|style.tip.(corner|mimic|border)$":function(){i.init()||i.destroy(),f.reposition()},"^style.tip.(height|width)$":function(){n={width:j.width,height:j.height},i.create(),i.update(),f.reposition()},"^content.title.text|style.(classes|widget)$":function(){k.tip&&i.update()}},a.extend(i,{init:function(){var b=i.detectCorner()&&(s||a.browser.msie);b&&(i.create(),i.update(),l.unbind(r).bind("tooltipmove"+r,t));return b},detectCorner:function(){var a=j.corner,d=f.options.position,e=d.at,h=d.my.string?d.my.string():d.my;if(a===c||h===c&&e===c)return c;a===b?i.corner=new g.Corner(h):a.string||(i.corner=new g.Corner(a),i.corner.fixed=b),m.corner=new g.Corner(i.corner.string());return i.corner.string()!=="centercenter"},detectColours:function(b){var c,d,e,g=k.tip.css("cssText",""),h=b||i.corner,m=h[h.precedance],p="border-"+m+"-color",r="border"+m.charAt(0)+m.substr(1)+"Color",s=/rgba?\(0, 0, 0(, 0)?\)|transparent|#123456/i,t="background-color",u="transparent",v=" !important",w=a(document.body).css("color"),x=f.elements.content.css("color"),y=k.titlebar&&(h.y==="top"||h.y==="center"&&g.position().top+n.height/2+j.offset<k.titlebar.outerHeight(1)),z=y?k.titlebar:k.content;l.addClass(q),o.fill=d=g.css(t),o.border=e=g[0].style[r]||g.css(p)||l.css(p);if(!d||s.test(d))o.fill=z.css(t)||u,s.test(o.fill)&&(o.fill=l.css(t)||d);if(!e||s.test(e)||e===w)o.border=z.css(p)||u,s.test(o.border)&&(o.border=e);a("*",g).add(g).css("cssText",t+":"+u+v+";border:0"+v+";"),l.removeClass(q)},create:function(){var b=n.width,c=n.height,d;k.tip&&k.tip.remove(),k.tip=a("<div />",{"class":"ui-tooltip-tip"}).css({width:b,height:c}).prependTo(l),s?a("<canvas />").appendTo(k.tip)[0].getContext("2d").save():(d='<vml:shape coordorigin="0,0" style="display:inline-block; position:absolute; behavior:url(#default#VML);"></vml:shape>',k.tip.html(d+d),a("*",k.tip).bind("click mousedown",function(a){a.stopPropagation()}))},update:function(e,f){var h=k.tip,l=h.children(),q=n.width,r=n.height,t="px solid ",v="px dashed transparent",x=j.mimic,y=Math.round,z,B,C,D,E;e||(e=m.corner||i.corner),x===c?x=e:(x=new g.Corner(x),x.precedance=e.precedance,x.x==="inherit"?x.x=e.x:x.y==="inherit"?x.y=e.y:x.x===x.y&&(x[e.precedance]=e[e.precedance])),z=x.precedance,i.detectColours(e),o.border!=="transparent"&&o.border!=="#123456"?(p=u(e,d,b),j.border===0&&p>0&&(o.fill=o.border),i.border=p=j.border!==b?j.border:p):i.border=p=0,C=A(x,q,r),i.size=E=w(e),h.css(E),e.precedance==="y"?D=[y(x.x==="left"?p:x.x==="right"?E.width-q-p:(E.width-q)/2),y(x.y==="top"?E.height-r:0)]:D=[y(x.x==="left"?E.width-q:0),y(x.y==="top"?p:x.y==="bottom"?E.height-r-p:(E.height-r)/2)],s?(l.attr(E),B=l[0].getContext("2d"),B.restore(),B.save(),B.clearRect(0,0,3e3,3e3),B.translate(D[0],D[1]),B.beginPath(),B.moveTo(C[0][0],C[0][1]),B.lineTo(C[1][0],C[1][1]),B.lineTo(C[2][0],C[2][1]),B.closePath(),B.fillStyle=o.fill,B.strokeStyle=o.border,B.lineWidth=p*2,B.lineJoin="miter",B.miterLimit=100,p&&B.stroke(),B.fill()):(C="m"+C[0][0]+","+C[0][1]+" l"+C[1][0]+","+C[1][1]+" "+C[2][0]+","+C[2][1]+" xe",D[2]=p&&/^(r|b)/i.test(e.string())?parseFloat(a.browser.version,10)===8?2:1:0,l.css({antialias:""+(x.string().indexOf("center")>-1),left:D[0]-D[2]*Number(z==="x"),top:D[1]-D[2]*Number(z==="y"),width:q+p,height:r+p}).each(function(b){var c=a(this);c[c.prop?"prop":"attr"]({coordsize:q+p+" "+(r+p),path:C,fillcolor:o.fill,filled:!!b,stroked:!b}).css({display:p||b?"block":"none"}),!b&&c.html()===""&&c.html('<vml:stroke weight="'+p*2+'px" color="'+o.border+'" miterlimit="1000" joinstyle="miter"  style="behavior:url(#default#VML); display:inline-block;" />')})),f!==c&&i.position(e)},position:function(d){var e=k.tip,f={},g=Math.max(0,j.offset),h,l,m;if(j.corner===c||!e)return c;d=d||i.corner,h=d.precedance,l=w(d),m=[d.x,d.y],h==="x"&&m.reverse(),a.each(m,function(a,c){var e,i;c==="center"?(e=h==="y"?"left":"top",f[e]="50%",f["margin-"+e]=-Math.round(l[h==="y"?"width":"height"]/2)+g):(e=u(d,c,b),i=v(d),f[c]=a?p?u(d,c):0:g+(i>e?i:0))}),f[d[h]]-=l[h==="x"?"width":"height"],e.css({top:"",bottom:"",left:"",right:"",margin:""}).css(f);return f},destroy:function(){k.tip&&k.tip.remove(),l.unbind(r)}}),i.init()}function A(a,b,c){var d=Math.ceil(b/2),e=Math.ceil(c/2),f={bottomright:[[0,0],[b,c],[b,0]],bottomleft:[[0,0],[b,0],[0,c]],topright:[[0,c],[b,0],[b,c]],topleft:[[0,0],[0,c],[b,c]],topcenter:[[0,c],[d,0],[b,c]],bottomcenter:[[0,0],[b,0],[d,c]],rightcenter:[[0,0],[b,e],[0,c]],leftcenter:[[b,0],[b,c],[0,e]]};f.lefttop=f.bottomright,f.righttop=f.bottomleft,f.leftbottom=f.topright,f.rightbottom=f.topleft;return f[a.string()]}function z(d){var e=this,f=d.elements.tooltip,g=d.options.content.ajax,h=".qtip-ajax",i=/<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi,j=b,k=c,l;d.checks.ajax={"^content.ajax":function(a,b,c){b==="ajax"&&(g=c),b==="once"?e.init():g&&g.url?e.load():f.unbind(h)}},a.extend(e,{init:function(){g&&g.url&&f.unbind(h)[g.once?"one":"bind"]("tooltipshow"+h,e.load);return e},load:function(b,f){function r(a,b,c){!k&&a.status!==0&&d.set("content.text",b+": "+c)}function q(b){k||(m&&(b=a("<div/>").append(b.replace(i,"")).find(m)),d.set("content.text",b))}function p(){k||(n&&(d.show(b.originalEvent),f=c),a.isFunction(g.complete)&&g.complete.apply(this,arguments))}var h=g.url.indexOf(" "),j=g.url,m,n=g.once&&!g.loading&&f;if(n)try{b.preventDefault()}catch(o){}else if(b&&b.isDefaultPrevented())return e;l&&l.abort&&l.abort(),h>-1&&(m=j.substr(h),j=j.substr(0,h)),l=a.ajax(a.extend({success:q,error:r,context:d},g,{url:j,complete:p}))},destroy:function(){l&&l.abort&&l.abort(),k=b}}),e.init()}function y(e,h){var i,j,k,l,m,n=a(this),o=a(document.body),p=this===document?o:n,q=n.metadata?n.metadata(h.metadata):d,r=h.metadata.type==="html5"&&q?q[h.metadata.name]:d,s=n.data(h.metadata.name||"qtipopts");try{s=typeof s==="string"?(new Function("return "+s))():s}catch(u){v("Unable to parse HTML5 attribute data: "+s)}l=a.extend(b,{},f.defaults,h,typeof s==="object"?w(s):d,w(r||q)),j=l.position,l.id=e;if("boolean"===typeof l.content.text){k=n.attr(l.content.attr);if(l.content.attr!==c&&k)l.content.text=k;else{v("Unable to locate content for tooltip! Aborting render of tooltip on element: ",n);return c}}j.container.length||(j.container=o),j.target===c&&(j.target=p),l.show.target===c&&(l.show.target=p),l.show.solo===b&&(l.show.solo=j.container.closest("body")),l.hide.target===c&&(l.hide.target=p),l.position.viewport===b&&(l.position.viewport=j.container),j.at=new g.Corner(j.at),j.my=new g.Corner(j.my);if(a.data(this,"qtip"))if(l.overwrite)n.qtip("destroy");else if(l.overwrite===c)return c;l.suppress&&(m=a.attr(this,"title"))&&a(this).removeAttr("title").attr(t,m),i=new x(n,l,e,!!k),a.data(this,"qtip",i),n.bind("remove.qtip-"+e+" removeqtip.qtip-"+e,function(){i.destroy()});return i}function x(r,s,v,x){function Q(){var b=[s.show.target[0],s.hide.target[0],y.rendered&&F.tooltip[0],s.position.container[0],s.position.viewport[0],window,document];y.rendered?a([]).pushStack(a.grep(b,function(a){return typeof a==="object"})).unbind(E):s.show.target.unbind(E+"-create")}function P(){function o(a){D.is(":visible")&&y.reposition(a)}function n(a){if(D.hasClass(l))return c;clearTimeout(y.timers.inactive),y.timers.inactive=setTimeout(function(){y.hide(a)},s.hide.inactive)}function k(b){if(D.hasClass(l)||B||C)return c;var f=a(b.relatedTarget||b.target),g=f.closest(m)[0]===D[0],h=f[0]===e.show[0];clearTimeout(y.timers.show),clearTimeout(y.timers.hide);if(d.target==="mouse"&&g||s.hide.fixed&&(/mouse(out|leave|move)/.test(b.type)&&(g||h)))try{b.preventDefault(),b.stopImmediatePropagation()}catch(i){}else s.hide.delay>0?y.timers.hide=setTimeout(function(){y.hide(b)},s.hide.delay):y.hide(b)}function j(a){if(D.hasClass(l))return c;clearTimeout(y.timers.show),clearTimeout(y.timers.hide);var d=function(){y.toggle(b,a)};s.show.delay>0?y.timers.show=setTimeout(d,s.show.delay):d()}var d=s.position,e={show:s.show.target,hide:s.hide.target,viewport:a(d.viewport),document:a(document),body:a(document.body),window:a(window)},g={show:a.trim(""+s.show.event).split(" "),hide:a.trim(""+s.hide.event).split(" ")},i=a.browser.msie&&parseInt(a.browser.version,10)===6;D.bind("mouseenter"+E+" mouseleave"+E,function(a){var b=a.type==="mouseenter";b&&y.focus(a),D.toggleClass(p,b)}),s.hide.fixed&&(e.hide=e.hide.add(D),D.bind("mouseover"+E,function(){D.hasClass(l)||clearTimeout(y.timers.hide)})),/mouse(out|leave)/i.test(s.hide.event)?s.hide.leave==="window"&&e.window.bind("mouseout"+E+" blur"+E,function(a){/select|option/.test(a.target)&&!a.relatedTarget&&y.hide(a)}):/mouse(over|enter)/i.test(s.show.event)&&e.hide.bind("mouseleave"+E,function(a){clearTimeout(y.timers.show)}),(""+s.hide.event).indexOf("unfocus")>-1&&d.container.closest("html").bind("mousedown"+E,function(b){var c=a(b.target),d=!D.hasClass(l)&&D.is(":visible"),e=c.parents(m).filter(D[0]).length>0;c[0]!==r[0]&&c[0]!==D[0]&&!e&&!r.has(c[0]).length&&!c.attr("disabled")&&y.hide(b)}),"number"===typeof s.hide.inactive&&(e.show.bind("qtip-"+v+"-inactive",n),a.each(f.inactiveEvents,function(a,b){e.hide.add(F.tooltip).bind(b+E+"-inactive",n)})),a.each(g.hide,function(b,c){var d=a.inArray(c,g.show),f=a(e.hide);d>-1&&f.add(e.show).length===f.length||c==="unfocus"?(e.show.bind(c+E,function(a){D.is(":visible")?k(a):j(a)}),delete g.show[d]):e.hide.bind(c+E,k)}),a.each(g.show,function(a,b){e.show.bind(b+E,j)}),"number"===typeof s.hide.distance&&e.show.add(D).bind("mousemove"+E,function(a){var b=G.origin||{},c=s.hide.distance,d=Math.abs;(d(a.pageX-b.pageX)>=c||d(a.pageY-b.pageY)>=c)&&y.hide(a)}),d.target==="mouse"&&(e.show.bind("mousemove"+E,function(a){h={pageX:a.pageX,pageY:a.pageY,type:"mousemove"}}),d.adjust.mouse&&(s.hide.event&&(D.bind("mouseleave"+E,function(a){(a.relatedTarget||a.target)!==e.show[0]&&y.hide(a)}),F.target.bind("mouseenter"+E+" mouseleave"+E,function(a){G.onTarget=a.type==="mouseenter"})),e.document.bind("mousemove"+E,function(a){G.onTarget&&!D.hasClass(l)&&D.is(":visible")&&y.reposition(a||h)}))),(d.adjust.resize||e.viewport.length)&&(a.event.special.resize?e.viewport:e.window).bind("resize"+E,o),(e.viewport.length||i&&D.css("position")==="fixed")&&e.viewport.bind("scroll"+E,o)}function O(b,d){function g(b){function i(e){e&&(delete h[e.src],clearTimeout(y.timers.img[e.src]),a(e).unbind(E)),a.isEmptyObject(h)&&(y.redraw(),d!==c&&y.reposition(G.event),b())}var g,h={};if((g=f.find("img[src]:not([height]):not([width])")).length===0)return i();g.each(function(b,c){if(h[c.src]===e){var d=0,f=3;(function g(){if(c.height||c.width||d>f)return i(c);d+=1,y.timers.img[c.src]=setTimeout(g,700)})(),a(c).bind("error"+E+" load"+E,function(){i(this)}),h[c.src]=c}})}var f=F.content;if(!y.rendered||!b)return c;a.isFunction(b)&&(b=b.call(r,G.event,y)||""),b.jquery&&b.length>0?f.empty().append(b.css({display:"block"})):f.html(b),y.rendered<0?D.queue("fx",g):(C=0,g(a.noop));return y}function N(b,d){var e=F.title;if(!y.rendered||!b)return c;a.isFunction(b)&&(b=b.call(r,G.event,y));if(b===c||!b&&b!=="")return J(c);b.jquery&&b.length>0?e.empty().append(b.css({display:"block"})):e.html(b),y.redraw(),d!==c&&y.rendered&&D.is(":visible")&&y.reposition(G.event)}function M(a){var b=F.button,d=F.title;if(!y.rendered)return c;a?(d||L(),K()):b.remove()}function L(){var c=A+"-title";F.titlebar&&J(),F.titlebar=a("<div />",{"class":j+"-titlebar "+(s.style.widget?"ui-widget-header":"")}).append(F.title=a("<div />",{id:c,"class":j+"-title","aria-atomic":b})).insertBefore(F.content).delegate(".ui-tooltip-close","mousedown keydown mouseup keyup mouseout",function(b){a(this).toggleClass("ui-state-active ui-state-focus",b.type.substr(-4)==="down")}).delegate(".ui-tooltip-close","mouseover mouseout",function(b){a(this).toggleClass("ui-state-hover",b.type==="mouseover")}),s.content.title.button?K():y.rendered&&y.redraw()}function K(){var b=s.content.title.button,d=typeof b==="string",e=d?b:"Close tooltip";F.button&&F.button.remove(),b.jquery?F.button=b:F.button=a("<a />",{"class":"ui-state-default ui-tooltip-close "+(s.style.widget?"":j+"-icon"),title:e,"aria-label":e}).prepend(a("<span />",{"class":"ui-icon ui-icon-close",html:"&times;"})),F.button.appendTo(F.titlebar).attr("role","button").click(function(a){D.hasClass(l)||y.hide(a);return c}),y.redraw()}function J(a){F.title&&(F.titlebar.remove(),F.titlebar=F.title=F.button=d,a!==c&&y.reposition())}function I(){var a=s.style.widget;D.toggleClass(k,a).toggleClass(n,s.style.def&&!a),F.content.toggleClass(k+"-content",a),F.titlebar&&F.titlebar.toggleClass(k+"-header",a),F.button&&F.button.toggleClass(j+"-icon",!a)}function H(a){var b=0,c,d=s,e=a.split(".");while(d=d[e[b++]])b<e.length&&(c=d);return[c||s,e.pop()]}var y=this,z=document.body,A=j+"-"+v,B=0,C=0,D=a(),E=".qtip-"+v,F,G;y.id=v,y.rendered=c,y.elements=F={target:r},y.timers={img:{}},y.options=s,y.checks={},y.plugins={},y.cache=G={event:{},target:a(),disabled:c,attr:x,onTarget:c},y.checks.builtin={"^id$":function(d,e,g){var h=g===b?f.nextid:g,i=j+"-"+h;h!==c&&h.length>0&&!a("#"+i).length&&(D[0].id=i,F.content[0].id=i+"-content",F.title[0].id=i+"-title")},"^content.text$":function(a,b,c){O(c)},"^content.title.text$":function(a,b,c){if(!c)return J();!F.title&&c&&L(),N(c)},"^content.title.button$":function(a,b,c){M(c)},"^position.(my|at)$":function(a,b,c){"string"===typeof c&&(a[b]=new g.Corner(c))},"^position.container$":function(a,b,c){y.rendered&&D.appendTo(c)},"^show.ready$":function(){y.rendered?y.toggle(b):y.render(1)},"^style.classes$":function(a,b,c){D.attr("class",j+" qtip ui-helper-reset "+c)},"^style.widget|content.title":I,"^events.(render|show|move|hide|focus|blur)$":function(b,c,d){D[(a.isFunction(d)?"":"un")+"bind"]("tooltip"+c,d)},"^(show|hide|position).(event|target|fixed|inactive|leave|distance|viewport|adjust)":function(){var a=s.position;D.attr("tracking",a.target==="mouse"&&a.adjust.mouse),Q(),P()}},a.extend(y,{render:function(d){if(y.rendered)return y;var e=s.content.text,f=s.content.title.text,h=s.position,i=a.Event("tooltiprender");a.attr(r[0],"aria-describedby",A),D=F.tooltip=a("<div/>",{id:A,"class":j+" qtip ui-helper-reset "+n+" "+s.style.classes+" "+j+"-pos-"+s.position.my.abbrev(),width:s.style.width||"",height:s.style.height||"",tracking:h.target==="mouse"&&h.adjust.mouse,role:"alert","aria-live":"polite","aria-atomic":c,"aria-describedby":A+"-content","aria-hidden":b}).toggleClass(l,G.disabled).data("qtip",y).appendTo(s.position.container).append(F.content=a("<div />",{"class":j+"-content",id:A+"-content","aria-atomic":b})),y.rendered=-1,B=C=1,f&&(L(),a.isFunction(f)||N(f,c)),a.isFunction(e)||O(e,c),y.rendered=b,I(),a.each(s.events,function(b,c){a.isFunction(c)&&D.bind(b==="toggle"?"tooltipshow tooltiphide":"tooltip"+b,c)}),a.each(g,function(){this.initialize==="render"&&this(y)}),P(),D.queue("fx",function(a){i.originalEvent=G.event,D.trigger(i,[y]),B=C=0,y.redraw(),(s.show.ready||d)&&y.toggle(b,G.event,c),a()});return y},get:function(a){var b,c;switch(a.toLowerCase()){case"dimensions":b={height:D.outerHeight(),width:D.outerWidth()};break;case"offset":b=g.offset(D,s.position.container);break;default:c=H(a.toLowerCase()),b=c[0][c[1]],b=b.precedance?b.string():b}return b},set:function(e,f){function m(a,b){var c,d,e;for(c in k)for(d in k[c])if(e=(new RegExp(d,"i")).exec(a))b.push(e),k[c][d].apply(y,b)}var g=/^position\.(my|at|adjust|target|container)|style|content|show\.ready/i,h=/^content\.(title|attr)|style/i,i=c,j=c,k=y.checks,l;"string"===typeof e?(l=e,e={},e[l]=f):e=a.extend(b,{},e),a.each(e,function(b,c){var d=H(b.toLowerCase()),f;f=d[0][d[1]],d[0][d[1]]="object"===typeof c&&c.nodeType?a(c):c,e[b]=[d[0],d[1],c,f],i=g.test(b)||i,j=h.test(b)||j}),w(s),B=C=1,a.each(e,m),B=C=0,D.is(":visible")&&y.rendered&&(i&&y.reposition(s.position.target==="mouse"?d:G.event),j&&y.redraw());return y},toggle:function(e,f){function q(){e?(a.browser.msie&&D[0].style.removeAttribute("filter"),D.css("overflow",""),"string"===typeof i.autofocus&&a(i.autofocus,D).focus(),p=a.Event("tooltipvisible"),p.originalEvent=f?G.event:d,D.trigger(p,[y]),i.target.trigger("qtip-"+v+"-inactive")):D.css({display:"",visibility:"",opacity:"",left:"",top:""})}if(!y.rendered)return e?y.render(1):y;var g=e?"show":"hide",i=s[g],j=D.is(":visible"),k=!f||s[g].target.length<2||G.target[0]===f.target,l=s.position,n=s.content,o,p;(typeof e).search("boolean|number")&&(e=!j);if(!D.is(":animated")&&j===e&&k)return y;if(f){if(/over|enter/.test(f.type)&&/out|leave/.test(G.event.type)&&f.target===s.show.target[0]&&D.has(f.relatedTarget).length)return y;G.event=a.extend({},f)}p=a.Event("tooltip"+g),p.originalEvent=f?G.event:d,D.trigger(p,[y,90]);if(p.isDefaultPrevented())return y;a.attr(D[0],"aria-hidden",!e),e?(G.origin=a.extend({},h),y.focus(f),a.isFunction(n.text)&&O(n.text,c),a.isFunction(n.title.text)&&N(n.title.text,c),!u&&l.target==="mouse"&&l.adjust.mouse&&(a(document).bind("mousemove.qtip",function(a){h={pageX:a.pageX,pageY:a.pageY,type:"mousemove"}}),u=b),y.reposition(f,arguments[2]),(p.solo=!!i.solo)&&a(m,i.solo).not(D).qtip("hide",p)):(clearTimeout(y.timers.show),delete G.origin,u&&!a(m+'[tracking="true"]:visible',i.solo).not(D).length&&(a(document).unbind("mousemove.qtip"),u=c),y.blur(f)),k&&D.stop(0,1),i.effect===c?(D[g](),q.call(D)):a.isFunction(i.effect)?(i.effect.call(D,y),D.queue("fx",function(a){q(),a()})):D.fadeTo(90,e?1:0,q),e&&i.target.trigger("qtip-"+v+"-inactive");return y},show:function(a){return y.toggle(b,a)},hide:function(a){return y.toggle(c,a)},focus:function(b){if(!y.rendered)return y;var c=a(m),d=parseInt(D[0].style.zIndex,10),e=f.zindex+c.length,g=a.extend({},b),h,i;D.hasClass(o)||(i=a.Event("tooltipfocus"),i.originalEvent=g,D.trigger(i,[y,e]),i.isDefaultPrevented()||(d!==e&&(c.each(function(){this.style.zIndex>d&&(this.style.zIndex=this.style.zIndex-1)}),c.filter("."+o).qtip("blur",g)),D.addClass(o)[0].style.zIndex=e));return y},blur:function(b){var c=a.extend({},b),d;D.removeClass(o),d=a.Event("tooltipblur"),d.originalEvent=c,D.trigger(d,[y]);return y},reposition:function(b,d){if(!y.rendered||B)return y;B=1;var e=s.position.target,f=s.position,i=f.my,k=f.at,l=f.adjust,m=l.method.split(" "),n=D.outerWidth(),o=D.outerHeight(),p=0,q=0,r=a.Event("tooltipmove"),t=D.css("position")==="fixed",u=f.viewport,v={left:0,top:0},w=f.container,x=c,A=y.plugins.tip,C={horizontal:m[0],vertical:m[1]=m[1]||m[0],enabled:u.jquery&&e[0]!==window&&e[0]!==z&&l.method!=="none",left:function(a){var b=C.horizontal==="shift",c=-w.offset.left+u.offset.left+u.scrollLeft,d=i.x==="left"?n:i.x==="right"?-n:-n/2,e=k.x==="left"?p:k.x==="right"?-p:-p/2,f=A&&A.size?A.size.width||0:0,g=A&&A.corner&&A.corner.precedance==="x"&&!b?f:0,h=c-a+g,j=a+n-u.width-c+g,m=d-(i.precedance==="x"||i.x===i.y?e:0)-(k.x==="center"?p/2:0),o=i.x==="center";b?(g=A&&A.corner&&A.corner.precedance==="y"?f:0,m=(i.x==="left"?1:-1)*d-g,v.left+=h>0?h:j>0?-j:0,v.left=Math.max(-w.offset.left+u.offset.left+(g&&A.corner.x==="center"?A.offset:0),a-m,Math.min(Math.max(-w.offset.left+u.offset.left+u.width,a+m),v.left))):(h>0&&(i.x!=="left"||j>0)?v.left-=m:j>0&&(i.x!=="right"||h>0)&&(v.left-=o?-m:m),v.left!==a&&o&&(v.left-=l.x),v.left<c&&-v.left>j&&(v.left=a));return v.left-a},top:function(a){var b=C.vertical==="shift",c=-w.offset.top+u.offset.top+u.scrollTop,d=i.y==="top"?o:i.y==="bottom"?-o:-o/2,e=k.y==="top"?q:k.y==="bottom"?-q:-q/2,f=A&&A.size?A.size.height||0:0,g=A&&A.corner&&A.corner.precedance==="y"&&!b?f:0,h=c-a+g,j=a+o-u.height-c+g,m=d-(i.precedance==="y"||i.x===i.y?e:0)-(k.y==="center"?q/2:0),n=i.y==="center";b?(g=A&&A.corner&&A.corner.precedance==="x"?f:0,m=(i.y==="top"?1:-1)*d-g,v.top+=h>0?h:j>0?-j:0,v.top=Math.max(-w.offset.top+u.offset.top+(g&&A.corner.x==="center"?A.offset:0),a-m,Math.min(Math.max(-w.offset.top+u.offset.top+u.height,a+m),v.top))):(h>0&&(i.y!=="top"||j>0)?v.top-=m:j>0&&(i.y!=="bottom"||h>0)&&(v.top-=n?-m:m),v.top!==a&&n&&(v.top-=l.y),v.top<0&&-v.top>j&&(v.top=a));return v.top-a}},E;if(a.isArray(e)&&e.length===2)k={x:"left",y:"top"},v={left:e[0],top:e[1]};else if(e==="mouse"&&(b&&b.pageX||G.event.pageX))k={x:"left",y:"top"},b=(b&&(b.type==="resize"||b.type==="scroll")?G.event:b&&b.pageX&&b.type==="mousemove"?b:h&&h.pageX&&(l.mouse||!b||!b.pageX)?{pageX:h.pageX,pageY:h.pageY}:!l.mouse&&G.origin&&G.origin.pageX&&s.show.distance?G.origin:b)||b||G.event||h||{},v={top:b.pageY,left:b.pageX};else{e==="event"?b&&b.target&&b.type!=="scroll"&&b.type!=="resize"?e=G.target=a(b.target):e=G.target:e=G.target=a(e.jquery?e:F.target),e=a(e).eq(0);if(e.length===0)return y;e[0]===document||e[0]===window?(p=g.iOS?window.innerWidth:e.width(),q=g.iOS?window.innerHeight:e.height(),e[0]===window&&(v={top:(u||e).scrollTop(),left:(u||e).scrollLeft()})):e.is("area")&&g.imagemap?v=g.imagemap(e,k,C.enabled?m:c):e[0].namespaceURI==="http://www.w3.org/2000/svg"&&g.svg?v=g.svg(e,k):(p=e.outerWidth(),q=e.outerHeight(),v=g.offset(e,w)),v.offset&&(p=v.width,q=v.height,x=v.flipoffset,v=v.offset);if(g.iOS<4.1&&g.iOS>3.1||g.iOS==4.3||!g.iOS&&t)E=a(window),v.left-=E.scrollLeft(),v.top-=E.scrollTop();v.left+=k.x==="right"?p:k.x==="center"?p/2:0,v.top+=k.y==="bottom"?q:k.y==="center"?q/2:0}v.left+=l.x+(i.x==="right"?-n:i.x==="center"?-n/2:0),v.top+=l.y+(i.y==="bottom"?-o:i.y==="center"?-o/2:0),C.enabled?(u={elem:u,height:u[(u[0]===window?"h":"outerH")+"eight"](),width:u[(u[0]===window?"w":"outerW")+"idth"](),scrollLeft:t?0:u.scrollLeft(),scrollTop:t?0:u.scrollTop(),offset:u.offset()||{left:0,top:0}},w={elem:w,scrollLeft:w.scrollLeft(),scrollTop:w.scrollTop(),offset:w.offset()||{left:0,top:0}},v.adjusted={left:C.horizontal!=="none"?C.left(v.left):0,top:C.vertical!=="none"?C.top(v.top):0},v.adjusted.left+v.adjusted.top&&D.attr("class",D[0].className.replace(/ui-tooltip-pos-\w+/i,j+"-pos-"+i.abbrev())),x&&v.adjusted.left&&(v.left+=x.left),x&&v.adjusted.top&&(v.top+=x.top)):v.adjusted={left:0,top:0},r.originalEvent=a.extend({},b),D.trigger(r,[y,v,u.elem||u]);if(r.isDefaultPrevented())return y;delete v.adjusted,d===c||isNaN(v.left)||isNaN(v.top)||e==="mouse"||!a.isFunction(f.effect)?D.css(v):a.isFunction(f.effect)&&(f.effect.call(D,y,a.extend({},v)),D.queue(function(b){a(this).css({opacity:"",height:""}),a.browser.msie&&this.style.removeAttribute("filter"),b()})),B=0;return y},redraw:function(){if(y.rendered<1||C)return y;var a=s.position.container,b,c,d,e;C=1,s.style.height&&D.css("height",s.style.height),s.style.width?D.css("width",s.style.width):(D.css("width","").addClass(q),c=D.width()+1,d=D.css("max-width")||"",e=D.css("min-width")||"",b=(d+e).indexOf("%")>-1?a.width()/100:0,d=(d.indexOf("%")>-1?b:1)*parseInt(d,10)||c,e=(e.indexOf("%")>-1?b:1)*parseInt(e,10)||0,c=d+e?Math.min(Math.max(c,e),d):c,D.css("width",Math.round(c)).removeClass(q)),C=0;return y},disable:function(b){"boolean"!==typeof b&&(b=!D.hasClass(l)&&!G.disabled),y.rendered?(D.toggleClass(l,b),a.attr(D[0],"aria-disabled",b)):G.disabled=!!b;return y},enable:function(){return y.disable(c)},destroy:function(){var b=r[0],c=a.attr(b,t),d=r.data("qtip");y.rendered&&(D.stop(1,0).remove(),a.each(y.plugins,function(){this.destroy&&this.destroy()})),clearTimeout(y.timers.show),clearTimeout(y.timers.hide),Q();if(!d||y===d)a.removeData(b,"qtip"),s.suppress&&c&&(a.attr(b,"title",c),r.removeAttr(t)),r.removeAttr("aria-describedby");r.unbind(".qtip-"+v),delete i[y.id];return r}})}function w(b){var e;if(!b||"object"!==typeof b)return c;if(b.metadata===d||"object"!==typeof b.metadata)b.metadata={type:b.metadata};if("content"in b){if(b.content===d||"object"!==typeof b.content||b.content.jquery)b.content={text:b.content};e=b.content.text||c,!a.isFunction(e)&&(!e&&!e.attr||e.length<1||"object"===typeof e&&!e.jquery)&&(b.content.text=c);if("title"in b.content){if(b.content.title===d||"object"!==typeof b.content.title)b.content.title={text:b.content.title};e=b.content.title.text||c,!a.isFunction(e)&&(!e&&!e.attr||e.length<1||"object"===typeof e&&!e.jquery)&&(b.content.title.text=c)}}if("position"in b)if(b.position===d||"object"!==typeof b.position)b.position={my:b.position,at:b.position};if("show"in b)if(b.show===d||"object"!==typeof b.show)b.show.jquery?b.show={target:b.show}:b.show={event:b.show};if("hide"in b)if(b.hide===d||"object"!==typeof b.hide)b.hide.jquery?b.hide={target:b.hide}:b.hide={event:b.hide};if("style"in b)if(b.style===d||"object"!==typeof b.style)b.style={classes:b.style};a.each(g,function(){this.sanitize&&this.sanitize(b)});return b}function v(){v.history=v.history||[],v.history.push(arguments);if("object"===typeof console){var a=console[console.warn?"warn":"log"],b=Array.prototype.slice.call(arguments),c;typeof arguments[0]==="string"&&(b[0]="qTip2: "+b[0]),c=a.apply?a.apply(console,b):a(b)}}"use strict";var b=!0,c=!1,d=null,e,f,g,h,i={},j="ui-tooltip",k="ui-widget",l="ui-state-disabled",m="div.qtip."+j,n=j+"-default",o=j+"-focus",p=j+"-hover",q=j+"-fluid",r="-31000px",s="_replacedByqTip",t="oldtitle",u;f=a.fn.qtip=function(g,h,i){var j=(""+g).toLowerCase(),k=d,l=a.makeArray(arguments).slice(1),m=l[l.length-1],n=this[0]?a.data(this[0],"qtip"):d;if(!arguments.length&&n||j==="api")return n;if("string"===typeof g){this.each(function(){var d=a.data(this,"qtip");if(!d)return b;m&&m.timeStamp&&(d.cache.event=m);if(j!=="option"&&j!=="options"||!h)d[j]&&d[j].apply(d[j],l);else if(a.isPlainObject(h)||i!==e)d.set(h,i);else{k=d.get(h);return c}});return k!==d?k:this}if("object"===typeof g||!arguments.length){n=w(a.extend(b,{},g));return f.bind.call(this,n,m)}},f.bind=function(d,j){return this.each(function(k){function r(b){function d(){p.render(typeof b==="object"||l.show.ready),m.show.add(m.hide).unbind(o)}if(p.cache.disabled)return c;p.cache.event=a.extend({},b),p.cache.target=b?a(b.target):[e],l.show.delay>0?(clearTimeout(p.timers.show),p.timers.show=setTimeout(d,l.show.delay),n.show!==n.hide&&m.hide.bind(n.hide,function(){clearTimeout(p.timers.show)})):d()}var l,m,n,o,p,q;q=a.isArray(d.id)?d.id[k]:d.id,q=!q||q===c||q.length<1||i[q]?f.nextid++:i[q]=q,o=".qtip-"+q+"-create",p=y.call(this,q,d);if(p===c)return b;l=p.options,a.each(g,function(){this.initialize==="initialize"&&this(p)}),m={show:l.show.target,hide:l.hide.target},n={show:a.trim(""+l.show.event).replace(/ /g,o+" ")+o,hide:a.trim(""+l.hide.event).replace(/ /g,o+" ")+o},/mouse(over|enter)/i.test(n.show)&&!/mouse(out|leave)/i.test(n.hide)&&(n.hide+=" mouseleave"+o),m.show.bind("mousemove"+o,function(a){h={pageX:a.pageX,pageY:a.pageY,type:"mousemove"},p.cache.onTarget=b}),m.show.bind(n.show,r),(l.show.ready||l.prerender)&&r(j)})},g=f.plugins={Corner:function(a){a=(""+a).replace(/([A-Z])/," $1").replace(/middle/gi,"center").toLowerCase(),this.x=(a.match(/left|right/i)||a.match(/center/)||["inherit"])[0].toLowerCase(),this.y=(a.match(/top|bottom|center/i)||["inherit"])[0].toLowerCase();var b=a.charAt(0);this.precedance=b==="t"||b==="b"?"y":"x",this.string=function(){return this.precedance==="y"?this.y+this.x:this.x+this.y},this.abbrev=function(){var a=this.x.substr(0,1),b=this.y.substr(0,1);return a===b?a:a==="c"||a!=="c"&&b!=="c"?b+a:a+b},this.clone=function(){return{x:this.x,y:this.y,precedance:this.precedance,string:this.string,abbrev:this.abbrev,clone:this.clone}}},offset:function(b,c){function j(a,b){d.left+=b*a.scrollLeft(),d.top+=b*a.scrollTop()}var d=b.offset(),e=b.closest("body")[0],f=c,g,h,i;if(f){do f.css("position")!=="static"&&(h=f.position(),d.left-=h.left+(parseInt(f.css("borderLeftWidth"),10)||0)+(parseInt(f.css("marginLeft"),10)||0),d.top-=h.top+(parseInt(f.css("borderTopWidth"),10)||0)+(parseInt(f.css("marginTop"),10)||0),!g&&(i=f.css("overflow"))!=="hidden"&&i!=="visible"&&(g=f));while((f=a(f[0].offsetParent)).length);g&&g[0]!==e&&j(g,1)}return d},iOS:parseFloat((""+(/CPU.*OS ([0-9_]{1,3})|(CPU like).*AppleWebKit.*Mobile/i.exec(navigator.userAgent)||[0,""])[1]).replace("undefined","3_2").replace("_","."))||c,fn:{attr:function(b,c){if(this.length){var d=this[0],e="title",f=a.data(d,"qtip");if(b===e&&f&&"object"===typeof f&&f.options.suppress){if(arguments.length<2)return a.attr(d,t);f&&f.options.content.attr===e&&f.cache.attr&&f.set("content.text",c);return this.attr(t,c)}}return a.fn["attr"+s].apply(this,arguments)},clone:function(b){var c=a([]),d="title",e=a.fn["clone"+s].apply(this,arguments);b||e.filter("["+t+"]").attr("title",function(){return a.attr(this,t)}).removeAttr(t);return e}}},a.each(g.fn,function(c,d){if(!d||a.fn[c+s])return b;var e=a.fn[c+s]=a.fn[c];a.fn[c]=function(){return d.apply(this,arguments)||e.apply(this,arguments)}}),a.ui||(a["cleanData"+s]=a.cleanData,a.cleanData=function(b){for(var c=0,d;(d=b[c])!==e;c++)try{a(d).triggerHandler("removeqtip")}catch(f){}a["cleanData"+s](b)}),f.version="nightly",f.nextid=0,f.inactiveEvents="click dblclick mousedown mouseup mousemove mouseleave mouseenter".split(" "),f.zindex=15e3,f.defaults={prerender:c,id:c,overwrite:b,suppress:b,content:{text:b,attr:"title",title:{text:c,button:c}},position:{my:"top left",at:"bottom right",target:c,container:c,viewport:c,adjust:{x:0,y:0,mouse:b,resize:b,method:"flip flip"},effect:function(b,d,e){a(this).animate(d,{duration:200,queue:c})}},show:{target:c,event:"mouseenter",effect:b,delay:90,solo:c,ready:c,autofocus:c},hide:{target:c,event:"mouseleave",effect:b,delay:0,fixed:c,inactive:c,leave:"window",distance:c},style:{classes:"",widget:c,width:c,height:c,def:b},events:{render:d,move:d,show:d,hide:d,toggle:d,visible:d,focus:d,blur:d}},g.ajax=function(a){var b=a.plugins.ajax;return"object"===typeof b?b:a.plugins.ajax=new z(a)},g.ajax.initialize="render",g.ajax.sanitize=function(a){var b=a.content,c;b&&"ajax"in b&&(c=b.ajax,typeof c!=="object"&&(c=a.content.ajax={url:c}),"boolean"!==typeof c.once&&c.once&&(c.once=!!c.once))},a.extend(b,f.defaults,{content:{ajax:{loading:b,once:b}}}),g.tip=function(a){var b=a.plugins.tip;return"object"===typeof b?b:a.plugins.tip=new B(a)},g.tip.initialize="render",g.tip.sanitize=function(a){var c=a.style,d;c&&"tip"in c&&(d=a.style.tip,typeof d!=="object"&&(a.style.tip={corner:d}),/string|boolean/i.test(typeof d.corner)||(d.corner=b),typeof d.width!=="number"&&delete d.width,typeof d.height!=="number"&&delete d.height,typeof d.border!=="number"&&d.border!==b&&delete d.border,typeof d.offset!=="number"&&delete d.offset)},a.extend(b,f.defaults,{style:{tip:{corner:b,mimic:c,width:6,height:6,border:b,offset:0}}})})



function setCookie(c_name,value,exdays) {
	var exdate=new Date();
	exdate.setDate(exdate.getDate() + exdays);
	var c_value=escape(value) + ((exdays==null) ? "" : "; expires="+exdate.toUTCString());
	document.cookie=c_name + "=" + c_value;
}

function getCookie(c_name) {
	var i,x,y,ARRcookies=document.cookie.split(";");
	for (i=0;i<ARRcookies.length;i++) {
		x=ARRcookies[i].substr(0,ARRcookies[i].indexOf("="));
		y=ARRcookies[i].substr(ARRcookies[i].indexOf("=")+1);
		x=x.replace(/^\s+|\s+$/g,"");
		if (x==c_name) return unescape(y);
		}
}

// http://ejohn.org/blog/fast-javascript-maxmin/
/*
Array.max = function( array ){
    return Math.max.apply( Math, array );
};

Array.min = function( array ){
    return Math.min.apply( Math, array );
};
*/

// current epoch in seconds
function currentEpoch() {
	return Math.round(new Date().getTime()/1000.0);
}

/*

	***********************************
	*** OB Initialization / Globals ***
	***********************************

*/

var $threadReplyTextArea; 
var $characterCount;
var previousSelectedText;

$(function() {
	obToolbar.initToolbar();
	
	// TODO: only do toolbar autocomplete stuff if logged in
	obToolbar.initAutocomplete();
	
	// TODO: only do toolbar notifications stuff if logged in
	if (toolbarNotificationsJson) {
		obToolbar.notifications = $.parseJSON(toolbarNotificationsJson).notifications;
		obToolbar.renderNotifications(obToolbar.notifications);
	}
	if (toolbarMessagesJson) {
		obToolbar.messages = $.parseJSON(toolbarMessagesJson).messages;
		obToolbar.renderMessages(obToolbar.messages);
	}
	if (obToolbar.messages || obToolbar.notifications) obToolbar.updateToolbarBadges();
	initGenericClickHandlers();
	initMemberTooltips();
	initForumControls();
});

function updateReplyCharacterCount($elementToCount, $resultElement, maxChars) {
	var chars = $elementToCount.val().length;
	$resultElement.text(chars + ' / ' + maxChars + ' characters');
	if (chars / maxChars < 0.75) 
		$resultElement.removeClass('almostFull').removeClass('full');
	else if (chars >= maxChars) 
		$resultElement.removeClass('almostFull').addClass('full');
	else 
		$resultElement.removeClass('full').addClass('almostFull');
}

function initForumControls() {
	if (!$('form#threadReply').length) return; // if the reply form isn't there, there's nothing to really do here!
	
	log('init\'ing forum controls');
	$threadReplyTextArea = $('form#threadReply  textarea');
	
	$('.postControls > .quote').click(function(e) {
		var $target = $(e.target);
		e.preventDefault();
		var quoteText = $(e.target).parent().parent().children('.replyBody').html();
		quoteText = $.trim(quoteText);
		quoteText = quoteText.replace(/<div class="addendum">(.*?)<\/div>/g,'$1'); // get rid of the addendum tags; preserve what's inside
		quoteText = quoteText.replace(/<blockquote>.*?<\/blockquote>/gm,'\n'); // remove blockquotes entirely
		quoteText = quoteText.replace(/<br>/g,'\n');	
		quoteText = quoteText.replace(/(<([^>]+)>)/ig,"") // strip the rest of the HTML
		quoteText = quoteText.replace(/ +\n/g,'\n'); // replace spaces preceding newlines
		quoteText = quoteText.replace(/\n{3,}/g,'\n\n'); // if 3+ consecutive newlines, replace w/ 2 newlines
		quoteText = quoteText.replace(/ {2,}/g,' '); // condense spaces
		quoteText = $.trim(quoteText);
		if (quoteText.length > 240) quoteText = quoteText.substring(0,240) + '…';
		
		quoteText = '[quote=' + $(e.target).data('login') + '] ' + quoteText + ' [/quote]\n\n';
		
		/*
		is the beginning of the textarea already equal to the value we're about to prepend? if so, 
		they probably double-clicked and don't actually want to paste it twice
		*/
		if ($threadReplyTextArea.val().substring(0,quoteText.length) != quoteText) {
			$threadReplyTextArea.val(quoteText + $threadReplyTextArea.val());
			document.getElementById('threadReply').scrollIntoView();
			updateReplyCharacterCount($threadReplyTextArea, $characterCount, 8000);
			$threadReplyTextArea.focus();
		}		
	});
	
	$characterCount = $('#threadReply span.replyCharacterCount');
	updateReplyCharacterCount($threadReplyTextArea, $characterCount, 8000); // because the textarea might get auto-populated text when they load the page
	
	$('#threadReply textarea').keyup(function(e) {
		updateReplyCharacterCount($threadReplyTextArea, $characterCount, 8000)
	});
	
	$('#threadReplyFormatControls button').click(function(e) {
		e.preventDefault();
		log('You probably want to format something!');
		var tag;
		switch (e.target.id) {
			case 'replyItalic':
				tag='i';
				break;
			case 'replyBold':
				tag='b';
				break;
			case 'replyCode':
				tag='code';
				break;
			case 'replyQuote':
				tag='quote';
				break;
		}
		// is there a selection inside the textarea?
		var selectionStart = $threadReplyTextArea[0].selectionStart;
		var selectionEnd = $threadReplyTextArea[0].selectionEnd;
		var val = $threadReplyTextArea.val();
		$threadReplyTextArea.val(val.substring(0,selectionStart) + '[' + tag + ']' + val.substring(selectionStart, selectionEnd) + '[/' + tag + ']' + val.substring(selectionEnd, 99999));
		$threadReplyTextArea.focus();	
	});
}


// TODO: Don't do this if it's a touch interface
function initMemberTooltips() {
	log('init\'ing member tooltips');
	$('a[data-id_member]').each(function() {
		$(this).qtip({
			content: {
				text: 'Loading',
				ajax: {
					url: '/webservices/json/member_tooltip.asp', // URL to the JSON script
					type: 'GET', // POST or GET
					data: { id: $(this).data('id_member') }, // Data to pass along with your request
					dataType: 'json', // Tell it we're retrieving JSON
					success: function(data, status) {
						if (status) {
							var content = ' My name is ' + data[0].login;
		 					this.set('content.text', memberTooltip(data, status)); 				// Now we set the content manually (required!)
		 				}
		 				else {
		 					this.set('content.text', 'Whoops!')
		 				}
					}
				}
			},
			position: {
				my: 'top left',
				target: 'mouse',
				viewport: $(window), // Keep it on-screen at all times if possible
				adjust: {
					x: 10,  y: 10
				} 
			},
			style: {
				classes: 'ui-tooltip-shadow ui-tooltip-rounded ui-tooltip-light'
			}
		});
	});
}

var tooltipTemplate = "\
	{{{img_tag}}}\
	<h3>{{login}} <small>{{age}}/{{gender}}</small></h3><p> \
	{{#city}}{{city}}, {{state}} {{country_if_not_usa}} {{#distancem}}({{distancem}}mi from you){{/distancem}}<br><br>{{/city}} \
	{{#current_relationship_description_others}}<strong>Currently:</strong> {{current_relationship_description_others}}<br>{{/current_relationship_description_others}} \
	{{#relationship_desired_description_others}}<strong>Looking For:</strong> {{relationship_desired_description_others}}<br>{{/relationship_desired_description_others}} \
	{{#gender_preference}}<strong>Interested In:</strong> {{gender_preference}}<br>{{/gender_preference}}\
	{{#logins_previous}}<strong>Previously Known As:</strong> {{logins_previous}}<br>{{/logins_previous}}\
	<strong>Joined:</strong> {{joined_site_relative}}<br>\
	<strong>Last Active:</strong> {{last_active_relative}}<br>\
	<strong>Viewed Your Profile:</strong> {{last_profile_view_relative}}<br>\
	</p>{{#about_self}}<p><em>&ldquo;{{about_self}}&rdquo;</em></p>{{/about_self}}\
	\
";

function memberTooltip(data, status) {
	data[0].img_tag = memberImgLinked(data[0], '150');
	return $.mustache(tooltipTemplate, data[0]);
}

// Just for my debugging purposes, because I am slow
function logEventInformation(e) {
	if (e.currentTarget==e.target)
		log('event came from [' + e.target.id + ']');
	else
		log('event started at [' + e.target.id + '] and bubbled to [' + e.currentTarget.id + ']');
}


/*
Rather than putting extra links all over the place, assume anything with
"data-id-thread" or "data-id-post" AND no "noNavigate" class is something 
that should navigate somewhere
*/
function initGenericClickHandlers() {
	log('init\'ing generic click handlers');
	
	$('[data-id-member-navigate]').click(function(e) {
		logEventInformation(e);
		log('You clicky for member ' + $(this).data('id-member-navigate'));
		window.location.href=member_profile_path($(this).data('id-member-navigate'));
	});
	
	$('[data-id-thread-navigate]').click(function(e) {
		logEventInformation(e);
		log('You clicky for thread ' + $(this).data('id-thread-navigate'));
		window.location.href=threadPath($(this).data('id-thread-navigate'));
	});
	
	$('[data-id-post-navigate]').click(function(e) {
		logEventInformation(e);
		log('You clicky for post ' + $(this).data('id-post-navigate'));
		window.location.href=postPath($(this).data('id-post-navigate'));
	});
	
	$('[data-id-message-navigate]').click(function(e) {
		logEventInformation(e);
		log('You clicky for message ' + $(this).data('id-message-navigate'));
		window.location.href=postPath($(this).data('id-message-navigate'));
	});
	
	$('[data-id-comment-navigate]').click(function(e) {
		logEventInformation(e);
		log('You clicky for comment ' + $(this).data('id-message-comment'));
		window.location.href=postPath($(this).data('id-message-comment'));
	});
}

/*

	****************************
	*** OB Toolbar Functions ***
	****************************
	
*/

var obToolbar = {
	cookieTTLDays: 30,
	windowShadeShowDelayMs: 500,			// ms to wait before showing windowshade when user mouses over toolbar.  setting too low will lead to accidental activation
	windowShadeHideDelayMs: 300,			// ms to wait before hiding windowshade when user mouses out, etc.
	windowShadeSlideUpDurationMs: 100,   	
	windowShadeSlideDownDurationMs: 200,
	windowShadeShowTimeout: undefined,				// timeout for windowshade display animation
	windowShadeHideTimeout:	undefined,			// timeout for windowshade hide animation
	inToolbar: false, 				// track whether or not mouse is in toolbar
	inWindowShade: false,		
	defaultSearchData: [{ noresults : true, label: "No results.", value:null }]
}


// Initialize OB toolbar 
obToolbar.initToolbar = function() {
	obToolbar.$toolbar=$('#obToolbar');
	obToolbar.$toolbarButtons=$('#obToolbar ul li');	
	obToolbar.$notificationList = $('#notificationWindowShade ul');
	obToolbar.$messageList = $('#messageWindowShade ul');	
	obToolbar.$unseenMessagesBadge = $('#unseenMessagesBadge');
	obToolbar.$unseenNotificationsBadge = $('#unseenNotificationsBadge');
	obToolbar.lastMessageSeen = lastMessageSeen || 0;
	obToolbar.lastNotificationSeen = lastNotificationSeen || 0;
	obToolbar.unseenMessageCount = unseenMessageCount || 0;
	obToolbar.unseenNotificationCount = unseenNotificationCount || 0;
	
	// position the WIDE leaves.  get toolbar offset, set all leaves so that their tops line up with the bottom of the toolbar
	var offset = obToolbar.$toolbar.offset();
	offset.top += obToolbar.$toolbar.outerHeight();
	offset.left = 5;  // i hav eno idea why this was necessary.  it was defaulting to -5 for some reason.  this makes it 0
	$('section.windowShade.wide').offset(offset);
	
	// position the NARROW leaves. these are centered over their parent buttons and have fixed width
	$('#toolbarNav li ').each(function(index) {
    	var $shade= $($(this).data('windowshade'));
    	if ($shade.hasClass('narrow')) {
    		offset.left = $(this).offset().left + ($(this).width()/2) - ($shade.width()/2)
    		$shade.offset(offset);
    	}
	});
	
	// event handlers
	$('#obToolbar ul').mouseleave(function() {
			obToolbar.inToolbar=false;
		}
	).mouseenter(function() {
			obToolbar.inToolbar=true;
		}
	);
	
	// clicks from inside the toolbar should not bubble up
	obToolbar.$toolbar.click(function(e) {
		e.stopPropagation();
	})
	
	// clicks from elsewhere on the page should bubble up to the body and cause the windowshades to shut
	$('body').click(function(e) {
		obToolbar.hideWindowShades('click that bubbled up',false);
	});
	
	// create event handlers for each windowshade.  each toolbar button should have the name of its corresponding windowshade in a "data-windowshade" attribute
	// ex: <li id="themeLi" data-windowShade="#themeWindowShade" style="width:60px"><a id="tbThemes" href="">Themes</a></li>
	$windowShades=$('section.windowShade');
	var $toolBarLinks = $('#obToolbar > ul > li > a');				// todo: should we just reference the UL's directly?
	for (var i=0; i<$toolBarLinks.length; i++) {
		var $anchor 			= $($toolBarLinks[i]);			// link in this toolbar li  
		var $anchorParent 		= $anchor.parent();					// the li
		var $windowShade		= $($anchorParent.data('windowshade'));	// leaf belonging to this li
		
		// we make the <ul> clickable, not the <a>, because it's a larger clickable area
		$anchorParent.click(function(e) {
			e.preventDefault()
			e.stopPropagation();
			clearTimeout(obToolbar.windowShadeShowTimeout);
			clearTimeout(obToolbar.windowShadeHideTimeout);
			obToolbar.showWindowShade(this,'from a click');
			if ((e.target.id=='tbNotifications') ||(e.target.id=='notificationLi')) 
				obToolbar.syncNotificationsToServer();
			else if ((e.target.id=='tbMessages') ||(e.target.id=='messageLi')) 
				obToolbar.syncMessagesToServer();
			
		});
		
		$anchorParent.mouseenter(function(e) {
			if (obToolbar.$activeWindowShade)
				obToolbar.showWindowShade(this,'moved here from another toolbar');
			else
				obToolbar.windowShadeShowTimeout = setTimeout(function() { 
					if (e.currentTarget.id=='notificationLi') 
						obToolbar.syncNotificationsToServer(); 
					else if (e.currentTarget.id=='messageLi')
						obToolbar.syncMessagesToServer(); 
					obToolbar.showWindowShade(e.currentTarget,'delayed show'); 
				}, obToolbar.windowShadeShowDelayMs);	
			clearTimeout(obToolbar.windowShadeHideTimeout);
		});
		
		$anchorParent.mouseleave(function() {
			clearTimeout(obToolbar.windowShadeShowTimeout);
			if (!obToolbar.inWindowShade) obToolbar.windowShadeHideTimeout = setTimeout(function() { obToolbar.hideWindowShades('from button mouseleave',false,true); }, obToolbar.windowShadeHideDelayMs);
		});
		
		$windowShade.mouseenter(function() { 
			clearTimeout(obToolbar.windowShadeHideTimeout); 
			obToolbar.inWindowShade=true;
		});
		
		$windowShade.mouseleave(function(e) {
			setTimeout(function() { obToolbar.hideWindowShades('from windowShade mouseleave',false,true); }, obToolbar.windowShadeHideDelayMs);
			obToolbar.inWindowShade=false;
		});
	};
}

// kludgin
obToolbar.numberToBadge = function(i) {
	return i > 100 ? i : i + ' New';
}

obToolbar.updateToolbarBadges = function() {
	if (obToolbar.unseenNotificationCount)
		obToolbar.$unseenNotificationsBadge.text(obToolbar.numberToBadge(obToolbar.unseenNotificationCount)).fadeIn("slow");
	else 
		obToolbar.$unseenNotificationsBadge.fadeOut("slow");

	if (obToolbar.unseenMessageCount)
		obToolbar.$unseenMessagesBadge.text(obToolbar.numberToBadge(obToolbar.unseenMessageCount)).fadeIn("slow");
	else 
		obToolbar.$unseenMessagesBadge.fadeOut("slow");
}

obToolbar.syncMessagesToServer = function() {
	if (obToolbar.messages[0].event_epoch > obToolbar.lastMessageSeen) {
		$.post('/webservices/json/update_messages_seen.asp', {lastSeen: obToolbar.messages[0].event_epoch}, function(data) {
			obToolbar.unseenMessageCount = 0;
			obToolbar.lastMessageSeen = obToolbar.messages[0].event_epoch;
			obToolbar.updateToolbarBadges(); 
			log('syncMessagesToServer: Syncd messages to server');
		});
	}
}


obToolbar.syncNotificationsToServer = function() {
	log('syncNotificationsToServer obToolbar.notifications[0].event_epoch: ' + obToolbar.notifications[0].event_epoch + ' >  obToolbar.lastNotificationSeen: ' + obToolbar.lastNotificationSeen + ' ?');
	if (obToolbar.notifications[0].event_epoch > obToolbar.lastNotificationSeen) {
		$.post('/webservices/json/update_notifications_seen.asp', {lastSeen: obToolbar.notifications[0].event_epoch}, function(data) {
			obToolbar.unseenNotificationCount = 0;
			obToolbar.lastNotificationSeen = obToolbar.notifications[0].event_epoch;
			obToolbar.updateToolbarBadges(); 
			log('syncNotificationsToServer: Syncd notifications to server');
		});
	}
}



obToolbar.showWindowShade = function(target,msg) {
	var $that=$(target);
	var $windowShade=$($that.data('windowshade'));	// selector for the windowShade element is stored in the $that's "data-windowShade" attribute ie <span data-windowShade="#somewindowShadeElement">Blah</span>
	
	// trying to re-show the current windowShade?  hide it instead
	if ((obToolbar.$activeWindowShade) && ($windowShade[0]===obToolbar.$activeWindowShade[0])) {
		obToolbar.hideWindowShades('from obToolbar.showWindowShade trying to re-show active windowShade. hiding instead', true,true);
		return;
	}
	
	// hide the other leaves, deactivate other buttons, active this button
	obToolbar.hideWindowShades('from obToolbar.showWindowShade',true,false,target.id);
	obToolbar.$toolbarButtons.removeClass('active');
	$that.addClass('active');
	
	// only do animation if there currently is no windowShade open 
	if (obToolbar.$activeWindowShade==null) 
		$windowShade.slideDown(obToolbar.windowShadeSlideUpDurationMs);
	else
		$windowShade.show();
	
	obToolbar.$activeWindowShade=$windowShade;
};


obToolbar.hideWindowShades = function(msg,force,fade,dontHideId) {	
	//return; // uncomment this line if you need the windowshades to stay open to debug CSS, etc.

	// if the pointer's in a windowshade or the toolbar, don't close shade
	if ((obToolbar.inWindowShade || obToolbar.inToolbar) && !force) return;
	
	if (dontHideId) {
		// hide everything BUT that id.  ie, a leaf is already open and we don't want to hide it nor animate
		$('section.windowShade[id!="' + dontHideId + '"]').hide();
		// If we're closing a shade OTHER than the nofications, then we want to clear this
		if (dontHideId!='notificationWindowShade') clearTimeout(obToolbar.notificationUpdateSeenTimeout);
		// If we're closing a shade OTHER than the nofications, then we want to clear this
		if (dontHideId!='messageWindowShade') clearTimeout(obToolbar.messagesUpdateSeenTimeout);		
	}
	else {	
		// no leaves are open.  hide them all and do the animation
		$windowShades.slideUp(obToolbar.windowShadeSlideDownDurationMs);
		obToolbar.$activeWindowShade=null;
		obToolbar.$toolbarButtons.removeClass('active');
		clearTimeout(obToolbar.notificationUpdateSeenTimeout);
		clearTimeout(obToolbar.messagesUpdateSeenTimeout);
	}	
}

obToolbar.messageTemplate = 
'<li {{#id_message}}data-id-message-navigate="{{id_message}}"{{/id_message}} {{#id_comment}}data-id-comment-navigate="{{id_comment}}"{{/id_comment}} {{#displayCssClass}}class="{{displayCssClass}}"{{/displayCssClass}}> \
  <p class="pic50"> \
  <img alt="{{login}}" src="{{memberPicturePath}}"> \
  </p> \
  <div> \
    <h4>{{login}} {{eventDescription}} <small>{{event_time_relative}}</small></h4> \
    {{#subject}}<small>Subject:</small> {{subject}}<br>{{/subject}} \
    <blockquote>"{{body}}"</blockquote> \
   </div> \
  </li>';

// the "outer" mustache.js template for notification items in the toolbar
obToolbar.notificationTemplate = 
'<li {{> data}}{{displayCssClass}}> \
  <p class="pic50"> \
  <img alt="{{login}}" src="{{memberPicturePath}}"> \
  </p> \
  <div> \
    <h4>{{> headline}}<small>{{event_time_relative}}</small></h4> \
    {{> body}} \
   </div> \
  </li>';

// the "inner" mustache.js template for notification items in the toolbar - varies by notification type
obToolbar.notificationSubTemplates= {
	'FOP Shared': {
		data:'data-id-member-navigate="{{id_member}}"', 
		headline:'{{login}} shared a Friends-Only Picture with you',  
		body: '<p><img src="{{pic1}}"></p>'},
	'FOPs Shared': {
		data:'data-id-member-navigate="{{id_member}}"', 
		headline: '{{login}} shared {{qty}} Friends-Only Pictures with you', 
		body:'<p><img src="{{pic1}}"> <img src="{{pic2}}"> <img src="{{pic3}}">{{#qty_remaining}}+ {{qty_remaining}} more{{/qty_remaining}}</p>'},
	'Friended': {
		data:'data-id-member-navigate="{{id_member}}"', 
		headline: '{{login}} friended you', 
		body:'<p>That\'s so cute.</p>'},
	'Moderated Post': {
		displayCssClass:'compact',
		data:'data-id-post-navigate="{{id_post}}"', 
		headline: 'Your post was moderated', 
		body:'<blockquote><small>Post:</small>"{{post_body}}…"</blockquote><blockquote><small>Moderator {{login}} Says:</small> {{description}}</blockquote>'},
	'Profile View': {
		displayCssClass:'tiny',
		data:'data-id-member-navigate="{{id_member}}"', 
		headline: '<p>{{login}} viewed your profile</p>', 
		body:''},
	'Thread Reply': {
		data:'data-id-post-navigate="{{id_post}}"', 
		headline: '{{login}} replied to your thread', 
		body: '<p><small>Thread:</small> {{post_subject}}</p><blockquote>"{{post_body}}"</blockquote>'}
}


obToolbar.renderNotifications = function(notifications) {
	obToolbar.$notificationList.empty();
	for (i in notifications) {
		var n = notifications[i];		
		var subTemplate = obToolbar.notificationSubTemplates[n.event_type];
		n.memberPicturePath = memberPicturePath(n.id_picture_member,'50');
		switch (n.event_type) {
			case 'FOPs Shared':
			case 'FOP Shared':
				n.pic1 = fopIdToPath(n.param1,'thumb');
				if (n.param2) n.pic2 = fopIdToPath(n.param2,'thumb');
				if (n.param3) n.pic3 = fopIdToPath(n.param3,'thumb');
				if (n.qty > 3) n.qty_remaining = n.qty-3;  // 3 is the max we show 
				break;
		}
		if (subTemplate.displayCssClass) n.displayCssClass=' class=' + subTemplate.displayCssClass + ''; // todo: any conditional logic in mustache?
		obToolbar.$notificationList.append($.mustache(obToolbar.notificationTemplate , n  , {data: subTemplate.data, body: subTemplate.body, headline: subTemplate.headline} ));
	}
}

obToolbar.renderNotification = function(notification) {
	var template = $.mustache(obToolbar.notificationSubTemplates[notification.event_type], notification);
	return template;
}

obToolbar.renderMessages = function(messages) {
	obToolbar.$messageList.empty();
	for (i in messages) {
		var m = messages[i];
		m.eventDescription = (m.id_comment ? 'sent you a comment' : 'sent you a message');
		m.memberPicturePath = memberPicturePath(m.id_picture_member,'50');
		if ((m.id_message) && (!m.timestamp_read)) m.unread = true;
		
		obToolbar.$messageList.append($.mustache(obToolbar.messageTemplate, m));
	}
}


// Initialize OB toolbar autocomplete
obToolbar.initAutocomplete = function() {
	$('#superSearch').autocomplete({
		minLength: 2,
		source: function (request, response) {
          $.ajax({
				url: "/webservices/json/member_search_ajax.asp",
				dataType: "json",
				data: request,
				success: function (data) {
					if (!data.length) { // expect [] or ""
						var def_data = typeof(obToolbar.defaultSearchData) == 'function' ?
						obToolbar.defaultSearchData() : obToolbar.defaultSearchData;
						response(def_data);
					} 
					else {
						response(data);
					}
				}
          });
      },
		focus: function(event, ui) { 
			event.stopImmediatePropagation(); 
			return false;	// "return false" cancels event; prevents text box from being updated w/ the id_member, which we don't want 
		},
		select: function( event, ui ) {
			event.stopImmediatePropagation();
			if (ui.item.id_member)	{
				log("redirecting..");
				window.location.href='/op.asp?i=' + ui.item.id_member;
			}
		},
		search: function(event, ui) { 
			console.log(event.currentTarget.value); console.log(ui); 
		} ,
		html: true,
		autoFocus: true,
		position: { my: "right top", at: "right bottom", collision: "none" },
		appendTo: '#obToolbarAutocomplete',
		open: function(event, ui) {
			$('#obToolbarAutocomplete ul').css('max-height', ($(window).height() -120) + 'px');
			//$('#obToolbarAutocomplete ul.ui-autocomplete').append('<li class="ui-menu-item" role="menuitem"><p class="protip">Protip: You can type part of a member\'s first name, current login, previous login, contact info, city&hellip;</li>');	
			$('#obToolbarAutocomplete').highlight($.trim(this.value));		
		}
		
		
	});
}


/*
Formats results for the toolbar member search
Takes a JSON item (representing a search result) and formats into HTML
TODO: Convert to mustache template?
*/

obToolbar.superSearchFormat = function(item) {
	var term = $.trim($('#superSearch').val().toLowerCase());
	var result='';
	if (item.id_picture_member)
		result = '<p style="background-image:url(' + memberIdToPath(item.id_picture_member) + ')">';
	else
		result = '<p>';
	// TODO: make this a class, not an image element
	if (item.is_friend) result += '  <img class="friendicon" src="/images/friendicon_15.png">';
	result += '<strong>' + item.login + '</strong> (' + item.age + '/' + item.gender + ')';
	// TODO: Would be nice to show distance as well.  ie, "New York, New York.  34mi from you."
	result += '<br>' + item.city + ', ' + item.state + '<br>';
	
	/* 
	Only show the following fields if they match the search term.
	If we don't show the following fields, users will be wondering why a match w/ the search term occurred
	*/
	if (item.logins_previous && item.logins_previous.toLowerCase().indexOf(term)>=0) result += 'Previously known as: ' + item.logins_previous + '<br>';
	if (item.email_public && item.email_public.toLowerCase().indexOf(term)>=0) result += 'Email: ' + item.email_public + '<br>';
	if (item.aim && item.aim.toLowerCase().indexOf(term)>=0) result += 'AIM: ' + item.aim + '<br>';
	if (item.msn && item.msn.toLowerCase().indexOf(term)>=0) result += 'MSN: ' + item.msn + '<br>';
	if (item.yahoo && item.yahoo.toLowerCase().indexOf(term)>=0) result += 'Yahoo: ' + item.yahoo + '<br>';
	if (item.twitter && item.twitter.toLowerCase().indexOf(term)>=0) result += 'Twitter: ' + item.Twitter + '<br>';
	if (item.steam_nickname && item.steam_nickname.toLowerCase().indexOf(term)>=0) result += 'Steam: ' + item.steam_nickname + '<br>';
	if (item.first_name && item.first_name.toLowerCase().indexOf(term)>=0) result += 'First name: ' + item.first_name + '<br>';
	result += '</p>';
	return result;
}


//todo: proper nerfing
function log(msg) {
	console.log(msg);
}

/*

******************************
*** OB Path/link "helpers" ***
******************************

*/

function memberImgLinked(data, size) {
	if (data.id_picture_member) {
		data.profile_path = member_profile_path(data.id_member);
		data.pic_path = memberPicturePath(data.id_picture_member, size)
		return $.mustache('<a data-id-member="{{id_member}}" href="{{profile_path}}"><img {{#login}}alt="{{login}}"{{/login}} src="{{pic_path}}"></a>', data);
	}
	else {
		return 'hmm...'; // &.mustache('');
	}	
}

function memberPicturePath(pictureMemberId,size) {
	if (pictureMemberId) return '/user/pic/' + pictureMemberId.toString().substring(0,2) + '/' + pictureMemberId + '_' + size + '.jpg';
}

function fopIdToPath(fopGuid,size) {
	if (fopGuid) return '/user/fop/' + fopGuid + '_' + size + '.jpg';
}

function memberIdToPath(idPictureMember) {
	return '/user/pic/' + (idPictureMember.toString().substring(0,2)) + '/' + idPictureMember + '_50.jpg';
}

function member_profile_path(idMember) {
	return 'op.asp?i=' + idMember;
}

function threadPath(idForum, pageNum) {
	return 'forum.asp?t=' + idForum + '&p=' + (pageNum || '1');
}

function postPath(idPost) {  
	return 'forum.asp?post=' + idPost + '&#post' + idPost ;
}


/* 

*******************************************************
*** Forum Buttons *************************************
*******************************************************

*/





/* 

*******************************************************
*** Legacy OB stuff - mostly needs to be overhauled ***
*******************************************************

*/

/*
function bannerClick(bannerGUID) {
    var xmlReq;
    var someURL;
    xmlReq = getXmlHttpObject();
    if (xmlReq) {
        if ((document.domain == '192.168.1.163') || (document.domain == 'talim')) someURL = 'http://' + document.domain + ':45678/ob_bclick.asp?guid=' + bannerGUID;
        else someURL = 'http://' + document.domain + '/ob_bclick.asp?guid=' + bannerGUID;
        xmlReq.open("POST", someURL, true);
        xmlReq.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xmlReq.send("hello");
    } else {}
    return true;
}
*/

