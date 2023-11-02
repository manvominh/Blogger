function ActiveMenu() {
	var a = document.querySelectorAll(".site-menu li a");
	for (var i = 0, length = a.length; i < length; i++) {
		a[i].onclick = function () {
			var b = document.querySelector(".site-menu li.active");
			if (b) b.classList.remove("active");
			this.parentNode.classList.add('active');
		};
	}
}
