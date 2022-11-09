(function(global, undefined) {
	
var demo = {};

//function populateValue() {
//		$get(demo.Label).innerHTML = 'jb- ' + $get(demo.Textbox1).value;
//		//the RadWindow's content template is an INaming container and the server code block is needed
//		$find(demo.contentTemplateID).close();
//	}

	function openWinContentTemplate() {
		$find(demo.templateWindowID).show();
	}
	function openWinNavigateUrl() {
		$find(demo.urlWindowID).show();
	}

    global.$windowContentDemo = demo;
	//global.populateValue = populateValue;
	global.openWinContentTemplate = openWinContentTemplate;
	global.openWinNavigateUrl = openWinNavigateUrl;
})(window);