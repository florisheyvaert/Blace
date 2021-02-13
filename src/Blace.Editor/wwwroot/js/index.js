
window.ace_load = function (id, theme, mode) {
    var editor = ace.edit(id);
    editor.setTheme(theme);
    editor.session.setMode(mode);
    editor.setShowPrintMargin(false);
    editor.session.on("change", function () {
        ace_change(editor);
    });
    //editor.commands.addCommand({
    //    name: 'SaveCmomand',
    //    bindKey: { win: 'Ctrl-S', mac: 'Command-S' },
    //    exec: function (editor) {
    //        console.log('test');
    //        DotNet.invokeMethodAsync('SoapBerry.Website', 'AceEditorSave', '');
    //    },
    //    readOnly: false // false if this command should not apply in readOnly mode
    //});
}

window.ace_set_theme = function (id, theme) {
    var editor = ace.edit(id);
    editor.setTheme(theme);
}

window.ace_set_mode = function (id, mode) {
    var editor = ace.edit(id);
    editor.session.setMode(mode);
}

window.ace_set_value = function (id, value) {
    var editor = ace.edit(id);
    editor.setValue(value, 1);
}

window.ace_change = function updateMessageCallerJS(editor) {
    DotNet.invokeMethodAsync('Blace.Editor', 'AceEditorValueChanged', editor.getValue());
}

window.ScrollIntoView = function (id, parentId) {
    var element = document.getElementById(id);
    var parentElement = document.getElementById(parentId);
    element.scrollIntoView();
    if (isLeftSide(element))
        parentElement.scrollLeft -= 20;
    else
        parentElement.scrollLeft += 20;
}

function isLeftSide(element) {
    var viewWidth = Math.max(document.documentElement.clientWidth, window.innerWidth);
    var x = element.getBoundingClientRect().x;
    return x <= (viewWidth / 2);
}