/*
 * Copyright 2014, kugou.com
 * Creator: Green
 * $Author: linweijian $
 * $Date: 2014-12-09 $
 */

// 全局变量
String.prototype.trim = function() {
	return this.replace(/^(\s|\u3000)*|(\s|\u3000)*$/g, "");
};
var JSON = function() {function f(n) {return n < 10 ? '0' + n: n; } Date.prototype.toJSON = function() {return this.getUTCFullYear() + '-' + f(this.getUTCMonth() + 1) + '-' + f(this.getUTCDate()) + 'T' + f(this.getUTCHours()) + ':' + f(this.getUTCMinutes()) + ':' + f(this.getUTCSeconds()) + 'Z'; }; var m = {'\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\'}; function stringify(value, whitelist) {var a, i, k, l, r = /["\\\x00-\x1f\x7f-\x9f]/g, v; switch (typeof value) {case 'string': return r.test(value) ? '"' + value.replace(r, function(a) {var c = m[a]; if (c) {return c; } c = a.charCodeAt(); return '\\u00' + Math.floor(c / 16).toString(16) + (c % 16).toString(16); }) + '"': '"' + value + '"'; case 'number': return isFinite(value) ? String(value) : 'null'; case 'boolean': case 'null': return String(value); case 'object': if (!value) {return 'null'; } if (typeof value.toJSON === 'function') {return stringify(value.toJSON()); } a = []; if (typeof value.length === 'number' && !(value.propertyIsEnumerable('length'))) {l = value.length; for (i = 0; i < l; i += 1) {a.push(stringify(value[i], whitelist) || 'null'); } return '[' + a.join(',') + ']'; } if (whitelist) {l = whitelist.length; for (i = 0; i < l; i += 1) {k = whitelist[i]; if (typeof k === 'string') {v = stringify(value[k], whitelist); if (v) {a.push(stringify(k) + ':' + v); } } } } else {for (k in value) {if (typeof k === 'string') {v = stringify(value[k], whitelist); if (v) {a.push(stringify(k) + ':' + v); } } } } return '{' + a.join(',') + '}'; } } return {stringify: stringify, parse: function(text, filter) {var j; function walk(k, v) {var i, n; if (v && typeof v === 'object') {for (i in v) {if (Object.prototype.hasOwnProperty.apply(v, [i])) {n = walk(i, v[i]); if (n !== undefined) {v[i] = n; } else {delete v[i]; } } } } return filter(k, v); } if (/^[\],:{}\s]*$/.test(text.replace(/\\["\\\/bfnrtu]/g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {j = eval('(' + text + ')'); return typeof filter === 'function' ? walk('', j) : j; } throw new SyntaxError('parseJSON'); } }; }();

//设置移动光标
function setCaretPosition(ctrl, start,end) {
	if (ctrl.setSelectionRange) {
		ctrl.focus();
		ctrl.setSelectionRange(start, end);
	} else if (ctrl.createTextRange) {
		var range = ctrl.createTextRange();
		range.collapse(true);
		range.moveEnd('character', end);
		range.moveStart('character', start);
		range.select();
	}
}
//获取光标位置 
function getCursortPosition(ctrl) {
	var CaretPos = 0;
	if (document.selection) {
		ctrl.focus();
		var Sel = document.selection.createRange();
		Sel.moveStart('character', -ctrl.value.length);
		CaretPos = Sel.text.length;
	} else if (ctrl.selectionStart || ctrl.selectionStart == '0') {
		CaretPos = ctrl.selectionStart;
	}
	return (CaretPos);
}
//阻止事件
function stopEvent(e) {
	e = window.event || e;
	if (e.stopPropagation) {
		e.stopPropagation();
	} else {
		e.cancelBubble = true;
	}
}
//分类统计
function logStat(p3, p4, name) {
	var param = [];
	param.push("type=" + encodeURIComponent(1));
	param.push("p1=" + encodeURIComponent("pc登录"));
	param.push("p2=" + encodeURIComponent("登录框"));
	p3 != undefined && param.push("p3=" + encodeURIComponent(p3));
	p4 != undefined && param.push("p4=" + encodeURIComponent(p4));
	name != undefined && param.push("name=" + encodeURIComponent(name));
	var url = "http://log.stat.kugou.com/statistics/statistics.html?" + param.join("&") + "&t=" + Math.random();
	setTimeout(function() {
		new Image().src = url;
	}, 1);
}
//入口
function pcLoginInit() {
	var _this_ = arguments.callee,
		$ = function(id) {
			return document.getElementById(id);
		},
		lgn_btn = $("lgn_btn"),
		user = $("user"),
		pwd = $("pwd"),
		remember = $("remember"),
		auto = $("auto"),
		arrow = $("arrow"),
		listbox = $("listbox"),
		lgn_msg = $("lgn_msg"),
		register = $("register"),
		forget = $("forget"),
		lgn_foot = $("lgn_foot").children[0],
		source = location.href.indexOf("source") != -1 ? location.href.match(/source=(.[^&]*)/)[1] : 0,
		userData = null;//用户列表数据
	//输入框交互
	function textBoxInit(ele, opt) {
		var box = ele.parentNode,
			tips = box.children[1],
			callback = function() {
				tips.style.display = ele.value == '' ? 'block' : 'none';
			}
		if (opt.keydownCallback) {
			ele.onfocus = function() {
				box.className += ' lgn_ipt_focus';
			};
			ele.onblur = function() {
				box.className = box.className.replace("lgn_ipt_focus", "");
			};
			ele.onkeyup = function(e) {
				callback();
				opt.keyupCallback && opt.keyupCallback(e = window.event || e);
			};
			ele.onkeydown = function(e) {
				callback();
				opt.keydownCallback && opt.keydownCallback(e = window.event || e);
			};
			ele.onselectstart = function(e) {
				stopEvent(e);
			};
			ele.onchange = function(e){
				ele.setAttribute("change", "1");
			};
			setTimeout(function() {
				ele.value = '';
			}, 1);
		} else {
			ele.value = opt.value;
			callback();
		}

	}
	//复选框交互
	function checkBoxInit(ele, ischeck,callback) {
		ischeck = +ischeck;
		var checked = "lgn_chk_checked",
			checkcallback = function(ext,callback) {
				if (ext) {
					callback && callback(ele,"nocheck");
					ele.className = ele.className.replace(/lgn_chk_checked/ig, "");
					ele.checked = false;
					if (ele.getAttribute("allnocheck")) {
						checkBoxInit(ele.parentNode.parentNode.children[1].children[0], false,callback);
					}
				} else {
					callback && callback(ele,"checked");
					ele.className += " " + checked;
					ele.checked = true;
					if (ele.getAttribute("allcheck")) {
						checkBoxInit(ele.parentNode.parentNode.children[0].children[0], true,callback);
					}
				}
			};
		if(isNaN(ischeck)){
			ele.onclick = function() {
				checkcallback(ele.className.indexOf(checked) != -1,callback);
			};
			ele.parentNode.children[1].onclick = function() {
				checkcallback(ele.className.indexOf(checked) != -1,callback);
			};
		} else {
			checkcallback(!ischeck,callback);
		}
	}
	//错误提示交互
	_this_.errorInit = function(msg,user) {
		if (msg) {
			lgn_msg.children[0].innerHTML = msg;
			lgn_msg.style.visibility = 'visible';
		} else {
			lgn_msg.children[0].innerHTML = '';
			lgn_msg.style.visibility = 'hidden';
		}
		//错误提示下默认显示
		if(user){
			for (var i in userData) {
				if (userData[i].user == user) {
					_this_.listBoxInit.choiseItem(i);
					setTimeout(function(){
						textBoxInit(pwd, {value:""});
					},1);
					return false;
					break;
				}
			}
		}
	};
	//下拉列表交互
	_this_.listBoxInit = function(data) {
		userData = data;
		var that = arguments.callee,
			listRangeCount = 9, //列表可见范围长度
			listItemHeight = 16; //列表项高度
		//切换列表
		that.toggleList = function() {
			if (listbox.style.display == 'none' && userData.length > 0 ) {
				if (arguments.length == 0 || arguments[0] == true) {
					listbox.style.display = 'block';
					var range = that.ListRange();
					that.listScroll.scrollT = range.scrollT;
					that.listScroll.scrollB = range.scrollB;
					that.listScroll(that.selectIndex());
					listbox.selectedIndex = null;
				}
				return false;
			} else if (userData.length == 0 ) {
				return false;
			}else{
				arguments.length == 0 ? (listbox.style.display = 'none') : "";
				return true;
			}
		};
		//列表上下限范围
		that.ListRange = function(){
			var index = 0;
			for (var i = 0; i < userData.length; i++) {
				if (listbox.scrollTop < listItemHeight * i) {
					index = i - 1;
					break;
				} else if (listbox.scrollTop == listItemHeight * i) {
					index = i;
					break;
				}
			}
			return {
				scrollT: index,
				scrollB: index + listRangeCount - 1
			};
		};
		//清空列表
		that.clearList = function() {
			for (var i = 0; i < listbox.children.length; i++) {
				listbox.children[i].className = '';
				arguments.length == 1 && userData[i].select && (userData[i].select = "0");
			}
		};
		//选中列表某项
		that.selectItem = function(i) {
			that.clearList();
			listbox.children[i].className = 'current';
			listbox.selectedIndex = i;
		};
		//选择列表某项
		that.choiseItem = function(i) {
			that.clearList(true);
			inputBind(userData[i]);
			that.bind();
		};
		//删除列表某项
		that.deleteItem = function(user,i) {
			w2c.delAccount({
				"user": user
			});
			w2c.addAction({
				"wactionid": "4500",
				"wcount": "1"
			});
			that.clearList(true);
			inputBind(userData[i + 1] || null);
			userData.splice(i, 1);
			that.bind();
			
		};
		//选中项
		that.selectIndex = function() {
			var selectIndex = '';
			for (var i in userData) {
				if (userData[i].select && userData[i].select == 1) {
					selectIndex = i;
					break;
				}
			}
			return selectIndex;
		};
		//列表滚动
		that.listScroll = function(index) {
			if (index > that.listScroll.scrollB) {
				that.listScroll.scrollT++;
				that.listScroll.scrollB++;
				listbox.scrollTop = (that.listScroll.scrollB - listRangeCount + 1) * listItemHeight;
			}
			if (index < that.listScroll.scrollT) {
				that.listScroll.scrollT--;
				that.listScroll.scrollB--;
				listbox.scrollTop = (that.listScroll.scrollB - listRangeCount + 1) * listItemHeight;
			}
		};
		that.bind = function() {
			listbox.style.display = 'none';
			listbox.className = 'lgn_list' + (userData && userData.length > listRangeCount ? ' lgn_list_scroll' : '');
			if (userData && userData.length > 0) {
				arrow.style.cursor = 'pointer';
				var str = '';
				for (var i in userData) {
					str += [
						'<li ' + (userData[i]["select"] == 1 ? ('class="current"') : '') + '>',
						'<span>' + userData[i]["user"] + '</span>',
						'<a href="javascript:;" title="删除账号信息"></a>',
						'</li>'
					].join("");
				}
				listbox.innerHTML = str;
				for (var i = 0; i < listbox.children.length; i++) {
					(function(i) {
						listbox.children[i].onmouseover = function() {
							that.selectItem(i);
						};
						listbox.children[i].onclick = function() {
							that.choiseItem(i);
						};
						listbox.children[i].children[1].onclick = function(e) {
							that.deleteItem(this.parentNode.children[0].innerHTML, i);
						};
					})(i);
				}
			} else {
				arrow.style.cursor = 'default';
				listbox.innerHTML = '';
			}
		};
		if (userData && userData.length > 0) {
			arrow.onclick = function(e) {
				that.toggleList();
				stopEvent(e);
			};
			document.body.onkeydown = function(e) {
				e = window.event || e;
				if (e.keyCode == 38 || e.keyCode == 40) {
					setTimeout(function() {
						var l = user.value.length; setCaretPosition(user, l, l);
					}, 1);
					if (that.toggleList(true)) {
						var index = typeof listbox.selectedIndex == "object" ? that.selectIndex() : listbox.selectedIndex;
						index = e.keyCode == 38 ? (--index < 0 ? 0 : index) : (++index > userData.length - 1 ? --index : index);
						that.selectItem(index);
						that.listScroll(index);
						listbox.selectedIndex = index;
					}
				}
			};
			listbox.onscroll = function() {
				var range = that.ListRange();
				that.listScroll.scrollT = range.scrollT;
				that.listScroll.scrollB = range.scrollB;
			};
			//默认选中
			that.choiseItem(that.selectIndex());
		}else{
			checkBoxInit(remember,1);
			checkBoxInit(auto,1);
		}
	};
	//数据绑定
	function inputBind(data) {
		if (data) {
			data.select = 1;
			data.pwd = (data.rmb == 1 ? "00000" : "");
			data.auto = 1;
			data.rmb = 1;
		} else {
			data = {user: "",auto: "1",rmb: "1",pwd: ""};
		}
		setTimeout(function() {
			textBoxInit(user,{
				value : data.user
			});
			textBoxInit(pwd,{
				value : data.pwd
			});
			checkBoxInit(remember, data.rmb);
			checkBoxInit(auto, data.auto);
		}, 1);
	}
	textBoxInit(user,{
		keydownCallback : function(e){
			if(typeof _this_.listBoxInit.toggleList == 'undefined'){
				c2w.accountList({data : []});
			}
			if (e.keyCode == 13) {
				if (_this_.listBoxInit.toggleList(false)) {
					var index = typeof listbox.selectedIndex == "object" ? _this_.listBoxInit.selectIndex() : listbox.selectedIndex;
					_this_.listBoxInit.choiseItem(index);
				}else{
					logStat("账号相关","Enter键登录量");
					lgn_btn.setAttribute("isenter",1);
					lgn_btn.onclick();
				}
			} 
			if (e.keyCode != 9) {
				setTimeout(function() {
					user.index = getCursortPosition(user);
				}, 0);
			}else{
				listbox.style.display = 'none';
			}
		},
		keyupCallback: function(e) {
			if(typeof _this_.listBoxInit.toggleList == 'undefined'){
				c2w.accountList({data : []});
			}
			var exists = false,
				code = e.keyCode,
				reg = new RegExp("^" + user.value),
				exec = (code >= 65 && code <= 90) || // 字母
				(code >= 48 && code <= 57) || //数字
				(code >= 96 && code <= 105) || //键盘数字
				(code >= 106 && code <= 111) || //键盘字符
				(code >= 186 && code <= 192) || //字符1
				(code >= 219 && code <= 222) || // 字符2
				(code == 8 || code == 46) || //删除键
				(code == 13) || //中文输入情况下回车
				(code == 32 && e.ctrlKey == false); //空格
			if (exec) {
				for (var i in userData) {
					if (user.value != "" && (userData[i].user == user.value || (reg.test(userData[i].user) && code != 8 && code != 46))) {
						_this_.listBoxInit.choiseItem(i);
						setTimeout(function() {
							setCaretPosition(user, user.index, user.value.length);
						}, 1);
						exists = true;
						break;
					}
				}
				if (!exists) {
					textBoxInit(pwd, {
						value: ""
					});
				}
			}
		}
	});
	textBoxInit(pwd,{
		keydownCallback : function(e){
			if (e.keyCode == 13) {
				logStat("账号相关","Enter键登录量");
				lgn_btn.setAttribute("isenter",1);
				lgn_btn.onclick();
			}
		}
	});
	checkBoxInit(remember, undefined, function(ele, type) {
		if (type == 'checked') {
			logStat("账号相关", "记住密码打钩");
		} else if (type == 'nocheck') {
			if(ele.getAttribute("allnocheck")){
				logStat("账号相关","记住密码取消");
			}else if(ele.checked === true){
				logStat("账号相关","自动登录取消");
			}
		}
	});
	checkBoxInit(auto, undefined, function(ele, type) {
		if (type == 'checked') {
			if(ele.getAttribute("allcheck")){
				logStat("账号相关","自动登录打钩");
			}else if(ele.checked === false || ele.checked === undefined){
				logStat("账号相关","记住密码打钩");
			}
		} else if (type == 'nocheck') {
			logStat("账号相关", "自动登录取消");
		}
	});
	lgn_btn.onclick = function(e) {
		if (this.getAttribute("isenter") !=1) {
			logStat("界面按钮", "登录按钮");
		}else{
			this.removeAttribute("isenter");
		}
		_this_.errorInit("");
		this.blur();
		if (user.value.trim() == '' && pwd.value.trim() == '') {
			_this_.errorInit("请输入用户名及密码");
			logStat("账号相关", "错误提示量");
			return false;
		}
		if (user.value.trim() == '') {
			_this_.errorInit("请输入用户名");
			logStat("账号相关", "错误提示量");
			return false;
		}
		if (pwd.value.trim() == '') {
			_this_.errorInit("请输入密码");
			logStat("账号相关", "错误提示量");
			return false;
		}
		user.blur();
		pwd.blur();
		setTimeout(function() {
			w2c.login({
				"user": user.value.trim(),
				"pwd": pwd.value.trim(),
				"rmb": remember.checked ? "1":"0",
				"auto": auto.checked ? "1":"0",
				"change": pwd.getAttribute("change") || "0"
			});
		}, 100);
	}
	register.onclick = function() {
		logStat("界面按钮","注册账号");
		setTimeout(function(){
			w2c.openRegister({
				"source": source,
				"force": "1"
			});
		},100);
	};
	forget.onclick = function() {
		logStat("界面按钮","找回密码");
	};
	for (var i = 0; i < lgn_foot.children.length; i++) {
		(function(i) {
			var n = lgn_foot.children[i];
			if (n.tagName.toLowerCase() == 'dd') {
				n.onclick = function() {
					var c = n.children[0];
					w2c.thirdPop({
						"name": c.getAttribute("type"),
						"width": c.getAttribute("width"),
						"height": c.getAttribute("height"),
						"url": c.getAttribute("url")
					});
					w2c.addAction({
						"wactionid": "4500",
						"wcount": "1"
					});
					logStat("第三方登录", c.getAttribute("title"));
				}
			}
		})(i);
	}
	document.onclick = function() {
		listbox.style.display = 'none';
	};
	document.onselectstart = function() {
		return false;
	};
	logStat("页面展示","页面展示量");
}
pcLoginInit();
//客户端-网页交互
var w2c = {
	login: function(json) {
		try {
			external.SuperCall(807, JSON.stringify(json));
		} catch (ex) {}
	},
	openRegister: function(json) {
		try {
			external.SuperCall(800); //关闭窗口
			external.SuperCall(521, JSON.stringify(json));
		} catch (ex) {}
	},
	thirdPop: function(json) {
		try {
			external.SuperCall(505, JSON.stringify(json));
		} catch (ex) {}
	},
	delAccount: function(json) {
		try {
			external.SuperCall(810, JSON.stringify(json));
		} catch (ex) {}
	},
	addAction: function(json) {
		try {
			external.SuperCall(507, JSON.stringify(json));
		} catch (ex) {}
	}
},
	c2w = {
		accountList: function(json) {
			pcLoginInit.listBoxInit(json.data);
		},
		loginErrorInfo: function(json) {
			pcLoginInit.errorInit(json.msg,json.user);
		}
	}, KgWebSupperCall = function(cmdid, jsonStr) {
		var json = eval("(" + jsonStr + ")");
		if (cmdid == 630) {
			c2w.accountList(json);
		} else if (cmdid == 631) {
			c2w.loginErrorInfo(json);
		}
	};
