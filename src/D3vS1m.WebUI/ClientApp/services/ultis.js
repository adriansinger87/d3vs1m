//info: import required Lib 
import ace from "ace-builds/src-noconflict/ace";
import aceDraculaTheme from "ace-builds/src-noconflict/theme-dracula";
import aceWorkerJson from 'ace-builds/src-noconflict/worker-json'
import aceJsonMode from 'ace-builds/src-noconflict/mode-json'
import aceWebPack from 'ace-builds/webpack-resolver'



function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
                    .toString(16)
                    .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
    s4() + '-' + s4() + s4() + s4();
}


String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

if (!Array.prototype.last) {
    Array.prototype.last = function () {
        return this[this.length - 1];
    };
}

function copyTextToClipboard(text) {
    var myText = text.replaceAll("<br>", "\r\n").replaceAll("<br />", "\r\n").replaceAll("&gt;_" ,"");

    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(myText);
        return;
    }
    navigator.clipboard.writeText(myText).then(
        function () {
            //showToast({ html: "Copied" });
            //TODO: refactor 
            var msg = "<span>Copied</span>";
            M.toast({ html: msg, classes: 'toast-success',  displayLength: 4000})
        },
        function (err) {
            var msg = "Could not copy text: " + err;
            console.error(msg);
            showErrorToast(msg);
        }
    );

    // -- inner function

    function fallbackCopyTextToClipboard(text) {
        var textArea = document.createElement("textarea");
        textArea.value = text;
        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();

        try {
            var successful = document.execCommand("copy");
            var msg = successful ? "successful" : "unsuccessful";
            showToast({ html: "Copied" });
        } catch (err) {
            var msg = "Could not copy text: " + err;
            console.error(msg);
            showErrorToast(msg);
        }

        document.body.removeChild(textArea);
    }
}

function initModal() {
    var elems= document.getElementsByClassName("modal"); 
    for (let index = 0; index < elems.length; index++) {
        M.Modal.init(elems[index]);
    }
}

function getModalInstance(ElementName) {
    var consolelElem = document.getElementById(ElementName); 
    var instance = M.Modal.getInstance(consolelElem);


    return instance;
}

function initSideNav() {
    var elem = document.getElementById('slide-out');
    var instance = M.Sidenav.init(elem);
    return instance;
}

function initAceEditor() {
    var editor = ace.edit("editor");
    editor.getSession().setUseWorker(false);
    editor.setTheme(aceDraculaTheme);
    editor.setShowPrintMargin(false);
    editor.getSession().setMode("ace/mode/json");
    return editor;
}


function closeModal() {

}



export default { guid, copyTextToClipboard, initModal, getModalInstance, initAceEditor, initSideNav  }